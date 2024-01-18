namespace PintoNS.Networking
{
    public class PacketRegister : PacketLogin
    {
        public PacketRegister() : base() { }

        public PacketRegister(byte protocolVersion, string clientVersion,
            string name, string passwordHash) : base(protocolVersion, clientVersion, name, passwordHash) { }

        public override void Handle(NetworkHandler netHandler)
        {
        }

        public override int GetID()
        {
            return 1;
        }
    }
}
