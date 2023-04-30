using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketAddContact : IPacket
    {
        public string ContactName { get; protected set; }

        public PacketAddContact() { }

        public PacketAddContact(string contactName)
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
            netHandler.HandleAddContactPacket(this);
        }

        public int GetID()
        {
            return 6;
        }
    }
}
