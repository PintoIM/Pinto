using System.IO;

namespace PintoNS.Networking.Packets
{
    public class PacketMessage : IPacket
    {
        public string ContactName { get; protected set; }
        public string Sender { get; protected set; }
        public string Payload { get; protected set; }

        public PacketMessage() { }

        public PacketMessage(string contactName, string message)
        {
            ContactName = contactName;
            Sender = "";
            Payload = message;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadPintoString(NetBaseHandler.USERNAME_MAX);
            Sender = reader.ReadPintoString(NetBaseHandler.USERNAME_MAX);
            Payload = reader.ReadPintoString(16384);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, NetBaseHandler.USERNAME_MAX);
            writer.WritePintoString(Sender, NetBaseHandler.USERNAME_MAX);
            writer.WritePintoString(Payload, 16384);
        }

        public int GetPacketSize()
        {
            return (NetBaseHandler.USERNAME_MAX * 2) + 16384;
        }

        public int GetID()
        {
            return 3;
        }
    }
}
