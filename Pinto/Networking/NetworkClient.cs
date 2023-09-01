using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using PintoNS.General;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        private ICryptoTransform cryptoDecryptor;
        private ICryptoTransform cryptoEncryptor;
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
                await tcpClient.ConnectAsync(ip, port);
                IP = ip;
                Port = port;
                IsConnected = true;

                tcpStream = tcpClient.GetStream();
                readThread = new Thread(new ThreadStart(ReadThread_Func));
                Handshake();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        private void Handshake()
        {
            Program.Console.WriteMessage("[Networking] Handshaking AES key...");

            BinaryReader binaryReader = new BinaryReader(tcpStream, Encoding.BigEndianUnicode);
            BinaryWriter binaryWriter = new BinaryWriter(tcpStream, Encoding.BigEndianUnicode);

            int sizeOfPublicKey = binaryReader.ReadBEInt();
            byte[] publicKey = binaryReader.ReadBytes(sizeOfPublicKey);

            string publicKeyStr = BitConverter.ToString(publicKey).Replace("-", "");
            string publicKeyStrSplit = string.Join("\n", Program.SplitStringIntoChunks(publicKeyStr, 64));
            Program.Console.WriteMessage($"[Networking] RSA public key:\n{publicKeyStrSplit}");

            aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.GenerateKey();
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

            cryptoDecryptor = aes.CreateDecryptor();
            cryptoEncryptor = aes.CreateEncryptor();
            readThread.Start();
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
            cryptoDecryptor = null;
            cryptoEncryptor = null;

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

            byte[] packetData = memoryStream.ToArray();
            byte[] encryptedPacketData = cryptoEncryptor.TransformFinalBlock(packetData, 0, packetData.Length);
            memoryStream.Dispose();

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
                    byte[] encryptedDataSize = new byte[4];
                    tcpStream.Read(encryptedDataSize, 0, encryptedDataSize.Length);

                    byte[] encryptedData = new byte[
                        IPAddress.NetworkToHostOrder(BitConverter.ToInt32(encryptedDataSize, 0))];
                    int readAmount = tcpStream.Read(encryptedData, 0, encryptedData.Length);
                    if (readAmount == 0) throw new ConnectionException("Client disconnect");

                    byte[] decryptedData = cryptoDecryptor.TransformFinalBlock(
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
