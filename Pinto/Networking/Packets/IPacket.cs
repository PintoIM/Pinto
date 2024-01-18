using System;
using System.IO;

namespace PintoNS.Networking
{
    [Obsolete("Networking is about to be re-written")]
    public interface IPacket
    {
        void Write(BinaryWriter writer);
        void Read(BinaryReader reader);
        void Handle(NetworkHandler netHandler);
        int GetID();
    }
}
