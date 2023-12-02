using System.IO;

namespace PintoNS.Networking
{
    public class PacketServerInfo : IPacket
    {
        public string ServerID { get; protected set; }
        public string ServerSoftware { get; protected set; }

        public PacketServerInfo() { }

        public PacketServerInfo(string serverID, string serverSoftware) 
        {
            ServerID = serverID;
            ServerSoftware = serverSoftware;
        }

        public void Read(BinaryReader reader)
        {
            ServerID = reader.ReadPintoString(36);
            ServerSoftware = reader.ReadPintoString(128);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(ServerID, 36);
            writer.WritePintoString(ServerSoftware, 128);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleServerInfoPacket(this);
        }

        public int GetID()
        {
            return 17;
        }
    }
}
