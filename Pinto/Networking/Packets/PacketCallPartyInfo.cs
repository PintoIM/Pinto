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
        public int Port { get; protected set; }

        public PacketCallPartyInfo() { }

        public PacketCallPartyInfo(string ipaddress, int port)
        {
            IPAddress = ipaddress;
            Port = port;
        }

        public void Read(BinaryReader reader)
        {
            IPAddress = reader.ReadUTF16String();
            Port = reader.ReadBEInt();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF16String(IPAddress);
            writer.WriteBE(Port);
        }

        public void Handle(NetworkHandler netHandler)
        {
            //netHandler.HandleCallPartyInfoPacket(this);
        }

        public int GetID()
        {
            return 13;
        }
    }
}
