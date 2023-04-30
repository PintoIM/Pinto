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
            Name = reader.ReadASCIIString();
            PasswordHash = reader.ReadASCIIString();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteASCIIString(Name);
            writer.WriteASCIIString(PasswordHash);
        }

        public void Handle(NetworkHandler netHandler)
        {
        }

        public int GetID()
        {
            return 1;
        }
    }
}
