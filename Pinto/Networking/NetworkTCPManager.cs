using Org.BouncyCastle.Security;
using PintoNS.Networking.Packets;
using PintoNS.Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace PintoNS.Networking
{
    public class NetworkTCPManager : INetworkManager
    {
        private const int PACKET_SIZE_MODIFIER = 4 + 16 + 4 + 4;
        private TcpClient tcpClient;
        private NetworkAddress address;
        private BinaryReader inputStream;
        private BinaryWriter outputStream;
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
            tcpClient.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, 24); // IPTOS_THROUGHPUT + IPTOS_LOWDELAY
            inputStream = new BinaryReader(tcpClient.GetStream());
            outputStream = new BinaryWriter(tcpClient.GetStream());
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
                BufferedStream bufferedStream = new BufferedStream(outputStream.BaseStream, 4096);
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
                int headerPart0 = inputStream.ReadByte();
                int headerPart1 = inputStream.ReadByte();
                int headerPart2 = inputStream.ReadByte();
                int headerPart3 = inputStream.ReadByte();

                // C#'s BinaryReader throws an exception if reached the end
                //if (headerPart0 == -1 ||
                //    headerPart1 == -1 ||
                //    headerPart2 == -1 ||
                //    headerPart3 == -1)
                //{
                //    Shutdown("Server disconnect");
                //    return;
                //}

                if (headerPart0 != 'P' ||
                    headerPart1 != 'M' ||
                    headerPart2 != 'S' ||
                    headerPart3 != 'G')
                {
                    Shutdown("Bad packet header");
                    return;
                }

                byte[] encryptedDataSize = new byte[4];
                inputStream.Read(encryptedDataSize, 0, 4);

                byte[] iv = new byte[16];
                inputStream.Read(iv, 0, 16);

                byte[] encryptedData = new byte[IPAddress.NetworkToHostOrder(BitConverter.ToInt32(encryptedDataSize, 0))];
                int readAmount = inputStream.Read(encryptedData, 0, encryptedData.Length);

                aes.IV = iv;
                byte[] decryptedData = aes.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                BinaryReader dataInputStream = new BinaryReader(new MemoryStream(decryptedData));

                int packetID = dataInputStream.ReadBEInt();
                IPacket packet = PacketFactory.GetPacketByID(packetID);

                if (packet == null)
                {
                    Shutdown("Bad packet ID " + packetID);
                    return;
                }

                packet.Read(dataInputStream);
                readPackets.Add(packet);
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
                inputStream.Close();
            }
            catch (Exception) { }

            try
            {
                outputStream.Close();
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

            if (readPackets.Count == 0) 
            {
                Program.Console.WriteMessage($"{timeSinceLastRead}");
                if (timeSinceLastRead++ == 600) // 30 seconds
                    Shutdown("No packet read within 30 seconds");
            }
            else
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

        public BinaryReader GetInputStream() => inputStream;

        public BinaryWriter GetOutputStream() => outputStream;

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
