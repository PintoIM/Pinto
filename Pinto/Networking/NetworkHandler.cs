using PintoNS.Forms;
using PintoNS.Forms.Notification;
using PintoNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PintoNS.Networking
{
    public class NetworkHandler
    {
        private MainForm mainForm;
        private NetworkClient networkClient;
        public bool LoggedIn;
        public string ServerName;
        public string ServerMOTD;

        public NetworkHandler(MainForm mainForm, NetworkClient networkClient)
        {
            this.mainForm = mainForm;
            this.networkClient = networkClient;
        }

        public void HandlePacket(IPacket packet) 
        {
            if (packet.GetID() == 0)
            {
                HandleLoginPacket((PacketLogin)packet);
            }
            else if (packet.GetID() == 1) 
            {
                HandleLogoutPacket((PacketLogout)packet);
            }
            else if (packet.GetID() == 2)
            {
                HandleMessagePacket((PacketMessage)packet);
            }
            else if (packet.GetID() == 3)
            {
                HandleTypingPacket((PacketTyping)packet);
            }
        }

        private void HandleLoginPacket(PacketLogin packet) 
        {
            LoggedIn = true;
            mainForm.Invoke(new Action(() => 
            {
                mainForm.PopupController.CreatePopup("This version of Pinto! still uses the old networking code." +
                    " You will need to use \"Public Chat\" to comunicate on this server.", 0);
                mainForm.OnLogin();
                NotificationUtil.ShowNotification(mainForm, packet.SessionID, packet.Name);
            }));
        }

        private void HandleLogoutPacket(PacketLogout packet)
        {
            mainForm.NetManager.IsActive = false;
            mainForm.NetManager.NetClient.Disconnect($"Kicked by the server -> {packet.Reason}");
            mainForm.Invoke(new Action(() =>
            {
                NotificationUtil.ShowNotification(mainForm, packet.Reason, "Kicked by the server", 
                    NotificationIconType.WARNING, true);
            }));
        }

        private void HandleMessagePacket(PacketMessage packet) 
        {
            mainForm.Invoke(new Action(() =>
            {
                mainForm.GetMessageFormFromReceiverID(0).WriteMessage(packet.Message, Color.Black);
            }));
        }

        private void HandleTypingPacket(PacketTyping packet)
        {
            string usernames = packet.Usernames;
            mainForm.Invoke(new Action(() =>
            {
                if (usernames.Length > 0)
                    mainForm.GetMessageFormFromReceiverID(0).tsslStatusBarTypingList.Text = $"{usernames} are typing...";
                else
                    mainForm.GetMessageFormFromReceiverID(0).tsslStatusBarTypingList.Text = $"";
            }));
        }

        public async Task SendLoginPacket(byte protocolVersion, string name, string sessionID) 
        {
            await networkClient.SendPacket(new PacketLogin(protocolVersion, name, sessionID));
        }

        public async Task SendMessagePacket(string message)
        {
            await networkClient.SendPacket(new PacketMessage(message));
        }

        public async Task SendTypingPacket(bool isTyping)
        {
            await networkClient.SendPacket(new PacketTyping(isTyping ? "Pinto!" : ""));
        }
    }
}
