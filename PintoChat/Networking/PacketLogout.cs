using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoChat.Networking
{
    public class PacketLogout : IPacket
    {
        public string Reason { get; protected set; }

        public PacketLogout() { }

        public PacketLogout(string reason)
        {
            Reason = reason;
        }

        public void Read(BinaryReader reader)
        {
            Reason = reader.ReadUTF8String().Trim();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF8String(Reason.PadRight(256));
        }

        public int GetID()
        {
            return 1;
        }

        public int GetLength()
        {
            // Reason
            return (256 + 2);
        }
    }
}
