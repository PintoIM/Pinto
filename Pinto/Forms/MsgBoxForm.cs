using PintoNS.UI;
using System;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class MsgBoxForm : Form
    {
        protected bool invokedAction;
        public Action<MsgBoxButtonType> UserPressedButton = delegate (MsgBoxButtonType button) { };

        public MsgBoxForm()
        {
            InitializeComponent();
        }

        private void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!invokedAction)
                UserPressedButton.Invoke(MsgBoxButtonType.CLOSE);
            invokedAction = true;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (invokedAction) return;
            UserPressedButton.Invoke(MsgBoxButtonType.YES);
            invokedAction = true;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (invokedAction) return;
            UserPressedButton.Invoke(MsgBoxButtonType.OK);
            invokedAction = true;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            if (invokedAction) return;
            UserPressedButton.Invoke(MsgBoxButtonType.NO);
            invokedAction = true;
            Close();
        }
    }
}
