using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketCallPartyInfo : IPacket
    {
        public string IPAddress { get; protected set; }

        public PacketCallPartyInfo() { }

        public PacketCallPartyInfo(string ipaddress)
        {
            IPAddress = ipaddress;
        }

        public void Read(BinaryReader reader)
        {
            IPAddress = reader.ReadUTF8String();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF8String(IPAddress);
        }

        public int GetID()
        {
            return 12;
        }
    }
}
