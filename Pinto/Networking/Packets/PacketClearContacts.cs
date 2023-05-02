using System.IO;

namespace PintoNS.Networking
{
    public class PacketClearContacts : IPacket
    {
        public void Read(BinaryReader reader)
        {
        }

        public void Write(BinaryWriter writer)
        {
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleClearContactsPacket();
        }

        public int GetID()
        {
            return 10;
        }
    }
}