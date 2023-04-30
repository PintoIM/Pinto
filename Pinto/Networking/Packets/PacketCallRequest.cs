using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketCallRequest : IPacket
    {
        public string ContactName { get; protected set; }

        public PacketCallRequest() { }

        public PacketCallRequest(string contactName)
        {
            ContactName = contactName;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadASCIIString();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteASCIIString(ContactName);
        }

        public void Handle(NetworkHandler netHandler)
        {
            //netHandler.HandleCallRequestPacket(this);
        }

        public int GetID()
        {
            return 12;
        }
    }
}
