using System;
using System.IO;
using System.Net;
using System.Text;

namespace PintoNS.Networking
{
    public static class BinaryWriterReaderExtensions
    {
        public const int TOKEN_MAX = int.MaxValue;
        public const int USERNAME_MAX = 16;

        public static void WriteBE(this BinaryWriter writer, short value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(value));
        }
        
        public static void WriteBE(this BinaryWriter writer, int value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(value));
        }

        public static void WritePintoString(this BinaryWriter writer, string str, int maxLength)
        {
            if (str.Length > maxLength)
                str = str.Substring(0, maxLength);
            byte[] stringData = Encoding.BigEndianUnicode.GetBytes(str);
            
            writer.WriteBE(stringData.Length);
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

        public static string ReadPintoString(this BinaryReader reader, int maxLength)
        {
            int length = reader.ReadBEInt();
            if (length < 0) 
                throw new InvalidDataException("Weird string, the length is less than 0!");
            if (length < 1) return "";

            byte[] buffer = new byte[length];
            reader.Read(buffer, 0, length);

            string str = Encoding.BigEndianUnicode.GetString(buffer);
            if (str.Length > maxLength)
                throw new ArgumentException($"Received more data than allowed ({str.Length} > {maxLength})");

            return str;
        }

        public static int GetPintoStringSize(string str) 
        {
            return 4 + Encoding.BigEndianUnicode.GetByteCount(str);
        }
    }
}
