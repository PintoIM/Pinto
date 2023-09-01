using System.IO;

namespace PintoNS.Networking
{
    public class PacketInWindowPopup : IPacket
    {
        public string Message { get; protected set; }
        public bool IsInfo { get; protected set; }

        public PacketInWindowPopup() { }

        public PacketInWindowPopup(string message, bool isInfo)
        {
            Message = message;
            IsInfo = isInfo;
        }

        public void Read(BinaryReader reader)
        {
            Message = reader.ReadPintoString(256);
            IsInfo = reader.ReadByte() == 0x01;
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(Message, 256);
            writer.Write(IsInfo ? 0x01 : 0x00);
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
