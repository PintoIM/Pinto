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
            ContactName = reader.ReadUTF8String();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF8String(ContactName);
        }

        public int GetID()
        {
            return 6;
        }
    }
}
