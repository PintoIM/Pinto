using PintoNS.Contacts;
using System.IO;

namespace PintoNS.Networking.Packets
{
    public class PacketAddContact : IPacket
    {
        public string ContactName { get; protected set; }
        public UserStatus Status { get; protected set; }
        public string MOTD { get; protected set; }

        public PacketAddContact() { }

        public PacketAddContact(string contactName, UserStatus status, string motd)
        {
            ContactName = contactName;
            Status = status;
            MOTD = motd;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadPintoString(NetBaseHandler.USERNAME_MAX);
            Status = (UserStatus)reader.ReadBEInt();
            MOTD = reader.ReadPintoString(64);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, NetBaseHandler.USERNAME_MAX);
            writer.WriteBEInt((int)Status);
            writer.WritePintoString(MOTD, 64);
        }

        public int GetPacketSize()
        {
            return NetBaseHandler.USERNAME_MAX + 4 + 64;
        }

        public int GetID()
        {
            return 6;
        }
    }
}
