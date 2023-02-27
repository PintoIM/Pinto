using PintoNS.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketStatus : IPacket
    {
        public string ContactName { get; protected set; }
        public UserStatus Status { get; protected set; }

        public PacketStatus() { }

        public PacketStatus(string contactName, UserStatus status)
        {
            ContactName = contactName;
            Status = status;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadUTF8String();
            Status = (UserStatus) reader.ReadBEInt();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF8String(ContactName);
            writer.WriteBE((int) Status);
        }

        public int GetID()
        {
            return 8;
        }
    }
}
