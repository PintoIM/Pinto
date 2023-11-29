using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class RSAKeyVerifierForm : Form
    {
        private int secondsTillEnabled = 5;
        public Action<VerifierResult> Callback;
        private bool alreadyCalled;

        public enum VerifierResult
        {
            ACCEPT,
            ONLY_ONCE,
            DISCONNECT
        }

        public RSAKeyVerifierForm(string server, string publicKey, bool failed)
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
            label2.Text = label2.Text.Replace("%SERVER%", server);
            label5.Text = label5.Text.Replace("%SERVER%", server);
            rtxtPublicKey.Text = publicKey;
            tcSections.SelectedTab = failed ? tpFailed : tpUnknown;
        }

        private void RSAKeyVerifierForm_Load(object sender, EventArgs e)
        {
            tcSections.Appearance = TabAppearance.FlatButtons;
            tcSections.ItemSize = new Size(0, 1);
            tcSections.SizeMode = TabSizeMode.Fixed;
            tEnableButtons.Start();
        }

        private void tEnableButtons_Tick(object sender, EventArgs e)
        {
            if (secondsTillEnabled <= 0)
            {
                btnAccept.Text = $"Accept";
                btnOnlyOnce.Text = $"Only Once";
                btnAccept.Enabled = true;
                btnOnlyOnce.Enabled = true;
                tEnableButtons.Stop();
                return;
            }

            btnAccept.Text = $"Accept ({secondsTillEnabled})";
            btnOnlyOnce.Text = $"Only Once ({secondsTillEnabled})";
            secondsTillEnabled--;
        }

        private void CallCallback(VerifierResult result)
        {
            if (alreadyCalled) return;
            alreadyCalled = true;
            if (Callback != null) Callback.Invoke(result);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            CallCallback(VerifierResult.ACCEPT);
            Close();
        }

        private void btnOnlyOnce_Click(object sender, EventArgs e)
        {
            CallCallback(VerifierResult.ONLY_ONCE);
            Close();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            CallCallback(VerifierResult.DISCONNECT);
            Close();
        }

        private void RSAKeyVerifierForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CallCallback(VerifierResult.DISCONNECT);
        }
    }
}
