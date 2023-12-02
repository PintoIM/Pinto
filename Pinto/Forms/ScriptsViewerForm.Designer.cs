namespace PintoNS.Forms
{
    partial class ScriptsViewerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.scComponents = new System.Windows.Forms.SplitContainer();
            this.dgvScripts = new System.Windows.Forms.DataGridView();
            this.lScriptInfo = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testedpintoversion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.scComponents)).BeginInit();
            this.scComponents.Panel1.SuspendLayout();
            this.scComponents.Panel2.SuspendLayout();
            this.scComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScripts)).BeginInit();
            this.SuspendLayout();
            // 
            // scComponents
            // 
            this.scComponents.BackColor = System.Drawing.Color.Silver;
            this.scComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scComponents.Location = new System.Drawing.Point(0, 0);
            this.scComponents.Name = "scComponents";
            this.scComponents.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scComponents.Panel1
            // 
            this.scComponents.Panel1.Controls.Add(this.dgvScripts);
            // 
            // scComponents.Panel2
            // 
            this.scComponents.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.scComponents.Panel2.Controls.Add(this.lScriptInfo);
            this.scComponents.Size = new System.Drawing.Size(658, 385);
            this.scComponents.SplitterDistance = 219;
            this.scComponents.SplitterWidth = 2;
            this.scComponents.TabIndex = 0;
            // 
            // dgvScripts
            // 
            this.dgvScripts.AllowUserToAddRows = false;
            this.dgvScripts.AllowUserToDeleteRows = false;
            this.dgvScripts.AllowUserToResizeColumns = false;
            this.dgvScripts.AllowUserToResizeRows = false;
            this.dgvScripts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvScripts.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvScripts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvScripts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvScripts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvScripts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScripts.ColumnHeadersVisible = false;
            this.dgvScripts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.author,
            this.version,
            this.testedpintoversion});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvScripts.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvScripts.Location = new System.Drawing.Point(0, 0);
            this.dgvScripts.MultiSelect = false;
            this.dgvScripts.Name = "dgvScripts";
            this.dgvScripts.ReadOnly = true;
            this.dgvScripts.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvScripts.RowHeadersVisible = false;
            this.dgvScripts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScripts.Size = new System.Drawing.Size(658, 219);
            this.dgvScripts.TabIndex = 0;
            this.dgvScripts.SelectionChanged += new System.EventHandler(this.dgvScripts_SelectionChanged);
            // 
            // lScriptInfo
            // 
            this.lScriptInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lScriptInfo.AutoSize = true;
            this.lScriptInfo.Location = new System.Drawing.Point(12, 12);
            this.lScriptInfo.Name = "lScriptInfo";
            this.lScriptInfo.Size = new System.Drawing.Size(92, 13);
            this.lScriptInfo.TabIndex = 0;
            this.lScriptInfo.Text = "No script selected";
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
            this.author.Visible = false;
            // 
            // version
            // 
            this.version.HeaderText = "Version";
            this.version.Name = "version";
            this.version.ReadOnly = true;
            this.version.Visible = false;
            // 
            // testedpintoversion
            // 
            this.testedpintoversion.HeaderText = "Tested Pinto Version";
            this.testedpintoversion.Name = "testedpintoversion";
            this.testedpintoversion.ReadOnly = true;
            this.testedpintoversion.Visible = false;
            // 
            // ScriptsViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(658, 385);
            this.Controls.Add(this.scComponents);
            this.Name = "ScriptsViewerForm";
            this.Text = "Pinto! - Scripts Viewer";
            this.Load += new System.EventHandler(this.ScriptsViewerForm_Load);
            this.scComponents.Panel1.ResumeLayout(false);
            this.scComponents.Panel2.ResumeLayout(false);
            this.scComponents.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scComponents)).EndInit();
            this.scComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScripts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.SplitContainer scComponents;
        public System.Windows.Forms.DataGridView dgvScripts;
        public System.Windows.Forms.Label lScriptInfo;
        public System.Windows.Forms.DataGridViewTextBoxColumn name;
        public System.Windows.Forms.DataGridViewTextBoxColumn author;
        public System.Windows.Forms.DataGridViewTextBoxColumn version;
        public System.Windows.Forms.DataGridViewTextBoxColumn testedpintoversion;
    }
}