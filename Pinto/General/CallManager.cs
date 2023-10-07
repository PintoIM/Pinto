using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PintoNS.General
{
    public class CallManager
    {
        public bool InCall;
        public bool IsHost;
        public UdpClient Client;
        public int ClientPort;
        public IPEndPoint CallHost;
        public readonly List<CallParticipant> Participants = new List<CallParticipant>();
        private Thread recvThread;
        public event Action<string> ParticipantAdded; 
        public event Action<string> ParticipantRemoved; 
        public event Action CallEnded;
        public event Action CallStarted;
        public event Action<string> CallFailed;
        public event Action<byte[]> CallReceivedAudio;
        public Timer TimeOutTimer;
        public int TicksSinceNoParticipant;
        public bool Started;
        private NATUPNPLib.UPnPNATClass upnpRouter;
        private NATUPNPLib.IStaticPortMapping upnpMapping;
        public string ExternalLocalIP;

        public class CallParticipant
        {
            public string IP;
            public int Port;
            public string Name;
            public int TicksSinceLastAudioPacket;
        }

        public struct CallPacket
        {
            public byte ID;
            public byte[] Data;

            public CallPacket(byte id, byte[] data)
            {
                ID = id;
                Data = data;
            }

            public static CallPacket FromData(byte[] data)
            {
                CallPacket packet = new CallPacket();
                packet.ID = data[0];
                packet.Data = data.Skip(1).ToArray();
                return packet;
            }

            public static byte[] ToData(CallPacket packet)
            {
                byte[] data = new byte[] { packet.ID }.Concat(packet.Data).ToArray();
                return data;
            }
        }

        public void StartCall()
        {
            Program.Console.WriteMessage("[CallManager] Starting to host call...");
            Client = new UdpClient(0);
            recvThread = new Thread(new ThreadStart(RecvThread_Func));
            ClientPort = ((IPEndPoint)Client.Client.LocalEndPoint).Port;
            TimeOutTimer = new Timer(new TimerCallback(TimeOutTimer_Func), null, 0, 1000);

            // -1744830452 = SIO_UDP_CONNRESET
            Client.Client.IOControl(
                (IOControlCode)(-1744830452),
                new byte[] { 0, 0, 0, 0 },
                null
            );
            IsHost = true;
            InCall = true;
            recvThread.Start();

            Program.Console.WriteMessage("[CallManager] Adding the UPnP mapping...");
            upnpRouter = new NATUPNPLib.UPnPNATClass();
            if (upnpRouter.StaticPortMappingCollection != null)
            {
                try
                {
                    upnpMapping = upnpRouter.StaticPortMappingCollection.Add(ClientPort, "UDP",
                        ClientPort, GetAllLocalIPv4(NetworkInterfaceType.Ethernet).FirstOrDefault(), true, "Pinto!");
                    ExternalLocalIP = upnpMapping.ExternalIPAddress;
                    Program.Console.WriteMessage($"[CallManager] Aquired public IP from UPnP: {ExternalLocalIP}");
                }
                catch (Exception ex)
                {
                    Program.Console.WriteMessage($"[CallManager] Unable to open port" +
                        $" {ClientPort} using UPnP: {ex}");
                    StopCall(true, "UPnP failed");
                    return;
                }
            }
            else
            {
                Program.Console.WriteMessage($"[CallManager] Router does not support UPnP");
                StopCall(true, "UPnP not supported");
                return;
            }

            Program.Console.WriteMessage($"[CallManager] Started hosting call, listening on {ClientPort}");
            Program.Console.WriteMessage($"[CallManager] IsHost: {IsHost}");
            Program.Console.WriteMessage($"[CallManager] InCall: {InCall}");
        }

        public void JoinCall(string hostIP, int hostPort, string name)
        {
            Program.Console.WriteMessage("[CallManager] Joining call...");
            Client = new UdpClient(0);
            recvThread = new Thread(new ThreadStart(RecvThread_Func));
            TimeOutTimer = new Timer(new TimerCallback(TimeOutTimer_Func), null, 0, 1000);

            // -1744830452 = SIO_UDP_CONNRESET
            Client.Client.IOControl(
                (IOControlCode)(-1744830452),
                new byte[] { 0, 0, 0, 0 },
                null
            );
            IsHost = false;
            InCall = true;
            CallHost = new IPEndPoint(IPAddress.Parse(hostIP), hostPort);
            recvThread.Start();

            Program.Console.WriteMessage($"[CallManager] Joined the call");
            Program.Console.WriteMessage($"[CallManager] IsHost: {IsHost}");
            Program.Console.WriteMessage($"[CallManager] InCall: {InCall}");

            SendPacket(new CallPacket(0x00, Encoding.ASCII.GetBytes(name)), CallHost);
        }

        public void StopCall(bool failed = false, string failureReason = "Authentication failure")
        {
            if (!InCall) return;
            Program.Console.WriteMessage("[CallManager] Stopping the call...");

            InCall = false;
            IsHost = false;

            Program.Console.WriteMessage("[CallManager] Removing the UPnP mapping...");
            try
            {
                if (upnpRouter != null && upnpRouter.StaticPortMappingCollection != null && upnpMapping != null)
                    upnpRouter.StaticPortMappingCollection.Remove(
                        upnpMapping.InternalPort, upnpMapping.Protocol);
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[CallManager] Failed to remove the UPnP mapping: {ex}");
            }

            if (Client != null) Client.Close();
            if (recvThread != null) recvThread.Abort();
            if (TimeOutTimer != null) TimeOutTimer.Dispose();

            Client = null;
            recvThread = null;
            ClientPort = 0;
            CallHost = null;
            TimeOutTimer = null;
            Participants.Clear();
            TicksSinceNoParticipant = 0;

            if (failed)
                if (CallFailed != null)
                    CallFailed.Invoke(failureReason);
            else
                if (CallEnded != null)
                    CallEnded.Invoke();

            Program.Console.WriteMessage($"[CallManager] Stopped the call");
        }

        public void SendToParticipants(CallPacket packet, params string[] exclusionList)
        {
            foreach (CallParticipant participant in Participants)
            {
                if (exclusionList.Contains(participant.Name)) continue;
                SendPacket(packet, new IPEndPoint(
                    IPAddress.Parse(participant.IP), participant.Port));
            }
        }

        public CallParticipant AddParticipant(string ip, int port, string name)
        {
            if (GetParticipant(ip, port) != null || GetParticipant(name) != null || 
                name.Length < 3 || name.Length > 16 || Participants.Count + 1 > 1)
                return null;
            CallParticipant callParticipant = new CallParticipant();
            callParticipant.IP = ip;
            callParticipant.Port = port;
            callParticipant.Name = name;
            Participants.Add(callParticipant);
            if (ParticipantAdded != null)
                ParticipantAdded.Invoke(name);
            return callParticipant;
        }

        public void RemoveParticipant(string name)
        {
            foreach (CallParticipant participant in Participants.ToArray())
            {
                if (participant.Name == name)
                {
                    Participants.Remove(participant);
                    if (ParticipantRemoved != null)
                        ParticipantRemoved.Invoke(name);
                    return;
                }
            }
        }

        public CallParticipant GetParticipant(string name)
        {
            foreach (CallParticipant participant in Participants)
            {
                if (participant.Name == name)
                    return participant;
            }

            return null;
        }

        public CallParticipant GetParticipant(string ip, int port)
        {
            foreach (CallParticipant participant in Participants)
            {
                if (participant.IP == ip && participant.Port == port)
                    return participant;
            }

            return null;
        }

        public void SendPacket(CallPacket packet, IPEndPoint endPoint)
        {
            byte[] data = CallPacket.ToData(packet);
            Client.Send(data, data.Length, endPoint);
        }

        // "Borrowed" from https://stackoverflow.com/a/24814027
        public static string[] GetAllLocalIPv4(NetworkInterfaceType type)
        {
            List<string> ipAddrList = new List<string>();

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            ipAddrList.Add(ip.Address.ToString());
                    }
                }
            }

            return ipAddrList.ToArray();
        }

        private void HandlePacket_Host(IPEndPoint receiveEndPoint, CallPacket receivePacket, 
            string receiveIP, int receivePort)
        {
            if (receivePacket.ID != 0x02)
                Program.Console.WriteMessage($"[CallManager]" +
                    $" {receiveEndPoint} sent packet {receivePacket.ID}");
            CallParticipant callParticipant = GetParticipant(receiveIP, receivePort);

            if (callParticipant == null && 
                receivePacket.ID != 0x00)
            {
                return;
            }

            switch (receivePacket.ID)
            {
                // LOGIN
                case 0x00:
                    string userName = Encoding.ASCII.GetString(receivePacket.Data);
                    Program.Console.WriteMessage($"[CallManager] Authenticating " +
                        $"{userName} ({receiveEndPoint})...");
                    callParticipant = AddParticipant(receiveIP, receivePort, userName);
            
                    if (callParticipant == null)
                    {
                        Program.Console.WriteMessage($"[CallManager]" +
                            $" Failed to authenticate {receiveEndPoint}!");

                        // Packet 1 is LOGIN_ERROR
                        SendPacket(new CallPacket(1, new byte[0]), receiveEndPoint);

                        // This is the first participant
                        if (Participants.Count - 1 < 1 && CallFailed != null)
                            StopCall(true);

                        return;
                    }

                    Program.Console.WriteMessage($"[CallManager]" +
                        $" {userName} ({receiveEndPoint}) has authenticated successfully");
                    SendPacket(new CallPacket(0, new byte[0]), receiveEndPoint);

                    // This is the first participant
                    if (Participants.Count - 1 < 1 && CallStarted != null)
                    {
                        CallStarted.Invoke();
                        Started = true;
                        TicksSinceNoParticipant = 0;
                    }

                    break;
                // AUDIO_DATA
                case 0x02:
                    callParticipant.TicksSinceLastAudioPacket = 0;
                    //SendToParticipants(receivePacket, callParticipant.Name);
                    if (CallReceivedAudio != null)
                        CallReceivedAudio.Invoke(receivePacket.Data);
                    break;
                default:
                    Program.Console.WriteMessage($"[CallManager] Received bad packet from {receiveEndPoint}");
                    break;
            }
        }

        private void HandlePacket_Client(CallPacket receivePacket)
        {
            if (receivePacket.ID != 0x02)
                Program.Console.WriteMessage($"[CallManager]" +
                    $" Server sent packet {receivePacket.ID}");

            switch (receivePacket.ID)
            {
                // LOGIN
                case 0x00:
                    Program.Console.WriteMessage("[CallManager] Authenticated successfully");
                    if (CallStarted != null)
                        CallStarted.Invoke();
                    Started = true;
                    TicksSinceNoParticipant = -1000;
                    break;
                // LOGIN_FAILED
                case 0x01:
                    Program.Console.WriteMessage("[CallManager] Failed to authenticate");
                    StopCall(true);
                    break;
                // AUDIO_DATA
                case 0x02:
                    if (CallReceivedAudio != null)
                        CallReceivedAudio.Invoke(receivePacket.Data);
                    break;
                default:
                    Program.Console.WriteMessage("[CallManager] Received bad packet");
                    break;
            }
        }

        private void TimeOutTimer_Func(object state)
        {
            if (!InCall) return;

            if (IsHost)
            {
                foreach (CallParticipant participant in Participants.ToArray())
                {
                    participant.TicksSinceLastAudioPacket++;

                    if (participant.TicksSinceLastAudioPacket >= 5)
                    {
                        Program.Console.WriteMessage($"[CallManager] {participant.Name} timed out");
                        RemoveParticipant(participant.Name);

                        // That was the last participant
                        if (Participants.Count < 1)
                        {
                            StopCall();
                            return;
                        }
                    }
                }

                if (Participants.Count < 1)
                {
                    TicksSinceNoParticipant++;

                    if (TicksSinceNoParticipant >= 5)
                    {
                        Program.Console.WriteMessage($"[CallManager] Timed out whilst waiting for participants");
                        StopCall(true, "Timed out");
                        return;
                    }
                }
            }
            else
            {
                if (TicksSinceNoParticipant >= 0)
                {
                    TicksSinceNoParticipant++;

                    if (TicksSinceNoParticipant >= 5)
                    {
                        Program.Console.WriteMessage($"[CallManager] Timed out whilst connecting to host");
                        StopCall(true, "Timed out");
                        return;
                    }
                }
            }
        }

        private void RecvThread_Func()
        {
            while (InCall && IsHost)
            {
                try
                {
                    IPEndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any, ClientPort);
                    CallPacket receivePacket = CallPacket.FromData(Client.Receive(ref receiveEndPoint));
                    string receiveIP = receiveEndPoint.Address.ToString();
                    int receivePort = receiveEndPoint.Port;
                    HandlePacket_Host(receiveEndPoint, receivePacket, receiveIP, receivePort);
                }
                catch
                {
                }
            }

            while (InCall && !IsHost)
            {
                try
                {
                    CallPacket receivePacket = CallPacket.FromData(Client.Receive(ref CallHost));
                    HandlePacket_Client(receivePacket);
                }
                catch
                {
                }
            }
        }
    }
}