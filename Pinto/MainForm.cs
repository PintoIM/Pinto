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
using System.Net;
using System.Net.Cache;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS
{
    public partial class MainForm : Form
    {
        private bool doNotCancelClose;
        private bool isPortable;
        public User CurrentUser = new User();
        public ContactsManager ContactsMgr;
        public InWindowPopupController InWindowPopupController;
        public PopupController PopupController;
        public List<MessageForm> MessageForms;
        public NetworkManager NetManager;
        private Thread loginPacketCheckThread;
        private bool serverHasRules;
        private bool serverHasWelcome;
        public CallStatus CurrentCallStatus = CallStatus.ENDED;

        public MainForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
            InWindowPopupController = new InWindowPopupController(this, 70);
            PopupController = new PopupController();
        }

        internal async void OnLogin()
        {
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
            tsmiMenuBarFileChangeStatus.Enabled = true;
            tsmiMenuBarFileLogOff.Enabled = true;
            Text = $"Pinto! Beta - {CurrentUser.Name}";
            new SoundPlayer(Sounds.LOGIN).Play();

            if (Settings.NoServerHTTP) return;
            await TaskEx.Run(new Action(() => 
            {
                WebClient webClient = new WebClient();
                webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

                string serverURL = $"http://{NetManager.NetClient.IP}:{NetManager.NetClient.Port + 10}";
                Program.Console.WriteMessage($"[HTTP] HTTP server URL: {serverURL}");
                try
                {
                    Program.Console.WriteMessage($"[HTTP] Checking HTTP server for server rules page...");
                    webClient.DownloadString(new Uri($"{serverURL}/rules.html"));
                    serverHasRules = true;
                }
                catch { serverHasRules = false; }
                try
                {
                    Program.Console.WriteMessage($"[HTTP] Checking HTTP server for welcome page...");
                    webClient.DownloadString(new Uri($"{serverURL}/welcome.html"));
                    serverHasWelcome = true;
                }
                catch { serverHasWelcome = false; }

                Invoke(new Action(() => 
                {
                    Program.Console.WriteMessage($"[HTTP] serverHasRules: {serverHasRules}");
                    Program.Console.WriteMessage($"[HTTP] serverHasWelcome: {serverHasWelcome}");
                    tsmiMenuBarToolsWelcomeDialog.Enabled = serverHasWelcome;
                    tsmiMenuBarToolsServerRules.Enabled = serverHasRules;

                    if (serverHasWelcome && !Settings.NoWelcomeDialog)
                        tsmiMenuBarToolsWelcomeDialog.PerformClick();
                }));
            }));
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
            tsddbStatusBarStatus.Enabled = status != UserStatus.OFFLINE;
            tsddbStatusBarStatus.Image = User.StatusToBitmap(status);
            tsslStatusBarStatusText.Text = status != UserStatus.OFFLINE ? User.StatusToText(status) : "Not logged in";
            tsddbStatusBarMOTD.Enabled = status != UserStatus.OFFLINE;
            tsddbStatusBarMOTD.Text = status != UserStatus.OFFLINE && 
                !string.IsNullOrWhiteSpace(motd.Trim()) ? motd.Trim() : "(no MOTD set)";

            CurrentUser.Status = status;
            CurrentUser.MOTD = motd;

            if (status == UserStatus.OFFLINE)
            {
                CurrentUser.Name = null;
                CurrentUser.MOTD = null;
            }

            SyncTray();
        }

        internal void OnLogout(bool noSound = false)
        {
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpLogin);
            UpdateQuickActions(false);
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
            tsmiMenuBarFileChangeStatus.Enabled = false;
            tsmiMenuBarFileLogOff.Enabled = false;
            Text = "Pinto! Beta";
            serverHasRules = false;
            serverHasWelcome = false;
            tsmiMenuBarToolsWelcomeDialog.Enabled = serverHasRules;
            tsmiMenuBarToolsServerRules.Enabled = serverHasRules;

            if (!noSound)
                new SoundPlayer(Sounds.LOGOUT).Play();
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
                lCallStatus.Text = $"{Program.FirstLetterToUpper(status.ToString().ToLower())}" +
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

        public static bool CallStatusMeansEnded(CallStatus status)
        {
            return status == CallStatus.ENDED || status == CallStatus.ERROR;
        }

        public void SyncTray()
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
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpConnecting);
            lConnectingStatus.Text = "Connecting...";
            Program.Console.WriteMessage($"[Networking] Signing in as {username} at {ip}:{port}...");

            NetManager = new NetworkManager(this);
            (bool, Exception) connectResult = await NetManager.Connect(ip, port);

            if (!connectResult.Item1)
            {
                Disconnect();
                lConnectingStatus.Text = "";
                Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}: {connectResult.Item2}");
                MsgBox.Show(this, $"Unable to connect to {ip}:{port}:" +
                    $" {connectResult.Item2.Message}", "Connection Error", MsgBoxIconType.ERROR);
            }
            else
            {
                CurrentUser.Name = username;
                lConnectingStatus.Text = "Authenticating...";
                NetManager.Login(username, password);

                if (loginPacketCheckThread != null)
                    loginPacketCheckThread.Abort();

                StartLoginPacketCheckThread(ip, port);
            }
        }

        public async Task ConnectRegister(string ip, int port, string username, string password)
        {
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpConnecting);
            lConnectingStatus.Text = "Connecting...";
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
                lConnectingStatus.Text = "Registering...";
                NetManager.Register(username, password);

                if (loginPacketCheckThread != null)
                    loginPacketCheckThread.Abort();

                StartLoginPacketCheckThread(ip, port);
            }
        }

        private void StartLoginPacketCheckThread(string ip, int port)
        {
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
                bool isBusy = CurrentUser.Status == UserStatus.BUSY;

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
            Settings.Import(Program.SettingsFile);

            OnLogout(true);
            if (File.Exists(".IS_PORTABLE_CHECK"))
                isPortable = true;

            if (Settings.AutoCheckForUpdates && !isPortable)
                await CheckForUpdates(false);
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
            bool wasLoggedIn = NetManager != null && NetManager.NetHandler.LoggedIn;
            OnLogout(true);
            Disconnect();
            InWindowPopupController.Dispose();

            if (loginPacketCheckThread != null)
                loginPacketCheckThread.Abort();

            if (!Settings.NoGracefulExit && wasLoggedIn)
                new Thread(new ThreadStart(() => 
                {
                    new SoundPlayer(Sounds.LOGOUT).PlaySync();
                })).Start();
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
            if (NetManager == null) return;
            Disconnect();
        }

        private void tsmiMenuBarHelpAbout_Click(object sender, EventArgs e) => new AboutForm().Show();

        private void tsmiStatusBarStatusOnline_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            NetManager.ChangeStatus(UserStatus.ONLINE, CurrentUser.MOTD);
        }

        private void tsmiStatusBarStatusAway_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            NetManager.ChangeStatus(UserStatus.AWAY, CurrentUser.MOTD);
        }

        private void tsmiStatusBarStatusBusy_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            InWindowPopupController.CreatePopup("You are now busy" +
                ", this means that you will not receive any non-important popups", true);
            NetManager.ChangeStatus(UserStatus.BUSY, CurrentUser.MOTD);
        }

        private void tsmiStatusBarStatusInvisible_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            MsgBox.Show(this, "If you choose to change your status to invisible," +
                " you will no longer be able to receive/send messages. Are you sure you want to continue?", 
                "Status change confirmation",
                MsgBoxIconType.WARNING, false, true, (MsgBoxButtonType button) =>
            {
                if (button == MsgBoxButtonType.YES)
                    NetManager.ChangeStatus(UserStatus.INVISIBLE, CurrentUser.MOTD);
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
            if (NetManager == null || NetManager.InCall) return;
            if (NetManager.NetHandler.Options.GetValueOrDefault("exp_calls", "0") != "1") return;

            if (dgvContacts.SelectedRows.Count > 0)
            {
                btnStartCall.Enabled = true;
                btnStartCall.Image = Assets.STARTCALL_ENABLED;
            }
            else
            {
                btnStartCall.Enabled = false;
                btnStartCall.Image = Assets.STARTCALL_DISABLED;
            }
        }

        private void btnStartCall_Click(object sender, EventArgs e)
        {
            if (NetManager == null || NetManager.InCall) return;
            NetManager.StartCall(ContactsMgr.GetContactNameFromRow(dgvContacts.SelectedRows[0].Index));
        }

        private void btnEndCall_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            NetManager.EndCall();
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
                            process.StartInfo.Arguments = "upgrade";
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

        private void tContactsOnlineUpdate_Tick(object sender, EventArgs e)
        {
            if (ContactsMgr == null) return;
            int online = ContactsMgr.GetContacts().Count((Contact contact) =>
            {
                return contact.Status != UserStatus.OFFLINE;
            });
            llStartContacts.Text = $"{online} Contacts Online";
        }

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

            startTalking.Click += new EventHandler((object sender2, EventArgs e2) =>
            {
                if (NetManager == null || NetManager.InCall) return;
                string contactName = ContactsMgr.GetContactNameFromRow(e.RowIndex);
                NetManager.StartCall(contactName);
            });

            removeContact.Click += new EventHandler((object sender2, EventArgs e2) =>
            {
                string contactName = ContactsMgr.GetContactNameFromRow(e.RowIndex);
                NetManager.NetHandler.SendRemoveContactPacket(contactName);
            });

            contextMenu.Items.Add(startMessaging);
            contextMenu.Items.Add(startTalking);
            contextMenu.Items.Add(removeContact);
            e.ContextMenuStrip = contextMenu;
        }

        private void tsddbStatusBarMOTD_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            ChangeMOTDForm changeMOTDForm = new ChangeMOTDForm(this);
            changeMOTDForm.ShowDialog(this);
        }

        private void tsmiMenuBarToolsServerRules_Click(object sender, EventArgs e)
        {
            string serverURL = $"http://{NetManager.NetClient.IP}:{NetManager.NetClient.Port + 10}";
            BrowserForm rulesDialog = new BrowserForm();
            rulesDialog.Text = "Pinto! - Server Rules";
            rulesDialog.Show();
            rulesDialog.wbBrowser.Navigate($"{serverURL}/rules.html");
        }

        private void tsmiMenuBarToolsWelcomeDialog_Click(object sender, EventArgs e)
        {
            string serverURL = $"http://{NetManager.NetClient.IP}:{NetManager.NetClient.Port + 10}";
            BrowserForm welcomeDialog = new BrowserForm();
            welcomeDialog.Text = "Pinto! - Welcome";
            welcomeDialog.Show();
            welcomeDialog.wbBrowser.Navigate($"{serverURL}/welcome.html");
        }
    }
}
