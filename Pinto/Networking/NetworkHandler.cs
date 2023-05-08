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
using System.Net;
using Newtonsoft.Json.Linq;

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
            if (packet.GetID() != 255) 
            {
                Program.Console.WriteMessage($"[Networking] Received packet {packet.GetType().Name.ToUpper()}" +
                    $" ({packet.GetID()})");
            }
            packet.Handle(this);
        }
        
        public void HandleLoginPacket(PacketLogin packet) 
        {
            LoggedIn = true;
            mainForm.Invoke(new Action(() => 
            {
                mainForm.OnLogin();
            }));
        }

        public void HandleLogoutPacket(PacketLogout packet)
        {
            mainForm.NetManager.IsActive = false;
            Program.Console.WriteMessage($"[Networking] Kicked by the server: {packet.Reason}");
            mainForm.NetManager.NetClient.Disconnect($"Kicked by the server -> {packet.Reason}");
            mainForm.Invoke(new Action(() =>
            {
                MsgBox.ShowNotification(mainForm, packet.Reason, "Kicked by the server", 
                    MsgBoxIconType.WARNING, true);
            }));
        }

        public void HandleMessagePacket(PacketMessage packet) 
        {
            mainForm.Invoke(new Action(() =>
            {
                MessageForm messageForm = mainForm.GetMessageFormFromReceiverName(packet.ContactName);
                if (messageForm == null) 
                {
                    Program.Console.WriteMessage($"[Networking]" +
                        $" Unable to get a message form for {packet.ContactName}!");
                    return;
                }

                if (packet.Sender.Trim().Length > 0)
                    messageForm.WriteMessage($"{packet.Sender}: ", Color.Black, false);
                /*
                if (packet.Message.StartsWith(@"{\rtf"))
                    messageForm.WriteRTF(packet.Message);
                else*/
                messageForm.WriteMessage(packet.Message, Color.Black);

                if (Form.ActiveForm != messageForm && 
                    !messageForm.HasBeenInactive && 
                    mainForm.CurrentUser.Status != UserStatus.BUSY)
                {
                    messageForm.HasBeenInactive = true;
                    mainForm.PopupController.CreatePopup($"Received a new message from {packet.ContactName}!", 
                        "New message");
                    new SoundPlayer() { Stream = Sounds.IM }.Play();
                }
            }));
        }

        public void HandleInWindowPopupPacket(PacketInWindowPopup packet)
        {
            mainForm.Invoke(new Action(() =>
            {
                mainForm.InWindowPopupController.CreatePopup(packet.Message);
            }));
        }

        public void HandleAddContactPacket(PacketAddContact packet)
        {
            Program.Console.WriteMessage($"[Contacts] Adding {packet.ContactName} to the contact list...");
            mainForm.Invoke(new Action(() =>
            {
                mainForm.ContactsMgr.AddContact(new Contact() { Name = packet.ContactName, Status = packet.Status });
            }));
        }

        public void HandleRemoveContactPacket(PacketRemoveContact packet)
        {
            Program.Console.WriteMessage($"[Contacts] Removing {packet.ContactName} from the contact list...");
            mainForm.Invoke(new Action(() =>
            {
                mainForm.ContactsMgr.RemoveContact(mainForm.ContactsMgr.GetContact(packet.ContactName));
            }));
        }

        public void HandleStatusPacket(PacketStatus packet)
        {
            Program.Console.WriteMessage(
                $"[General] Status change: " +
                $"{(string.IsNullOrWhiteSpace(packet.ContactName) ? "SELF" : packet.ContactName)} -> {packet.Status}");
            
            mainForm.Invoke(new Action(() =>
            {
                if (string.IsNullOrWhiteSpace(packet.ContactName))
                    mainForm.OnStatusChange(packet.Status);
                else
                {
                    Contact contact = mainForm.ContactsMgr.GetContact(packet.ContactName);

                    if (contact == null) 
                    {
                        Program.Console.WriteMessage($"[General] Received invalid status change" +
                            $", \"{packet.ContactName}\" is not a valid contact!");
                        return;
                    }

                    if (mainForm.CurrentUser.Status != UserStatus.BUSY) 
                    {
                        if (packet.Status == UserStatus.OFFLINE &&
                            contact.Status != UserStatus.OFFLINE)
                        {
                            mainForm.PopupController.CreatePopup($"{packet.ContactName} is now offline",
                                "Status change");
                            new SoundPlayer() { Stream = Sounds.OFFLINE }.Play();
                        }
                        else if (packet.Status != UserStatus.OFFLINE &&
                            contact.Status == UserStatus.OFFLINE)
                        {
                            mainForm.PopupController.CreatePopup($"{packet.ContactName} is now online",
                                "Status change");
                            new SoundPlayer() { Stream = Sounds.ONLINE }.Play();
                        }
                    }

                    if (packet.Status == UserStatus.BUSY &&
                        contact.Status != UserStatus.BUSY)
                    {
                        MessageForm msgForm = mainForm
                            .GetMessageFormFromReceiverName(packet.ContactName, true);
                        if (msgForm != null)
                            msgForm.InWindowPopupController.CreatePopup(
                                $"{packet.ContactName} is now busy" +
                                $" and may not see your messages");
                    }

                    mainForm.ContactsMgr.UpdateContact(new Contact()
                    {
                        Name = packet.ContactName,
                        Status = packet.Status
                    });
                }
            }));
        }

        public void HandleContactRequestPacket(PacketContactRequest packet)
        {
            Program.Console.WriteMessage($"[Networking] Received contact request from {packet.ContactName}");
            mainForm.Invoke(new Action(() =>
            {
                MsgBox.ShowPromptNotification(mainForm,
                    $"{packet.ContactName} wants to add you to their contact list. Proceed?", "Contact request",
                    MsgBoxIconType.QUESTION, true,
                    (MsgBoxButtonType button) =>
                    {
                        SendContactRequestPacket(packet.ContactName, button == MsgBoxButtonType.YES);
                    });
            }));
        }

        public void HandleClearContactsPacket()
        {
            Program.Console.WriteMessage($"[Contacts] Clearing contact list...");
            mainForm.Invoke(new Action(() =>
            {
                mainForm.dgvContacts.Rows.Clear();
                mainForm.ContactsMgr = new ContactsManager(mainForm);
            }));
        }

        public void SendLoginPacket(byte protocolVersion, string clientVersion, 
            string name, string sessionID) 
        {
            networkClient.SendPacket(new PacketLogin(protocolVersion, clientVersion, name, sessionID));
        }

        public void SendRegisterPacket(byte protocolVersion, string clientVersion, 
            string name, string sessionID)
        {
            networkClient.SendPacket(new PacketRegister(protocolVersion, clientVersion, name, sessionID));
        }

        public void SendMessagePacket(string contactName, string message)
        {
            networkClient.SendPacket(new PacketMessage(contactName, message));
        }

        public void SendStatusPacket(UserStatus status)
        {
            networkClient.SendPacket(new PacketStatus("", status));
        }

        public void SendContactRequestPacket(string name, bool approved)
        {
            networkClient.SendPacket(new PacketContactRequest($"{name}:{(approved ? "yes" : "no")}"));
        }

        public void SendAddContactPacket(string name)
        {
            networkClient.SendPacket(new PacketAddContact(name, UserStatus.OFFLINE));
        }

        public void SendRemoveContactPacket(string name)
        {
            networkClient.SendPacket(new PacketRemoveContact(name));
        }
    }
}
