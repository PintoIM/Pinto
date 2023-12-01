using PintoNS.General;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class ConsoleForm : Form
    {
        private const int SM_ITEM_CLEAR = 0x01;

        public ConsoleForm()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            IntPtr sysMenu = PInvoke.GetSystemMenu(Handle, false);
            PInvoke.AppendMenu(sysMenu, PInvoke.MF_SEPARATOR, 0x00, "");
            PInvoke.AppendMenu(sysMenu, PInvoke.MF_STRING, SM_ITEM_CLEAR, "&Clear Console");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == PInvoke.WM_SYSCOMMAND && 
                (int)m.WParam == SM_ITEM_CLEAR) 
            {
                rtxtLog.Clear();
            }
        }

        public void WriteMessage(string msg)
        {
            WriteMessage(msg, true);
        }

        public void WriteMessage(string msg, bool newLine = true)
        {
            Invoke(new Action(() => 
            {
                if (Disposing || IsDisposed) return;

                Invoke(new Action(() =>
                {
                    rtxtLog.SelectionStart = rtxtLog.TextLength;
                    rtxtLog.SelectionLength = 0;
                    rtxtLog.AppendText(msg + (newLine ? Environment.NewLine : ""));
                }));
            }));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string cmd = txtInput.Text.Trim();
            txtInput.Text = "";

            WriteMessage("Commands are currently unavailable!");
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
