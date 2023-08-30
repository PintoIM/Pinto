using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class FatalErrorForm : Form
    {
        public FatalErrorForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
        }
    }
}
