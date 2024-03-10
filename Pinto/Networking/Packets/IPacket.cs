using System.IO;

namespace PintoNS.Networking.Packets
{
    public interface IPacket
    {
        int GetPacketSize();

        void Read(BinaryReader stream);

        void Write(BinaryWriter stream);

        int GetID();
    }
}