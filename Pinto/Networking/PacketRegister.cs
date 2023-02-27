using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketRegister : IPacket
    {
        public string Name { get; protected set; }
        public string PasswordHash { get; protected set; }

        public PacketRegister() { }

        public PacketRegister(string name, string passwordHash)
        {
            Name = name;
            PasswordHash = passwordHash;
        }

        public void Read(BinaryReader reader)
        {
            Name = reader.ReadUTF8String();
            PasswordHash = reader.ReadUTF8String();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF8String(Name);
            writer.WriteUTF8String(PasswordHash);
        }

        public int GetID()
        {
            return 1;
        }
    }
}
