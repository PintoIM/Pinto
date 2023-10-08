namespace PintoPluginViewerNS
{
    partial class PluginViewerForm
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
            this.dgvPlugins = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlugins)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPlugins
            // 
            this.dgvPlugins.AllowUserToAddRows = false;
            this.dgvPlugins.AllowUserToDeleteRows = false;
            this.dgvPlugins.AllowUserToOrderColumns = true;
            this.dgvPlugins.AllowUserToResizeRows = false;
            this.dgvPlugins.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlugins.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPlugins.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPlugins.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvPlugins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlugins.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.author,
            this.version});
            this.dgvPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlugins.GridColor = System.Drawing.Color.Black;
            this.dgvPlugins.Location = new System.Drawing.Point(0, 0);
            this.dgvPlugins.Name = "dgvPlugins";
            this.dgvPlugins.ReadOnly = true;
            this.dgvPlugins.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPlugins.RowHeadersVisible = false;
            this.dgvPlugins.Size = new System.Drawing.Size(622, 325);
            this.dgvPlugins.TabIndex = 0;
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // author
            // 
            this.author.HeaderText = "Author";
            this.author.Name = "author";
            this.author.ReadOnly = true;
            // 
            // version
            // 
            this.version.HeaderText = "Version";
            this.version.Name = "version";
            this.version.ReadOnly = true;
            // 
            // PluginViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 325);
            this.Controls.Add(this.dgvPlugins);
            this.Name = "PluginViewerForm";
            this.ShowIcon = false;
            this.Text = "Plugins";
            this.Load += new System.EventHandler(this.PluginViewerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlugins)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPlugins;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn author;
        private System.Windows.Forms.DataGridViewTextBoxColumn version;
    }
}