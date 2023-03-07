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
using System.Windows.Forms;
using PintoNS.General;

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
            switch (packet.GetID()) 
            {
                case 0:
                    HandleLoginPacket((PacketLogin)packet);
                    break;
                case 2:
                    HandleLogoutPacket((PacketLogout)packet);
                    break;
                case 3:
                    HandleMessagePacket((PacketMessage)packet);
                    break;
                case 5:
                    HandleInWindowPopupPacket((PacketInWindowPopup)packet);
                    break;
                case 6:
                    HandleAddContactPacket((PacketAddContact)packet);
                    break;
                case 7:
                    HandleRemoveContactPacket((PacketRemoveContact)packet);
                    break;
                case 8:
                    HandleStatusPacket((PacketStatus)packet);
                    break;
            }
        }

        private void HandleLoginPacket(PacketLogin packet) 
        {
            LoggedIn = true;
            mainForm.Invoke(new Action(() => 
            {
                mainForm.OnLogin();
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
                mainForm.GetMessageFormFromReceiverName(packet.ContactName).WriteMessage(packet.Message, Color.Black);
            }));
        }

        private void HandleInWindowPopupPacket(PacketInWindowPopup packet)
        {
            mainForm.Invoke(new Action(() =>
            {
                mainForm.PopupController.CreatePopup(packet.Message);
            }));
        }

        private void HandleAddContactPacket(PacketAddContact packet)
        {
            mainForm.Invoke(new Action(() =>
            {
                mainForm.ContactsMgr.AddContact(new Contact() { Name = packet.ContactName, Status = UserStatus.OFFLINE });
            }));
        }

        private void HandleRemoveContactPacket(PacketRemoveContact packet)
        {
            mainForm.Invoke(new Action(() =>
            {
                mainForm.ContactsMgr.RemoveContact(mainForm.ContactsMgr.GetContact(packet.ContactName));
            }));
        }

        private void HandleStatusPacket(PacketStatus packet)
        {
            mainForm.Invoke(new Action(() =>
            {
                if (string.IsNullOrWhiteSpace(packet.ContactName))
                    mainForm.OnStatusChange(packet.Status);
                else
                    mainForm.ContactsMgr.UpdateContact(new Contact() { Name = packet.ContactName, Status = packet.Status });
            }));
        }

        public async Task SendLoginPacket(byte protocolVersion, string name, string sessionID) 
        {
            await Task.Run(new Action(() => 
            {
                networkClient.SendPacket(new PacketLogin(protocolVersion, name, sessionID));
            }));
        }

        public async Task SendMessagePacket(string contactName, string message)
        {
            await Task.Run(new Action(() =>
            {
                networkClient.SendPacket(new PacketMessage(contactName, message));
            }));
        }

        public async Task SendTypingPacket(bool isTyping)
        {
            await Task.Run(new Action(() =>
            {
                networkClient.SendPacket(new PacketTyping(isTyping ? "Pinto!" : ""));
            }));
        }

        public async Task SendStatusPacket(UserStatus status)
        {
            await Task.Run(new Action(() =>
            {
                networkClient.SendPacket(new PacketStatus("", status));
            }));
        }
    }
}
