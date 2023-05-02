using System.IO;

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
            ContactName = reader.ReadPintoString(BinaryWriterReaderExtensions.USERNAME_MAX);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, BinaryWriterReaderExtensions.USERNAME_MAX);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleRemoveContactPacket(this);
        }

        public int GetID()
        {
            return 7;
        }
    }
}
