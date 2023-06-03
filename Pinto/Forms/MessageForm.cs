using PintoNS.General;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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
            Icon = Program.GetFormIcon();
            this.mainForm = mainForm;
            InWindowPopupController = new InWindowPopupController(this, 25);
            Receiver = receiver;
            Text = $"Pinto! - Instant Messaging - Chatting with {Receiver.Name}";

            if (mainForm.NetManager.NetHandler.ServerID == null) 
            {
                MsgBox.Show(this,
                    "The server has not yet sent it's server ID," +
                    " this chat will not be saved till the server does so", "Server Warning",
                    MsgBoxIconType.WARNING, true);
                return;
            }
            if (!Directory.Exists(Path.Combine(mainForm.DataFolder, 
                "chats", mainForm.CurrentUser.Name, mainForm.NetManager.NetHandler.ServerID)))
                Directory.CreateDirectory(Path.Combine(mainForm.DataFolder,
                    "chats", mainForm.CurrentUser.Name, mainForm.NetManager.NetHandler.ServerID));
            LoadChat();
        }

        private void LoadChat()
        {
            Program.Console.WriteMessage("[General] Loading chat...");
            try
            {
                string filePath = Path.Combine(mainForm.DataFolder, "chats", mainForm.CurrentUser.Name,
                    mainForm.NetManager.NetHandler.ServerID, $"{Receiver.Name}.rtf");
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
                string filePath = Path.Combine(mainForm.DataFolder, "chats", mainForm.CurrentUser.Name,
                    mainForm.NetManager.NetHandler.ServerID, $"{Receiver.Name}.rtf");
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
                string filePath = Path.Combine(mainForm.DataFolder, "chats", mainForm.CurrentUser.Name,
                    mainForm.NetManager.NetHandler.ServerID, $"{Receiver.Name}.rtf");
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

        private void WriteMessageRaw(string msg, Color color) 
        {
            Invoke(new Action(() =>
            {
                rtxtMessages.SelectionStart = rtxtMessages.Text.Length;
                rtxtMessages.SelectionColor = color;
                rtxtMessages.SelectedText = msg;
                rtxtMessages.SelectionColor = rtxtMessages.ForeColor;

                // Save the chat
                SaveChat();
            }));
        }

        public void WriteMessage(string msg, Color color, bool newLine = true)
        {
            string buffer = "";
            Color currentColor = color;

            for (int i = 0; i < msg.Length; ++i)
            {
                switch (msg[i])
                {
                    case '#':
                        if (i + 2 < msg.Length &&
                            msg[i + 1] == '#' &&
                            msg[i + 2] == '#' &&
                            i + 2 + 6 < msg.Length)
                        {
                            WriteMessageRaw(buffer, currentColor);

                            buffer = "";
                            try
                            {
                                currentColor = ColorTranslator.FromHtml(msg.Substring(i + 2, 7));
                            }
                            catch
                            {
                                currentColor = Color.Black;
                            }

                            // 0 (#) + 2 (##) + 6 (RRGGBB)
                            i += 8;
                        }
                        else
                            buffer += msg[i];

                        break;
                    default:
                        buffer += msg[i];
                        break;
                }
            }

            WriteMessageRaw(buffer + (newLine ? Environment.NewLine : ""), currentColor);
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
            int caretPosition = rtxtInput.SelectionStart;
            rtxtInput.Clear();
            rtxtInput.Text = text;
            rtxtInput.SelectionStart = caretPosition;
            rtxtInput.SelectionLength = 0;

            if (mainForm.NetManager == null) return;

            if (!string.IsNullOrWhiteSpace(text) && !isTypingLastStatus)
            {
                isTypingLastStatus = true;
            }
            else if (string.IsNullOrWhiteSpace(text) && isTypingLastStatus) 
            {
                isTypingLastStatus = false;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string input = rtxtInput.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                MsgBox.Show(this, "The specified message is invalid!", "Error", 
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

        private void tsmiMenuBarHelpAbout_Click(object sender, EventArgs e) => new AboutForm().Show();

        private void MessageForm_Activated(object sender, EventArgs e) => HasBeenInactive = false;

        private void btnTalk_Click(object sender, EventArgs e)
        {
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (cdPicker.ShowDialog() != DialogResult.OK) return;
            Color color = cdPicker.Color;
            rtxtInput.SelectionLength = 0;
            rtxtInput.AppendText($"###{color.R:X2}{color.G:X2}{color.B:X2}");
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

        private void rtxtMessages_TextChanged(object sender, EventArgs e)
        {
            rtxtMessages.SelectionStart = rtxtMessages.Text.Length;
            rtxtMessages.ScrollToCaret();
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
        }

        private void rtxtInput_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            // While loop here to bypass a bug when scrolling in with the mouse
            while (rtxtInput.ZoomFactor != 1.0f)
                rtxtInput.ZoomFactor = 1.0f;
        }
    }
}
