namespace PintoNS.Forms
{
    partial class NetMonitorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvPackets = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scSections = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSections)).BeginInit();
            this.scSections.Panel1.SuspendLayout();
            this.scSections.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPackets
            // 
            this.dgvPackets.AllowUserToAddRows = false;
            this.dgvPackets.AllowUserToDeleteRows = false;
            this.dgvPackets.AllowUserToResizeRows = false;
            this.dgvPackets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPackets.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPackets.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPackets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPackets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.id,
            this.size,
            this.sender,
            this.data});
            this.dgvPackets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPackets.Location = new System.Drawing.Point(0, 0);
            this.dgvPackets.MultiSelect = false;
            this.dgvPackets.Name = "dgvPackets";
            this.dgvPackets.ReadOnly = true;
            this.dgvPackets.RowHeadersVisible = false;
            this.dgvPackets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPackets.Size = new System.Drawing.Size(556, 181);
            this.dgvPackets.TabIndex = 0;
            // 
            // name
            // 
            this.name.HeaderText = "Packet Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // size
            // 
            this.size.HeaderText = "Size";
            this.size.Name = "size";
            this.size.ReadOnly = true;
            // 
            // sender
            // 
            this.sender.HeaderText = "Sender";
            this.sender.Name = "sender";
            this.sender.ReadOnly = true;
            // 
            // data
            // 
            this.data.HeaderText = "Data";
            this.data.Name = "data";
            this.data.ReadOnly = true;
            this.data.Visible = false;
            // 
            // scSections
            // 
            this.scSections.BackColor = System.Drawing.Color.Silver;
            this.scSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSections.Location = new System.Drawing.Point(0, 0);
            this.scSections.Name = "scSections";
            this.scSections.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scSections.Panel1
            // 
            this.scSections.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.scSections.Panel1.Controls.Add(this.dgvPackets);
            // 
            // scSections.Panel2
            // 
            this.scSections.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.scSections.Size = new System.Drawing.Size(556, 362);
            this.scSections.SplitterDistance = 181;
            this.scSections.SplitterWidth = 3;
            this.scSections.TabIndex = 1;
            // 
            // NetMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 362);
            this.Controls.Add(this.scSections);
            this.Name = "NetMonitorForm";
            this.Text = "Pinto! - Network Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetMonitorForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackets)).EndInit();
            this.scSections.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scSections)).EndInit();
            this.scSections.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPackets;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn size;
        private System.Windows.Forms.DataGridViewTextBoxColumn sender;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.SplitContainer scSections;
    }
}