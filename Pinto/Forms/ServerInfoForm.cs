using PintoNS.General;
using System;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class ServerInfoForm : Form
    {
        public ServerInfoForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
        }
    }
}
