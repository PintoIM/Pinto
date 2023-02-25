using PintoNS.Localization;
using PintoNS.Networking;
using PintoNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PintoNS.Forms.Notification;
using System.Threading.Tasks;
using PintoNS.Forms;
using PintoNS.General;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Media;

namespace PintoNS
{
    public partial class MainForm : Form
    {
        public readonly LocalizationManager LocalizationMgr = new LocalizationManager();
        public ContactsManager ContactsMgr;
        public InWindowPopupController PopupController;
        public User CurrentAccount;
        public NetworkManager NetManager;
        public List<MessageForm> MessageForms;

        public MainForm()
        {
            InitializeComponent();
            PopupController = new InWindowPopupController(this, 70);
        }

        internal void OnLogin() 
        {
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpContacts);
            dgvContacts.Rows.Clear();
            ContactsMgr = new ContactsManager(this);
            MessageForms = new List<MessageForm>();
            new SoundPlayer(Sounds.LOGIN).Play();
            ContactsMgr.AddContact(new Contact() { ID = 0, Status = UserStatus.ONLINE, Name = "Public Chat" });
        }

        internal void OnLogout(bool noSound = false) 
        {
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpLogin);

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

            if (!noSound)
                new SoundPlayer(Sounds.LOGOUT).Play();
        }

        public async Task Connect(string ip, int port, string username, string password) 
        {
            tcTabs.TabPages.Clear();
            tcTabs.TabPages.Add(tpConnecting);
            lConnectingStatus.Text = "Connecting...";
            await Task.Delay(1000);

            NetManager = new NetworkManager(this);
            (bool, Exception) connectResult = await NetManager.Connect(ip, port);

            if (!connectResult.Item1)
            {
                Disconnect();
                NotificationUtil.ShowNotification(this, $"Unable to connect to {ip}:{port}:" +
                    $" {connectResult.Item2.Message}", "Connection Error", NotificationIconType.ERROR);
            }
            else 
            {
                lConnectingStatus.Text = "Authenticating...";
                await NetManager.Login(username, password);
            }
        }

        public void Disconnect() 
        {
            bool wasLoggedIn = false;
            if (NetManager != null) 
            {
                wasLoggedIn = NetManager.NetHandler.LoggedIn;
                if (NetManager.IsActive)
                    NetManager.Disconnect("User requested disconnect");
            }
            OnLogout(!wasLoggedIn);
            NetManager = null;
            CurrentAccount = null;
        }
        
        public MessageForm GetMessageFormFromReceiverID(int id) 
        {
            foreach (MessageForm msgForm in MessageForms.ToArray()) 
            {
                if (msgForm.Receiver.ID == id)
                    return msgForm;
            }

            MessageForm messageForm = new MessageForm(this, ContactsMgr.GetContact(0));
            MessageForms.Add(messageForm);
            messageForm.Show();

            return messageForm;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OnLogout(true);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void dgvContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int contactID = ContactsMgr.GetContactIDFromRow(e.RowIndex);
            Contact contact = ContactsMgr.GetContact(contactID);

            if (contactID != -1 && contact != null) 
            {
                MessageForm messageForm = GetMessageFormFromReceiverID(contactID);
                messageForm.WindowState = FormWindowState.Normal;
                messageForm.BringToFront();
                messageForm.Focus();
            }
        }

        private void llLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new DialogConnectToServerForm(this).ShowDialog();
        }

        private void tsmiMenuBarFileLogOut_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void tsmiMenuBarHelpAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }
    }
}