using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PintoSetupNS
{
    public partial class UninstallForm : Form
    {
        public bool CanClose;

        public UninstallForm()
        {
            InitializeComponent();
        }

        private void UninstallForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CanClose) 
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
