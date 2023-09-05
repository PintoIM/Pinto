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
    public partial class MoreFontOptionsForm : Form
    {
        private RichTextBox richTextBox;

        public MoreFontOptionsForm(RichTextBox richTextBox)
        {
            InitializeComponent();
            this.richTextBox = richTextBox;
        }

        public new void CenterToParent()
        {
            base.CenterToParent();
        }

        private void btnEnableBold_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText(string.Format("{0}####BY", (char)0xA7));
            Close();
        }

        private void btnEnableItalic_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText(string.Format("{0}####IY", (char)0xA7));
            Close();
        }

        private void btnEnableUnderline_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText(string.Format("{0}####UY", (char)0xA7));
            Close();
        }

        private void btnEnableStrikeout_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText(string.Format("{0}####SY", (char)0xA7));
            Close();
        }

        private void btnDisableBold_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText(string.Format("{0}####BN", (char)0xA7));
            Close();
        }

        private void btnDisableItalic_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText(string.Format("{0}####IN", (char)0xA7));
            Close();
        }

        private void btnDisableUnderline_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText(string.Format("{0}####UN", (char)0xA7));
            Close();
        }

        private void btnDisableStrikeout_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText(string.Format("{0}####SN", (char)0xA7));
            Close();
        }
    }
}
