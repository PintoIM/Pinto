using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private TcpClient tcpClient;
        private NetworkStream tcpStream;
        private Thread readThread;
        private Thread sendThread;
        public Action<string> Disconnected = delegate (string reason) { };
        public Action<IPacket> ReceivedPacket = delegate (IPacket packet) { };
        private LinkedList<IPacket> packetSendQueue = new LinkedList<IPacket>();

        public async Task<(bool, Exception)> Connect(string ip, int port) 
        {
            try
            {
                if (IsConnected) Disconnect("Reconnecting");
                ignoreDisconnectReason = false;

                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(ip, port);
                IsConnected = true;

                tcpStream = tcpClient.GetStream();
                readThread = new Thread(new ThreadStart(ReadThread_Func));
                readThread.Start();
                sendThread = new Thread(new ThreadStart(SendThread_Func));
                sendThread.Start();

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

            tcpClient = null;
            tcpStream = null;
            readThread = null;
            sendThread = null;

            if (IsConnected && !ignoreDisconnectReasonValue) 
            {
                Disconnected.Invoke(reason);
            }
            IsConnected = false;
        }

        public void AddToSendQueue(IPacket packet) 
        {
            if (!IsConnected) return;
            Program.Console.WriteMessage($"[Networking] Added packet {packet.GetType().Name.ToUpper()}" +
                $" ({packet.GetID()}) to the send queue");
            packetSendQueue.AddLast(packet);
        }

        public void ClearSendQueue() 
        {
            packetSendQueue.Clear();
        }

        public void FlushSendQueue() 
        {
            if (!IsConnected) return;

            foreach (IPacket packet in packetSendQueue.ToArray()) 
            {
                try
                {
                    if (!IsConnected) return;
                    if (packet == null) continue;
                    BinaryWriter writer = new BinaryWriter(tcpStream, Encoding.UTF8, true);
                    writer.Write((byte)packet.GetID());
                    packet.Write(writer);
                    writer.Flush();
                    writer.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                packetSendQueue.Remove(packet);
            }
        }

        private void ReadThread_Func() 
        {
            while (IsConnected)
            {
                try
                {
                    int packetID = tcpStream.ReadByte();
                    IPacket packet = Packets.GetPacketByID(packetID);

                    if (packetID != -1)
                    {
                        if (packet != null)
                        {
                            BinaryReader reader = new BinaryReader(tcpStream, Encoding.UTF8, true);
                            packet.Read(reader);
                            reader.Dispose();
                            ReceivedPacket.Invoke(packet);
                        }
                        else
                        {
                            throw new ConnectionException("Received invalid packet -> " + packetID);
                        }
                    }
                    else
                    {
                        throw new ConnectionException("Server disconnect");
                    }

                    Thread.Sleep(1);
                }
                catch (Exception ex)
                {
                    if (!(ex is IOException || ex is ConnectionException))
                    {
                        Disconnect($"Internal error -> {ex.Message}");
                        Program.Console.WriteMessage($"Internal error: {ex}");
                    }
                    else 
                    {
                        Disconnect(ex.Message);
                    }
                    return;
                }
            }
        }

        private void SendThread_Func() 
        {
            while (IsConnected) 
            {
                FlushSendQueue();
                Thread.Sleep(1);
            }
        }
    }
}
