using System.IO;

namespace PintoNS.Networking
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
            ContactName = reader.ReadPintoString(BinaryWriterReaderExtensions.USERNAME_MAX);
            State = reader.ReadByte() == 0x01;
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, BinaryWriterReaderExtensions.USERNAME_MAX);
            writer.Write(State ? 0x01 : 0x00);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleTypingPacket(this);
        }

        public int GetID()
        {
            return 18;
        }
    }
}
