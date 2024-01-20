using System.IO;

namespace PintoNS.Networking.Packets
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
            ContactName = reader.ReadPintoString(NetBaseHandler.USERNAME_MAX);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, NetBaseHandler.USERNAME_MAX);
        }

        public int GetPacketSize()
        {
            return NetBaseHandler.USERNAME_MAX;
        }

        public int GetID()
        {
            return 7;
        }
    }
}
