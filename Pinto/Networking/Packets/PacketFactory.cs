using System;
using System.Collections.Generic;

namespace PintoNS.Networking.Packets
{
    public static class PacketFactory
    {
        private static Dictionary<int, Type> packetMap = new Dictionary<int, Type>();

        static PacketFactory()
        {
            packetMap.Add(0, typeof(PacketLogin));
            packetMap.Add(1, typeof(PacketRegister));
            packetMap.Add(2, typeof(PacketLogout));
            packetMap.Add(3, typeof(PacketMessage));
            packetMap.Add(4, typeof(PacketNotification));
            packetMap.Add(6, typeof(PacketAddContact));
            packetMap.Add(7, typeof(PacketRemoveContact));
            packetMap.Add(8, typeof(PacketStatus));
            packetMap.Add(9, typeof(PacketContactRequest));
            packetMap.Add(10, typeof(PacketClearContacts));
            packetMap.Add(11, typeof(PacketCallChangeStatus));
            packetMap.Add(17, typeof(PacketServerInfo));
            packetMap.Add(18, typeof(PacketTyping));
            packetMap.Add(255, typeof(PacketKeepAlive));
        }

        public static IPacket GetPacketByID(int id)
        {
            // Get the packet class by ID
            Type packetType = packetMap.GetValueOrDefault(id, null);

            // Check if the specified ID map was successful
            if (packetType != null)
            {
                try
                {
                    return (IPacket)Activator.CreateInstance(packetType);
                }
                catch (Exception ex)
                {
                    // Failure?
                    Program.Console.WriteMessage($"[Networking] Failed to create a packet: {ex}");
                }
            }

            return null;
        }
    }
}