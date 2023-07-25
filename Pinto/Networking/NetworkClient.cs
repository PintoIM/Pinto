using PintoNS.General;
using System;
using System.IO;
using System.Net.Sockets;
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
            bool sendEvent = IsConnected && !ignoreDisconnectReason;
            IsConnected = false;
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

            if (sendEvent) 
                Disconnected.Invoke(reason);
        }

        public void SendPacket(IPacket packet) 
        {
            if (!IsConnected) return;

            MemoryStream packetData = new MemoryStream();
            BinaryWriter packetDataWriter = new BinaryWriter(packetData);

            lock (sendLock)
            {
                packetDataWriter.Write(Encoding.ASCII.GetBytes("PMSG"));
                packetDataWriter.WriteBE(packet.GetID());
                packet.Write(packetDataWriter);

                packetData.Flush();
                packetData.WriteTo(tcpStream);
                packetData.Dispose();
                packetDataWriter.Dispose();
                
                tcpStream.Flush();
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

                    int id = tcpBinaryReader.ReadBEInt();
                    IPacket packet = Packets.GetPacketByID(id);

                    if (packet == null)
                        throw new ConnectionException($"Bad packet ID: {id}");

                    packet.Read(tcpBinaryReader);
                    ReceivedPacket.Invoke(packet);
                    Thread.Sleep(1);
                }
                catch (Exception ex)
                {
                    if (!IsConnected) return;

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
                }
            }
        }
    }
}
