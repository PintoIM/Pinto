using PintoNS.Forms;
using PintoNS.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace PintoNS.Networking
{
    public class NetworkHandler
    {
        private MainForm mainForm;
        private NetworkClient networkClient;
        public bool LoggedIn;
        public string ServerID;
        public string ServerSoftware;

        public NetworkHandler(MainForm mainForm, NetworkClient networkClient)
        {
            this.mainForm = mainForm;
            this.networkClient = networkClient;
        }

        public void HandlePacket(IPacket packet)
        {
            if (!(packet is PacketKeepAlive))
                Program.Console.WriteMessage($"[Networking] Received packet {packet.GetType().Name.ToUpper()}" +
                $" ({packet.GetID()})");
            packet.Handle(this);
        }

        public void HandleLoginPacket(PacketLogin packet) 
        {
            LoggedIn = true;
            mainForm.NetManager.IsConnected = true;

            mainForm.Invoke(new Action(() => 
            {
                if (mainForm.NetManager.WasLoggedInOnce || mainForm.NetManager.IsCached) 
                {
                    mainForm.SetConnectingState(false);
                    return;
                }
                mainForm.NetManager.WasLoggedInOnce = true;

                UsingPintoForm.SetHasLoggedIn(true);
                mainForm.OnLogin();
            }));
        }

        public void HandleServerInfoPacket(PacketServerInfo packet)
        {
            ServerID = packet.ServerID;
            ServerSoftware = packet.ServerSoftware;
            Program.Console.WriteMessage($"[Networking] The ID of the server is {ServerID}");
            Program.Console.WriteMessage($"[Networking] Server software: {ServerSoftware}");
        }

        public void HandleLogoutPacket(PacketLogout packet)
        {
            mainForm.NetManager.IsActive = false;
            Program.Console.WriteMessage($"[Networking] Kicked by the server: {packet.Reason.Replace("\n", "\\n")}");
            networkClient.Disconnect($"Kicked by the server -> {packet.Reason.Replace("\n", "\\n")}");
            mainForm.Invoke(new Action(() =>
            {
                UsingPintoForm.SetHasLoggedIn(false);
                MsgBox.Show(mainForm, packet.Reason, "Kicked by the server", 
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
                {
                    messageForm.WriteMessage($"{packet.Sender}",
                        packet.Sender == mainForm.LocalUser.Name ? 
                            MessageForm.MsgSelfSenderColor : MessageForm.MsgOtherSenderColor, false);
                    messageForm.WriteMessage($" - ", MessageForm.MsgSeparatorColor, false);
                    messageForm.WriteMessage($"{DateTime.Now.ToString("HH:mm:ss")}",
                        MessageForm.MsgTimeColor, false);
                    messageForm.WriteMessage($":", MessageForm.MsgSeparatorColor);
                    messageForm.WriteMessage($"  {packet.Message}", MessageForm.MsgContentColor);
                }
                else
                    messageForm.WriteMessage($"{packet.Message}", MessageForm.MsgContentColor);

                if (Form.ActiveForm != messageForm && 
                    !messageForm.HasBeenInactive && 
                    mainForm.LocalUser.Status != UserStatus.BUSY)
                {
                    messageForm.HasBeenInactive = true;
                    mainForm.PopupController.CreatePopup($"Received a new message from {packet.ContactName}!", 
                        "New message");
                    new SoundPlayer() { Stream = Sounds.IM }.Play();
                }
            }));
        }

        public void HandleNotificationPacket(PacketNotification packet)
        {
            mainForm.Invoke(new Action(() =>
            {
                switch (packet.Type)
                {
                    case 0:
                        mainForm.InWindowPopupController.CreatePopup(packet.Body, false, packet.AutoCloseDelay);
                        break;
                    case 1:
                        mainForm.InWindowPopupController.CreatePopup(packet.Body, true, packet.AutoCloseDelay);
                        break;
                    case 2:
                        mainForm.PopupController.CreatePopup(packet.Body, packet.Title, packet.AutoCloseDelay);
                        break;
                }
            }));
        }

        public void HandleAddContactPacket(PacketAddContact packet)
        {
            Program.Console.WriteMessage($"[Contacts] Adding {packet.ContactName} to the contact list...");
            mainForm.Invoke(new Action(() =>
            {
                ContactsManager contactsMgr = mainForm.ContactsMgr;
                if (contactsMgr == null) return;
                contactsMgr.AddContact(new Contact() 
                { 
                    Name = packet.ContactName, 
                    Status = packet.Status,
                    MOTD = packet.MOTD
                });
                LastContacts.AddToLastContacts(packet.ContactName);
            }));
        }

        public void HandleRemoveContactPacket(PacketRemoveContact packet)
        {
            Program.Console.WriteMessage($"[Contacts] Removing {packet.ContactName} from the contact list...");
            mainForm.Invoke(new Action(() =>
            {
                ContactsManager contactsMgr = mainForm.ContactsMgr;
                if (contactsMgr == null) return;
                contactsMgr.RemoveContact(contactsMgr.GetContact(packet.ContactName));
                LastContacts.RemoveFromLastContacts(packet.ContactName);
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
                    mainForm.OnStatusChange(packet.Status, packet.MOTD);
                else
                {
                    Contact contact = mainForm.ContactsMgr.GetContact(packet.ContactName);

                    if (contact == null) 
                    {
                        Program.Console.WriteMessage($"[General] Received invalid status change" +
                            $", \"{packet.ContactName}\" is not a valid contact!");
                        return;
                    }

                    if (mainForm.LocalUser.Status != UserStatus.BUSY) 
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

                    if ((packet.Status == UserStatus.BUSY &&
                        contact.Status != UserStatus.BUSY) || 
                        (packet.Status == UserStatus.AWAY && 
                        contact.Status != UserStatus.AWAY))
                    {
                        MessageForm msgForm = mainForm
                            .GetMessageFormFromReceiverName(packet.ContactName, true);
                        if (msgForm != null)
                            msgForm.InWindowPopupController.CreatePopup(
                                $"{packet.ContactName} is now {packet.Status.ToString().ToLower()} and may not see your messages", true);
                    }

                    mainForm.ContactsMgr.UpdateContact(new Contact()
                    {
                        Name = packet.ContactName,
                        Status = packet.Status,
                        MOTD = packet.MOTD
                    });
                }
            }));
        }

        public void HandleContactRequestPacket(PacketContactRequest packet)
        {
            Program.Console.WriteMessage($"[Networking] Received contact request from {packet.ContactName}");
            mainForm.Invoke(new Action(() =>
            {
                MsgBox.Show(mainForm,
                    $"{packet.ContactName} wants to add you to their contact list. Proceed?", "Contact request",
                    MsgBoxIconType.QUESTION, true, true,
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
                mainForm.ContactsMgr.Clear();
                LastContacts.ClearLastContacts();
            }));
        }

        public void HandleKeepAlivePacket() 
        {
            networkClient.SendPacket(new PacketKeepAlive());
        }

        public void HandleTypingPacket(PacketTyping packet)
        {
            mainForm.Invoke(new Action(() =>
            {
                MessageForm msgForm = mainForm.GetMessageFormFromReceiverName(packet.ContactName, true);
                if (msgForm != null)
                    msgForm.SetReceiverTypingState(packet.State);
            }));
        }

        public void HandleCallChangeStatusPacket(PacketCallChangeStatus packet)
        {
            mainForm.Invoke(new Action(() =>
            {
                mainForm.NetManager.ChangeCallStatus(packet.CallStatus, packet.Details);
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

        public void SendStatusPacket(UserStatus status, string motd)
        {
            networkClient.SendPacket(new PacketStatus("", status, motd));
        }

        public void SendContactRequestPacket(string name, bool approved)
        {
            networkClient.SendPacket(new PacketContactRequest($"{name}:{(approved ? "yes" : "no")}"));
        }

        public void SendAddContactPacket(string name)
        {
            networkClient.SendPacket(new PacketAddContact(name, UserStatus.OFFLINE, ""));
        }

        public void SendRemoveContactPacket(string name)
        {
            networkClient.SendPacket(new PacketRemoveContact(name));
        }

        public void SendTypingPacket(string contactName, bool state)
        {
            networkClient.SendPacket(new PacketTyping(contactName, state));
        }

        public void SendCallChangeStatusPacket(CallStatus callStatus, string details)
        {
            networkClient.SendPacket(new PacketCallChangeStatus(callStatus, details));
        }
    }
}
