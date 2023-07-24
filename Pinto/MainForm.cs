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
        public LoginForm LoginFrm { get; private set; }
        public ContactsManager ContactsMgr;
        public InWindowPopupController InWindowPopupController;
        public PopupController PopupController;
        private int connectingStatusTrayFrame = 0;
        private Icon[] connectingStatusTrayFrames = new Icon[] { Statuses.CONNECTING_0, Statuses.CONNECTING_2, 
            Statuses.CONNECTING_4, Statuses.CONNECTING_6, Statuses.CONNECTING_8 };
        public bool AllowClosing;

        public MainForm(LoginForm loginForm)
        {
            InitializeComponent();
            LoginFrm = loginForm;
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
            // Yes, we keep the old data source just in case something tries to access it
            // at the wrong time, this is not what we should do, too bad
            //dgvContacts.DataSource = null;

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

        public async Task ChangeStatus(UserStatus status) 
        {
            Program.Console.WriteMessage($"[General] Changing status to {status}...");

            if (LoginFrm.NetManager != null && status == UserStatus.OFFLINE) 
                LoginFrm.Disconnect();
            else if (LoginFrm.NetManager == null && status != UserStatus.OFFLINE) 
            {
                SwitchToConnectingStatus();
                await LoginFrm.Connect(LoginFrm.AuthIP, LoginFrm.AuthPort, LoginFrm.AuthToken);
                if (LoginFrm.NetManager != null) LoginFrm.NetManager.ChangeStatus(status, LoginFrm.CurrentUser.MOTD);
            }
            else if (status != UserStatus.OFFLINE)
                LoginFrm.NetManager.ChangeStatus(status, LoginFrm.CurrentUser.MOTD);

            if (status == UserStatus.OFFLINE) OnStatusChange(UserStatus.OFFLINE, "");
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            tcTabs.DisplayStyleProvider = new ModernTabControlStyleProvider(tcTabs);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClosing)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                return;
            }
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
        
        private void tsmiMenuBarFileLogOut_Click(object sender, EventArgs e) => LoginFrm.Disconnect(true);

        private void tsmiMenuBarHelpAbout_Click(object sender, EventArgs e) => new AboutForm().Show();

        private async void tsmiUserInfoStatusOnline_Click(object sender, EventArgs e) 
            => await ChangeStatus(UserStatus.ONLINE);

        private async void tsmiUserInfoStatusAway_Click(object sender, EventArgs e) 
            => await ChangeStatus(UserStatus.AWAY);

        private async void tsmiUserInfoStatusBusy_Click(object sender, EventArgs e) 
            => await ChangeStatus(UserStatus.BUSY);
        
        private async void tsmiUserInfoStatusInvisible_Click(object sender, EventArgs e) 
            => await ChangeStatus(UserStatus.INVISIBLE);

        private async void tsmiUserInfoStatusOffline_Click(object sender, EventArgs e)
            => await ChangeStatus(UserStatus.OFFLINE);

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
            Form form = LoginFrm.Visible ? (Form)LoginFrm : (Form)this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.BringToFront();
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
                    if (answer == MsgBoxButtonType.YES) LoginFrm.Shutdown();
                });
        }

        private async void tsmiMenuBarHelpCheckForUpdates_Click(object sender, EventArgs e)
            => await LoginFrm.CheckForUpdates(true);

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