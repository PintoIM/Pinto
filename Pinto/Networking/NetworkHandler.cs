using PintoNS.Forms;
using PintoNS.General;
using System;
using System.Data;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Networking
{
    public class NetworkHandler
    {
        private LoginForm loginForm;
        private MainForm mainForm;
        private NetworkClient networkClient;
        public bool LoggedIn;
        public string ServerID;

        public NetworkHandler(LoginForm loginForm, MainForm mainForm, NetworkClient networkClient)
        {
            this.loginForm = loginForm;
            this.mainForm = mainForm;
            this.networkClient = networkClient;
        }

        public void HandlePacket(IPacket packet)
        {
            if (packet.GetID() != 255)
                Program.Console.WriteMessage($"[Networking] Received packet {packet.GetType().Name.ToUpper()}" +
                    $" ({packet.GetID()})");
            packet.Handle(this);
        }

        public void HandleLoginPacket(PacketLogin packet) 
        {
            LoggedIn = true;
        }

        public void HandleServerIDPacket(PacketServerID packet)
        {
            ServerID = packet.ServerID;
            Program.Console.WriteMessage($"[Networking] The ID of the server is {ServerID}");
        }

        public void HandleLogoutPacket(PacketLogout packet)
        {
            loginForm.NetManager.IsActive = false;
            mainForm.ChangeStatus(UserStatus.OFFLINE);
            Program.Console.WriteMessage($"[Networking] Kicked by the server: {packet.Reason}");
            loginForm.Invoke(new Action(() =>
            {
                MsgBox.Show(loginForm, packet.Reason, "Kicked by the server", 
                    MsgBoxIconType.WARNING, true);
                loginForm.Disconnect(true);
            }));
        }

        public void HandleMessagePacket(PacketMessage packet) 
        {
            mainForm.Invoke(new Action(() =>
            {
                // TODO: Change to new messaging logic
            }));
        }

        public void HandlePopupPacket(PacketPopup packetPopup)
        {
            mainForm.Invoke(new Action(() =>
            {
                mainForm.PopupController.CreatePopup(packetPopup.Body, packetPopup.Title);
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
                mainForm.ContactsMgr.AddContact(new Contact() { Name = packet.ContactName, Status = packet.Status, MOTD = packet.MOTD });
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

                    if (loginForm.CurrentUser.Status != UserStatus.BUSY) 
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
                object dataSource = mainForm.dgvContacts.DataSource;
                Program.Console.WriteMessage($"DataSource is null: {dataSource == null}");
                
                if (dataSource != null) 
                {
                    (dataSource as DataTable).Rows.Clear();
                    mainForm.ContactsMgr = new ContactsManager(mainForm);
                }
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
            }));
        }

        public void SendLoginPacket(byte protocolVersion, string clientVersion, string token) 
        {
            networkClient.SendPacket(new PacketLogin(protocolVersion, clientVersion, token));
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
    }
}
