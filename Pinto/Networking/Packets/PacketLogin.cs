using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Networking
{
    public class PacketLogin : IPacket
    {
        public byte ProtocolVersion { get; protected set; }
        public string ClientVersion { get; protected set; }
        public string Name { get; protected set; }
        public string PasswordHash { get; protected set; }

        public PacketLogin() { }

        public PacketLogin(byte protocolVersion, string clientVersion, 
            string name, string passwordHash)
        {
            ProtocolVersion = protocolVersion;
            ClientVersion = clientVersion;
            Name = name;
            PasswordHash = passwordHash;
        }

        public void Read(BinaryReader reader)
        {
            ProtocolVersion = reader.ReadByte();
            ClientVersion = reader.ReadUTF16String();
            Name = reader.ReadUTF16String();
            PasswordHash = reader.ReadUTF16String();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ProtocolVersion);
            writer.WriteUTF16String(ClientVersion);
            writer.WriteUTF16String(Name);
            writer.WriteUTF16String(PasswordHash);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleLoginPacket(this);
        }

        public int GetID()
        {
            return 0;
        }
    }
}
