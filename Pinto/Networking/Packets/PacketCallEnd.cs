using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketCallEnd : IPacket
    {
        public PacketCallEnd() { }

        public void Read(BinaryReader reader)
        {
        }

        public void Write(BinaryWriter writer)
        {
        }

        public void Handle(NetworkHandler netHandler)
        {
            //netHandler.HandleCallEndPacket();
        }

        public int GetID()
        {
            return 14;
        }
    }
}
