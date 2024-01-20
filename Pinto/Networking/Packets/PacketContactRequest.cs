using System.IO;

namespace PintoNS.Networking.Packets
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
            ContactName = reader.ReadPintoString(NetBaseHandler.USERNAME_MAX + 4);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, NetBaseHandler.USERNAME_MAX + 4);
        }

        public int GetPacketSize()
        {
            return NetBaseHandler.USERNAME_MAX + 4;
        }

        public int GetID()
        {
            return 9;
        }
    }
}
