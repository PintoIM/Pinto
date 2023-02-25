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

namespace PintoChat.Networking
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
        public Action<string> Disconnected = delegate (string reason) { };
        public Action<IPacket> ReceivedPacket = delegate (IPacket packet) { };

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

            if (IsConnected && !ignoreDisconnectReasonValue) 
            {
                Disconnected.Invoke(reason);
            }
            IsConnected = false;
        }

        public async Task SendPacket(IPacket packet) 
        {
            MemoryStream stream = new MemoryStream(new byte[packet.GetLength()]);
            BinaryWriter writer = new BinaryWriter(stream);
            packet.Write(writer);

            writer.Flush();
            byte[] data = new byte[] { (byte)packet.GetID() }.Concat(stream.ToArray()).ToArray();
            writer.Close();

            await SendData(data);
        }

        public async Task SendData(byte[] data)
        {
            try
            {
                if (tcpStream != null)
                    await tcpStream.WriteAsync(data, 0, data.Length);
            }
            catch { }
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
                            int packetSize = packet.GetLength();
                            byte[] buffer = new byte[packetSize];

                            int readBytesTotal = 0;
                            int readBytes = readBytesTotal = tcpStream.Read(buffer, 0, packetSize);
;
                            while (readBytesTotal < packetSize)
                            {
                                readBytes = tcpStream.Read(buffer, readBytesTotal, packetSize - readBytesTotal);
                                readBytesTotal += readBytes;
                            }

                            BinaryReader reader = new BinaryReader(new MemoryStream(buffer), Encoding.UTF8);
                            packet.Read(reader);
                            reader.Close();
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
                }
                catch (Exception ex)
                {
                    if (!(ex is IOException || ex is ConnectionException))
                    {
                        Disconnect($"Internal error -> {ex.Message}");
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
