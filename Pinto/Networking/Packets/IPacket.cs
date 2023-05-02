using System.IO;

namespace PintoNS.Networking
{
    public interface IPacket
    {
        void Write(BinaryWriter writer);
        void Read(BinaryReader reader);
        void Handle(NetworkHandler netHandler);
        int GetID();
    }
}
