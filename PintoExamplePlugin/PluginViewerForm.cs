using PintoNS;
using PintoNS.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PintoPluginViewerNS
{
    public partial class PluginViewerForm : Form
    {
        private PintoPluginViewer pluginInstance;

        public PluginViewerForm(PintoPluginViewer pluginInstance)
        {
            InitializeComponent();
            this.pluginInstance = pluginInstance;
        }

        private void PluginViewerForm_Load(object sender, EventArgs e)
        {
            foreach (PintoPluginHost plugin in Program.Plugins)
            {
                PintoPluginInfo info = plugin.Plugin.GetInfo();
                dgvPlugins.Rows.Add(info.Name, info.Author, info.Version);
            }
        }
    }
}
