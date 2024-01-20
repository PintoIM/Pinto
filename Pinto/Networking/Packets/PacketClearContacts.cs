using System.IO;

namespace PintoNS.Networking.Packets
{
    public class PacketClearContacts : IPacket
    {
        public void Read(BinaryReader reader)
        {
        }

        public void Write(BinaryWriter writer)
        {
        }

        public int GetPacketSize()
        {
            return 0;
        }

        public int GetID()
        {
            return 10;
        }
    }
}