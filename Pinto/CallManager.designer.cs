
namespace PintoNS.Forms
{
    partial class CallManager
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
            this.cbMicrophones = new System.Windows.Forms.ComboBox();
            this.cbSpeakers = new System.Windows.Forms.ComboBox();
            this.lMicrophones = new System.Windows.Forms.Label();
            this.lSpeakers = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtRemoteIP = new System.Windows.Forms.TextBox();
            this.lRemoteHost = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.lExternalIP = new System.Windows.Forms.Label();
            this.lLatency = new System.Windows.Forms.Label();
            this.lPacketsPerSecond = new System.Windows.Forms.Label();
            this.lTime = new System.Windows.Forms.Label();
            this.tCall = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // cbMicrophones
            // 
            this.cbMicrophones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMicrophones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMicrophones.FormattingEnabled = true;
            this.cbMicrophones.Location = new System.Drawing.Point(12, 25);
            this.cbMicrophones.Name = "cbMicrophones";
            this.cbMicrophones.Size = new System.Drawing.Size(605, 21);
            this.cbMicrophones.TabIndex = 0;
            // 
            // cbSpeakers
            // 
            this.cbSpeakers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSpeakers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSpeakers.FormattingEnabled = true;
            this.cbSpeakers.Location = new System.Drawing.Point(12, 68);
            this.cbSpeakers.Name = "cbSpeakers";
            this.cbSpeakers.Size = new System.Drawing.Size(605, 21);
            this.cbSpeakers.TabIndex = 1;
            // 
            // lMicrophones
            // 
            this.lMicrophones.AutoSize = true;
            this.lMicrophones.Location = new System.Drawing.Point(9, 9);
            this.lMicrophones.Name = "lMicrophones";
            this.lMicrophones.Size = new System.Drawing.Size(71, 13);
            this.lMicrophones.TabIndex = 2;
            this.lMicrophones.Text = "Microphones:";
            // 
            // lSpeakers
            // 
            this.lSpeakers.AutoSize = true;
            this.lSpeakers.Location = new System.Drawing.Point(9, 52);
            this.lSpeakers.Name = "lSpeakers";
            this.lSpeakers.Size = new System.Drawing.Size(55, 13);
            this.lSpeakers.TabIndex = 3;
            this.lSpeakers.Text = "Speakers:";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(542, 134);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtRemoteIP
            // 
            this.txtRemoteIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemoteIP.Location = new System.Drawing.Point(12, 108);
            this.txtRemoteIP.Name = "txtRemoteIP";
            this.txtRemoteIP.Size = new System.Drawing.Size(605, 20);
            this.txtRemoteIP.TabIndex = 5;
            // 
            // lRemoteHost
            // 
            this.lRemoteHost.AutoSize = true;
            this.lRemoteHost.Location = new System.Drawing.Point(9, 92);
            this.lRemoteHost.Name = "lRemoteHost";
            this.lRemoteHost.Size = new System.Drawing.Size(72, 13);
            this.lRemoteHost.TabIndex = 6;
            this.lRemoteHost.Text = "Remote Host:";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(461, 134);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lExternalIP
            // 
            this.lExternalIP.AutoSize = true;
            this.lExternalIP.Location = new System.Drawing.Point(9, 144);
            this.lExternalIP.Name = "lExternalIP";
            this.lExternalIP.Size = new System.Drawing.Size(97, 13);
            this.lExternalIP.TabIndex = 12;
            this.lExternalIP.Text = "External IP: 0.0.0.0";
            // 
            // lLatency
            // 
            this.lLatency.AutoSize = true;
            this.lLatency.Location = new System.Drawing.Point(160, 144);
            this.lLatency.Name = "lLatency";
            this.lLatency.Size = new System.Drawing.Size(54, 13);
            this.lLatency.TabIndex = 13;
            this.lLatency.Text = "Latency: -";
            // 
            // lPacketsPerSecond
            // 
            this.lPacketsPerSecond.AutoSize = true;
            this.lPacketsPerSecond.Location = new System.Drawing.Point(258, 144);
            this.lPacketsPerSecond.Name = "lPacketsPerSecond";
            this.lPacketsPerSecond.Size = new System.Drawing.Size(146, 13);
            this.lPacketsPerSecond.TabIndex = 14;
            this.lPacketsPerSecond.Text = "Received Packets/Second: -";
            // 
            // lTime
            // 
            this.lTime.AutoSize = true;
            this.lTime.Location = new System.Drawing.Point(9, 131);
            this.lTime.Name = "lTime";
            this.lTime.Size = new System.Drawing.Size(39, 13);
            this.lTime.TabIndex = 15;
            this.lTime.Text = "Time: -";
            // 
            // tCall
            // 
            this.tCall.Interval = 1000;
            this.tCall.Tick += new System.EventHandler(this.tCall_Tick);
            // 
            // CallManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 164);
            this.Controls.Add(this.lTime);
            this.Controls.Add(this.lPacketsPerSecond);
            this.Controls.Add(this.lLatency);
            this.Controls.Add(this.lExternalIP);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lRemoteHost);
            this.Controls.Add(this.txtRemoteIP);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lSpeakers);
            this.Controls.Add(this.lMicrophones);
            this.Controls.Add(this.cbSpeakers);
            this.Controls.Add(this.cbMicrophones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(645, 203);
            this.MinimumSize = new System.Drawing.Size(645, 203);
            this.Name = "CallManager";
            this.Text = "Pinto! - Call Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cbMicrophones;
        public System.Windows.Forms.ComboBox cbSpeakers;
        public System.Windows.Forms.Label lMicrophones;
        public System.Windows.Forms.Label lSpeakers;
        public System.Windows.Forms.Button btnStart;
        public System.Windows.Forms.TextBox txtRemoteIP;
        public System.Windows.Forms.Label lRemoteHost;
        public System.Windows.Forms.Button btnStop;
        public System.Windows.Forms.Label lExternalIP;
        public System.Windows.Forms.Label lLatency;
        public System.Windows.Forms.Label lPacketsPerSecond;
        public System.Windows.Forms.Label lTime;
        public System.Windows.Forms.Timer tCall;
    }
}

