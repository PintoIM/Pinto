using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoChat.Networking
{
    public class PacketMessage : IPacket
    {
        public byte UserID { get; protected set; }
        public string Message { get; protected set; }

        public PacketMessage() { }

        public PacketMessage(string message)
        {
            UserID = byte.MaxValue;
            Message = message;
        }

        public void Read(BinaryReader reader)
        {
            UserID = reader.ReadByte();
            Message = reader.ReadUTF8String().Trim();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(UserID);
            writer.WriteUTF8String(Message.PadRight(512));
        }

        public int GetID()
        {
            return 2;
        }

        public int GetLength()
        {
            // User ID + Message
            return 1 + (512 + 2);
        }
    }
}
