using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
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
            Usernames = reader.ReadUTF8String();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF8String(Usernames);
        }

        public int GetID()
        {
            return 4;
        }
    }
}
