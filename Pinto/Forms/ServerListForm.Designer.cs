
namespace PintoNS.Forms
{
    partial class ServerListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerListForm));
            this.btnUse = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvServers = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.users = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max_users = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcSections = new System.Windows.Forms.TabControl();
            this.tpLoading = new System.Windows.Forms.TabPage();
            this.lLoading = new System.Windows.Forms.Label();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.tpServers = new System.Windows.Forms.TabPage();
            this.lError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).BeginInit();
            this.tcSections.SuspendLayout();
            this.tpLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.tpServers.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUse
            // 
            this.btnUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUse.Location = new System.Drawing.Point(599, 425);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(75, 23);
            this.btnUse.TabIndex = 0;
            this.btnUse.Text = "Use";
            this.btnUse.UseVisualStyleBackColor = true;
            this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(518, 425);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvServers
            // 
            this.dgvServers.AllowUserToAddRows = false;
            this.dgvServers.AllowUserToDeleteRows = false;
            this.dgvServers.AllowUserToResizeColumns = false;
            this.dgvServers.AllowUserToResizeRows = false;
            this.dgvServers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.ip,
            this.port,
            this.users,
            this.max_users});
            this.dgvServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServers.Location = new System.Drawing.Point(3, 3);
            this.dgvServers.MultiSelect = false;
            this.dgvServers.Name = "dgvServers";
            this.dgvServers.ReadOnly = true;
            this.dgvServers.RowHeadersVisible = false;
            this.dgvServers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServers.ShowEditingIcon = false;
            this.dgvServers.Size = new System.Drawing.Size(648, 375);
            this.dgvServers.TabIndex = 2;
            this.dgvServers.SelectionChanged += new System.EventHandler(this.dgvServers_SelectionChanged);
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // ip
            // 
            this.ip.HeaderText = "IP";
            this.ip.Name = "ip";
            this.ip.ReadOnly = true;
            // 
            // port
            // 
            this.port.HeaderText = "Port";
            this.port.Name = "port";
            this.port.ReadOnly = true;
            // 
            // users
            // 
            this.users.HeaderText = "Users";
            this.users.Name = "users";
            this.users.ReadOnly = true;
            // 
            // max_users
            // 
            this.max_users.HeaderText = "Max Users";
            this.max_users.Name = "max_users";
            this.max_users.ReadOnly = true;
            // 
            // tcSections
            // 
            this.tcSections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcSections.Controls.Add(this.tpLoading);
            this.tcSections.Controls.Add(this.tpServers);
            this.tcSections.Location = new System.Drawing.Point(12, 12);
            this.tcSections.Name = "tcSections";
            this.tcSections.SelectedIndex = 0;
            this.tcSections.Size = new System.Drawing.Size(662, 407);
            this.tcSections.TabIndex = 3;
            // 
            // tpLoading
            // 
            this.tpLoading.Controls.Add(this.lLoading);
            this.tpLoading.Controls.Add(this.pbLoading);
            this.tpLoading.Location = new System.Drawing.Point(4, 22);
            this.tpLoading.Name = "tpLoading";
            this.tpLoading.Size = new System.Drawing.Size(654, 381);
            this.tpLoading.TabIndex = 1;
            this.tpLoading.Text = "Loading";
            this.tpLoading.UseVisualStyleBackColor = true;
            // 
            // lLoading
            // 
            this.lLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lLoading.AutoSize = true;
            this.lLoading.Location = new System.Drawing.Point(314, 260);
            this.lLoading.Name = "lLoading";
            this.lLoading.Size = new System.Drawing.Size(45, 13);
            this.lLoading.TabIndex = 1;
            this.lLoading.Text = "Loading";
            // 
            // pbLoading
            // 
            this.pbLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbLoading.Image = global::PintoNS.Logo.LOADING;
            this.pbLoading.Location = new System.Drawing.Point(267, 107);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(128, 128);
            this.pbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLoading.TabIndex = 0;
            this.pbLoading.TabStop = false;
            // 
            // tpServers
            // 
            this.tpServers.Controls.Add(this.dgvServers);
            this.tpServers.Location = new System.Drawing.Point(4, 22);
            this.tpServers.Name = "tpServers";
            this.tpServers.Padding = new System.Windows.Forms.Padding(3);
            this.tpServers.Size = new System.Drawing.Size(654, 381);
            this.tpServers.TabIndex = 0;
            this.tpServers.Text = "Servers";
            this.tpServers.UseVisualStyleBackColor = true;
            // 
            // lError
            // 
            this.lError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lError.AutoSize = true;
            this.lError.ForeColor = System.Drawing.Color.Red;
            this.lError.Location = new System.Drawing.Point(13, 422);
            this.lError.Name = "lError";
            this.lError.Size = new System.Drawing.Size(54, 13);
            this.lError.TabIndex = 4;
            this.lError.Text = "Error: null ";
            this.lError.Visible = false;
            // 
            // ServerListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 460);
            this.Controls.Add(this.lError);
            this.Controls.Add(this.tcSections);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(291, 323);
            this.Name = "ServerListForm";
            this.Text = "Pinto! - Server List";
            this.Load += new System.EventHandler(this.ServerListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).EndInit();
            this.tcSections.ResumeLayout(false);
            this.tpLoading.ResumeLayout(false);
            this.tpLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.tpServers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUse;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvServers;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn port;
        private System.Windows.Forms.DataGridViewTextBoxColumn users;
        private System.Windows.Forms.DataGridViewTextBoxColumn max_users;
        private System.Windows.Forms.TabControl tcSections;
        private System.Windows.Forms.TabPage tpServers;
        private System.Windows.Forms.TabPage tpLoading;
        private System.Windows.Forms.PictureBox pbLoading;
        private System.Windows.Forms.Label lLoading;
        private System.Windows.Forms.Label lError;
    }
}