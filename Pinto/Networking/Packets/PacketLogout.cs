using System.IO;

namespace PintoNS.Networking.Packets
{
    public class PacketLogout : IPacket
    {
        public string Reason { get; protected set; }

        public PacketLogout() { }

        public PacketLogout(string reason)
        {
            Reason = reason;
        }

        public void Read(BinaryReader reader)
        {
            Reason = reader.ReadPintoString(256);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(Reason, 256);
        }

        public int GetPacketSize()
        {
            return 256;
        }

        public int GetID()
        {
            return 2;
        }
    }
}
