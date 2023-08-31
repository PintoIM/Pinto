using System.IO;

namespace PintoNS.Networking
{
    public class PacketLogin : IPacket
    {
        public byte ProtocolVersion { get; protected set; }
        public string ClientVersion { get; protected set; }
        public string Name { get; protected set; }
        public string PasswordHash { get; protected set; }

        public PacketLogin() { }

        public PacketLogin(byte protocolVersion, string clientVersion, 
            string name, string passwordHash)
        {
            ProtocolVersion = protocolVersion;
            ClientVersion = clientVersion;
            Name = name;
            PasswordHash = passwordHash;
        }

        public void Read(BinaryReader reader)
        {
            ProtocolVersion = reader.ReadByte();
            ClientVersion = reader.ReadPintoString(32);
            Name = reader.ReadPintoString(BinaryWriterReaderExtensions.USERNAME_MAX);
            PasswordHash = reader.ReadPintoString(64);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ProtocolVersion);
            writer.WritePintoString(ClientVersion, 32);
            writer.WritePintoString(Name, BinaryWriterReaderExtensions.USERNAME_MAX);
            writer.WritePintoString(PasswordHash, 64);
        }

        public virtual void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleLoginPacket(this);
        }

        public virtual int GetID()
        {
            return 0;
        }
    }
}
