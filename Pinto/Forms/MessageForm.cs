using PintoNS.Contacts;
using PintoNS.Networking;
using PintoNS.UI;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class MessageForm : Form
    {
        public static Color MsgSelfSenderColor = Color.Blue;
        public static Color MsgOtherSenderColor = Color.Red;
        public static Color MsgSeparatorColor = Color.Black;
        public static Color MsgTimeColor = Color.Gray;
        private MainForm mainForm;
        public Contact Receiver;
        private bool isTypingLastStatus;
        public bool HasBeenInactive;
        public InWindowPopupController InWindowPopupController;
        private int rateLimitTicks;

        public MessageForm(MainForm mainForm, Contact receiver)
        {
            InitializeComponent();
            tsslStatusStripTyping.Text = "";

            Icon = Program.GetFormIcon();
            Text = $"Pinto! - Instant Messaging - Chatting with {receiver.Name}";
            this.mainForm = mainForm;
            Receiver = receiver;
            InWindowPopupController = new InWindowPopupController(this, 25);

            if (mainForm.NetHandler.ServerID == null)
            {
                MsgBox.Show(this,
                    "The server has not yet sent it's server ID," +
                    " this chat will not be saved till the server does so", "Server Warning",
                    MsgBoxIconType.WARNING, true);
                return;
            }

            if (!Directory.Exists(Path.Combine(Program.DataFolder,
                "chats", mainForm.LocalUser.Name, mainForm.NetHandler.ServerID)))
                Directory.CreateDirectory(Path.Combine(Program.DataFolder,
                    "chats", mainForm.LocalUser.Name, mainForm.NetHandler.ServerID));

            LoadChat();
        }

        private void LoadChat()
        {
            Program.Console.WriteMessage("[General] Loading chat...");
            try
            {
                string filePath = Path.Combine(Program.DataFolder, "chats", mainForm.LocalUser.Name,
                    mainForm.NetHandler.ServerID, $"{Receiver.Name.Replace(":", "%3A")}.rtf");
                if (!File.Exists(filePath)) return;
                rtxtMessages.Rtf = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[General]" +
                    $" Unable to load the chat: {ex}");
                MsgBox.Show(this,
                    "Unable to load the chat!",
                    "Error", MsgBoxIconType.ERROR);
            }
        }

        private void SaveChat()
        {
            Program.Console.WriteMessage("[General] Saving chat...");
            try
            {
                string filePath = Path.Combine(Program.DataFolder, "chats", mainForm.LocalUser.Name,
                    mainForm.NetHandler.ServerID, $"{Receiver.Name.Replace(":", "%3A")}.rtf");
                File.WriteAllText(filePath, rtxtMessages.Rtf);
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[General]" +
                    $" Unable to save the chat: {ex}");
                MsgBox.Show(this,
                    "Unable to save the chat",
                    "Error", MsgBoxIconType.ERROR);
            }
        }

        private void DeleteChat()
        {
            Program.Console.WriteMessage("[General] Deleting chat...");
            try
            {
                string filePath = Path.Combine(Program.DataFolder, "chats", mainForm.LocalUser.Name,
                    mainForm.NetHandler.ServerID, $"{Receiver.Name.Replace(":", "%3A")}.rtf");
                if (!File.Exists(filePath)) return;
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[General]" +
                    $" Unable to delete the chat: {ex}");
                MsgBox.Show(this,
                    "Unable to delete the chat",
                    "Error", MsgBoxIconType.ERROR);
            }
        }

        private FontStyle GetFontStyles(bool bold, bool italic, bool strikeout, bool underLine)
        {
            FontStyle style = FontStyle.Regular;
            if (bold) style |= FontStyle.Bold;
            if (italic) style |= FontStyle.Italic;
            if (strikeout) style |= FontStyle.Strikeout;
            if (underLine) style |= FontStyle.Underline;
            return style;
        }

        public void WriteRTF(string rtf)
        {
            Invoke(new Action(() =>
            {
                try
                {
                    rtxtMessages.SelectionStart = rtxtMessages.Text.Length;
                    rtxtMessages.SelectedRtf = rtf;
                }
                catch
                {
                    WriteMessage("(INVALID RTF) ", Color.Red, false);
                    WriteMessage(rtf, Color.Red);
                }
            }));
        }

        public void WriteMessage(string msg, Color color, bool newLine = true, bool bold = false,
            bool italic = false, bool strikeout = false, bool underLine = false)
        {
            Invoke(new Action(() =>
            {
                msg += newLine ? Environment.NewLine : "";

                if (string.IsNullOrWhiteSpace(msg.Trim()))
                {
                    rtxtMessages.AppendText(msg);
                }
                else
                {
                    rtxtMessages.SelectionStart = rtxtMessages.Text.Length;
                    rtxtMessages.SelectionFont = new Font(rtxtMessages.Font,
                        GetFontStyles(bold, italic, strikeout, underLine));
                    rtxtMessages.SelectionColor = color;
                    rtxtMessages.SelectedText = msg;

                    // Reset the richtextbox
                    rtxtMessages.SelectionFont = rtxtMessages.Font;
                    rtxtMessages.SelectionColor = rtxtMessages.ForeColor;
                }
            }));
        }

        public void WriteFeatureMessage(string msg, Color color, bool newLine = true)
        {
            string buffer = "";
            Color currentColor = color;
            bool bold = false;
            bool italic = false;
            bool strikeout = false;
            bool underline = false;

            for (int i = 0; i < msg.Length; ++i)
            {
                switch (msg[i])
                {
                    case (char)0xA7:
                        WriteMessage(buffer, currentColor, false, bold, italic, strikeout, underline);

                        buffer = "";
                        try
                        {
                            string code = msg.Substring(i + 1, 6).Replace("####", "");
                            if (code.Length == 2)
                            {
                                char featureType = code[0];
                                bool featureEnabled = code[1] == 'Y';

                                if (featureType == 'B')
                                    bold = featureEnabled;
                                else if (featureType == 'I')
                                    italic = featureEnabled;
                                else if (featureType == 'S')
                                    strikeout = featureEnabled;
                                else if (featureType == 'U')
                                    underline = featureEnabled;
                            }
                            else
                                currentColor = ColorTranslator.FromHtml("#" + code);
                        }
                        catch
                        {
                            currentColor = Color.Black;
                        }

                        // 0 (0xA7) + 6 (RRGGBB)
                        i += 6;

                        break;
                    default:
                        buffer += msg[i];
                        break;
                }
            }

            WriteMessage(buffer, currentColor, newLine,
                bold, italic, strikeout, underline);
        }

        private void rtxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Modifiers.HasFlag(Keys.Control) &&
                e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void rtxtInput_TextChanged(object sender, EventArgs e)
        {
            // Strip RTF
            string text = rtxtInput.Text;

            if (text.Trim().Length < 1)
                btnSend.Enabled = false;
            else
                btnSend.Enabled = true;

            if (mainForm.NetHandler == null) return;

            if (!Settings.NoTypingIndicator)
            {
                if (!string.IsNullOrWhiteSpace(text) && !isTypingLastStatus)
                {
                    isTypingLastStatus = true;
                    mainForm.NetHandler.ChangeTypingState(Receiver.Name, true);
                }
                else if (string.IsNullOrWhiteSpace(text) && isTypingLastStatus)
                {
                    isTypingLastStatus = false;
                    mainForm.NetHandler.ChangeTypingState(Receiver.Name, false);
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string input = rtxtInput.Rtf;

            if (string.IsNullOrWhiteSpace(rtxtInput.Text))
            {
                return;
            }

            if (rateLimitTicks > 0)
            {
                InWindowPopupController.ClearPopups();
                InWindowPopupController.CreatePopup(
                    "Slow down!\nWait 1.2 seconds before sending another message!", false, 2f);
                return;
            }

            if (rtxtInput.Text.StartsWith("/"))
                input = rtxtInput.Text;

            rtxtInput.Clear();
            if (mainForm.NetHandler != null)
                mainForm.NetHandler.MessageContact(Receiver.Name, new PMSGMessage(input));
            rateLimitTicks = 12;
        }

        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm.MessageForms != null)
                mainForm.MessageForms.Remove(this);
            mainForm.NetHandler.ChangeTypingState(Receiver.Name, false);
            InWindowPopupController.Dispose();
            SaveChat();
        }

        private void tsmiMenuBarHelpAbout_Click(object sender, EventArgs e) => new AboutForm().ShowDialog(this);

        private void MessageForm_Activated(object sender, EventArgs e) => HasBeenInactive = false;

        private void btnTalk_Click(object sender, EventArgs e)
        {
            //if (mainForm.NetHandler == null || mainForm.NetHandler.InCall) return;
            //mainForm.NetHandler.StartCall(Receiver.Name);
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (cdPicker.ShowDialog() != DialogResult.OK) return;
            rtxtInput.SelectionColor = cdPicker.Color;
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            if (fdPicker.ShowDialog() != DialogResult.OK) return;
            rtxtInput.SelectionFont = fdPicker.Font;
        }

        private void tsmiMenuBarFileClearSavedData_Click(object sender, EventArgs e)
        {
            rtxtMessages.Rtf = null;
            DeleteChat();
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            if (Receiver.Status == UserStatus.BUSY || Receiver.Status == UserStatus.AWAY)
                InWindowPopupController.CreatePopup($"{Receiver.Name} is {Receiver.Status.ToString().ToLower()}" +
                    $" and may not see your messages", true);
        }

        private void rtxtMessages_LinkClicked(object sender, LinkClickedEventArgs e) => Process.Start(e.LinkText);

        private void tsmiMessagesCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtxtMessages.SelectedText)) return;
            Clipboard.SetText(rtxtMessages.SelectedText);
        }

        private void tsmiInputCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtxtInput.SelectedText)) return;
            Clipboard.SetText(rtxtInput.SelectedText);
        }

        private void tsmiInputPaste_Click(object sender, EventArgs e)
        {
            rtxtInput.SelectionLength = 0;
            rtxtInput.SelectedText = Clipboard.GetText();
        }

        private void tsmiMenuFileZoomIn_Click(object sender, EventArgs e)
        {
            if (rtxtMessages.ZoomFactor + 0.1f > 5.0f) return;
            rtxtMessages.ZoomFactor += 0.1f;
        }

        private void tsmiMenuFileZoomOut_Click(object sender, EventArgs e)
        {
            if (rtxtMessages.ZoomFactor - 0.1f < 0.1f) return;
            rtxtMessages.ZoomFactor -= 0.1f;
        }

        private void tsmiMenuFileZoomReset_Click(object sender, EventArgs e)
        {
            // While loop here to bypass a bug when scrolling in with the mouse
            while (rtxtMessages.ZoomFactor != 1.0f)
                rtxtMessages.ZoomFactor = 1.0f;

            while (rtxtInput.ZoomFactor != 1.0f)
                rtxtInput.ZoomFactor = 1.0f;
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == PInvoke.WM_SYSCOMMAND &&
                (int)message.WParam == PInvoke.SC_RESTORE)
                Invalidate();

            base.WndProc(ref message);
        }

        private void tRateLimit_Tick(object sender, EventArgs e)
        {
            if (rateLimitTicks > 0)
            {
                rateLimitTicks--;
                tspbMenuBarRateLimit.PerformStep();
                if (rateLimitTicks < 1) tspbMenuBarRateLimit.Value = 0;
            }
        }

        private void rtxtMessages_TextChanged(object sender, EventArgs e)
        {
            rtxtMessages.SelectionStart = rtxtMessages.TextLength;
            rtxtMessages.ScrollToCaret();
        }

        public void SetReceiverTypingState(bool state)
        {
            tsslStatusStripTyping.Text = state ? $"{Receiver.Name} is typing..." : "";
        }
    }
}
