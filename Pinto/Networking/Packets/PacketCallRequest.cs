using System.IO;

namespace PintoNS.Networking
{
    public class PacketCallRequest : IPacket
    {
        public void Read(BinaryReader reader)
        {
        }

        public void Write(BinaryWriter writer)
        {
        }

        public void Handle(NetworkHandler netHandler)
        {
        }

        public int GetID()
        {
            return 11;
        }
    }
}