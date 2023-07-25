using System.IO;

namespace PintoNS.Networking
{
    public class PacketServerID : IPacket
    {
        public string ServerID { get; protected set; }

        public PacketServerID() { }

        public PacketServerID(string serverID) 
        {
            ServerID = serverID;
        }

        public void Read(BinaryReader reader)
        {
            ServerID = reader.ReadPintoString(36);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ServerID, 36);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleServerIDPacket(this);
        }

        public int GetID()
        {
            return 17;
        }
    }
}
