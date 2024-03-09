using PintoNS.Networking.Packets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class NetMonitorForm : Form
    {
        public static NetMonitorForm Instance;
        public bool DoNotCancelClose;
        private List<DataGridViewRow> packetsToAdd = new List<DataGridViewRow>();

        public NetMonitorForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
            Instance = this;
            Program.Console.WriteMessage($"Created NetMonitorForm instance: {this}");
        }

        public void AddPacket(IPacket packet, byte[] data, bool received) 
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dgvPackets, packet.GetType().Name, packet.GetID(), data.Length,
                    received ? "Server" : "Client", data);
            lock (packetsToAdd) packetsToAdd.Add(row);
        }

        public void UpdateMonitor() 
        {
            dgvPackets.Rows.AddRange(packetsToAdd.ToArray());
            packetsToAdd.Clear();
        }

        public void Clear() 
        {
            dgvPackets.Rows.Clear();
        }

        private void NetMonitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DoNotCancelClose) return;
            e.Cancel = true;
            Hide();
        }
    }
}
