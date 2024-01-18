using PintoNS.Calls;
using System.IO;

namespace PintoNS.Networking
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
            writer.WriteBE((int)CallStatus);
            writer.WritePintoString(Details, 64);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleCallChangeStatusPacket(this);
        }

        public int GetID()
        {
            return 11;
        }
    }
}