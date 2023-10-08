using PintoNS;
using PintoNS.General;
using PintoNS.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PintoPluginViewerNS
{
    public class PintoPluginViewer : IPintoPlugin, IPintoPluginListener
    {
        private MainForm mainForm;

        public PintoPluginInfo GetInfo()
        {
            return new PintoPluginInfo()
            {
                Name = "Plugin Viewer",
                Author = "PintoIM",
                Version = "1.0"
            };
        }

        public IPintoPluginListener GetListener()
        {
            return this;
        }

        public bool OnLoad()
        {
            return true;
        }

        public void OnMainFormLoad(MainForm mainForm)
        {
            this.mainForm = mainForm;

            ToolStripMenuItem button = new ToolStripMenuItem();
            button.Text = "View Plugins";
            button.Click += (object sender, EventArgs e) =>
            {
                new PluginViewerForm(this).ShowDialog();
            };

            mainForm.tsddbMenuBarHelp.DropDownItems.Add(new ToolStripSeparator());
            mainForm.tsddbMenuBarHelp.DropDownItems.Add(button);
        }
    }
}
