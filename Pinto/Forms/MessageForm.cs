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
using System.IO;

namespace PintoNS.Forms
{
    public partial class MessageForm : Form
    {
        private MainForm mainForm;
        public Contact Receiver;
        private bool isTypingLastStatus;
        public bool HasBeenInactive;
        public InWindowPopupController InWindowPopupController;

        public MessageForm(MainForm mainForm, Contact receiver)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            InWindowPopupController = new InWindowPopupController(this, 25);
            Receiver = receiver;
            Text = $"Pinto! - Instant Messaging - Chatting with {Receiver.Name}";
            LoadChat();
        }

        private void LoadChat()
        {
            Program.Console.WriteMessage("[General] Loading chat...");
            try
            {
                string filePath = Path.Combine(mainForm.DataFolder, "chats",  $"{Receiver.Name}.txt");
                if (!File.Exists(filePath)) return;
                rtxtMessages.Rtf = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[General]" +
                    $" Unable to load the chat: {ex}");
                MsgBox.ShowNotification(this,
                    "Unable to load the chat!",
                    "Error", MsgBoxIconType.ERROR);
            }
        }

        private void SaveChat()
        {
            Program.Console.WriteMessage("[General] Saving chat...");
            try
            {
                string filePath = Path.Combine(mainForm.DataFolder, "chats", $"{Receiver.Name}.txt");
                File.WriteAllText(filePath, rtxtMessages.Rtf);
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[General]" +
                    $" Unable to save the chat: {ex}");
                MsgBox.ShowNotification(this,
                    "Unable to save the chat",
                    "Error", MsgBoxIconType.ERROR);
            }
        }

        private void DeleteChat() 
        {
            Program.Console.WriteMessage("[General] Deleting chat...");
            try
            {
                string filePath = Path.Combine(mainForm.DataFolder, "chats", $"{Receiver.Name}.txt");
                if (!File.Exists(filePath)) return;
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[General]" +
                    $" Unable to delete the chat: {ex}");
                MsgBox.ShowNotification(this,
                    "Unable to delete the chat",
                    "Error", MsgBoxIconType.ERROR);
            }
        }

        public void AppendColorText(string msg, Color baseColor, bool newLine = true)
        {
            string buf = "";
            Color curColor = baseColor;
            for (int i = 0; i < msg.Length; ++i)
            {
                switch (msg[i])
                {
                    case '#':
                        if (i + 1 < msg.Length && msg[i + 1] == '#')
                        {
                            buf += "#";
                            ++i;
                        }
                        else if (i + 6 < msg.Length)
                        {
                            rtxtMessages.SelectionStart = rtxtMessages.TextLength;
                            rtxtMessages.SelectionLength = 0;
                            rtxtMessages.SelectionColor = curColor;
                            rtxtMessages.AppendText(buf);
                            buf = "";

                            //string strColor = msg.Substring(i, 7);
                            curColor = System.Drawing.ColorTranslator.FromHtml(strColor);
                            i += 6;
                        }
                        break;
                    default:
                        buf += msg[i];
                        break;
                }
            }
            rtxtMessages.SelectionStart  = rtxtMessages.TextLength;
            rtxtMessages.SelectionLength = 0;
            rtxtMessages.SelectionColor  = curColor;
            rtxtMessages.AppendText(buf + (newLine ? Environment.NewLine : ""));
        }

        public void WriteMessage(string msg, Color color, bool newLine = true)
        {
            Invoke(new Action(() =>
            {
                AppendColorText(msg, color, newLine);
                /*
                rtxtMessages.SelectionStart = rtxtMessages.TextLength;
                rtxtMessages.SelectionLength = 0;
                rtxtMessages.SelectionColor = color;
                rtxtMessages.AppendText(msg + (newLine ? Environment.NewLine : ""));
                rtxtMessages.SelectionColor = rtxtMessages.ForeColor;
                */
                SaveChat();
            }));
        }

        /*
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
                SaveChat();
            }));
        }*/

        private void rtxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Keyboard.Modifiers.HasFlag(System.Windows
                .Input.ModifierKeys.Control) && e.KeyCode == Keys.Enter)
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
            // Strip RTF
            int caretPosition = rtxtInput.SelectionStart;
            string text = rtxtInput.Text;
            rtxtInput.Clear();
            rtxtInput.Text = text;
            rtxtInput.SelectionStart = caretPosition;

            if (mainForm.NetManager == null) return;

            if (!string.IsNullOrWhiteSpace(text) && !isTypingLastStatus)
                isTypingLastStatus = true;
            else if (string.IsNullOrWhiteSpace(text) && isTypingLastStatus)
                isTypingLastStatus = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string input = rtxtInput.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                MsgBox.ShowNotification(this, "The specified message is invalid!", "Error", 
                    MsgBoxIconType.ERROR);
                return;
            }

            rtxtInput.Clear();
            if (mainForm.NetManager != null) 
                mainForm.NetManager.NetHandler.SendMessagePacket(Receiver.Name, input);
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
            // Appends a color code to the message.
            var color    = cdPicker.Color;
            var strColor = String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
            rtxtInput.AppendText(strColor);
        }

        private void rtxtInput_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void tsmiMenuBarFileClearSavedData_Click(object sender, EventArgs e)
        {
            rtxtMessages.Rtf = null;
            DeleteChat();
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            if (Receiver.Status == UserStatus.BUSY)
                InWindowPopupController.CreatePopup($"{Receiver.Name} is busy" +
                    $" and may not see your messages");
        }
    }
}
