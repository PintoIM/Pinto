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
using System.Diagnostics;

namespace PintoNS.Forms
{
    public partial class MessageForm : Form
    {
        private MainForm mainForm;
        public Contact Receiver;
        private bool isTypingLastStatus;
        public bool HasBeenInactive;

        public MessageForm(MainForm mainForm, Contact receiver)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            Receiver = receiver;
            Text = $"Pinto! - Instant Messaging - Chatting with {Receiver.Name}";
            UpdateColorPicker();
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

        public void WriteRTF(string msg) 
        {
            Invoke(new Action(() => 
            {
                rtxtMessages.SelectionStart = rtxtMessages.TextLength;
                rtxtMessages.SelectionLength = 0;
                rtxtMessages.SelectionColor = Color.Black;
                try
                {
                    rtxtMessages.SelectedRtf = msg;
                }
                catch 
                { 
                    WriteMessage("** IMPROPERLY FORMATED MESSAGE **", Color.Red); 
                }
            }));
        }

        public void UpdateColorPicker() 
        {
            Bitmap b = new Bitmap(16, 16);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(new SolidBrush(rtxtInput.SelectionColor), 0, 0, 16, 16);
            btnColor.Image = b;
        }

        private void rtxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (!System.Windows.Input.Keyboard.Modifiers
                    .HasFlag(System.Windows.Input.ModifierKeys.Control) && 
                    e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void rtxtMessages_TextChanged(object sender, EventArgs e)
        {
            rtxtMessages.SelectionStart = rtxtMessages.Text.Length;
            rtxtMessages.ScrollToCaret();
        }

        private void rtxtInput_TextChanged(object sender, EventArgs e)
        {
            if (mainForm.NetManager == null) return;
            string text = rtxtInput.Text;

            if (!string.IsNullOrWhiteSpace(text) && !isTypingLastStatus)
            {
                isTypingLastStatus = true;
                //mainForm.NetManager.NetHandler.SendTypingPacket(Receiver.Name, true);
            }
            else if (string.IsNullOrWhiteSpace(text) && isTypingLastStatus)
            {
                isTypingLastStatus = false;
                //mainForm.NetManager.NetHandler.SendTypingPacket(Receiver.Name, false);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string input = rtxtInput.Rtf;
            string inputStripped = rtxtInput.Text;

            if (string.IsNullOrWhiteSpace(inputStripped))
            {
                MsgBox.ShowNotification(this, "The specified message is invalid!", "Error", 
                    MsgBoxIconType.ERROR);
                return;
            }

            rtxtInput.Clear();
            if (mainForm.NetManager != null) 
            {
                mainForm.NetManager.NetHandler.SendMessagePacket(Receiver.Name, input);
            }
        }

        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm.MessageForms != null)
                mainForm.MessageForms.Remove(this);
        }

        private void tsmiMenuBarHelpAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(this);
        }

        private void MessageForm_Activated(object sender, EventArgs e)
        {
            HasBeenInactive = false;
        }

        private void rtxtMessages_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void btnTalk_Click(object sender, EventArgs e)
        {
            MsgBox.ShowNotification(this,
                "This option is unavailable in this version!",
                "Option Unavailable",
                MsgBoxIconType.WARNING);
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            MsgBox.ShowNotification(this,
                "This option is unavailable in this version!",
                "Option Unavailable",
                MsgBoxIconType.WARNING);
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            cdPicker.ShowDialog();
            rtxtInput.SelectionColor = cdPicker.Color;
            UpdateColorPicker();
        }

        private void rtxtInput_SelectionChanged(object sender, EventArgs e)
        {
            UpdateColorPicker();
        }
    }
}
