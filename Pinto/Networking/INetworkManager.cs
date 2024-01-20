using Mono.CSharp;
using PintoNS.Networking.Packets;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace PintoNS.Networking
{
    public interface INetworkManager
    {
        void OnHandshaked(Aes aes);

        void SetNetHandler(NetBaseHandler netHandler);

        void AddToQueue(IPacket packet);

        void Shutdown(string reason);

        void ProcessReceivedPackets();

        NetworkAddress GetAddress();

        BinaryReader GetInputStream();

        BinaryWriter GetOutputStream();

        void Interrupt();

        void Close();
    }
}