using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
        }
    }
}
