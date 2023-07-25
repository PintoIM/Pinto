using System.IO;

namespace PintoNS.Networking
{
    public class PacketInWindowPopup : IPacket
    {
        public string Message { get; protected set; }

        public PacketInWindowPopup() { }

        public PacketInWindowPopup(string message)
        {
            Message = message;
        }

        public void Read(BinaryReader reader)
        {
            Message = reader.ReadPintoString(256);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(Message, 256);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleInWindowPopupPacket(this);
        }

        public int GetID()
        {
            return 5;
        }
    }
}
