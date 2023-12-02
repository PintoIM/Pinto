using PintoNS.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class ScriptsViewerForm : Form
    {
        public ScriptsViewerForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
        }

        private void ScriptsViewerForm_Load(object sender, EventArgs e)
        {
            foreach (IPintoScript script in Program.Scripts) 
            {
                PintoScriptInfo info = script.GetScriptInfo();
                dgvScripts.Rows.Add(info.Name, info.Author, info.Version, info.TestedPintoVersion);
            }
            dgvScripts.ClearSelection();
        }

        private void dgvScripts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvScripts.SelectedRows.Count < 1) 
            {
                lScriptInfo.Text = "No script selected";
                return;
            }

            DataGridViewRow selectedRow = null;

            foreach (DataGridViewRow row in dgvScripts.Rows) 
            {
                if (row.Index == dgvScripts.SelectedRows[0].Index) 
                {
                    selectedRow = row;
                    break;
                }
            }

            if (selectedRow == null)
                return;

            string name = (string) selectedRow.Cells["name"].Value;
            string author = (string) selectedRow.Cells["author"].Value;
            string version = (string) selectedRow.Cells["version"].Value;
            string testedpintoversion = (string) selectedRow.Cells["testedpintoversion"].Value;
            lScriptInfo.Text = $"" +
                $"Name: {name}\n" +
                $"Author: {author}\n" +
                $"Version: {version}\n" +
                $"Tested Pinto Version: {testedpintoversion}";
        }
    }
}
