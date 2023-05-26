using PintoNS.General;
using System.IO;

namespace PintoNS.Networking
{
    public class PacketAddContact : IPacket
    {
        public string ContactName { get; protected set; }
        public UserStatus Status { get; protected set; }

        public PacketAddContact() { }

        public PacketAddContact(string contactName, UserStatus status)
        {
            ContactName = contactName;
            Status = status;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadPintoString(BinaryWriterReaderExtensions.USERNAME_MAX);
            Status = (UserStatus)reader.ReadBEInt();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, BinaryWriterReaderExtensions.USERNAME_MAX);
            writer.WriteBE((int)Status);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleAddContactPacket(this);
        }

        public int GetID()
        {
            return 6;
        }

        public int GetSize()
        {
            return BinaryWriterReaderExtensions.GetPintoStringSize(ContactName) + 4;
        }
    }
}
