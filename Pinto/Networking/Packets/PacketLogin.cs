using System.IO;

namespace PintoNS.Networking
{
    public class PacketLogin : IPacket
    {
        public byte ProtocolVersion { get; protected set; }
        public string ClientVersion { get; protected set; }
        public string Token { get; protected set; }

        public PacketLogin() { }

        public PacketLogin(byte protocolVersion, string clientVersion, string token)
        {
            ProtocolVersion = protocolVersion;
            ClientVersion = clientVersion;
            Token = token;
        }

        public void Read(BinaryReader reader)
        {
            ProtocolVersion = reader.ReadByte();
            ClientVersion = reader.ReadPintoString(32);
            Token = reader.ReadPintoString(BinaryWriterReaderExtensions.TOKEN_MAX);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ProtocolVersion);
            writer.WritePintoString(ClientVersion, 32);
            writer.WritePintoString(Token, BinaryWriterReaderExtensions.TOKEN_MAX);
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
