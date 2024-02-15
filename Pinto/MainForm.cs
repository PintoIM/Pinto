﻿using PintoNS.Calls;
using PintoNS.Contacts;
using PintoNS.Forms;
using PintoNS.Scripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PintoNS.UI;
using PintoNS.Networking;

namespace PintoNS
{
    public partial class MainForm : Form
    {
        private bool doNotCancelClose;
        private bool isPortable;
        public User LocalUser = new User();
        public ContactsManager ContactsMgr;
        public InWindowPopupController InWindowPopupController;
        public PopupController PopupController;
        public List<MessageForm> MessageForms;
        public NetClientHandler NetHandler;
        internal Thread loginPacketCheckThread;
        public CallStatus CurrentCallStatus = CallStatus.ENDED;

        public MainForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
            InWindowPopupController = new InWindowPopupController(this, 70);
            PopupController = new PopupController();
        }

        internal void OnLogin()
        {
            Program.Console.WriteMessage("[General] Changing UI state to logged in");
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpStart);
            tcTabs.TabPages.Add(tpContacts);

            if (!Settings.AutoStartPage)
                tcTabs.SelectedTab = tpContacts;

            UpdateQuickActions(true);
            OnStatusChange(UserStatus.ONLINE, "");
            MessageForms = new List<MessageForm>();

            // Use a DataTable to allow usage of more options than a plain DataGridView
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("contactStatus", typeof(Bitmap));
            dataTable.Columns.Add("contactName", typeof(string));
            dataTable.Columns.Add("contactMOTD", typeof(string));
            dgvContacts.DataSource = dataTable;
            ContactsMgr = new ContactsManager(this);
            ContactsMgr.Clear();

            DataGridViewColumn contactStatus = dgvContacts.Columns["contactStatus"];
            contactStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            contactStatus.FillWeight = 24;
            contactStatus.Width = 24;
            contactStatus.Width = 24;

            DataGridViewColumn contactMOTD = dgvContacts.Columns["contactMOTD"];
            contactMOTD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            txtSearchBox.Enabled = true;
            lContactsNoContacts.Visible = true;

            tsmiMenuBarToolsAddContact.Enabled = true;
            tsmiMenuBarToolsRemoveContact.Enabled = true;
            tsmiMenuBarToolsServerInfo.Enabled = true;
            tsmiMenuBarFileChangeStatus.Enabled = true;
            tsmiMenuBarFileLogOff.Enabled = true;
            Text = $"Pinto! Beta - {LocalUser.Name}";
            new SoundPlayer(Sounds.LOGIN).Play();
        }

        internal void UpdateQuickActions(bool loggedInState)
        {
            if (loggedInState)
            {
                btnQAAddContact.Image = Assets.ADDCONTACT_ENABLED;
                btnQAAddContact.Enabled = true;
            }
            else
            {
                btnQAAddContact.Image = Assets.ADDCONTACT_DISABLED;
                btnQAAddContact.Enabled = false;
            }
        }

        internal void OnStatusChange(UserStatus status, string motd)
        {
            Program.Console.WriteMessage($"[General] Changing user status to: {status} ({motd})");
            bool isOnline = status != UserStatus.OFFLINE && status != UserStatus.CONNECTING;
            tsddbStatusBarStatus.Enabled = isOnline;
            tsddbStatusBarStatus.Image = User.StatusToBitmap(status);
            tsslStatusBarStatusText.Text = status != UserStatus.OFFLINE ? User.StatusToText(status) : "Not logged in";
            tsddbStatusBarMOTD.Enabled = isOnline;
            tsddbStatusBarMOTD.Text = isOnline &&
                !string.IsNullOrWhiteSpace(motd.Trim()) ? motd.Trim() : "(no MOTD set)";

            LocalUser.Status = status;
            LocalUser.MOTD = motd;

            SyncTray();
        }

        internal void OnLogout(bool noSound = false)
        {
            Program.Console.WriteMessage("[General] Changing UI state to logged out");
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpLogin);
            UpdateQuickActions(false);
            OnStatusChange(UserStatus.OFFLINE, "");
            LocalUser.Name = null;
            LocalUser.MOTD = null;

            if (MessageForms != null && MessageForms.Count > 0)
            {
                foreach (MessageForm msgForm in MessageForms.ToArray())
                {
                    msgForm.Hide();
                    msgForm.Dispose();
                }
            }

            ContactsMgr = null;
            MessageForms = null;

            btnStartCall.Enabled = false;
            btnStartCall.Image = Assets.STARTCALL_DISABLED;
            btnEndCall.Enabled = false;
            btnEndCall.Image = Assets.ENDCALL_DISABLED;

            txtSearchBox.Text = "";
            txtSearchBox.ChangeTextDisplayed();
            txtSearchBox.Enabled = false;
            lContactsNoContacts.Visible = false;

            tsmiMenuBarToolsAddContact.Enabled = false;
            tsmiMenuBarToolsRemoveContact.Enabled = false;
            tsmiMenuBarToolsServerInfo.Enabled = false;
            tsmiMenuBarFileChangeStatus.Enabled = false;
            tsmiMenuBarFileLogOff.Enabled = false;
            Text = "Pinto! Beta";

            if (!noSound)
                new SoundPlayer(Sounds.LOGOUT).Play();
        }

        internal void SetConnectingState(bool state)
        {
            UpdateQuickActions(!state);
            tsmiMenuBarToolsAddContact.Enabled = !state;
            tsmiMenuBarToolsRemoveContact.Enabled = !state;
            tsmiMenuBarToolsServerInfo.Enabled = !state;
            tsmiMenuBarFileChangeStatus.Enabled = !state;

            if (state)
                OnStatusChange(UserStatus.CONNECTING, "");
            else
                OnStatusChange(UserStatus.ONLINE, "");
        }

        internal void OnCallStatusChanged(CallStatus status, string callWith = null)
        {
            CallStatus previousStatus = CurrentCallStatus;
            bool previousStatusEnded = CallStatusMeansEnded(previousStatus);
            bool callEnded = CallStatusMeansEnded(status);
            CurrentCallStatus = status;
            Program.Console.WriteMessage($"[General] Changed call status: {previousStatus} -> {CurrentCallStatus}");

            if (!previousStatusEnded && callEnded)
            {
                btnStartCall.Enabled = false;
                btnStartCall.Image = Assets.STARTCALL_DISABLED;
                btnEndCall.Enabled = false;
                btnEndCall.Image = Assets.ENDCALL_DISABLED;
                tcTabs.TabPages.Remove(tpCall);
                tcTabs.SelectedTab = tpContacts;
                dgvContacts.ClearSelection();
            }
            else if (previousStatusEnded && !callEnded)
            {
                btnStartCall.Enabled = false;
                btnStartCall.Image = Assets.STARTCALL_DISABLED;
                btnEndCall.Enabled = true;
                btnEndCall.Image = Assets.ENDCALL_ENABLED;
                tcTabs.TabPages.Add(tpCall);
                tcTabs.SelectedTab = tpCall;
            }

            if (!callEnded)
            {
                Program.Console.WriteMessage($"[General] Updated call details ({callWith}, {status})");
                tpCall.Text = callWith;
                lCallTarget.Text = $"In call with {callWith}";
                lCallStatus.Text = $"{Utils.FirstLetterToUpper(status.ToString().ToLower())}" +
                    $"{(status == CallStatus.CONNECTING ? "..." : "")}";
            }
            else
            {
                Program.Console.WriteMessage($"[General] Cleared call details");
                tpCall.Text = null;
                lCallTarget.Text = "In call with";
                lCallStatus.Text = "-";
            }
        }

        internal void UpdateOnlineContacts()
        {
            if (ContactsMgr == null) return;
            int online = ContactsMgr.GetContacts().Count((Contact contact) =>
            {
                return !contact.Name.StartsWith("G:") &&
                        contact.Status != UserStatus.OFFLINE &&
                        contact.Status != UserStatus.CONNECTING;
            });
            if(online==0)
                llStartContacts.Text = $"No Contacts Online";
            else if(online==1)
                llStartContacts.Text = $"1 Contact Online";
            else
                llStartContacts.Text = $"{online} Contacts Online";
        }

        public static bool CallStatusMeansEnded(CallStatus status)
        {
            return status == CallStatus.ENDED || status == CallStatus.ERROR;
        }

        public void SyncTray()
        {
            Program.Console.WriteMessage("[General] Synchronizing tray status...");
            bool isOnline = LocalUser.Status != UserStatus.OFFLINE && LocalUser.Status != UserStatus.CONNECTING;
            niTray.Visible = true;
            niTray.Icon = User.StatusToIcon(LocalUser.Status);
            niTray.Text = $"Pinto! Beta - " + (isOnline ? $"{LocalUser.Name} - " +
                $"{User.StatusToText(LocalUser.Status)}" : 
                LocalUser.Status == UserStatus.CONNECTING ? "Connecting..." : "Not logged in");
            tsmiTrayChangeStatus.Enabled = isOnline;
        }

        public void ConnectCached(string ip, int port, string username, string password)
        {
            //Program.Console.WriteMessage($"[Networking] Cache connecting at {ip}:{port} as {username}...");
            //LocalUser.Name = username;
            //NetManager = new NetworkManager(this, true, ip, port, username, password);
            //NetManager.ScheduleConnecting();

            //OnLogin();
            //SetConnectingState(true);

            //foreach (string contact in LastContacts.GetLastContacts())
            //{
            //    ContactsMgr.AddContact(new Contact()
            //    {
            //        Name = contact,
            //        MOTD = "",
            //        Status = UserStatus.CONNECTING
            //    });
            //}
        }

        public async Task Connect(string ip, int port, string username, string password, bool register)
        {
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpConnecting);
            OnStatusChange(UserStatus.CONNECTING, "");

            Action<string> changeConnectionStatus = (string status) =>
            {
                Invoke(new Action(() =>
                {
                    lConnectingStatus.Text = status;
                }));
            };

            try 
            {
                Program.Console.WriteMessage($"[Networking] Connecting at {ip}:{port} as {username}...");
                NetHandler = await NetClientHandlerFactory.Create(this, ip, port, changeConnectionStatus);
                tNetUpdate.Start();
                LocalUser.Name = username;
                lConnectingStatus.Text = register ? "Registering..." : "Authenticating...";

                if (!register)
                    NetHandler.Login(username, password);
                else
                    NetHandler.Register(username, password);

                if (loginPacketCheckThread != null)
                    loginPacketCheckThread.Abort();

                StartLoginPacketCheckThread(ip, port);
            }
            catch (Exception ex) 
            {
                Disconnect();
                lConnectingStatus.Text = "";
                if (!(ex is PintoVerificationException))
                {
                    Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}: {ex}");
                    MsgBox.Show(this, $"Unable to connect to {ip}:{port}: {ex.Message}", 
                        "Connection Error", MsgBoxIconType.ERROR);
                }
                UsingPintoForm.SetHasLoggedIn(false);
            }
        }

        private void StartLoginPacketCheckThread(string ip, int port)
        {
            loginPacketCheckThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    Thread.Sleep(5000);

                    if (NetHandler != null && !NetHandler.LoggedIn)
                    {
                        Invoke(new Action(() =>
                        {
                            Disconnect();
                            Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}:" +
                                $" No login packet received from the server in an acceptable time frame");
                            MsgBox.Show(this,
                                $"No login packet received from the server in an acceptable time frame",
                                "Connection Error", MsgBoxIconType.ERROR);
                        }));
                    }
                }
                catch { }
            }));
            loginPacketCheckThread.Start();
        }

        public void Disconnect()
        {
            Program.Console.WriteMessage("[Networking] Disconnecting...");
            bool wasLoggedIn = NetHandler != null ? NetHandler.LoggedIn : false;

            tNetUpdate.Stop();
            if (NetHandler != null)
                NetHandler.Disconnect();
            NetHandler = null;

            lConnectingStatus.Text = "";
            OnLogout(!wasLoggedIn);
        }

        public MessageForm GetMessageFormFromReceiverName(string name, bool doNotCreate = false)
        {
            Program.Console.WriteMessage($"Getting MessageForm for {name}...");

            if (MessageForms == null)
                return null;

            foreach (MessageForm msgForm in MessageForms.ToArray())
            {
                if (msgForm.Receiver.Name == name)
                    return msgForm;
            }

            MessageForm messageForm = null;

            if (!doNotCreate)
            {
                Program.Console.WriteMessage($"Creating MessageForm for {name}...");
                messageForm = new MessageForm(this, ContactsMgr.GetContact(name));
                MessageForms.Add(messageForm);
                bool isBusy = LocalUser.Status == UserStatus.BUSY;

                if (ActiveForm == null || !(ActiveForm is MessageForm))
                    messageForm.Show();
                else
                {
                    // TODO: Fix MessageForm yoinking focus
                    if (isBusy) messageForm.WindowState = FormWindowState.Minimized;
                }
            }

            return messageForm;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            Program.Console.WriteMessage("[General] Performing first time initialization...");

            OnLogout(true);
            isPortable = true;
            if (File.Exists(".IS_PORTABLE_CHECK"))
                isPortable = true;

            if (Settings.AutoCheckForUpdates && !isPortable)
                await CheckForUpdates(false);

            Program.Scripts.ForEach((IPintoScript script) => { script.OnPintoInit(); });
            //llLogin_LinkClicked(this, null);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Settings.NoMinimizeToSysTray && !doNotCancelClose && e.CloseReason == CloseReason.UserClosing)
            {
                if (!Settings.DoNotShowSysTrayNotice)
                {
                    Settings.DoNotShowSysTrayNotice = true;
                    Settings.Export(Program.SettingsFile);
                    niTray.ShowBalloonTip(0, "Pinto!", "Pinto! is still running in the system tray," +
                        " you can change this behaviour in the settings of Pinto!, to exit," +
                        " go to the \"File\" menu or right click the system tray", ToolTipIcon.Info);
                }

                e.Cancel = true;
                Hide();
                return;
            }

            Program.Console.WriteMessage("Quitting...");
            bool wasLoggedIn = NetHandler != null && NetHandler.LoggedIn;
            if (NetHandler != null) NetHandler.LoggedIn = false;
            Disconnect();
            InWindowPopupController.Dispose();

            if (loginPacketCheckThread != null)
                loginPacketCheckThread.Abort();

            if (!Settings.NoGracefulExit && wasLoggedIn)
                new Thread(new ThreadStart(() =>
                {
                    new SoundPlayer(Sounds.LOGOUT).PlaySync();
                })).Start();
            else
                Environment.Exit(0);
        }

        private void dgvContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Stolen from https://stackoverflow.com/a/50999419
            string contactName = ContactsMgr.GetContactNameFromRow(
                ((DataTable)dgvContacts.DataSource).Rows.IndexOf(
                    ((DataRowView)BindingContext[dgvContacts.DataSource].Current).Row));

            if (contactName != null)
            {
                MessageForm messageForm = GetMessageFormFromReceiverName(contactName);
                messageForm.WindowState = FormWindowState.Normal;
                messageForm.BringToFront();
                messageForm.Focus();
            }
        }

        private void llLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new UsingPintoForm(this).ShowDialog();
        }

        private void tsmiMenuBarFileLogOut_Click(object sender, EventArgs e)
        {
            if (NetHandler == null) return;
            UsingPintoForm.SetHasLoggedIn(false);
            Disconnect();
        }

        private void tsmiMenuBarHelpAbout_Click(object sender, EventArgs e) => new AboutForm().ShowDialog(this);

        private void tsmiStatusBarStatusOnline_Click(object sender, EventArgs e)
        {
            if (NetHandler == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            NetHandler.SendStatusChange(UserStatus.ONLINE, LocalUser.MOTD);
        }

        private void tsmiStatusBarStatusAway_Click(object sender, EventArgs e)
        {
            if (NetHandler == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            NetHandler.SendStatusChange(UserStatus.AWAY, LocalUser.MOTD);
        }

        private void tsmiStatusBarStatusBusy_Click(object sender, EventArgs e)
        {
            if (NetHandler == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            InWindowPopupController.CreatePopup("You are now busy" +
                ", this means that you will not receive any non-important popups", true);
            NetHandler.SendStatusChange(UserStatus.BUSY, LocalUser.MOTD);
        }

        private void tsmiStatusBarStatusInvisible_Click(object sender, EventArgs e)
        {
            if (NetHandler == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            MsgBox.Show(this, "If you choose to change your status to invisible," +
                " you will no longer be able to receive/send messages. Are you sure you want to continue?",
                "Status change confirmation",
                MsgBoxIconType.WARNING, false, true, (MsgBoxButtonType button) =>
            {
                if (button == MsgBoxButtonType.YES)
                    NetHandler.SendStatusChange(UserStatus.INVISIBLE, LocalUser.MOTD);
            });
        }

        private void tsmiMenuBarToolsAddContact_Click(object sender, EventArgs e)
        {
            if (NetHandler == null) return;
            AddContactForm addContactForm = new AddContactForm(this);
            addContactForm.ShowDialog(this);
        }

        private void tsmiMenuBarToolsRemoveContact_Click(object sender, EventArgs e)
        {
            if (NetHandler == null) return;
            if (dgvContacts.SelectedRows.Count < 1)
            {
                MsgBox.Show(this, "You have not selected any contact!", "Error", MsgBoxIconType.ERROR);
                return;
            }
            string contactName = ContactsMgr.GetContactNameFromRow(dgvContacts.SelectedRows[0].Index);
            NetHandler.RemoveContact(contactName);
        }

        private void dgvContacts_SelectionChanged(object sender, EventArgs e)
        {
            /*
            if (NetManager == null || NetManager.InCall) return;

            if (dgvContacts.SelectedRows.Count > 0)
            {
                btnStartCall.Enabled = true;
                btnStartCall.Image = Assets.STARTCALL_ENABLED;
            }
            else
            {
                btnStartCall.Enabled = false;
                btnStartCall.Image = Assets.STARTCALL_DISABLED;
            }*/
        }

        private void btnStartCall_Click(object sender, EventArgs e)
        {
            //if (NetHandler == null || NetHandler.InCall) return;
            //NetHandler.StartCall(ContactsMgr.GetContactNameFromRow(dgvContacts.SelectedRows[0].Index));
        }

        private void btnEndCall_Click(object sender, EventArgs e)
        {
            //if (NetHandler == null) return;
            //NetHandler.EndCall();
        }

        private void tsmiMenuBarHelpToggleConsole_Click(object sender, EventArgs e)
        {
            if (Program.Console.Visible)
                Program.Console.Hide();
            else
                Program.Console.Show();
        }

        private void niTray_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
        }

        private void tsmiMenuBarFileOptions_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm(this);
            optionsForm.ShowDialog(this);
        }

        private void tsmiMenuBarFileExit_Click(object sender, EventArgs e)
        {
            if (Settings.NoExitPrompt)
            {
                Shutdown();
                return;
            }

            MsgBox.Show(null, "Are you sure you want to close Pinto?" +
                " You will no longer receive messages or calls if you do so.", "Quit Pinto?",
                MsgBoxIconType.QUESTION, false, true, (MsgBoxButtonType answer) =>
                {
                    if (answer == MsgBoxButtonType.YES) Shutdown();
                });
        }

        public void Shutdown()
        {
            doNotCancelClose = true;
            Close();
        }

        public async Task CheckForUpdates(bool showLatestMessage)
        {
            if (isPortable)
            {
                MsgBox.Show(this, "Checking for updates is not available on the portable version!",
                    "Updates Unavailable", MsgBoxIconType.WARNING, true);
                return;
            }

            if (!await UpdaterUtils.IsLatest())
                MsgBox.Show(this,
                    "An update is available, do you want to download it and install it?",
                    "Update Available",
                    MsgBoxIconType.QUESTION,
                    true, true, async (MsgBoxButtonType btn) =>
                    {
                        if (btn == MsgBoxButtonType.YES)
                        {
                            string path = Path.Combine(Program.DataFolder, "PintoSetup.exe");
                            if (File.Exists(path))
                                File.Delete(path);

                            byte[] file = await UpdaterUtils.GetUpdateFile();
                            if (file == null) return;
                            File.WriteAllBytes(path, file);
                            Program.Console.WriteMessage($"[Updater] Saved update file at {path}");

                            Program.Console.WriteMessage($"[Updater] Running installer at {path}...");
                            Process process = new Process()
                            {
                                StartInfo = new ProcessStartInfo()
                                {
                                    FileName = "PintoSetup.exe",
                                    Arguments = "upgrade",
                                    WorkingDirectory = Program.DataFolder
                                }
                            };
                            process.Start();

                            Program.Console.WriteMessage($"[Updater] Exitting...");
                            doNotCancelClose = true;
                            Close();
                        }
                    });
            else if (showLatestMessage)
                MsgBox.Show(this, "You are already on the latest version of Pinto!",
                    "Latest version", MsgBoxIconType.INFORMATION, true);
        }

        private async void tsmiMenuBarHelpCheckForUpdates_Click(object sender, EventArgs e)
            => await CheckForUpdates(true);

        private void txtSearchBox_TextChanged2(object sender, EventArgs e)
        {
            tcTabs.SelectedTab = tpContacts;
            txtSearchBox.Focus();
            DataTable dataTable = dgvContacts.DataSource as DataTable;
            if (string.IsNullOrWhiteSpace(txtSearchBox.Text))
                dataTable.DefaultView.RowFilter = "";
            else
                dataTable.DefaultView.RowFilter = $"contactName Like '{txtSearchBox.Text}*'";
            dgvContacts.Refresh();
        }

        private void llStartContacts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tcTabs.SelectedTab = tpContacts;
        }

        private void tsmiMenuBarHelpReportAProblem_Click(object sender, EventArgs e)
            => Process.Start("https://github.com/PintoIM/Pinto/issues");

        private void dgvContacts_CellContextMenuStripNeeded(object sender,
            DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.ShowImageMargin = false;

            ToolStripMenuItem startMessaging = new ToolStripMenuItem("Start Messaging");
            ToolStripMenuItem startTalking = new ToolStripMenuItem("Start Talking");
            ToolStripMenuItem removeContact = new ToolStripMenuItem("Remove Contact");

            startMessaging.Click += new EventHandler((object sender2, EventArgs e2) =>
            {
                string contactName = ContactsMgr.GetContactNameFromRow(e.RowIndex);
                MessageForm messageForm = GetMessageFormFromReceiverName(contactName);
                messageForm.WindowState = FormWindowState.Normal;
                messageForm.BringToFront();
                messageForm.Focus();
            });

            startTalking.Enabled = false;
            startTalking.Click += new EventHandler((object sender2, EventArgs e2) =>
            {
                //if (NetHandler == null || NetHandler.InCall) return;
                //string contactName = ContactsMgr.GetContactNameFromRow(e.RowIndex);
                //NetHandler.StartCall(contactName);
            });

            removeContact.Click += new EventHandler((object sender2, EventArgs e2) =>
            {
                string contactName = ContactsMgr.GetContactNameFromRow(e.RowIndex);
                NetHandler.RemoveContact(contactName);
            });

            contextMenu.Items.Add(startMessaging);
            contextMenu.Items.Add(startTalking);
            contextMenu.Items.Add(removeContact);
            e.ContextMenuStrip = contextMenu;
        }

        private void tsddbStatusBarMOTD_Click(object sender, EventArgs e)
        {
            if (NetHandler == null) return;
            ChangeMOTDForm changeMOTDForm = new ChangeMOTDForm(this);
            changeMOTDForm.ShowDialog(this);
        }

        private void tsmiMenuBarToolsServerInfo_Click(object sender, EventArgs e)
        {
            if (NetHandler == null) return;
            ServerInfoForm form = new ServerInfoForm();
            form.lInfo.Text = string.Format(form.lInfo.Text, NetHandler.ServerID,
                NetHandler.ServerSoftware);
            form.Show();
            form.MoveCenteredToWindow(this);
        }

        private void tsmiMenuBarHelpViewScripts_Click(object sender, EventArgs e)
        {
            ScriptsViewerForm form = new ScriptsViewerForm();
            form.Show();
        }

        private void tNetUpdate_Tick(object sender, EventArgs e)
        {
            if (NetHandler == null) return;
            NetHandler.OnUpdate();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Retarded hack to bypass shitty windowing issues on start-up
            TopMost = true;
            Focus();
            Invalidate();
            Update();
            Refresh();
            Application.DoEvents();
            TopMost = false;
            Focus();
        }
    }
}
