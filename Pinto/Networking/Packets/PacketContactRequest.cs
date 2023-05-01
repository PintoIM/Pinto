using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketContactRequest : IPacket
    {
        public string ContactName { get; protected set; }

        public PacketContactRequest() { }

        public PacketContactRequest(string contactName)
        {
            ContactName = contactName;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadUTF16String();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF16String(ContactName);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleContactRequestPacket(this);
        }

        public int GetID()
        {
            return 9;
        }
    }
}
