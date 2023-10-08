using PintoNS.General;
using System.IO;

namespace PintoNS.Networking
{
    public class PacketSetOption : IPacket
    {
        public string Option;
        public string Value;

        public PacketSetOption() { }

        public PacketSetOption(string option, string value)
        {
            Option = option;
            Value = value;
        }

        public void Read(BinaryReader reader)
        {
            Option = reader.ReadPintoString(64);
            Value = reader.ReadPintoString(128);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(Option, 64);
            writer.WritePintoString(Value, 128);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleSetOptionPacket(this);
        }

        public int GetID()
        {
            return 12;
        }
    }
}