using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public static class Packets
    {
        private static Dictionary<int, Type> PacketMap = new Dictionary<int, Type>();

        static Packets() 
        {
            PacketMap.Add(0, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketRegister));
            PacketMap.Add(2, typeof(PacketLogout));
            PacketMap.Add(3, typeof(PacketMessage));
            PacketMap.Add(5, typeof(PacketInWindowPopup));
            PacketMap.Add(6, typeof(PacketAddContact));
            PacketMap.Add(7, typeof(PacketRemoveContact));
            PacketMap.Add(8, typeof(PacketStatus));
            PacketMap.Add(9, typeof(PacketContactRequest));
            PacketMap.Add(10, typeof(PacketClearContacts));
            /*
            PacketMap.Add(11, typeof(PacketCallStart));
            PacketMap.Add(12, typeof(PacketCallRequest));
            PacketMap.Add(13, typeof(PacketCallPartyInfo));
            PacketMap.Add(14, typeof(PacketCallEnd));*/
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
