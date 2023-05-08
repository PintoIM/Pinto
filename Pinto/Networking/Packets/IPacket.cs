using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public interface IPacket
    {
        void Write(BinaryWriter writer);
        void Read(BinaryReader reader);
        void Handle(NetworkHandler netHandler);
        int GetID();
        int GetSize();
    }
}
