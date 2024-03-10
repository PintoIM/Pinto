using System;
using System.IO;
using System.Net;
using System.Text;

namespace PintoNS.Networking
{
    public static class NetExtensions
    {
        public static void WritePintoString(this BinaryWriter writer, string str, int maxLength)
        {
            if (str.Length > maxLength)
                str = str.Substring(0, maxLength);
            byte[] stringData = Encoding.BigEndianUnicode.GetBytes(str);

            writer.WriteBEInt(stringData.Length);
            if (stringData.Length < 1) return;

            writer.Write(stringData);
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

        public static void WriteBytes(this BufferedStream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }

        public static void WriteBytes(this BinaryWriter writer, byte[] bytes)
        {
            writer.Write(bytes, 0, bytes.Length);
        }

        public static void WriteBEShort(this BinaryWriter writer, short value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(value));
        }

        public static void WriteBEInt(this BinaryWriter writer, int value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(value));
        }

        public static void WriteBELong(this BinaryWriter writer, long value)
        {
            writer.Write(IPAddress.HostToNetworkOrder(value));
        }

        public static void WriteBEFloat(this BinaryWriter writer, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            writer.WriteBytes(bytes);
        }

        public static void WriteBEDouble(this BinaryWriter writer, double value)
        {
            writer.WriteBELong(BitConverter.DoubleToInt64Bits(value));
        }

        public static void WriteJavaUTFStr(this BinaryWriter writer, string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            short length = (short)data.Length;
            writer.WriteBEShort(length);
            writer.WriteBytes(data);
        }

        public static void WriteMCClassicString(this BinaryWriter writer, string str)
        {
            writer.WriteBytes(Encoding.ASCII.GetBytes(str.PadRight(64, ' ').Substring(0, 64)));
        }

        public static short ReadBEShort(this BinaryReader reader)
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt16());
        }

        public static int ReadBEInt(this BinaryReader reader)
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt32());
        }

        public static long ReadBELong(this BinaryReader reader)
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt64());
        }

        public static float ReadBEFloat(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        public static double ReadBEDouble(this BinaryReader reader)
        {
            return BitConverter.Int64BitsToDouble(reader.ReadBELong());
        }

        public static string ReadJavaUTFStr(this BinaryReader reader)
        {
            short length = reader.ReadBEShort();
            byte[] data = reader.ReadBytes(length);
            if (data.Length < length) throw new EndOfStreamException();
            return Encoding.UTF8.GetString(data);
        }

        public static string ReadMCClassicString(this BinaryReader reader)
        {
            return Encoding.ASCII.GetString(reader.ReadBytes(64)).TrimEnd(new char[] { ' ' });
        }
    }
}
