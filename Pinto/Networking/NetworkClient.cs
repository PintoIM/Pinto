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
        private NetworkStream tcpStream;
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

                try { await Task.Run(() => tcpClient.ConnectAsync(ip, port).Wait(5000)); }
                catch (AggregateException ex) { throw ex.InnerException; }
                if (!tcpClient.Connected) throw new ConnectionException("Timed out");

                IP = ip;
                Port = port;
                IsConnected = true;
                tcpStream = tcpClient.GetStream();
                readThread = new Thread(new ThreadStart(ReadThread_Func));

                if (!await Task.Run(Handshake))
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

            BinaryReader binaryReader = new BinaryReader(tcpStream, Encoding.BigEndianUnicode);
            BinaryWriter binaryWriter = new BinaryWriter(tcpStream, Encoding.BigEndianUnicode);

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

            byte[] encryptedAESKey = rsa.Encrypt(aes.Key, RSAEncryptionPadding.Pkcs1);
            binaryWriter.WriteBE(encryptedAESKey.Length);
            binaryWriter.Write(encryptedAESKey);
            Program.Console.WriteMessage("[Networking] Handshaking done");

            readThread.Start();
            return true;
        }

        public void Disconnect(string reason) 
        {
            bool ignoreDisconnectReasonValue = ignoreDisconnectReason;
            ignoreDisconnectReason = true;
            if (tcpStream != null) tcpStream.Dispose();
            if (tcpClient != null) tcpClient.Close();

            IP = null;
            Port = 0;
            tcpClient = null;
            tcpStream = null;
            readThread = null;

            if (IsConnected && !ignoreDisconnectReasonValue) 
            {
                Disconnected.Invoke(reason);
            }
            IsConnected = false;
        }

        public void SendPacket(IPacket packet) 
        {
            if (!IsConnected) return;

            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

            lock (sendLock)
            {
                binaryWriter.Write(Encoding.ASCII.GetBytes("PMSG")); // Header
                binaryWriter.WriteBE(packet.GetID()); // ID
                packet.Write(binaryWriter); // Data
                binaryWriter.Flush();
            }

            aes.GenerateIV();
            byte[] packetData = memoryStream.ToArray();
            byte[] encryptedPacketData = aes.CreateEncryptor()
                .TransformFinalBlock(packetData, 0, packetData.Length);
            memoryStream.Dispose();

            tcpStream.Write(aes.IV, 0, 16);
            tcpStream.Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(encryptedPacketData.Length)), 0, 4);
            tcpStream.Write(encryptedPacketData, 0, encryptedPacketData.Length);
            tcpStream.Flush();

            if (packet.GetID() != 255)
                Program.Console.WriteMessage($"[Networking] Sent packet" +
                    $" {packet.GetType().Name.ToUpper()} ({packet.GetID()})");
        }

        private void ReadThread_Func() 
        {
            while (IsConnected)
            {
                try
                {
                    byte[] iv = new byte[16];
                    tcpStream.Read(iv, 0, iv.Length);
                    byte[] encryptedDataSize = new byte[4];
                    tcpStream.Read(encryptedDataSize, 0, encryptedDataSize.Length);

                    byte[] encryptedData = new byte[
                        IPAddress.NetworkToHostOrder(BitConverter.ToInt32(encryptedDataSize, 0))];
                    int readAmount = tcpStream.Read(encryptedData, 0, encryptedData.Length);
                    if (readAmount == 0) throw new ConnectionException("Client disconnect");

                    aes.IV = iv;
                    byte[] decryptedData = aes.CreateDecryptor().TransformFinalBlock(
                        encryptedData, 0, encryptedData.Length);
                    BinaryReader binaryReader = new BinaryReader(
                        new MemoryStream(decryptedData), Encoding.BigEndianUnicode);

                    int headerPart0 = binaryReader.ReadByte();
                    int headerPart1 = binaryReader.ReadByte();
                    int headerPart2 = binaryReader.ReadByte();
                    int headerPart3 = binaryReader.ReadByte();

                    // PMSG
                    if (headerPart0 != 'P' || 
                        headerPart1 != 'M' || 
                        headerPart2 != 'S' || 
                        headerPart3 != 'G')
                        throw new ConnectionException("Bad packet header!");

                    int id = binaryReader.ReadBEInt();
                    IPacket packet = Packets.GetPacketByID(id);

                    if (packet == null)
                        throw new ConnectionException($"Bad packet ID: {id}");

                    packet.Read(binaryReader);
                    ReceivedPacket.Invoke(packet);

                    Thread.Sleep(1);
                }
                catch (Exception ex)
                {
                    if (!(ex is IOException || ex is ConnectionException))
                    {
                        Disconnect($"Internal error -> {ex.Message}");
                        Program.Console.WriteMessage($"Internal error: {ex}");
                        MsgBox.Show(null, 
                            "An internal error has occured! For more information," +
                            " check the console (Help > Toggle Console)", 
                            "Internal Error", 
                            MsgBoxIconType.ERROR);
                    }
                    else 
                    {
                        Disconnect(ex.Message);
                    }
                    return;
                }
            }
        }
    }
}
