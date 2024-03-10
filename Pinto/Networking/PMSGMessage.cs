using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace PintoNS.Networking
{
    public class PMSGMessage
    {
        public Dictionary<string, string> Headers;
        public byte[] Data;

        public PMSGMessage()
        {
            Headers = new Dictionary<string, string>();
            Data = new byte[0];
        }

        public PMSGMessage(string body)
        {
            Headers = new Dictionary<string, string>()
            {
                { "Content-Type", "pinto/text" }
            };
            Data = Encoding.BigEndianUnicode.GetBytes(body);
        }

        public PMSGMessage(byte[] data, bool isImage)
        {
            Headers = new Dictionary<string, string>()
            {
                { "Content-Type", $"pinto/{(isImage ? "file-plain" : "file-image")}" }
            };
            Data = data;
        }

        private static byte[] EncodeGZIP(byte[] data)
        {
            MemoryStream outputStream = new MemoryStream();
            GZipStream gzipOutputStream = new GZipStream(outputStream, CompressionMode.Compress);
            gzipOutputStream.Write(data, 0, data.Length);
            gzipOutputStream.Close();
            return outputStream.ToArray();
        }

        private static byte[] DecodeGZIP(byte[] data)
        {
            MemoryStream inputStream = new MemoryStream(data);
            GZipStream gzipInputStream = new GZipStream(inputStream, CompressionMode.Decompress);
            MemoryStream decodedStream = new MemoryStream();
            gzipInputStream.CopyTo(decodedStream);
            gzipInputStream.Close();
            decodedStream.Close();
            return decodedStream.ToArray();
        }

        public byte[] Encode()
        {
            MemoryStream stream = new MemoryStream();

            foreach (KeyValuePair<string, string> kp in Headers)
            {
                byte[] data = Encoding.ASCII.GetBytes($"{kp.Key}: {kp.Value}\r\n");
                stream.Write(data, 0, data.Length);
            }

            byte[] headersEnd = Encoding.ASCII.GetBytes("\r\n");
            byte[] dataEncoded = EncodeGZIP(Data);
            stream.Write(headersEnd, 0, headersEnd.Length);
            stream.Write(dataEncoded, 0, dataEncoded.Length);
            stream.Close();

            return stream.ToArray();
        }

        private static byte[] ReadToEnd(Stream stream)
        {
            MemoryStream data = new MemoryStream();
            byte[] buffer = new byte[1024];
            int readCount;

            while ((readCount = stream.Read(buffer, 0, buffer.Length)) > 0)
                data.Write(buffer, 0, readCount);

            return data.ToArray();
        }

        private static string ReadLine(Stream stream)
        {
            string str = "";
            int prevC = 0;
            int c;

            while ((c = stream.ReadByte()) != -1 && (c != '\n' || prevC != '\r'))
            {
                prevC = c;
                str += (char)c;
            }

            return str.Trim();
        }

        public static PMSGMessage Decode(byte[] payload)
        {
            MemoryStream stream = new MemoryStream(payload, false);
            PMSGMessage message = new PMSGMessage();
            message.Headers = new Dictionary<string, string>();

            string str;
            while ((str = ReadLine(stream)).Length > 0)
                message.Headers[str.Split(':')[0]] = str.Split(':')[1].TrimStart();
            message.Data = DecodeGZIP(ReadToEnd(stream));

            return message;
        }
    }
}
