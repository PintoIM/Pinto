using PintoNS.General;
using System.IO;

namespace PintoNS.Networking
{
    public class PacketStatus : IPacket
    {
        public string ContactName { get; protected set; }
        public UserStatus Status { get; protected set; }
        public string MOTD { get; protected set; }

        public PacketStatus() { }

        public PacketStatus(string contactName, UserStatus status, string motd)
        {
            ContactName = contactName;
            Status = status;
            MOTD = motd;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadPintoString(BinaryWriterReaderExtensions.USERNAME_MAX);
            Status = (UserStatus) reader.ReadBEInt();
            MOTD = reader.ReadPintoString(64);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, BinaryWriterReaderExtensions.USERNAME_MAX);
            writer.WriteBE((int) Status);
            writer.WritePintoString(MOTD, 64);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleStatusPacket(this);
        }

        public int GetID()
        {
            return 8;
        }
    }
}
