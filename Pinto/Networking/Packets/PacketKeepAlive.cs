using System.IO;

namespace PintoNS.Networking
{
    public class PacketKeepAlive : IPacket
    {
        public void Read(BinaryReader reader)
        {
        }

        public void Write(BinaryWriter writer)
        {
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleKeepAlivePacket();
        }

        public int GetID()
        {
            return 255;
        }
    }
}
