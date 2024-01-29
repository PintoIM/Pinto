using PintoNS.UI.Controls;

namespace PintoNS.Forms
{
    partial class MessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
            this.btnSend = new System.Windows.Forms.Button();
            this.cmsInput = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiInputCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInputPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuBar = new System.Windows.Forms.ToolStrip();
            this.tsddbMenuBarFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiMenuFileZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuFileZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuFileZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMenuBarFileClearSavedData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddbMenuBarHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiMenuBarHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslMenuBarMessageTimeout = new System.Windows.Forms.ToolStripLabel();
            this.tspbMenuBarRateLimit = new System.Windows.Forms.ToolStripProgressBar();
            this.ssStatusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslStatusStripTyping = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnBlock = new System.Windows.Forms.Button();
            this.ilButtons = new System.Windows.Forms.ImageList(this.components);
            this.btnTalk = new System.Windows.Forms.Button();
            this.cmsMessages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMessagesCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tRateLimit = new System.Windows.Forms.Timer(this.components);
            this.cdPicker = new System.Windows.Forms.ColorDialog();
            this.fdPicker = new System.Windows.Forms.FontDialog();
            this.scSections = new System.Windows.Forms.SplitContainer();
            this.rtxtMessages = new PintoNS.UI.Controls.ExRichTextBox();
            this.rtxtInput = new PintoNS.UI.Controls.ExRichTextBox();
            this.btnFont = new System.Windows.Forms.Button();
            this.cmsInput.SuspendLayout();
            this.tsMenuBar.SuspendLayout();
            this.ssStatusStrip.SuspendLayout();
            this.cmsMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSections)).BeginInit();
            this.scSections.Panel1.SuspendLayout();
            this.scSections.Panel2.SuspendLayout();
            this.scSections.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(349, 24);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(60, 59);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // cmsInput
            // 
            this.cmsInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInputCopy,
            this.tsmiInputPaste});
            this.cmsInput.Name = "cmsMessages";
            this.cmsInput.ShowImageMargin = false;
            this.cmsInput.Size = new System.Drawing.Size(78, 48);
            // 
            // tsmiInputCopy
            // 
            this.tsmiInputCopy.Name = "tsmiInputCopy";
            this.tsmiInputCopy.Size = new System.Drawing.Size(77, 22);
            this.tsmiInputCopy.Text = "Copy";
            this.tsmiInputCopy.Click += new System.EventHandler(this.tsmiInputCopy_Click);
            // 
            // tsmiInputPaste
            // 
            this.tsmiInputPaste.Name = "tsmiInputPaste";
            this.tsmiInputPaste.Size = new System.Drawing.Size(77, 22);
            this.tsmiInputPaste.Text = "Paste";
            this.tsmiInputPaste.Click += new System.EventHandler(this.tsmiInputPaste_Click);
            // 
            // tsMenuBar
            // 
            this.tsMenuBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbMenuBarFile,
            this.tsddbMenuBarHelp,
            this.toolStripSeparator2,
            this.tslMenuBarMessageTimeout,
            this.tspbMenuBarRateLimit});
            this.tsMenuBar.Location = new System.Drawing.Point(0, 0);
            this.tsMenuBar.Name = "tsMenuBar";
            this.tsMenuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsMenuBar.Size = new System.Drawing.Size(433, 25);
            this.tsMenuBar.TabIndex = 0;
            this.tsMenuBar.Text = "toolStrip1";
            // 
            // tsddbMenuBarFile
            // 
            this.tsddbMenuBarFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuBarFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuFileZoomIn,
            this.tsmiMenuFileZoomOut,
            this.tsmiMenuFileZoomReset,
            this.toolStripSeparator1,
            this.tsmiMenuBarFileClearSavedData});
            this.tsddbMenuBarFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuBarFile.Name = "tsddbMenuBarFile";
            this.tsddbMenuBarFile.ShowDropDownArrow = false;
            this.tsddbMenuBarFile.Size = new System.Drawing.Size(29, 22);
            this.tsddbMenuBarFile.Text = "File";
            this.tsddbMenuBarFile.ToolTipText = "File";
            // 
            // tsmiMenuFileZoomIn
            // 
            this.tsmiMenuFileZoomIn.Name = "tsmiMenuFileZoomIn";
            this.tsmiMenuFileZoomIn.ShortcutKeyDisplayString = "Ctrl + Scroll Up";
            this.tsmiMenuFileZoomIn.Size = new System.Drawing.Size(232, 22);
            this.tsmiMenuFileZoomIn.Text = "Zoom In";
            this.tsmiMenuFileZoomIn.Click += new System.EventHandler(this.tsmiMenuFileZoomIn_Click);
            // 
            // tsmiMenuFileZoomOut
            // 
            this.tsmiMenuFileZoomOut.Name = "tsmiMenuFileZoomOut";
            this.tsmiMenuFileZoomOut.ShortcutKeyDisplayString = "Ctrl + Scroll Down";
            this.tsmiMenuFileZoomOut.Size = new System.Drawing.Size(232, 22);
            this.tsmiMenuFileZoomOut.Text = "Zoom Out";
            this.tsmiMenuFileZoomOut.Click += new System.EventHandler(this.tsmiMenuFileZoomOut_Click);
            // 
            // tsmiMenuFileZoomReset
            // 
            this.tsmiMenuFileZoomReset.Name = "tsmiMenuFileZoomReset";
            this.tsmiMenuFileZoomReset.Size = new System.Drawing.Size(232, 22);
            this.tsmiMenuFileZoomReset.Text = "Zoom Reset";
            this.tsmiMenuFileZoomReset.Click += new System.EventHandler(this.tsmiMenuFileZoomReset_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
            // 
            // tsmiMenuBarFileClearSavedData
            // 
            this.tsmiMenuBarFileClearSavedData.Name = "tsmiMenuBarFileClearSavedData";
            this.tsmiMenuBarFileClearSavedData.Size = new System.Drawing.Size(232, 22);
            this.tsmiMenuBarFileClearSavedData.Text = "Clear Saved Data";
            this.tsmiMenuBarFileClearSavedData.Click += new System.EventHandler(this.tsmiMenuBarFileClearSavedData_Click);
            // 
            // tsddbMenuBarHelp
            // 
            this.tsddbMenuBarHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuBarHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuBarHelpAbout});
            this.tsddbMenuBarHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuBarHelp.Name = "tsddbMenuBarHelp";
            this.tsddbMenuBarHelp.ShowDropDownArrow = false;
            this.tsddbMenuBarHelp.Size = new System.Drawing.Size(36, 22);
            this.tsddbMenuBarHelp.Text = "Help";
            this.tsddbMenuBarHelp.ToolTipText = "Help";
            // 
            // tsmiMenuBarHelpAbout
            // 
            this.tsmiMenuBarHelpAbout.Name = "tsmiMenuBarHelpAbout";
            this.tsmiMenuBarHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.tsmiMenuBarHelpAbout.Text = "About";
            this.tsmiMenuBarHelpAbout.Click += new System.EventHandler(this.tsmiMenuBarHelpAbout_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslMenuBarMessageTimeout
            // 
            this.tslMenuBarMessageTimeout.Name = "tslMenuBarMessageTimeout";
            this.tslMenuBarMessageTimeout.Size = new System.Drawing.Size(103, 22);
            this.tslMenuBarMessageTimeout.Text = "Message Timeout:";
            // 
            // tspbMenuBarRateLimit
            // 
            this.tspbMenuBarRateLimit.Maximum = 6;
            this.tspbMenuBarRateLimit.Name = "tspbMenuBarRateLimit";
            this.tspbMenuBarRateLimit.Size = new System.Drawing.Size(100, 22);
            this.tspbMenuBarRateLimit.Step = 1;
            // 
            // ssStatusStrip
            // 
            this.ssStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatusStripTyping});
            this.ssStatusStrip.Location = new System.Drawing.Point(0, 351);
            this.ssStatusStrip.Name = "ssStatusStrip";
            this.ssStatusStrip.Size = new System.Drawing.Size(433, 22);
            this.ssStatusStrip.TabIndex = 5;
            this.ssStatusStrip.Text = "statusStrip1";
            // 
            // tsslStatusStripTyping
            // 
            this.tsslStatusStripTyping.Name = "tsslStatusStripTyping";
            this.tsslStatusStripTyping.Size = new System.Drawing.Size(61, 17);
            this.tsslStatusStripTyping.Text = "is typing...";
            // 
            // btnColor
            // 
            this.btnColor.FlatAppearance.BorderSize = 0;
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColor.Image = global::PintoNS.Assets.COLOR_PALETTE;
            this.btnColor.Location = new System.Drawing.Point(3, 3);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(18, 18);
            this.btnColor.TabIndex = 6;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnBlock
            // 
            this.btnBlock.BackColor = System.Drawing.Color.Transparent;
            this.btnBlock.Enabled = false;
            this.btnBlock.FlatAppearance.BorderSize = 0;
            this.btnBlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBlock.ImageKey = "BLOCK";
            this.btnBlock.ImageList = this.ilButtons;
            this.btnBlock.Location = new System.Drawing.Point(86, 28);
            this.btnBlock.Name = "btnBlock";
            this.btnBlock.Size = new System.Drawing.Size(68, 41);
            this.btnBlock.TabIndex = 4;
            this.btnBlock.Text = "Block";
            this.btnBlock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBlock.UseVisualStyleBackColor = false;
            this.btnBlock.Click += new System.EventHandler(this.btnBlock_Click);
            // 
            // ilButtons
            // 
            this.ilButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilButtons.ImageStream")));
            this.ilButtons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilButtons.Images.SetKeyName(0, "BLOCK");
            this.ilButtons.Images.SetKeyName(1, "TALK");
            // 
            // btnTalk
            // 
            this.btnTalk.BackColor = System.Drawing.Color.Transparent;
            this.btnTalk.Enabled = false;
            this.btnTalk.FlatAppearance.BorderSize = 0;
            this.btnTalk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTalk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTalk.ImageKey = "TALK";
            this.btnTalk.ImageList = this.ilButtons;
            this.btnTalk.Location = new System.Drawing.Point(12, 28);
            this.btnTalk.Name = "btnTalk";
            this.btnTalk.Size = new System.Drawing.Size(68, 41);
            this.btnTalk.TabIndex = 3;
            this.btnTalk.Text = "Talk";
            this.btnTalk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTalk.UseVisualStyleBackColor = false;
            this.btnTalk.Click += new System.EventHandler(this.btnTalk_Click);
            // 
            // cmsMessages
            // 
            this.cmsMessages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMessagesCopy});
            this.cmsMessages.Name = "cmsMessages";
            this.cmsMessages.ShowImageMargin = false;
            this.cmsMessages.Size = new System.Drawing.Size(78, 26);
            // 
            // tsmiMessagesCopy
            // 
            this.tsmiMessagesCopy.Name = "tsmiMessagesCopy";
            this.tsmiMessagesCopy.Size = new System.Drawing.Size(77, 22);
            this.tsmiMessagesCopy.Text = "Copy";
            this.tsmiMessagesCopy.Click += new System.EventHandler(this.tsmiMessagesCopy_Click);
            // 
            // tRateLimit
            // 
            this.tRateLimit.Enabled = true;
            this.tRateLimit.Tick += new System.EventHandler(this.tRateLimit_Tick);
            // 
            // scSections
            // 
            this.scSections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scSections.BackColor = System.Drawing.Color.DarkGray;
            this.scSections.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scSections.Location = new System.Drawing.Point(12, 69);
            this.scSections.Name = "scSections";
            this.scSections.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scSections.Panel1
            // 
            this.scSections.Panel1.Controls.Add(this.rtxtMessages);
            this.scSections.Panel1MinSize = 95;
            // 
            // scSections.Panel2
            // 
            this.scSections.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.scSections.Panel2.Controls.Add(this.rtxtInput);
            this.scSections.Panel2.Controls.Add(this.btnSend);
            this.scSections.Panel2.Controls.Add(this.btnColor);
            this.scSections.Panel2.Controls.Add(this.btnFont);
            this.scSections.Panel2MinSize = 65;
            this.scSections.Size = new System.Drawing.Size(409, 270);
            this.scSections.SplitterDistance = 189;
            this.scSections.SplitterWidth = 2;
            this.scSections.TabIndex = 8;
            // 
            // rtxtMessages
            // 
            this.rtxtMessages.BackColor = System.Drawing.SystemColors.Window;
            this.rtxtMessages.ContextMenuStrip = this.cmsMessages;
            this.rtxtMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtMessages.HideSelection = false;
            this.rtxtMessages.Location = new System.Drawing.Point(0, 0);
            this.rtxtMessages.Name = "rtxtMessages";
            this.rtxtMessages.ReadOnly = true;
            this.rtxtMessages.Size = new System.Drawing.Size(409, 189);
            this.rtxtMessages.TabIndex = 7;
            this.rtxtMessages.Text = "";
            this.rtxtMessages.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtMessages_LinkClicked);
            this.rtxtMessages.TextChanged += new System.EventHandler(this.rtxtMessages_TextChanged);
            // 
            // rtxtInput
            // 
            this.rtxtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtInput.ContextMenuStrip = this.cmsInput;
            this.rtxtInput.HideSelection = false;
            this.rtxtInput.Location = new System.Drawing.Point(0, 24);
            this.rtxtInput.MaxLength = 512;
            this.rtxtInput.Name = "rtxtInput";
            this.rtxtInput.Size = new System.Drawing.Size(348, 59);
            this.rtxtInput.TabIndex = 0;
            this.rtxtInput.Text = "";
            this.rtxtInput.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtMessages_LinkClicked);
            this.rtxtInput.TextChanged += new System.EventHandler(this.rtxtInput_TextChanged);
            this.rtxtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtxtInput_KeyDown);
            // 
            // btnFont
            // 
            this.btnFont.FlatAppearance.BorderSize = 0;
            this.btnFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFont.Image = global::PintoNS.Assets.PLUS_ENABLED;
            this.btnFont.Location = new System.Drawing.Point(27, 3);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(18, 18);
            this.btnFont.TabIndex = 8;
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 373);
            this.Controls.Add(this.ssStatusStrip);
            this.Controls.Add(this.btnBlock);
            this.Controls.Add(this.btnTalk);
            this.Controls.Add(this.tsMenuBar);
            this.Controls.Add(this.scSections);
            this.MinimumSize = new System.Drawing.Size(187, 239);
            this.Name = "MessageForm";
            this.Activated += new System.EventHandler(this.MessageForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageForm_FormClosing);
            this.Load += new System.EventHandler(this.MessageForm_Load);
            this.cmsInput.ResumeLayout(false);
            this.tsMenuBar.ResumeLayout(false);
            this.tsMenuBar.PerformLayout();
            this.ssStatusStrip.ResumeLayout(false);
            this.ssStatusStrip.PerformLayout();
            this.cmsMessages.ResumeLayout(false);
            this.scSections.Panel1.ResumeLayout(false);
            this.scSections.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scSections)).EndInit();
            this.scSections.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btnSend;
        public ExRichTextBox rtxtInput;
        public System.Windows.Forms.Button btnTalk;
        public System.Windows.Forms.Button btnBlock;
        public System.Windows.Forms.ToolStrip tsMenuBar;
        public System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarHelp;
        public System.Windows.Forms.StatusStrip ssStatusStrip;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarHelpAbout;
        public System.Windows.Forms.Button btnColor;
        public System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarFile;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileClearSavedData;
        public ExRichTextBox rtxtMessages;
        public System.Windows.Forms.ContextMenuStrip cmsMessages;
        public System.Windows.Forms.ToolStripMenuItem tsmiMessagesCopy;
        public System.Windows.Forms.ContextMenuStrip cmsInput;
        public System.Windows.Forms.ToolStripMenuItem tsmiInputCopy;
        public System.Windows.Forms.ToolStripMenuItem tsmiInputPaste;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuFileZoomIn;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuFileZoomOut;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuFileZoomReset;
        public System.Windows.Forms.Timer tRateLimit;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripProgressBar tspbMenuBarRateLimit;
        public System.Windows.Forms.ToolStripStatusLabel tsslStatusStripTyping;
        public System.Windows.Forms.ToolStripLabel tslMenuBarMessageTimeout;
        public System.Windows.Forms.ImageList ilButtons;
        public System.Windows.Forms.ColorDialog cdPicker;
        private System.Windows.Forms.FontDialog fdPicker;
        private System.Windows.Forms.SplitContainer scSections;
        public System.Windows.Forms.Button btnFont;
    }
}