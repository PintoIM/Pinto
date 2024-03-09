using PintoNS.Networking.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using PintoNS.Forms;

namespace PintoNS.Networking
{
    public class NetworkTCPManager : INetworkManager
    {
        private const int PACKET_SIZE_MODIFIER = 4 + 4 + 16 + 4;
        private TcpClient tcpClient;
        private NetworkAddress address;
        private BinaryReader streamReader;
        private BinaryWriter streamWriter;
        private Aes aes;
        private bool connected;
        private bool isTerminating;
        private bool isClosing;
        private string terminationReason;
        private object sendQueueLock = new object();
        private int sendQueueByteLength;
        private readonly List<IPacket> readPackets = new List<IPacket>();
        private readonly List<IPacket> sendPackets = new List<IPacket>();
        private Thread writeThread;
        private Thread readThread;
        private NetBaseHandler netHandler;
        private int timeSinceLastRead;

        public NetworkTCPManager(TcpClient tcpClient, string threadName, NetBaseHandler netHandler)
        {
            if (tcpClient == null)
                throw new ArgumentException("No socket specified");
            
            this.tcpClient = tcpClient;
            this.netHandler = netHandler;
            if (Settings.SpecifySocketTOS)
                tcpClient.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, 24); // IPTOS_THROUGHPUT + IPTOS_LOWDELAY
            streamReader = new BinaryReader(tcpClient.GetStream());
            streamWriter = new BinaryWriter(tcpClient.GetStream());
            address = new NetworkAddress(tcpClient.Client);
            connected = true;

            readThread = new Thread(() =>
            {
                while (true)
                {
                    if (!connected || isClosing)
                        break;
                    ReadPacket();
                }
            })
            {
                Name = threadName + "-Reader"
            };

            writeThread = new Thread(() =>
            {
                while (true)
                {
                    if (!connected)
                        break;
                    SendPacket();
                }
            })
            {
                Name = threadName + "-Writer"
            };
        }

        public void OnHandshaked(Aes aes)
        {
            if (aes == null)
                throw new ArgumentException("No secret key specified");

            this.aes = aes;
            readThread.Start();
            writeThread.Start();
        }

        public void SetNetHandler(NetBaseHandler netHandler)
        {
            this.netHandler = netHandler;
        }

        public void AddToQueue(IPacket packet)
        {
            if (isClosing)
                return;

            lock (sendQueueLock)
            {
                // XXX: Should be fine to use the un-encrypted size
                sendQueueByteLength += PACKET_SIZE_MODIFIER + packet.GetPacketSize();
                sendPackets.Add(packet);
            }
        }

        private void SendPacket()
        {
            try
            {
                if (sendPackets.Count == 0)
                {
                    Thread.Sleep(10);
                    return;
                }

                MemoryStream memoryStream = new MemoryStream();
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                BufferedStream bufferedStream = new BufferedStream(streamWriter.BaseStream, 4096);
                IPacket packet;

                lock (sendQueueLock)
                {
                    packet = sendPackets[0];
                    sendPackets.RemoveAt(0);
                    // XXX: Should be fine to use the un-encrypted size
                    sendQueueByteLength -= PACKET_SIZE_MODIFIER + packet.GetPacketSize();
                }

                // Write the packet
                binaryWriter.WriteInt(packet.GetID());
                packet.Write(binaryWriter);

                aes.GenerateIV();
                byte[] packetData = memoryStream.ToArray();
                byte[] encryptedPacketData = aes.CreateEncryptor()
                    .TransformFinalBlock(packetData, 0, packetData.Length);

                bufferedStream.Write(Encoding.ASCII.GetBytes("PMSG"), 0, 4);
                bufferedStream.WriteBytes(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(encryptedPacketData.Length)));
                bufferedStream.Write(aes.IV, 0, 16);
                bufferedStream.WriteBytes(encryptedPacketData);
                bufferedStream.Flush();

                // Network monitor snippet
                if (!(packet is PacketKeepAlive))
                    NetMonitorForm.Instance.AddPacket(packet, packetData, false);
            }
            catch (ThreadInterruptedException)
            {
            }
            catch (Exception ex)
            {
                if (isTerminating || isClosing)
                    return;
                HandleNetworkError(ex);
            }
        }

        private void ReadPacket()
        {
            try
            {
                int headerPart0 = streamReader.ReadByte();
                int headerPart1 = streamReader.ReadByte();
                int headerPart2 = streamReader.ReadByte();
                int headerPart3 = streamReader.ReadByte();

                if (headerPart0 != 'P' ||
                    headerPart1 != 'M' ||
                    headerPart2 != 'S' ||
                    headerPart3 != 'G')
                {
                    Shutdown("Bad packet header");
                    return;
                }

                byte[] encryptedDataSizeRaw = new byte[4];
                streamReader.Read(encryptedDataSizeRaw, 0, 4);

                byte[] iv = new byte[16];
                streamReader.Read(iv, 0, 16);

                int encryptedDataSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(encryptedDataSizeRaw, 0));
                byte[] encryptedData = streamReader.ReadBytes(encryptedDataSize);

                aes.IV = iv;
                byte[] decryptedData = aes.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                BinaryReader binaryReader = new BinaryReader(new MemoryStream(decryptedData));

                int packetID = binaryReader.ReadBEInt();
                IPacket packet = PacketFactory.GetPacketByID(packetID);

                if (packet == null)
                {
                    Shutdown("Bad packet ID " + packetID);
                    return;
                }

                packet.Read(binaryReader);
                readPackets.Add(packet);

                // Network monitor snippet
                if (!(packet is PacketKeepAlive))
                    NetMonitorForm.Instance.AddPacket(packet, decryptedData, true);
            }
            catch (Exception ex)
            {
                if (isTerminating || isClosing)
                    return;

                if (ex is EndOfStreamException) 
                {
                    Shutdown("Server disconnect");
                    return;
                }

                HandleNetworkError(ex);
            }
        }

        private void HandleNetworkError(Exception ex)
        {
            Shutdown(ex.ToString());
        }

        public void Shutdown(string reason)
        {
            if (!connected)
                return;
            isTerminating = true;
            terminationReason = reason;

            new Thread(() =>
            {
                try
                {
                    Thread.Sleep(5000);
                    if (readThread.IsAlive)
                    {
                        try
                        {
                            readThread.Abort();
                        }
                        catch (Exception) { }
                    }

                    if (writeThread.IsAlive) 
                    {
                        try
                        {
                            writeThread.Abort();
                        }
                        catch (Exception) { }
                    }
                }
                catch (ThreadInterruptedException) { }
            })
            {
                Name = "Network-Terminator"
            }.Start();
            connected = false;

            try
            {
                streamReader.Close();
            }
            catch (Exception) { }

            try
            {
                streamWriter.Close();
            }
            catch (Exception) { }

            try
            {
                tcpClient.Close();
            }
            catch (Exception) { }
        }

        public void ProcessReceivedPackets()
        {
            if (sendQueueByteLength > 0x100000) // A megabyte
                Shutdown("Send buffer overflow");

            if (readPackets.Count == 0 && timeSinceLastRead++ == 600) // 30 seconds
                Shutdown("No packet read within 30 seconds");
            else if (readPackets.Count > 0)
                timeSinceLastRead = 0;

            int packetsLimit = 100;
            while (readPackets.Count > 0 && packetsLimit-- >= 0)
            {
                IPacket packet = readPackets[0];
                readPackets.RemoveAt(0);
                netHandler.HandlePacket(packet);
            }

            if (isTerminating && readPackets.Count == 0)
                netHandler.HandleTermination(terminationReason);
        }

        public NetworkAddress GetAddress() => address;

        public BinaryReader GetInputStream() => streamReader;

        public BinaryWriter GetOutputStream() => streamWriter;

        public void Interrupt()
        {
            readThread.Interrupt();
            writeThread.Interrupt();
        }

        public void Close()
        {
            isClosing = true;
            readThread.Interrupt();

            new Thread(() =>
            {
                try
                {
                    Thread.Sleep(2000);
                    if (connected)
                    {
                        writeThread.Interrupt();
                        Shutdown("Connection closed");
                    }
                }
                catch (Exception) { }
            })
            {
                Name = "Network-Closer"
            }.Start();
        }
    }
}
