using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoChat.Networking
{
    public class PacketTyping : IPacket
    {
        public string Usernames { get; protected set; }
        
        public PacketTyping() { }

        public PacketTyping(string usernames)
        {
            Usernames = usernames;
        }

        public void Read(BinaryReader reader)
        {
            Usernames = reader.ReadUTF8String().Trim();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF8String(Usernames.PadRight(256));
        }

        public int GetID()
        {
            return 3;
        }

        public int GetLength()
        {
            // User names
            return (512 + 2);
        }
    }
}
