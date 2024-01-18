using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using PintoNS.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PintoConnectionException : Exception
    {
        public PintoConnectionException(string message) : base(message) { }
    }

    public class PintoVerificationException : Exception
    {
    }

    public class NetworkClient
    {
        private bool ignoreDisconnectReason;
        public bool IsConnected { get; private set; }
        private string serverIP;
        private int serverPort;
        private TcpClient tcpClient;
        private NetworkStream netStream;
        private Thread readThread;
        private Aes aes;
        public Action<string> Disconnected = delegate (string reason) { };
        public Action<IPacket> ReceivedPacket = delegate (IPacket packet) { };
        private object sendLock = new object();

        public async Task<(bool, Exception)> Connect(string ip, int port, Action<string> changeConnectionStatus)
        {
            try
            {
                changeConnectionStatus.Invoke("Connecting...");
                if (IsConnected) Disconnect("Reconnecting");
                ignoreDisconnectReason = false;

                tcpClient = new TcpClient();
                tcpClient.ReceiveTimeout = 30000;

                try { await TaskEx.Run(() => tcpClient.ConnectAsync(ip, port).Wait(5000)); }
                catch (AggregateException ex) { throw ex.InnerException; }
                if (!tcpClient.Connected) throw new PintoConnectionException("Timed out");

                serverIP = ip;
                serverPort = port;
                IsConnected = true;
                netStream = tcpClient.GetStream();
                readThread = new Thread(new ThreadStart(ReadThread_Func));

                changeConnectionStatus.Invoke("Handshaking...");
                if (!Handshake())
                    return (false, new PintoVerificationException());

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        private void MemorizeHost(string publicKeyBase64, string host)
        {
            File.AppendAllLines(
                Path.Combine(Program.DataFolder, "known_hosts.txt"),
                new string[] { $"{host};{publicKeyBase64}" });
        }

        private bool DoPublicKeyVerification(byte[] publicKeyRaw, CancellationToken token)
        {
            string knownHostsPath = Path.Combine(Program.DataFolder, "known_hosts.txt");
            if (!File.Exists(knownHostsPath)) File.WriteAllText(knownHostsPath, "");

            List<string> knownHosts = File.ReadAllLines(knownHostsPath).ToList();
            string host = $"{serverIP}:{serverPort}";
            string publicKey = Convert.ToBase64String(publicKeyRaw);

            bool result = false;
            bool failedVerification = false;
            string oldKnownHostPair = null;

            foreach (string knownHostPair in knownHosts.ToArray())
            {
                string[] knownHostPairSplitted = knownHostPair.Split(';');
                string knownHost = knownHostPairSplitted[0];
                string knownPublicKey = knownHostPairSplitted[1];

                if (knownHost == host)
                {
                    if (knownPublicKey != publicKey)
                    {
                        oldKnownHostPair = knownHostPair;
                        failedVerification = true;
                        break;
                    }
                    else
                        return true;
                }
            }

            RSAKeyVerifierForm verifier = new RSAKeyVerifierForm(host, publicKey, failedVerification)
            {
                Callback = (RSAKeyVerifierForm.VerifierResult result2) =>
                {
                    if (result2 == RSAKeyVerifierForm.VerifierResult.DISCONNECT)
                        result = false;
                    else if (result2 == RSAKeyVerifierForm.VerifierResult.ONLY_ONCE)
                        result = true;
                    else
                    {
                        if (oldKnownHostPair != null)
                        {
                            knownHosts.Remove(oldKnownHostPair);
                            File.WriteAllLines(knownHostsPath, knownHosts);
                        }

                        MemorizeHost(publicKey, host);
                        result = true;
                    }
                }
            };

            new Thread(new ThreadStart(() =>
            {
                try
                {
                    Program.Console.WriteMessage("[Networking] Starting verifier form result thread");
                    while (!verifier.IsDisposed)
                    {
                        token.ThrowIfCancellationRequested();
                        Thread.Sleep(100);
                    }
                }
                catch
                {
                    Program.Console.WriteMessage("[Networking] Aborting verifier form result thread");
                    verifier.Close();
                    result = false;
                }
            })).Start();
            verifier.ShowDialog();

            return result;
        }

        private bool Handshake()
        {
            Program.Console.WriteMessage("[Networking] Handshaking AES key...");

            BinaryReader binaryReader = new BinaryReader(netStream, Encoding.BigEndianUnicode);
            BinaryWriter binaryWriter = new BinaryWriter(netStream, Encoding.BigEndianUnicode);

            int sizeOfPublicKey = binaryReader.ReadBEInt();
            byte[] publicKey = binaryReader.ReadBytes(sizeOfPublicKey);
            string publicKeyStr = BitConverter.ToString(publicKey).Replace("-", "");
            string publicKeyStrSplit = string.Join("\n", Program.SplitStringIntoChunks(publicKeyStr, 64));
            Program.Console.WriteMessage($"[Networking] Server RSA public key:\n{publicKeyStrSplit}");

            bool finishedVerif = false;
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            Thread staller = new Thread(new ThreadStart(() =>
            {
                try
                {
                    while (!finishedVerif)
                    {
                        binaryWriter.WriteBE(0x7FFFFFFF);
                        try { Thread.Sleep(1000); } catch { }
                    }
                }
                catch
                {
                    cancellationToken.Cancel();
                }
            }));
            staller.Start();

            if (!DoPublicKeyVerification(publicKey, cancellationToken.Token))
            {
                finishedVerif = true;
                staller.Interrupt();
                staller.Abort();
                return false;
            }
            finishedVerif = true;
            staller.Interrupt();
            staller.Abort();

            aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.GenerateKey();
            aes.GenerateIV();
            Program.Console.WriteMessage($"[Networking] AES key: {BitConverter.ToString(aes.Key).Replace("-", "")}");

            RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)PublicKeyFactory.CreateKey(publicKey);
            RSAParameters rsaParameters = new RSAParameters();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsaParameters.Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned();
            rsaParameters.Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned();
            rsa.ImportParameters(rsaParameters);

            byte[] encryptedAESKey = rsa.Encrypt(aes.Key, false);
            binaryWriter.WriteBE(encryptedAESKey.Length);
            binaryWriter.Write(encryptedAESKey);
            Program.Console.WriteMessage("[Networking] Handshaking done");

            readThread.Start();
            return true;
        }

        public void Disconnect(string reason)
        {
            bool sendEvent = IsConnected && !ignoreDisconnectReason;

            IsConnected = false;
            ignoreDisconnectReason = true;
            if (netStream != null) netStream.Dispose();
            if (tcpClient != null) tcpClient.Close();

            serverIP = null;
            serverPort = 0;
            tcpClient = null;
            netStream = null;
            readThread = null;

            if (sendEvent)
                Disconnected.Invoke(reason);
        }

        public void HandleError(Exception ex)
        {
            Disconnect($"Internal error -> {ex.Message}");
            Program.Console.WriteMessage($"[Networking] Internal error: {ex}");
        }

        public void SendPacket(IPacket packet)
        {
            if (!IsConnected) return;

            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
            BufferedStream bufferedNetStream = new BufferedStream(netStream, 4096);

            try
            {
                lock (sendLock)
                {
                    binaryWriter.WriteBE(packet.GetID()); // ID
                    packet.Write(binaryWriter); // Data
                    binaryWriter.Flush();
                }

                // Encrypt the data
                aes.GenerateIV();
                byte[] packetData = memoryStream.ToArray();
                byte[] encryptedPacketData = aes.CreateEncryptor()
                    .TransformFinalBlock(packetData, 0, packetData.Length);
                memoryStream.Dispose();

                // Write the packet
                byte[] packetHeader = Encoding.ASCII.GetBytes("PMSG");
                bufferedNetStream.Write(packetHeader, 0, 4); // Header
                bufferedNetStream.Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(encryptedPacketData.Length)), 0, 4); // Encrypted Data Length
                bufferedNetStream.Write(aes.IV, 0, 16); // IV
                bufferedNetStream.Write(encryptedPacketData, 0, encryptedPacketData.Length); // Encrypted Data
                bufferedNetStream.Flush();

                if (!(packet is PacketKeepAlive))
                    Program.Console.WriteMessage($"[Networking] Sent packet" +
                        $" {packet.GetType().Name.ToUpper()} ({packet.GetID()})");
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void ProcessReceivedEncryptedData(byte[] encryptedData, byte[] iv)
        {
            aes.IV = iv;
            byte[] decryptedData = aes.CreateDecryptor().TransformFinalBlock(
                encryptedData, 0, encryptedData.Length);
            BinaryReader binaryReader = new BinaryReader(
                new MemoryStream(decryptedData), Encoding.BigEndianUnicode);

            int id = binaryReader.ReadBEInt();
            IPacket packet = Packets.GetPacketByID(id);

            if (packet == null)
                throw new PintoConnectionException($"Bad packet ID: {id}");

            packet.Read(binaryReader);
            ReceivedPacket.Invoke(packet);
        }

        private void ReadThread_Func()
        {
            while (IsConnected)
            {
                try
                {
                    int headerPart0 = netStream.ReadByte();
                    int headerPart1 = netStream.ReadByte();
                    int headerPart2 = netStream.ReadByte();
                    int headerPart3 = netStream.ReadByte();

                    if (headerPart0 == -1 ||
                        headerPart1 == -1 ||
                        headerPart2 == -1 ||
                        headerPart3 == -1)
                        throw new PintoConnectionException("Client disconnect");

                    // Packet header
                    if (headerPart0 != 'P' ||
                        headerPart1 != 'M' ||
                        headerPart2 != 'S' ||
                        headerPart3 != 'G')
                        throw new PintoConnectionException("Bad packet header!");

                    byte[] encryptedDataSize = new byte[4];
                    netStream.Read(encryptedDataSize, 0, encryptedDataSize.Length);

                    byte[] iv = new byte[16];
                    netStream.Read(iv, 0, iv.Length);

                    byte[] encryptedData = new byte[
                        IPAddress.NetworkToHostOrder(BitConverter.ToInt32(encryptedDataSize, 0))];
                    int readAmount = netStream.Read(encryptedData, 0, encryptedData.Length);

                    if (readAmount == 0)
                        throw new PintoConnectionException("Client disconnect");

                    ProcessReceivedEncryptedData(encryptedData, iv);
                    Thread.Sleep(1);
                }
                catch (Exception ex)
                {
                    if (!IsConnected)
                    {
                        Program.Console.WriteMessage($"[Networking] Ignoring network client exception" +
                            $" as we aren't connected");
                        return;
                    }

                    if (!(ex is IOException || ex is PintoConnectionException))
                        HandleError(ex);
                    else
                        Disconnect(ex.Message);
                }
            }
        }
    }
}
