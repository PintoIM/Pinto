﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintoNS.Networking
{
    public class PacketCallStart : IPacket
    {
        public string ContactName { get; protected set; }

        public PacketCallStart() { }

        public PacketCallStart(string contactName)
        {
            ContactName = contactName;
        }

        public void Read(BinaryReader reader)
        {
            ContactName = reader.ReadUTF16String();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteUTF16String(ContactName);
        }

        public void Handle(NetworkHandler netHandler)
        {
        }

        public int GetID()
        {
            return 11;
        }
    }
}
