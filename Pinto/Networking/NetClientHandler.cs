﻿using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using PintoNS.Contacts;
using PintoNS.Forms;
using PintoNS.Networking.Packets;
using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace PintoNS.Networking
{
    public class NetClientHandler : NetBaseHandler
    {
        internal MainForm instance;
        private NetClientPacketsHandler packetsHandler;
        public bool LoggedIn { get; internal set; }
        public string ServerID;
        public string ServerSoftware;

        internal NetClientHandler(MainForm instance, TcpClient tcpClient)
        {
            this.instance = instance;
            NetManager = new NetworkTCPManager(tcpClient, "Network-Client", this);
            packetsHandler = new NetClientPacketsHandler(instance, this);
        }

        internal void Handshake()
        {
            Program.Console.WriteMessage($"[Networking] Handshaking...");
            BinaryReader binaryReader = NetManager.GetInputStream();
            BinaryWriter binaryWriter = NetManager.GetOutputStream();

            // Receive the public RSA key
            int sizeOfPublicKey = binaryReader.ReadBEInt();
            byte[] publicKey = binaryReader.ReadBytes(sizeOfPublicKey);
            
            if (!FingerprintValidator.Validate(publicKey, this))
                throw new PintoVerificationException();

            // Parse RSA key
            RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)PublicKeyFactory.CreateKey(publicKey);
            RSAParameters rsaParameters = new RSAParameters();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsaParameters.Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned();
            rsaParameters.Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned();
            rsa.ImportParameters(rsaParameters);

            // Generate AES
            Aes aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.GenerateKey();
            aes.GenerateIV();
            Program.Console.WriteMessage($"[Networking] AES key: {BitConverter.ToString(aes.Key).Replace("-", "")}");

            // Send the encrypted AES key
            byte[] encryptedAESKey = rsa.Encrypt(aes.Key, false);
            binaryWriter.WriteBEInt(encryptedAESKey.Length);
            binaryWriter.Write(encryptedAESKey);

            NetManager.OnHandshaked(aes);
            Program.Console.WriteMessage($"[Networking] Done handshaking");
        }

        public override void OnUpdate()
        {
            if (ConnectionClosed)
                return;
            NetManager.Interrupt();
            NetManager.ProcessReceivedPackets();
            NetMonitorForm.Instance.UpdateMonitor();
        }

        protected override void OnDisconnect()
        {
            instance.Disconnect();
        }

        public override void HandlePacket(IPacket packet)
        {
            try
            {
                if (!(packet is PacketKeepAlive))
                    Program.Console.WriteMessage($"[Networking] Received {packet.GetType().Name}");

                if (packetsHandler == null)
                {
                    OnBadPacket();
                    return;
                }

                string packetName = packet.GetType().Name.Replace("Packet", "");
                MethodInfo handler = packetsHandler.GetType().GetMethod(
                    $"Handle{packetName}Packet", new Type[] { packet.GetType() });

                if (handler != null)
                    handler.Invoke(packetsHandler, new object[] { packet });
                else
                    OnBadPacket();
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[Networking] Encountered an error whilst processing a packet: {ex}");
                NetManager.Shutdown("Internal Client Error");
            }
        }

        public void Disconnect()
        {
            if (ConnectionClosed)
                return;
            ConnectionClosed = true;
            SendPacket(new PacketLogout(""));
            NetManager.Interrupt();
            NetManager.Close();
        }

        public void Login(string username, string password)
        {
            Username = username;
            SendPacket(new PacketLogin(Program.PROTOCOL_VERSION, Program.VERSION_STRING,
                username, Utils.GetSHA256Hash(Encoding.UTF8.GetBytes(password))));
        }

        public void Register(string username, string password)
        {
            Username = username;
            SendPacket(new PacketRegister(Program.PROTOCOL_VERSION, Program.VERSION_STRING,
                username, Utils.GetSHA256Hash(Encoding.UTF8.GetBytes(password))));
        }

        public void SendStatusChange(UserStatus status, string motd)
        {
            SendPacket(new PacketStatus("", status, motd));
        }

        public void AddContact(string contact)
        {
            SendPacket(new PacketAddContact(contact, UserStatus.OFFLINE, ""));
        }

        public void RemoveContact(string contact)
        {
            SendPacket(new PacketRemoveContact(contact));
        }

        public void RespondContactRequest(string contact, bool accepted)
        {
            SendPacket(new PacketContactRequest($"{contact}:{(accepted ? "yes" : "no")}"));
        }

        public void ChangeTypingState(string contact, bool state)
        {
            SendPacket(new PacketTyping(contact, state));
        }

        public void MessageContact(string contact, PMSGMessage msg)
        {
            SendPacket(new PacketMessage(contact, msg));
        }
    }
}
