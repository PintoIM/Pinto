using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketMessage : IPacket
    {
        public string ContactName { get; protected set; }
        public string Sender { get; protected set; }
        public string Message { get; protected set; }

        public PacketMessage() { }

        public PacketMessage(string contactName, string message)
        {
            ContactName = contactName;
            Sender = "";
            Message = message;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadUTF16String();
            Sender = reader.ReadUTF16String();
            Message = reader.ReadUTF16String();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF16String(ContactName);
            writer.WriteUTF16String(Sender);
            writer.WriteUTF16String(Message);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleMessagePacket(this);
        }

        public int GetID()
        {
            return 3;
        }
    }
}
