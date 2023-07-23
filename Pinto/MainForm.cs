using NLua;
using PintoNS.Controls;
using PintoNS.Forms;
using PintoNS.General;
using PintoNS.Networking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS
{
    public partial class MainForm : Form
    {
        private bool doNotCancelClose;
        private bool isPortable;
        private LoginForm loginForm;
        public User CurrentUser = new User();
        public ContactsManager ContactsMgr;
        public InWindowPopupController InWindowPopupController;
        public PopupController PopupController;
        public List<MessageForm> MessageForms;
        public NetworkManager NetManager;
        private Thread loginPacketCheckThread;
        private int connectingStatusTrayFrame = 0;
        private Icon[] connectingStatusTrayFrames = new Icon[] { Statuses.CONNECTING_0, Statuses.CONNECTING_2, 
            Statuses.CONNECTING_4, Statuses.CONNECTING_6, Statuses.CONNECTING_8 };

        public MainForm(LoginForm loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            Icon = Program.GetFormIcon();
            InWindowPopupController = new InWindowPopupController(this, scSections.Panel1.Width, Height - 21 * 3);
            PopupController = new PopupController();
        }

        // From https://stackoverflow.com/a/2613272
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        internal void OnLogin()
        {
            OnStatusChange(UserStatus.ONLINE, "");
            MessageForms = new List<MessageForm>();

            // Use a DataTable to allow usage of more options than a plain DataGridView
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("contactStatus", typeof(Bitmap));
            dataTable.Columns.Add("contactName", typeof(string));
            dataTable.Columns.Add("contactMOTD", typeof(string));
            dgvContacts.DataSource = dataTable;

            DataGridViewColumn contactStatus = dgvContacts.Columns["contactStatus"];
            contactStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            contactStatus.FillWeight = 24;
            contactStatus.Width = 24;
            contactStatus.Width = 24;

            DataGridViewColumn contactMOTD = dgvContacts.Columns["contactMOTD"];
            contactMOTD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 

            txtSearchBox.Enabled = true;
            lContactsNoContacts.Visible = true;
            lUserInfoName.Text = CurrentUser.Name;
            wbPintoNews.DocumentText = HTML.PINTO_NEWS_ERROR;

            tsmiMenuBarToolsAddContact.Enabled = true;
            tsmiMenuBarToolsRemoveContact.Enabled = true;
            tsmiMenuBarFileChangeStatus.Enabled = true;
            tsmiMenuBarFileLogOff.Enabled = true;
            Text = $"Pinto! Beta - {CurrentUser.Name}";

            new SoundPlayer(Sounds.LOGIN).Play();
        }

        internal void OnStatusChange(UserStatus status, string motd)
        {
            ResetConnectingStatus();
            mbUserInfoStatus.Image = User.StatusToBitmap(status);
            /*
            tsddbStatusBarMOTD.Text = status != UserStatus.OFFLINE && 
                !string.IsNullOrWhiteSpace(motd.Trim()) ? motd.Trim() : "(no MOTD set)";*/

            CurrentUser.Status = status;
            CurrentUser.MOTD = motd;

            if (status == UserStatus.OFFLINE)
            {
                CurrentUser.Name = null;
                CurrentUser.MOTD = null;
            }

            SyncTray();
            Program.CallExtensionsEvent("OnLogin");
        }

        internal void SwitchToConnectingStatus()
        {
            tConnectingTray.Start();
            tConnectingTray_Tick(this, EventArgs.Empty);
            mbUserInfoStatus.Image = Statuses.CONNECTING;
        }

        internal void ResetConnectingStatus()
        {
            tConnectingTray.Stop();
            connectingStatusTrayFrame = 0;
        }

        internal void OnLogout(bool noSound = false, bool doNotShowLoginForm = false)
        {
            OnStatusChange(UserStatus.OFFLINE, "");

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
            dgvContacts.DataSource = null;

            txtSearchBox.Text = "";
            txtSearchBox.ChangeTextDisplayed();
            txtSearchBox.Enabled = false;
            lContactsNoContacts.Visible = false;
            lUserInfoName.Text = "PintoUser";
            wbPintoNews.Navigate("about:blank");

            tsmiMenuBarToolsAddContact.Enabled = false;
            tsmiMenuBarToolsRemoveContact.Enabled = false;
            tsmiMenuBarFileChangeStatus.Enabled = false;
            tsmiMenuBarFileLogOff.Enabled = false;
            Text = "Pinto! Beta";

            if (!noSound)
                new SoundPlayer(Sounds.LOGOUT).Play();
            Program.CallExtensionsEvent("OnLogout");
            
            if (!doNotShowLoginForm) 
            {
                loginForm.Show();
                Close();
            }
        }

        internal void SyncTray()
        {
            niTray.Visible = true;
            niTray.Icon = User.StatusToIcon(CurrentUser.Status);
            niTray.Text = $"Pinto! Beta - " +
                (CurrentUser.Status != UserStatus.OFFLINE ?
                $"{CurrentUser.Name} - {User.StatusToText(CurrentUser.Status)}" : "Not logged in");
            tsmiTrayChangeStatus.Enabled = CurrentUser.Status != UserStatus.OFFLINE;
        }

        public async Task Connect(string ip, int port, string username, string password)
        {
            Program.Console.WriteMessage($"[Networking] Signing in as {username} at {ip}:{port}...");

            NetManager = new NetworkManager(this);
            (bool, Exception) connectResult = await NetManager.Connect(ip, port);

            if (!connectResult.Item1)
            {
                Disconnect();
                Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}: {connectResult.Item2}");
                MsgBox.Show(this, $"Unable to connect to {ip}:{port}:" +
                    $" {connectResult.Item2.Message}", "Connection Error", MsgBoxIconType.ERROR);
            }
            else
            {
                CurrentUser.Name = username;
                NetManager.Login(username, password);

                if (loginPacketCheckThread != null)
                    loginPacketCheckThread.Abort();

                loginPacketCheckThread = new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        Thread.Sleep(5000);

                        if (NetManager != null &&
                            NetManager.NetHandler != null &&
                            !NetManager.NetHandler.LoggedIn)
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
        }

        public async Task ConnectRegister(string ip, int port, string username, string password)
        {
            tcTabs.TabPages.Clear();
            Program.Console.WriteMessage($"[Networking] Registering in as {username} at {ip}:{port}...");

            NetManager = new NetworkManager(this);
            (bool, Exception) connectResult = await NetManager.Connect(ip, port);

            if (!connectResult.Item1)
            {
                Disconnect();
                Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}: {connectResult.Item2}");
                MsgBox.Show(this, $"Unable to connect to {ip}:{port}:" +
                    $" {connectResult.Item2.Message}", "Connection Error", MsgBoxIconType.ERROR);
            }
            else
            {
                CurrentUser.Name = username;
                NetManager.Register(username, password);

                if (loginPacketCheckThread != null)
                    loginPacketCheckThread.Abort();

                loginPacketCheckThread = new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        Thread.Sleep(5000);

                        if (NetManager != null &&
                            NetManager.NetHandler != null &&
                            !NetManager.NetHandler.LoggedIn)
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
        }

        public void Disconnect()
        {
            Program.Console.WriteMessage("[Networking] Disconnecting...");
            bool wasLoggedIn = false;

            if (NetManager != null)
            {
                wasLoggedIn = NetManager.NetHandler.LoggedIn;
                if (NetManager.IsActive)
                    NetManager.Disconnect("User requested disconnect");
            }

            NetManager = null;
            OnLogout(!wasLoggedIn);

            Program.CallExtensionsEvent("OnDisconnect");
        }

        public MessageForm GetMessageFormFromReceiverName(string name, bool doNotCreate = false)
        {
            Program.Console.WriteMessage($"Getting MessageForm for {name}...");

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
                messageForm.Show();
            }

            return messageForm;
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            tcTabs.DisplayStyleProvider = new ModernTabControlStyleProvider(tcTabs);
            OnLogout(true, true);

            if (File.Exists(".IS_PORTABLE_CHECK"))
                isPortable = true;
            if (Settings.AutoCheckForUpdates && !isPortable)
                await CheckForUpdates(false);
            
            Program.CallExtensionsEvent("OnFormLoad");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (!Settings.NoMinimizeToSysTray && !doNotCancelClose && e.CloseReason == CloseReason.UserClosing) 
            {
                if (!Settings.DoNotShowSysTrayNotice) 
                {
                    Settings.DoNotShowSysTrayNotice = true;
                    Settings.Export(Program.SettingsFile);
                    niTray.ShowBalloonTip(0, "Pinto!", "Pinto! is still running in the system tray," +
                        " you can change this behaviour in the settings, to exit," +
                        " go to the \"File\" menu or right click the system tray", ToolTipIcon.Info);
                }

                e.Cancel = true;
                Hide();
                return;
            }

            Program.Console.WriteMessage("Quitting...");
            bool wasLoggedIn = NetManager != null && NetManager.NetHandler.LoggedIn;
            OnLogout(true);
            Disconnect();

            if (loginPacketCheckThread != null)
                loginPacketCheckThread.Abort();

            if (!Settings.NoGracefulExit && wasLoggedIn)
                new Thread(new ThreadStart(() => 
                {
                    new SoundPlayer(Sounds.LOGOUT).PlaySync();
                })).Start();*/
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
        
        private void tsmiMenuBarFileLogOut_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Disconnect();
        }

        private void tsmiMenuBarHelpAbout_Click(object sender, EventArgs e) => new AboutForm().Show();

        private void tsmiStatusBarStatusOnline_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            SwitchToConnectingStatus();
            NetManager.ChangeStatus(UserStatus.ONLINE, CurrentUser.MOTD);
        }

        private void tsmiStatusBarStatusAway_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            SwitchToConnectingStatus();
            NetManager.ChangeStatus(UserStatus.AWAY, CurrentUser.MOTD);
        }

        private void tsmiStatusBarStatusBusy_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            SwitchToConnectingStatus();
            NetManager.ChangeStatus(UserStatus.BUSY, CurrentUser.MOTD);
        }

        private void tsmiStatusBarStatusInvisible_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            MsgBox.Show(this, "If you choose to change your status to invisible," +
                " your contacts will not be able to send you messages. Are you sure you want to continue?", 
                "Status change confirmation",
                MsgBoxIconType.WARNING, false, true, (MsgBoxButtonType button) =>
            {
                if (button == MsgBoxButtonType.YES) 
                {
                    SwitchToConnectingStatus();
                    NetManager.ChangeStatus(UserStatus.INVISIBLE, CurrentUser.MOTD);
                }
            });
        }

        private void tsmiMenuBarToolsAddContact_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            AddContactForm addContactForm = new AddContactForm(this);
            addContactForm.ShowDialog(this);
        }

        private void tsmiMenuBarToolsRemoveContact_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            if (dgvContacts.SelectedRows.Count < 1)
            {
                MsgBox.Show(this, "You have not selected any contact!", "Error", MsgBoxIconType.ERROR);
                return;
            }
            string contactName = ContactsMgr.GetContactNameFromRow(dgvContacts.SelectedRows[0].Index);
            NetManager.NetHandler.SendRemoveContactPacket(contactName);
        }

        private void dgvContacts_SelectionChanged(object sender, EventArgs e)
        {
            /*
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

            if (!await Updater.IsLatest())
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

                            byte[] file = await Updater.GetUpdateFile();
                            if (file == null) return;
                            File.WriteAllBytes(path, file);
                            Program.Console.WriteMessage($"[Updater] Saved update file at {path}");

                            Program.Console.WriteMessage($"[Updater] Running installer at {path}...");
                            Process process = new Process();
                            process.StartInfo.FileName = "PintoSetup.exe";
                            process.StartInfo.Arguments = " upgrade";
                            process.StartInfo.WorkingDirectory = Program.DataFolder;
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
            ToolStripMenuItem removeContact = new ToolStripMenuItem("Remove Contact");

            startMessaging.Click += new EventHandler((object sender2, EventArgs e2) =>
            {
                string contactName = ContactsMgr.GetContactNameFromRow(e.RowIndex);
                MessageForm messageForm = GetMessageFormFromReceiverName(contactName);
                messageForm.WindowState = FormWindowState.Normal;
                messageForm.BringToFront();
                messageForm.Focus();
            });

            removeContact.Click += new EventHandler((object sender2, EventArgs e2) =>
            {
                string contactName = ContactsMgr.GetContactNameFromRow(e.RowIndex);
                NetManager.NetHandler.SendRemoveContactPacket(contactName);
            });

            contextMenu.Items.Add(startMessaging);
            contextMenu.Items.Add(removeContact);
            e.ContextMenuStrip = contextMenu;
        }

        private void tsddbStatusBarMOTD_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            ChangeMOTDForm changeMOTDForm = new ChangeMOTDForm(this);
            changeMOTDForm.ShowDialog(this);
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            InWindowPopupController.UpdateSizes(scSections.Panel1.Width, Height - 21 * 3);
        }

        private void scSections_SplitterMoved(object sender, SplitterEventArgs e)
        {
            InWindowPopupController.UpdateSizes(scSections.Panel1.Width, Height - 21 * 3);
        }

        private void tConnectingTray_Tick(object sender, EventArgs e)
        {
            if (connectingStatusTrayFrame + 1 > connectingStatusTrayFrames.Length) connectingStatusTrayFrame = 0;
            niTray.Icon = connectingStatusTrayFrames[connectingStatusTrayFrame];
            niTray.Text = $"Pinto! Beta - Connecting{"".PadRight(connectingStatusTrayFrame + 1, '.')}";
            connectingStatusTrayFrame++;
        }
    }
}