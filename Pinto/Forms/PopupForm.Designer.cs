namespace PintoNS.Forms
{
    partial class PopupForm
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
            this.components = new System.ComponentModel.Container();
            this.pNotification = new System.Windows.Forms.Panel();
            this.lSeeContent = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lBody = new System.Windows.Forms.Label();
            this.lTitle = new System.Windows.Forms.Label();
            this.tAnim = new System.Windows.Forms.Timer(this.components);
            this.tSizeCheck = new System.Windows.Forms.Timer(this.components);
            this.pNotification.SuspendLayout();
            this.SuspendLayout();
            // 
            // pNotification
            // 
            this.pNotification.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pNotification.Controls.Add(this.lSeeContent);
            this.pNotification.Controls.Add(this.btnClose);
            this.pNotification.Controls.Add(this.lBody);
            this.pNotification.Controls.Add(this.lTitle);
            this.pNotification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pNotification.Location = new System.Drawing.Point(0, 0);
            this.pNotification.Name = "pNotification";
            this.pNotification.Size = new System.Drawing.Size(200, 177);
            this.pNotification.TabIndex = 0;
            // 
            // lSeeContent
            // 
            this.lSeeContent.AutoSize = true;
            this.lSeeContent.Location = new System.Drawing.Point(61, 155);
            this.lSeeContent.Name = "lSeeContent";
            this.lSeeContent.Size = new System.Drawing.Size(78, 13);
            this.lSeeContent.TabIndex = 3;
            this.lSeeContent.TabStop = true;
            this.lSeeContent.Text = "See all content";
            this.lSeeContent.Visible = false;
            this.lSeeContent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lSeeContent_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(166, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(20, 20);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lBody
            // 
            this.lBody.Location = new System.Drawing.Point(13, 36);
            this.lBody.Name = "lBody";
            this.lBody.Size = new System.Drawing.Size(175, 119);
            this.lBody.TabIndex = 1;
            this.lBody.Text = "Body";
            this.lBody.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lTitle
            // 
            this.lTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.Location = new System.Drawing.Point(10, 7);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(150, 23);
            this.lTitle.TabIndex = 0;
            this.lTitle.Text = "Title";
            // 
            // tAnim
            // 
            this.tAnim.Enabled = true;
            this.tAnim.Tick += new System.EventHandler(this.tAnim_Tick);
            // 
            // tSizeCheck
            // 
            this.tSizeCheck.Enabled = true;
            this.tSizeCheck.Interval = 1000;
            this.tSizeCheck.Tick += new System.EventHandler(this.tSizeCheck_Tick);
            // 
            // PopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 177);
            this.Controls.Add(this.pNotification);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PopupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NotificationForm";
            this.pNotification.ResumeLayout(false);
            this.pNotification.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pNotification;
        public System.Windows.Forms.Label lTitle;
        public System.Windows.Forms.Label lBody;
        public System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer tAnim;
        private System.Windows.Forms.LinkLabel lSeeContent;
        private System.Windows.Forms.Timer tSizeCheck;
    }
}