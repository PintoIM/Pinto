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
            this.label1 = new System.Windows.Forms.Label();
            this.lSize = new System.Windows.Forms.Label();
            this.lID = new System.Windows.Forms.Label();
            this.hbData = new Be.Windows.Forms.HexBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSections)).BeginInit();
            this.scSections.Panel1.SuspendLayout();
            this.scSections.Panel2.SuspendLayout();
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
            this.dgvPackets.Size = new System.Drawing.Size(788, 238);
            this.dgvPackets.TabIndex = 0;
            this.dgvPackets.SelectionChanged += new System.EventHandler(this.dgvPackets_SelectionChanged);
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
            this.scSections.Panel2.Controls.Add(this.label2);
            this.scSections.Panel2.Controls.Add(this.label1);
            this.scSections.Panel2.Controls.Add(this.lSize);
            this.scSections.Panel2.Controls.Add(this.lID);
            this.scSections.Panel2.Controls.Add(this.hbData);
            this.scSections.Size = new System.Drawing.Size(788, 478);
            this.scSections.SplitterDistance = 238;
            this.scSections.SplitterWidth = 3;
            this.scSections.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Decrypted data:";
            // 
            // lSize
            // 
            this.lSize.AutoSize = true;
            this.lSize.Location = new System.Drawing.Point(12, 24);
            this.lSize.Name = "lSize";
            this.lSize.Size = new System.Drawing.Size(36, 13);
            this.lSize.TabIndex = 3;
            this.lSize.Text = "Size: -";
            // 
            // lID
            // 
            this.lID.AutoSize = true;
            this.lID.Location = new System.Drawing.Point(12, 9);
            this.lID.Name = "lID";
            this.lID.Size = new System.Drawing.Size(27, 13);
            this.lID.TabIndex = 2;
            this.lID.Text = "ID: -";
            // 
            // hbData
            // 
            this.hbData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hbData.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hbData.LineInfoVisible = true;
            this.hbData.Location = new System.Drawing.Point(3, 58);
            this.hbData.Name = "hbData";
            this.hbData.ReadOnly = true;
            this.hbData.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hbData.Size = new System.Drawing.Size(782, 178);
            this.hbData.StringViewVisible = true;
            this.hbData.TabIndex = 1;
            this.hbData.VScrollBarVisible = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(372, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(413, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Some of the packets leak sensitive information, you have been warned!";
            // 
            // NetMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 478);
            this.Controls.Add(this.scSections);
            this.Name = "NetMonitorForm";
            this.Text = "Pinto! - Network Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetMonitorForm_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.NetMonitorForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackets)).EndInit();
            this.scSections.Panel1.ResumeLayout(false);
            this.scSections.Panel2.ResumeLayout(false);
            this.scSections.Panel2.PerformLayout();
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
        private Be.Windows.Forms.HexBox hbData;
        private System.Windows.Forms.Label lID;
        private System.Windows.Forms.Label lSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}