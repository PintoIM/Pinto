using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class BrowserForm : Form
    {
        public BrowserForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
        }

        private void wbBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Program.Console.WriteMessage($"[BrowserWindow] Navigating to {e.Url}");
        }
    }
}
