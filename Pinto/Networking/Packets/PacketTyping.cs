using PintoNS.General;
using System.IO;

namespace PintoNS.Networking
{
    public class PacketTyping : IPacket
    {
        public string ContactName { get; protected set; }
        public bool Typing { get; protected set; }

        public PacketTyping() { }

        public PacketTyping(string contactName, bool typing)
        {
            ContactName = contactName;
            Typing = typing;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadPintoString(BinaryWriterReaderExtensions.USERNAME_MAX);
            Typing = reader.ReadByte() == 0x01;
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, BinaryWriterReaderExtensions.USERNAME_MAX);
            writer.Write(Typing ? 0x01 : 0x00);
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
