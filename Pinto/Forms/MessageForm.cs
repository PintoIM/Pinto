using PintoNS.Forms.Notification;
using PintoNS.General;
using PintoNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class MessageForm : Form
    {
        private MainForm mainForm;
        public Contact Receiver;
        private bool isTypingLastStatus;

        public MessageForm(MainForm mainForm, Contact receiver)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            Receiver = receiver;
            Text = $"Pinto! - Instant Messaging - Chatting with {Receiver.Name}";
        }

        public void WriteMessage(string msg, Color color, bool newLine = true)
        {
            Invoke(new Action(() =>
            {
                rtxtMessages.SelectionStart = rtxtMessages.TextLength;
                rtxtMessages.SelectionLength = 0;
                rtxtMessages.SelectionColor = color;
                rtxtMessages.AppendText(msg + (newLine ? Environment.NewLine : ""));
                rtxtMessages.SelectionColor = rtxtMessages.ForeColor;
            }));
        }

        private void rtxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
                e.Handled = true;
            }
        }

        private void rtxtMessages_TextChanged(object sender, EventArgs e)
        {
            rtxtMessages.SelectionStart = rtxtMessages.Text.Length;
            rtxtMessages.ScrollToCaret();
        }

        private async void rtxtInput_TextChanged(object sender, EventArgs e)
        {
            if (mainForm.NetManager == null) return;
            string text = rtxtInput.Text;

            if (!string.IsNullOrWhiteSpace(text) && !isTypingLastStatus)
            {
                isTypingLastStatus = true;
                await mainForm.NetManager.NetHandler.SendTypingPacket(true);
            }
            else if (string.IsNullOrWhiteSpace(text) && isTypingLastStatus)
            {
                isTypingLastStatus = false;
                await mainForm.NetManager.NetHandler.SendTypingPacket(false);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string input = rtxtInput.Text
                .Replace("\n", string.Empty)
                .Replace("\r", string.Empty)
                .Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                NotificationUtil.ShowNotification(this, "The specified message is invalid!", "Error", 
                    NotificationIconType.ERROR);
                return;
            }

            rtxtInput.Clear();
            if (mainForm.NetManager != null) 
            {
                await mainForm.NetManager.NetHandler.SendMessagePacket(Receiver.Name, input);
            }
        }

        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.MessageForms.Remove(this);
        }

        private void tsmiMenuBarHelpAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }
    }
}
