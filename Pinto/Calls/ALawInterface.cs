using NAudio.Codecs;
using System;

namespace PintoNS.Calls
{
    // Taken from https://github.com/naudio/NAudio/blob/master/NAudioDemo/NetworkChatDemo/ALawChatCodec.cs
    public class ALawInterface
    {
        public int SampleRate = 16000;
        public int BitsPerSecond => SampleRate * 8;

        public byte[] Encode(byte[] data) => Encode(data, 0, data.Length);

        public byte[] Decode(byte[] data) => Decode(data, 0, data.Length);

        public byte[] Encode(byte[] data, int offset, int length)
        {
            byte[] encoded = new byte[length / 2];
            int outIndex = 0;
            for (int n = 0; n < length; n += 2)
                encoded[outIndex++] = ALawEncoder.LinearToALawSample(BitConverter.ToInt16(data, offset + n));
            return encoded;
        }

        public byte[] Decode(byte[] data, int offset, int length)
        {
            byte[] decoded = new byte[length * 2];
            int outIndex = 0;
            for (int n = 0; n < length; n++)
            {
                short decodedSample = ALawDecoder.ALawToLinearSample(data[n + offset]);
                decoded[outIndex++] = (byte)(decodedSample & 0xFF);
                decoded[outIndex++] = (byte)(decodedSample >> 8);
            }
            return decoded;
        }
    }
}
