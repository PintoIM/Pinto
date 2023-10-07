using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using PintoNS.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Networking
{
    public class ConnectionException : Exception 
    {
        public ConnectionException(string message) : base(message) { }
    }

    public class NetworkClient
    {
        private bool ignoreDisconnectReason;
        public bool IsConnected { get; private set; }
        public string IP;
        public int Port;
        private TcpClient tcpClient;
        private NetworkStream netStream;
        private Thread readThread;
        private Aes aes;
        public Action<string> Disconnected = delegate (string reason) { };
        public Action<IPacket> ReceivedPacket = delegate (IPacket packet) { };
        private object sendLock = new object();
        
        public async Task<(bool, Exception)> Connect(string ip, int port) 
        {
            try
            {
                if (IsConnected) Disconnect("Reconnecting");
                ignoreDisconnectReason = false;

                tcpClient = new TcpClient();
                tcpClient.ReceiveTimeout = 10000;

                try { await TaskEx.Run(() => tcpClient.ConnectAsync(ip, port).Wait(5000)); }
                catch (AggregateException ex) { throw ex.InnerException; }
                if (!tcpClient.Connected) throw new ConnectionException("Timed out");

                IP = ip;
                Port = port;
                IsConnected = true;
                netStream = tcpClient.GetStream();
                readThread = new Thread(new ThreadStart(ReadThread_Func));

                if (!await TaskEx.Run(Handshake))
                    throw new Exception("Public key memorization failed");

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

        private bool DoPublicKeyMemorization(byte[] publicKeyRaw)
        {
            string knownHostsPath = Path.Combine(Program.DataFolder, "known_hosts.txt");
            if (!File.Exists(knownHostsPath)) File.WriteAllText(knownHostsPath, "");
            List<string> knownHosts = File.ReadAllLines(knownHostsPath).ToList();
            string host = $"{IP}:{Port}";
            string publicKey = Convert.ToBase64String(publicKeyRaw);
            
            foreach (string knownHostPair in knownHosts.ToArray())
            {
                string[] knownHostPairSplitted = knownHostPair.Split(';');
                string knownHost = knownHostPairSplitted[0];
                string knownPublicKey = knownHostPairSplitted[1];

                if (knownHost == host)
                {
                    if (knownPublicKey != publicKey)
                    {
                        DialogResult result = MessageBox.Show(
                            $"!!! WARNING !!!{Environment.NewLine}" +
                            $"THE PUBLIC KEY OF THE SERVER {host} HAS CHANGED" +
                            $" SINCE LAST TIME YOU CONNECTED{Environment.NewLine}" +
                            $"{Environment.NewLine}" +
                            $"THIS INDICATES THE FOLLOWING POSSIBILITIES:{Environment.NewLine}" +
                            $"1. THE ADMINISTRATOR HAS CHANGED THE PUBLIC KEY{Environment.NewLine}" +
                            $"2. YOUR CONNECTION IS BEING TAMPERED WITH (more likely){Environment.NewLine}" +
                            $"{Environment.NewLine}" +
                            $"IF THE SERVER ADMINISTRATOR HASN'T TOLD YOU ABOUT ANY PUBLIC KEY CHANGES," +
                            $" THIS IS MOST LIKELY THE LATTER POSSIBILITY{Environment.NewLine}" +
                            $"{Environment.NewLine}" +
                            $"Press \"yes\" to continue and update the stored key{Environment.NewLine}" +
                            $"Press \"no\" to continue without updating the stored key{Environment.NewLine}" +
                            $"Press \"cancel\" to abandon the connection{Environment.NewLine}" +
                            $"{Environment.NewLine}" +
                            $"Old public key: {knownPublicKey}{Environment.NewLine}" +
                            $"New public key: {publicKey}{Environment.NewLine}",
                            "Pinto! - Security Alert - PUBLIC KEY MISMATCH",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Warning
                        );

                        if (result == DialogResult.Cancel)
                            return false;
                        else if (result == DialogResult.No)
                            return true;
                        else
                        {
                            knownHosts.Remove(knownHostPair);
                            File.WriteAllLines(knownHostsPath, knownHosts);
                            MemorizeHost(publicKey, host);
                            return true;
                        }
                    }
                    else return true;
                }
            }

            DialogResult result2 = MessageBox.Show(
                $"The server's public key is not known. " +
                $"You have no guarantee that the server is the computer you think it is{Environment.NewLine}" +
                $"The server's public key is: {publicKey}{Environment.NewLine}" +
                $"If you trust this server, press \"yes\" to add this key to the known hosts{Environment.NewLine}" +
                $"If you want to connect just once, press \"no\"{Environment.NewLine}" +
                $"If you do not trust this host, press \"cancel\" to abandon the connection",
                "Pinto! - Security Alert - Unknown Host",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (result2 == DialogResult.Cancel)
                return false;
            else if (result2 == DialogResult.No)
                return true;
            else
            {
                MemorizeHost(publicKey, host);
                return true;
            }
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

            Program.Console.WriteMessage($"[Networking] RSA public key:\n{publicKeyStrSplit}");
            if (!DoPublicKeyMemorization(publicKey)) return false;

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

            IP = null;
            Port = 0;
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
                bufferedNetStream.Write(aes.IV, 0, 16); // IV
                bufferedNetStream.Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(encryptedPacketData.Length)), 0, 4); // Encrypted Data Length
                bufferedNetStream.Write(encryptedPacketData, 0, encryptedPacketData.Length); // Encrypted Data
                bufferedNetStream.Flush();

                if (packet.GetID() != 255)
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
                throw new ConnectionException($"Bad packet ID: {id}");

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
                        throw new ConnectionException("Client disconnect");
                    
                    // Packet header
                    if (headerPart0 != 'P' ||
                        headerPart1 != 'M' ||
                        headerPart2 != 'S' ||
                        headerPart3 != 'G')
                        throw new ConnectionException("Bad packet header!");

                    byte[] iv = new byte[16];
                    netStream.Read(iv, 0, iv.Length);

                    byte[] encryptedDataSize = new byte[4];
                    netStream.Read(encryptedDataSize, 0, encryptedDataSize.Length);

                    byte[] encryptedData = new byte[
                        IPAddress.NetworkToHostOrder(BitConverter.ToInt32(encryptedDataSize, 0))];
                    int readAmount = netStream.Read(encryptedData, 0, encryptedData.Length);

                    if (readAmount == 0)
                        throw new ConnectionException("Client disconnect");

                    ProcessReceivedEncryptedData(encryptedData, iv);

                    Thread.Sleep(1);
                }
                catch (Exception ex)
                {
                    if (!IsConnected)
                    {
                        Program.Console.WriteMessage($"Ignoring network client exception" +
                            $" as we aren't connected");
                        return;
                    }

                    if (!(ex is IOException || ex is ConnectionException))
                        HandleError(ex);
                    else
                        Disconnect(ex.Message);
                }
            }
        }
    }
}
