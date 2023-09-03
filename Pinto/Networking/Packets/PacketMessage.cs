using System.IO;

namespace PintoNS.Networking
{
    public class PacketMessage : IPacket
    {
        public string ContactName { get; protected set; }
        public string Sender { get; protected set; }
        public string Message { get; protected set; }

        public PacketMessage() { }

        public PacketMessage(string contactName, string message)
        {
            ContactName = contactName;
            Sender = "";
            Message = message;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadPintoString(BinaryWriterReaderExtensions.USERNAME_MAX);
            Sender = reader.ReadPintoString(BinaryWriterReaderExtensions.USERNAME_MAX);
            Message = reader.ReadPintoString(512);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ContactName, BinaryWriterReaderExtensions.USERNAME_MAX);
            writer.WritePintoString(Sender, BinaryWriterReaderExtensions.USERNAME_MAX);
            writer.WritePintoString(Message, 512);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleMessagePacket(this);
        }

        public int GetID()
        {
            return 3;
        }
    }
}
