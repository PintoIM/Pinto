using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketLogout : IPacket
    {
        public string Reason { get; protected set; }

        public PacketLogout() { }

        public PacketLogout(string reason)
        {
            Reason = reason;
        }

        public void Read(BinaryReader reader)
        {
            Reason = reader.ReadPintoString(256);
        }

        public void Write(BinaryWriter writer)
        {
            writer.WritePintoString(Reason, 256);
        }

        public void Handle(NetworkHandler netHandler)
        {
            netHandler.HandleLogoutPacket(this);
        }

        public int GetID()
        {
            return 2;
        }
    }
}
