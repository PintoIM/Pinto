using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketInWindowPopup : IPacket
    {
        public string Message { get; protected set; }

        public PacketInWindowPopup() { }

        public PacketInWindowPopup(string message)
        {
            Message = message;
        }

        public void Read(BinaryReader reader)
        {
            Message = reader.ReadUTF8String();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF8String(Message);
        }

        public int GetID()
        {
            return 5;
        }
    }
}
