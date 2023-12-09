using System.IO;

namespace PintoNS.Networking
{
    public class PacketNotification : IPacket
    {
        public byte Type { get; protected set; }
        public int AutoCloseDelay { get; protected set; }
        public string Title { get; protected set; }
        public string Body { get; protected set; }

        public PacketNotification() { }

        public PacketNotification(byte type, int autoCloseDelay, string title, string body)
        {
            Type = type;
            AutoCloseDelay = autoCloseDelay;
            Title = title;
            Body = body;
        }

        public void Read(BinaryReader reader)
        {
            Type = reader.ReadByte();
            AutoCloseDelay = reader.ReadBEInt();
            Title = reader.ReadPintoString(32);
            Body = reader.ReadPintoString(1024);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Type);
            writer.WriteBE(AutoCloseDelay);
            writer.WritePintoString(Title, 32);
            writer.WritePintoString(Body, 1024);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleNotificationPacket(this);
        }

        public int GetID()
        {
            return 4;
        }
    }
}