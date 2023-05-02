
namespace PintoNS.Forms
{
    partial class OptionsForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Unavailable", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.tvSections = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.header1 = new PintoNS.Controls.Header();
            this.SuspendLayout();
            // 
            // tvSections
            // 
            this.tvSections.Location = new System.Drawing.Point(24, 23);
            this.tvSections.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tvSections.Name = "tvSections";
            treeNode1.Name = "nUnavailableGeneral";
            treeNode1.Text = "General";
            treeNode2.Name = "nUnavailable";
            treeNode2.Text = "Unavailable";
            this.tvSections.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvSections.Size = new System.Drawing.Size(266, 737);
            this.tvSections.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(300, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nothing to see here yet";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(433, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Options will be available in the next release!";
            // 
            // header1
            // 
            this.header1.Content = "Unavailable";
            this.header1.Location = new System.Drawing.Point(306, 23);
            this.header1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(802, 20);
            this.header1.TabIndex = 8;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 815);
            this.Controls.Add(this.header1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvSections);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1138, 825);
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pinto! - Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView tvSections;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.Header header1;
    }
}