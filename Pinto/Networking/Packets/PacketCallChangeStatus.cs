using PintoNS.Calls;
using System.IO;

namespace PintoNS.Networking.Packets
{
    public class PacketCallChangeStatus : IPacket
    {
        public CallStatus CallStatus;
        public string Details;

        public PacketCallChangeStatus() { }

        public PacketCallChangeStatus(CallStatus callStatus, string details)
        {
            CallStatus = callStatus;
            Details = details;
        }

        public void Read(BinaryReader reader)
        {
            CallStatus = (CallStatus)reader.ReadBEInt();
            Details = reader.ReadPintoString(64);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteBEInt((int)CallStatus);
            writer.WritePintoString(Details, 64);
        }

        public int GetPacketSize()
        {
            return 4 + 64;
        }

        public int GetID()
        {
            return 11;
        }
    }
}