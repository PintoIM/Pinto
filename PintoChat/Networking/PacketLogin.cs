using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoChat.Networking
{
    public class PacketLogin : IPacket
    {
        public byte ProtocolVersion { get; protected set; }
        public string Name { get; protected set; }
        public string SessionID { get; protected set; }

        public PacketLogin() { }

        public PacketLogin(byte protocolVersion, string name, string sessionID)
        {
            ProtocolVersion = protocolVersion;
            Name = name;
            SessionID = sessionID;
        }

        public void Read(BinaryReader reader)
        {
            ProtocolVersion = reader.ReadByte();
            Name = reader.ReadUTF8String().Trim();
            SessionID = reader.ReadUTF8String().Trim();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ProtocolVersion);  
            writer.WriteUTF8String(Name.PadRight(128).Substring(0, 128));
            writer.WriteUTF8String(SessionID.PadRight(128).Substring(0, 128));
        }

        public int GetID()
        {
            return 0;
        }

        public int GetLength()
        {
            // Protocol version + Name + Session ID
            return 1 + (128 + 2) + (128 + 2);
        }
    }
}
