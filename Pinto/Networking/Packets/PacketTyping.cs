using System.IO;

namespace PintoNS.Networking.Packets
{
    public class PacketTyping : IPacket
    {
        public string ContactName { get; protected set; }
        public bool State { get; protected set; }

        public PacketTyping() { }

        public PacketTyping(string contactName, bool state)
        {
            ContactName = contactName;
            State = state;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadPintoString(NetBaseHandler.USERNAME_MAX);
            State = reader.ReadByte() == 0x01;
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, NetBaseHandler.USERNAME_MAX);
            writer.Write(State ? 0x01 : 0x00);
        }

        public int GetPacketSize()
        {
            return NetBaseHandler.USERNAME_MAX + 1;
        }

        public int GetID()
        {
            return 18;
        }
    }
}
