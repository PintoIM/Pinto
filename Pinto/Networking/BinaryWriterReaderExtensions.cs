using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Networking
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

        public static void WriteUTF16String(this BinaryWriter writer, string str)
        {
            byte[] stringData = Encoding.BigEndianUnicode.GetBytes(str);
            writer.WriteBE((short)stringData.Length);
            if (stringData.Length < 1) return;
            writer.Write(stringData);
        }

        public static short ReadBEShort(this BinaryReader reader)
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt16());
        }

        public static int ReadBEInt(this BinaryReader reader)
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt32());
        }

        public static string ReadUTF16String(this BinaryReader reader)
        {
            short length = reader.ReadBEShort();
            if (length < 1) return "";
            byte[] buffer = new byte[length];
            reader.Read(buffer, 0, length);
            return Encoding.BigEndianUnicode.GetString(buffer);
        }
    }
}
