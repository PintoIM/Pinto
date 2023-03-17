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
using System.Media;

namespace PintoNS.Networking
{
    public class NetworkHandler
    {
        private MainForm mainForm;
        private NetworkClient networkClient;
        public bool LoggedIn;

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
                case 9:
                    HandleContactRequestPacket((PacketContactRequest)packet);
                    break;
                case 10:
                    HandleClearContactsPacket();
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
                MessageForm messageForm = mainForm.GetMessageFormFromReceiverName(packet.ContactName);
                messageForm.WriteMessage(packet.Message, Color.Black);

                if (Form.ActiveForm != messageForm && !messageForm.HasBeenInactive)
                {
                    messageForm.HasBeenInactive = true;
                    mainForm.PopupController.CreatePopup($"Received a new message from {packet.ContactName}!", 
                        "New message");
                    new SoundPlayer() { Stream = Sounds.IM }.Play();
                }
            }));
        }

        private void HandleInWindowPopupPacket(PacketInWindowPopup packet)
        {
            mainForm.Invoke(new Action(() =>
            {
                mainForm.InWindowPopupController.CreatePopup(packet.Message);
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
                {
                    Contact contact = mainForm.ContactsMgr.GetContact(packet.ContactName);

                    if (packet.Status == UserStatus.OFFLINE && contact.Status != UserStatus.OFFLINE)
                    {
                        mainForm.PopupController.CreatePopup($"{packet.ContactName} is now offline",
                            "Status change");
                        new SoundPlayer() { Stream = Sounds.OFFLINE }.Play();
                    }
                    else if (packet.Status != UserStatus.OFFLINE && contact.Status == UserStatus.OFFLINE)
                    {
                        mainForm.PopupController.CreatePopup($"{packet.ContactName} is now online",
                            "Status change");
                        new SoundPlayer() { Stream = Sounds.ONLINE }.Play();
                    }

                    mainForm.ContactsMgr.UpdateContact(new Contact() { Name = packet.ContactName, Status = packet.Status });
                }
            }));
        }

        private void HandleContactRequestPacket(PacketContactRequest packet)
        {
            mainForm.Invoke(new Action(() =>
            {
                NotificationUtil.ShowPromptNotification(mainForm,
                    $"{packet.ContactName} wants to add you to their contact list. Proceed?", "Contact request",
                    NotificationIconType.QUESTION, true,
                    (NotificationButtonType button) =>
                    {
                        SendContactRequestPacket(packet.ContactName, button == NotificationButtonType.YES);
                    });
            }));
        }

        private void HandleClearContactsPacket()
        {
            mainForm.Invoke(new Action(() =>
            {
                mainForm.dgvContacts.Rows.Clear();
                mainForm.ContactsMgr = new ContactsManager(mainForm);
            }));
        }

        public void SendLoginPacket(byte protocolVersion, string name, string sessionID) 
        {
            networkClient.AddToSendQueue(new PacketLogin(protocolVersion, name, sessionID));
        }

        public void SendRegisterPacket(string name, string sessionID)
        {
            networkClient.AddToSendQueue(new PacketRegister(name, sessionID));
        }

        public void SendMessagePacket(string contactName, string message)
        {
            networkClient.AddToSendQueue(new PacketMessage(contactName, message));
        }

        public void SendTypingPacket(bool isTyping)
        {
            networkClient.AddToSendQueue(new PacketTyping(isTyping ? "Pinto!" : ""));
        }

        public void SendStatusPacket(UserStatus status)
        {
            networkClient.AddToSendQueue(new PacketStatus("", status));
        }

        public void SendContactRequestPacket(string name, bool approved)
        {
            networkClient.AddToSendQueue(new PacketContactRequest($"{name}:{(approved ? "yes" : "no")}"));
        }

        public void SendAddContactPacket(string name)
        {
            networkClient.AddToSendQueue(new PacketAddContact(name));
        }

        public void SendRemoveContactPacket(string name)
        {
            networkClient.AddToSendQueue(new PacketRemoveContact(name));
        }
    }
}
