using Be.Windows.Forms;
using Mono.CSharp;
using Newtonsoft.Json.Linq;
using PintoNS.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Program.Console.WriteMessage($"[General] Created NetMonitorForm instance");
        }

        public void AddPacket(IPacket packet, byte[] data, bool received) 
        {
            byte[] dataNoID = data.Skip(4).ToArray();
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dgvPackets, packet.GetType().Name, packet.GetID(), dataNoID.Length,
                    received ? "Server" : "Client", dataNoID);
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

        private void ClearSelection() 
        {
            hbData.ByteProvider = null;
            lID.Text = "ID: -";
            lSize.Text = "Size: -";
        }

        private void dgvPackets_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPackets.SelectedRows.Count < 1) 
            {
                ClearSelection();
                return;
            }
            DataGridViewRow row = dgvPackets.SelectedRows[0];
            byte[] value = (byte[])row.Cells["data"].Value;
            lID.Text = $"ID: {(string)row.Cells["name"].Value} ({(int)row.Cells["id"].Value})";
            lSize.Text = $"Size: {value.Length}";
            hbData.ByteProvider = new DynamicByteProvider(value); 
        }
    }
}
