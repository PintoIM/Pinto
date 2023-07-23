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
        private bool isPortable;
        public LoginForm LoginFrm { get; private set; }
        public ContactsManager ContactsMgr;
        public InWindowPopupController InWindowPopupController;
        public PopupController PopupController;
        private int connectingStatusTrayFrame = 0;
        private Icon[] connectingStatusTrayFrames = new Icon[] { Statuses.CONNECTING_0, Statuses.CONNECTING_2, 
            Statuses.CONNECTING_4, Statuses.CONNECTING_6, Statuses.CONNECTING_8 };

        public MainForm(LoginForm loginForm)
        {
            InitializeComponent();
            this.LoginFrm = loginForm;
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
            lUserInfoName.Text = LoginFrm.CurrentUser.Name;
            wbPintoNews.DocumentText = HTML.PINTO_NEWS_ERROR;

            tsmiMenuBarToolsAddContact.Enabled = true;
            tsmiMenuBarToolsRemoveContact.Enabled = true;
            tsmiMenuBarFileChangeStatus.Enabled = true;
            tsmiMenuBarFileLogOff.Enabled = true;
            Text = $"Pinto! Beta - {LoginFrm.CurrentUser.Name}";

            new SoundPlayer(Sounds.LOGIN).Play();
        }

        internal void OnStatusChange(UserStatus status, string motd)
        {
            ResetConnectingStatus();
            mbUserInfoStatus.Image = User.StatusToBitmap(status);
            /*
            tsddbStatusBarMOTD.Text = status != UserStatus.OFFLINE && 
                !string.IsNullOrWhiteSpace(motd.Trim()) ? motd.Trim() : "(no MOTD set)";*/

            LoginFrm.CurrentUser.Status = status;
            LoginFrm.CurrentUser.MOTD = motd;

            if (status == UserStatus.OFFLINE)
            {
                LoginFrm.CurrentUser.Name = null;
                LoginFrm.CurrentUser.MOTD = null;
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

        internal void OnLogout(bool noSound = false)
        {
            OnStatusChange(UserStatus.OFFLINE, "");

            // TODO: Add clean-up procedure for messaging

            ContactsMgr = null;
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
        }

        internal void SyncTray()
        {
            niTray.Visible = true;
            niTray.Icon = User.StatusToIcon(LoginFrm.CurrentUser.Status);
            niTray.Text = $"Pinto! Beta - " +
                (LoginFrm.CurrentUser.Status != UserStatus.OFFLINE ?
                $"{LoginFrm.CurrentUser.Name} - {User.StatusToText(LoginFrm.CurrentUser.Status)}" : "Not logged in");
            tsmiTrayChangeStatus.Enabled = LoginFrm.CurrentUser.Status != UserStatus.OFFLINE;
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            tcTabs.DisplayStyleProvider = new ModernTabControlStyleProvider(tcTabs);

            if (File.Exists(".IS_PORTABLE_CHECK"))
                isPortable = true;
            if (Settings.AutoCheckForUpdates && !isPortable)
                await CheckForUpdates(false);
            
            Program.CallExtensionsEvent("OnFormLoad");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void dgvContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Stolen from https://stackoverflow.com/a/50999419
            string contactName = ContactsMgr.GetContactNameFromRow(
                ((DataTable)dgvContacts.DataSource).Rows.IndexOf(
                    ((DataRowView)BindingContext[dgvContacts.DataSource].Current).Row));

            if (contactName != null)
            {
                // TODO: Change to new messaging logic
            }
        }
        
        private void tsmiMenuBarFileLogOut_Click(object sender, EventArgs e)
        {
            if (LoginFrm.NetManager == null) return;
            LoginFrm.Disconnect();
        }

        private void tsmiMenuBarHelpAbout_Click(object sender, EventArgs e) => new AboutForm().Show();

        private void tsmiStatusBarStatusOnline_Click(object sender, EventArgs e)
        {
            if (LoginFrm.NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            SwitchToConnectingStatus();
            LoginFrm.NetManager.ChangeStatus(UserStatus.ONLINE, LoginFrm.CurrentUser.MOTD);
        }

        private void tsmiStatusBarStatusAway_Click(object sender, EventArgs e)
        {
            if (LoginFrm.NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            SwitchToConnectingStatus();
            LoginFrm.NetManager.ChangeStatus(UserStatus.AWAY, LoginFrm.CurrentUser.MOTD);
        }

        private void tsmiStatusBarStatusBusy_Click(object sender, EventArgs e)
        {
            if (LoginFrm.NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            SwitchToConnectingStatus();
            LoginFrm.NetManager.ChangeStatus(UserStatus.BUSY, LoginFrm.CurrentUser.MOTD);
        }

        private void tsmiStatusBarStatusInvisible_Click(object sender, EventArgs e)
        {
            if (LoginFrm.NetManager == null) return;
            Program.Console.WriteMessage("[General] Changing status...");
            MsgBox.Show(this, "If you choose to change your status to invisible," +
                " your contacts will not be able to send you messages. Are you sure you want to continue?", 
                "Status change confirmation",
                MsgBoxIconType.WARNING, false, true, (MsgBoxButtonType button) =>
            {
                if (button == MsgBoxButtonType.YES) 
                {
                    SwitchToConnectingStatus();
                    LoginFrm.NetManager.ChangeStatus(UserStatus.INVISIBLE, LoginFrm.CurrentUser.MOTD);
                }
            });
        }

        private void tsmiMenuBarToolsAddContact_Click(object sender, EventArgs e)
        {
            if (LoginFrm.NetManager == null) return;
            AddContactForm addContactForm = new AddContactForm(this);
            addContactForm.ShowDialog(this);
        }

        private void tsmiMenuBarToolsRemoveContact_Click(object sender, EventArgs e)
        {
            if (LoginFrm.NetManager == null) return;
            if (dgvContacts.SelectedRows.Count < 1)
            {
                MsgBox.Show(this, "You have not selected any contact!", "Error", MsgBoxIconType.ERROR);
                return;
            }
            string contactName = ContactsMgr.GetContactNameFromRow(dgvContacts.SelectedRows[0].Index);
            LoginFrm.NetManager.NetHandler.SendRemoveContactPacket(contactName);
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
            MsgBox.Show(null, "Are you sure you want to close Pinto?" +
                " You will no longer receive messages or calls if you do so.", "Quit Pinto?",
                MsgBoxIconType.QUESTION, false, true, (MsgBoxButtonType answer) =>
                {
                    if (answer == MsgBoxButtonType.YES) Shutdown();
                });
        }

        public void Shutdown() 
        {
            LoginFrm.Close();
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
                            Shutdown();
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
                // TODO: Change to the new logic
            });

            removeContact.Click += new EventHandler((object sender2, EventArgs e2) =>
            {
                string contactName = ContactsMgr.GetContactNameFromRow(e.RowIndex);
                LoginFrm.NetManager.NetHandler.SendRemoveContactPacket(contactName);
            });

            contextMenu.Items.Add(startMessaging);
            contextMenu.Items.Add(removeContact);
            e.ContextMenuStrip = contextMenu;
        }

        private void tsddbStatusBarMOTD_Click(object sender, EventArgs e)
        {
            if (LoginFrm.NetManager == null) return;
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