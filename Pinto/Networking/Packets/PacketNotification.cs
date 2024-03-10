using System.IO;

namespace PintoNS.Networking.Packets
{
    public class PacketNotification : IPacket
    {
        public byte Type { get; protected set; }
        public int AutoCloseDelay { get; protected set; }
        public string Title { get; protected set; }
        public string Body { get; protected set; }

        public PacketNotification() { }

        /// <summary>
        /// Types:<para />
        /// - 0 -> In Window Pop-up (Warning)<para />
        /// - 1 -> In Window Pop-up (Information)<para />
        /// - 2 -> Pop-up (Notification)<para />
        /// </summary>
        /// <param name="type"></param>
        /// <param name="autoCloseDelay"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
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
            writer.WriteBEInt(AutoCloseDelay);
            writer.WritePintoString(Title, 32);
            writer.WritePintoString(Body, 1024);
        }

        public int GetPacketSize()
        {
            return 1 + 4 + 32 + 1024;
        }

        public int GetID()
        {
            return 4;
        }
    }
}