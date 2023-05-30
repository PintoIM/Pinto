using PintoNS.Forms;
using PintoNS.Forms.Notification;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS
{
    public partial class MainForm : Form
    {
        public readonly string DataFolder = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), "Pinto!");
        public ContactsManager ContactsMgr;
        public InWindowPopupController InWindowPopupController;
        public PopupController PopupController;
        public NetworkManager NetManager;
        public User CurrentUser = new User();
        public List<MessageForm> MessageForms;
        private bool doNotCancelClose;

        public MainForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
            InWindowPopupController = new InWindowPopupController(this, 70);
            PopupController = new PopupController();
        }

        internal void OnLogin()
        {
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpStart);
            tcTabs.TabPages.Add(tpContacts);
            UpdateQuickActions(true);
            OnStatusChange(UserStatus.ONLINE);

            // Use a DataTable to allow usage of more options than a plain DataGridView
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("contactStatus", typeof(Bitmap));
            dataTable.Columns.Add("contactName", typeof(string));

            dgvContacts.DataSource = dataTable;
            dgvContacts.Columns["contactStatus"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvContacts.Columns["contactStatus"].FillWeight = 24;
            dgvContacts.Columns["contactStatus"].Width = 24;

            MessageForms = new List<MessageForm>();
            txtSearchBox.Enabled = true;
            tsmiMenuBarToolsAddContact.Enabled = true;
            tsmiMenuBarToolsRemoveContact.Enabled = true;
            tsmiMenuBarFileLogOff.Enabled = true;
            Text = $"Pinto! - {CurrentUser.Name}";

            new SoundPlayer(Sounds.LOGIN).Play();
        }

        internal void UpdateQuickActions(bool loggedInState) 
        {
            if (loggedInState) 
            {
                pbQAAddContact.Image = Assets.ADDCONTACT_ENABLED;
                pbQAAddContact.Enabled = true;
            }
            else 
            {
                pbQAAddContact.Image = Assets.ADDCONTACT_DISABLED;
                pbQAAddContact.Enabled = false;
            }
        }

        internal void OnStatusChange(UserStatus status)
        {
            tsddbStatusBarStatus.Enabled = status != UserStatus.OFFLINE;
            tsddbStatusBarStatus.Image = User.StatusToBitmap(status);
            tsslStatusBarStatusText.Text = status != UserStatus.OFFLINE ? User.StatusToText(status) : "Not logged in";
            CurrentUser.Status = status;
            SyncTray();
        }

        internal void OnLogout(bool noSound = false)
        {
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpLogin);
            UpdateQuickActions(false);
            OnStatusChange(UserStatus.OFFLINE);

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

            btnStartCall.Enabled = false;
            btnStartCall.Image = Assets.STARTCALL_DISABLED;
            btnEndCall.Enabled = false;
            btnEndCall.Image = Assets.ENDCALL_DISABLED;

            txtSearchBox.Text = "";
            txtSearchBox.ChangeTextDisplayed();
            txtSearchBox.Enabled = false;
            tsmiMenuBarToolsAddContact.Enabled = false;
            tsmiMenuBarToolsRemoveContact.Enabled = false;
            tsmiMenuBarFileLogOff.Enabled = false;
            Text = "Pinto!";

            if (!noSound)
                new SoundPlayer(Sounds.LOGOUT).Play();
        }

        public void SyncTray()
        {
            niTray.Visible = true;
            niTray.Icon = User.StatusToIcon(CurrentUser.Status);
            niTray.Text = $"Pinto! - " +
                (CurrentUser.Status != UserStatus.OFFLINE ?
                $"{CurrentUser.Name} - {User.StatusToText(CurrentUser.Status)}" : "Not logged in");
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
                MsgBox.ShowNotification(this, $"Unable to connect to {ip}:{port}:" +
                    $" {connectResult.Item2.Message}", "Connection Error", MsgBoxIconType.ERROR);
            }
            else
            {
                CurrentUser.Name = username;
                lConnectingStatus.Text = "Authenticating...";
                NetManager.Login(username, password);
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
                lConnectingStatus.Text = "";
                Program.Console.WriteMessage($"[Networking] Unable to connect to {ip}:{port}: {connectResult.Item2}");
                MsgBox.ShowNotification(this, $"Unable to connect to {ip}:{port}:" +
                    $" {connectResult.Item2.Message}", "Connection Error", MsgBoxIconType.ERROR);
            }
            else
            {
                CurrentUser.Name = username;
                lConnectingStatus.Text = "Registering...";
                NetManager.Register(username, password);
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
            OnLogout(!wasLoggedIn);
            NetManager = null;
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

        private async void MainForm_Load(object sender, EventArgs e)
        {
            Program.Console.WriteMessage("Performing first time initialization...");

            OnLogout(true);
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);
            if (!Directory.Exists(Path.Combine(DataFolder, "chats")))
                Directory.CreateDirectory(Path.Combine(DataFolder, "chats"));

            //await CheckForUpdates();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!doNotCancelClose && e.CloseReason == CloseReason.UserClosing) 
            {
                e.Cancel = true;
                Hide();
                return;
            }
            Program.Console.WriteMessage("Quitting...");
            Disconnect();
        }

        private void dgvContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string contactName = ContactsMgr.GetContactNameFromRow(e.RowIndex);

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
            NetManager.ChangeStatus(UserStatus.ONLINE);
        }

        private void tsmiStatusBarStatusAway_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            NetManager.ChangeStatus(UserStatus.AWAY);
        }

        private void tsmiStatusBarStatusBusy_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            InWindowPopupController.CreatePopup("You are now busy" +
                ", this means that you will not receive any non-important popups");
            NetManager.ChangeStatus(UserStatus.BUSY);
        }

        private void tsmiStatusBarStatusInvisible_Click(object sender, EventArgs e)
        {
            if (NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            MsgBox.ShowPromptNotification(this, "If you choose to change your status to invisible," +
                " your contacts will not be able to send you messages. Are you sure you want to continue?", "Status change confirmation",
                MsgBoxIconType.WARNING, false, (MsgBoxButtonType button) =>
            {
                if (button == MsgBoxButtonType.YES)
                    NetManager.ChangeStatus(UserStatus.INVISIBLE);
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
                MsgBox.ShowNotification(this, "You have not selected any contact!", "Error", MsgBoxIconType.ERROR);
                return;
            }
            string contactName = ContactsMgr.GetContactNameFromRow(dgvContacts.SelectedRows[0].Index);
            NetManager.NetHandler.SendRemoveContactPacket(contactName);
        }

        private void dgvContacts_SelectionChanged(object sender, EventArgs e)
        {
            //if (InCall) return;

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
        }

        private void btnEndCall_Click(object sender, EventArgs e)
        {
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
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.ShowDialog(this);
        }

        private void tsmiMenuBarFileExit_Click(object sender, EventArgs e) 
        {
            MsgBox.ShowPromptNotification(null, "Are you sure you want to close Pinto?" +
                " You will no longer receive messages or calls if you do so.", "Quit Pinto?",
                MsgBoxIconType.QUESTION, false, (MsgBoxButtonType answer) =>
                {
                    if (answer == MsgBoxButtonType.YES) 
                    {
                        doNotCancelClose = true;
                        Close();
                    }
                });
        }

        public async Task CheckForUpdates() 
        {
            if (!await Updater.IsLatest()) 
            {
                MsgBox.ShowPromptNotification(this,
                    "An update is available, do you want to download it and install it?",
                    "Update Available",
                    MsgBoxIconType.QUESTION,
                    true, async (MsgBoxButtonType btn) => 
                    {
                        if (btn == MsgBoxButtonType.YES) 
                        {
                            string path = Path.Combine(DataFolder, "PintoSetup.msi");
                            if (File.Exists(path))
                                File.Delete(path);

                            byte[] file = await Updater.GetUpdateFile();
                            if (file == null) return;
                            File.WriteAllBytes(path, file);
                            Program.Console.WriteMessage($"[Updater] Saved update file at {path}");

                            Program.Console.WriteMessage($"[Updater] Running msi installer at {path}...");
                            Process process = new Process();
                            process.StartInfo.FileName = "msiexec.exe";
                            process.StartInfo.Arguments = " /i PintoSetup.msi /passive";
                            process.StartInfo.WorkingDirectory = DataFolder;
                            process.Start();

                            Program.Console.WriteMessage($"[Updater] Exitting...");
                            Close();
                        }
                    });
            }
        }

        private async void tsmiMenuBarHelpCheckForUpdates_Click(object sender, EventArgs e) => await CheckForUpdates();

        private void txtSearchBox_TextChanged2(object sender, EventArgs e)
        {
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
    }
}