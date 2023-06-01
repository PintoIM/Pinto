using PintoNS.Forms.Notification;
using System;
using System.IO;
using System.Net.Sockets;
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
        private BinaryReader tcpBinaryReader;
        private BinaryWriter tcpBinaryWriter;
        private Thread readThread;
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
                tcpBinaryReader = new BinaryReader(tcpStream, Encoding.BigEndianUnicode);
                tcpBinaryWriter = new BinaryWriter(tcpStream, Encoding.BigEndianUnicode);
                readThread = new Thread(new ThreadStart(ReadThread_Func));
                readThread.Start();

                return (true, null);
            }
            catch (Exception ex)
            {
                Disconnect(null);
                return (false, ex);
            }
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
            tcpBinaryReader = null;
            tcpBinaryWriter = null;

            if (IsConnected && !ignoreDisconnectReasonValue) 
            {
                Disconnected.Invoke(reason);
            }
            IsConnected = false;
        }

        public void SendPacket(IPacket packet) 
        {
            if (!IsConnected) return;

            lock (sendLock)
            {
                // Header
                tcpBinaryWriter.Write(Encoding.ASCII.GetBytes("PMSG"));

                // Size
                tcpBinaryWriter.WriteBE(packet.GetSize());

                // ID
                tcpBinaryWriter.WriteBE(packet.GetID());

                if (packet.GetSize() > 0) 
                {
                    // Data
                    packet.Write(tcpBinaryWriter);
                    tcpBinaryWriter.Flush();
                }
            }

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
                    int headerPart0 = tcpBinaryReader.ReadByte();
                    int headerPart1 = tcpBinaryReader.ReadByte();
                    int headerPart2 = tcpBinaryReader.ReadByte();
                    int headerPart3 = tcpBinaryReader.ReadByte();

                    if (headerPart0 == -1 || 
                        headerPart1 == -1 ||
                        headerPart2 == -1 ||
                        headerPart3 == -1)
                        throw new ConnectionException("Client disconnect");

                    // PMSG
                    if (headerPart0 != 0x50 || 
                        headerPart1 != 0x4d || 
                        headerPart2 != 0x53 || 
                        headerPart3 != 0x47)
                        throw new ConnectionException("Bad packet header!");

                    int size = tcpBinaryReader.ReadBEInt();
                    int id = tcpBinaryReader.ReadBEInt();
                    IPacket packet = Packets.GetPacketByID(id);

                    if (packet == null)
                        throw new ConnectionException($"Bad packet ID: {id}");

                    if (size > 0) 
                    {
                        byte[] data = tcpBinaryReader.ReadBytes(size);
                        BinaryReader tempReader = new BinaryReader(new MemoryStream(data));
                        packet.Read(tempReader);
                        tempReader.Close();
                        tempReader.Dispose();
                    }

                    ReceivedPacket.Invoke(packet);
                    Thread.Sleep(1);
                }
                catch (Exception ex)
                {
                    if (!(ex is IOException || ex is ConnectionException))
                    {
                        Disconnect($"Internal error -> {ex.Message}");
                        Program.Console.WriteMessage($"Internal error: {ex}");
                        MsgBox.ShowNotification(null, 
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
