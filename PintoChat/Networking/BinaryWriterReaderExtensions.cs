using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PintoChat.Networking
{
    public static class BinaryWriterReaderExtensions
    {
        public static void WriteBE(this BinaryWriter writer, short value) 
        {
            writer.Write(IPAddress.HostToNetworkOrder(value));
        }

        public static void WriteBE(this BinaryWriter writer, int value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(value));
        }

        public static void WriteUTF8String(this BinaryWriter writer, string str) 
        {
            writer.WriteBE((short)str.Length);
            writer.Write(Encoding.UTF8.GetBytes(str));
        }

        public static short ReadBEShort(this BinaryReader reader)
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt16());
        }

        public static int ReadBEInt(this BinaryReader reader)
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt32());
        }

        public static string ReadUTF8String(this BinaryReader reader)
        {
            short length = reader.ReadBEShort();
            if (length < 0) 
                return "";
            byte[] buffer = new byte[length];
            reader.Read(buffer, 0, length);
            return Encoding.UTF8.GetString(buffer);
        }
    }
}
