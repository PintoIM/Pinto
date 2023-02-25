using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoChat.Networking
{
    public static class Packets
    {
        private static Dictionary<int, Type> PacketMap = new Dictionary<int, Type>();

        static Packets() 
        {
            PacketMap.Add(0, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogout));
            PacketMap.Add(2, typeof(PacketMessage));
            PacketMap.Add(3, typeof(PacketTyping));
        }

        public static IPacket GetPacketByID(int id) 
        {
            Type packetType;

            if (PacketMap.TryGetValue(id, out packetType)) 
            {
                if (typeof(IPacket).IsAssignableFrom(packetType)) 
                {
                    return (IPacket) Activator.CreateInstance(packetType);
                }
            }

            return null;
        }
    }
}
