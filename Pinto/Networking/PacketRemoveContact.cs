using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketRemoveContact : IPacket
    {
        public string ContactName { get; protected set; }

        public PacketRemoveContact() { }

        public PacketRemoveContact(string contactName)
        {
            ContactName = contactName;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadUTF8String();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF8String(ContactName);
        }

        public int GetID()
        {
            return 7;
        }
    }
}
