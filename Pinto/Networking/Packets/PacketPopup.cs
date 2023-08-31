using System.IO;

namespace PintoNS.Networking
{
    public class PacketPopup : IPacket
    {
        public string Title { get; protected set; }
        public string Body { get; protected set; }

        public PacketPopup() { }

        public PacketPopup(string title, string body)
        {
            Title = title;
            Body = body;
        }

        public void Read(BinaryReader reader)
        {
            Title = reader.ReadPintoString(32);
            Body = reader.ReadPintoString(1024);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(Title, 32);
            writer.WritePintoString(Body, 1024);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandlePopupPacket(this);
        }

        public int GetID()
        {
            return 4;
        }
    }
}