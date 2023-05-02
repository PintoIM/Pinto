using System.IO;

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
            ContactName = reader.ReadPintoString(BinaryWriterReaderExtensions.USERNAME_MAX);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, BinaryWriterReaderExtensions.USERNAME_MAX);
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
