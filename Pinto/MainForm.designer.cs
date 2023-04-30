using PintoNS.Controls;

namespace PintoNS
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pQuickActionBar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tcTabs = new System.Windows.Forms.TabControl();
            this.tpLogin = new System.Windows.Forms.TabPage();
            this.llLogin = new System.Windows.Forms.LinkLabel();
            this.tpConnecting = new System.Windows.Forms.TabPage();
            this.lConnectingStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tpContacts = new System.Windows.Forms.TabPage();
            this.dgvContacts = new System.Windows.Forms.DataGridView();
            this.contactStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.contactName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpCall = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.lCallTarget = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ilTabImages = new System.Windows.Forms.ImageList(this.components);
            this.ssStatusBar = new System.Windows.Forms.StatusStrip();
            this.tsddbStatusBarStatus = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiStatusBarStatusOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStatusBarStatusAway = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStatusBarStatusBusy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStatusBarStatusInvisible = new System.Windows.Forms.ToolStripMenuItem();
            this.tsslStatusBarStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMenuBar = new System.Windows.Forms.ToolStrip();
            this.tsddbMenuBarFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiMenuBarFileAddContact = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileRemoveContact = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddbMenuBarTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbMenuBarHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiMenuBarHelpToggleConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnEndCall = new System.Windows.Forms.PictureBox();
            this.btnStartCall = new System.Windows.Forms.PictureBox();
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSearchBox = new PintoNS.Controls.TextBoxWithPlaceholderSupport();
            this.pQuickActionBar.SuspendLayout();
            this.tcTabs.SuspendLayout();
            this.tpLogin.SuspendLayout();
            this.tpConnecting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tpContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).BeginInit();
            this.tpCall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.ssStatusBar.SuspendLayout();
            this.tsMenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEndCall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStartCall)).BeginInit();
            this.cmsTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // pQuickActionBar
            // 
            this.pQuickActionBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pQuickActionBar.Controls.Add(this.label1);
            this.pQuickActionBar.Location = new System.Drawing.Point(0, 24);
            this.pQuickActionBar.Name = "pQuickActionBar";
            this.pQuickActionBar.Size = new System.Drawing.Size(269, 24);
            this.pQuickActionBar.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quick actions coming soon";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tcTabs
            // 
            this.tcTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcTabs.Controls.Add(this.tpLogin);
            this.tcTabs.Controls.Add(this.tpConnecting);
            this.tcTabs.Controls.Add(this.tpContacts);
            this.tcTabs.Controls.Add(this.tpCall);
            this.tcTabs.ImageList = this.ilTabImages;
            this.tcTabs.Location = new System.Drawing.Point(3, 47);
            this.tcTabs.Name = "tcTabs";
            this.tcTabs.SelectedIndex = 0;
            this.tcTabs.Size = new System.Drawing.Size(266, 270);
            this.tcTabs.TabIndex = 2;
            // 
            // tpLogin
            // 
            this.tpLogin.BackColor = System.Drawing.SystemColors.Window;
            this.tpLogin.Controls.Add(this.llLogin);
            this.tpLogin.ImageKey = "HOUSE.png";
            this.tpLogin.Location = new System.Drawing.Point(4, 23);
            this.tpLogin.Name = "tpLogin";
            this.tpLogin.Size = new System.Drawing.Size(258, 243);
            this.tpLogin.TabIndex = 0;
            this.tpLogin.Text = "Login";
            // 
            // llLogin
            // 
            this.llLogin.AutoSize = true;
            this.llLogin.Location = new System.Drawing.Point(14, 11);
            this.llLogin.Name = "llLogin";
            this.llLogin.Size = new System.Drawing.Size(94, 13);
            this.llLogin.TabIndex = 0;
            this.llLogin.TabStop = true;
            this.llLogin.Text = "Click here to log in";
            this.llLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLogin_LinkClicked);
            // 
            // tpConnecting
            // 
            this.tpConnecting.BackColor = System.Drawing.SystemColors.Window;
            this.tpConnecting.Controls.Add(this.lConnectingStatus);
            this.tpConnecting.Controls.Add(this.pictureBox1);
            this.tpConnecting.ImageKey = "LOCK_ENABLED.png";
            this.tpConnecting.Location = new System.Drawing.Point(4, 23);
            this.tpConnecting.Name = "tpConnecting";
            this.tpConnecting.Padding = new System.Windows.Forms.Padding(3);
            this.tpConnecting.Size = new System.Drawing.Size(258, 243);
            this.tpConnecting.TabIndex = 2;
            this.tpConnecting.Text = "Connecting";
            // 
            // lConnectingStatus
            // 
            this.lConnectingStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lConnectingStatus.Location = new System.Drawing.Point(68, 46);
            this.lConnectingStatus.Name = "lConnectingStatus";
            this.lConnectingStatus.Size = new System.Drawing.Size(128, 16);
            this.lConnectingStatus.TabIndex = 1;
            this.lConnectingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::PintoNS.Logo.LOADING;
            this.pictureBox1.Location = new System.Drawing.Point(68, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tpContacts
            // 
            this.tpContacts.BackColor = System.Drawing.SystemColors.Window;
            this.tpContacts.Controls.Add(this.dgvContacts);
            this.tpContacts.ImageKey = "CONTACT.png";
            this.tpContacts.Location = new System.Drawing.Point(4, 23);
            this.tpContacts.Name = "tpContacts";
            this.tpContacts.Padding = new System.Windows.Forms.Padding(3);
            this.tpContacts.Size = new System.Drawing.Size(258, 243);
            this.tpContacts.TabIndex = 1;
            this.tpContacts.Text = "Contacts";
            // 
            // dgvContacts
            // 
            this.dgvContacts.AllowUserToAddRows = false;
            this.dgvContacts.AllowUserToDeleteRows = false;
            this.dgvContacts.AllowUserToResizeColumns = false;
            this.dgvContacts.AllowUserToResizeRows = false;
            this.dgvContacts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvContacts.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvContacts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvContacts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvContacts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContacts.ColumnHeadersVisible = false;
            this.dgvContacts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.contactStatus,
            this.contactName});
            this.dgvContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContacts.Location = new System.Drawing.Point(3, 3);
            this.dgvContacts.MultiSelect = false;
            this.dgvContacts.Name = "dgvContacts";
            this.dgvContacts.ReadOnly = true;
            this.dgvContacts.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvContacts.RowHeadersVisible = false;
            this.dgvContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContacts.Size = new System.Drawing.Size(252, 237);
            this.dgvContacts.TabIndex = 0;
            this.dgvContacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContacts_CellDoubleClick);
            this.dgvContacts.SelectionChanged += new System.EventHandler(this.dgvContacts_SelectionChanged);
            // 
            // contactStatus
            // 
            this.contactStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.contactStatus.FillWeight = 24F;
            this.contactStatus.HeaderText = "Contact Status";
            this.contactStatus.Name = "contactStatus";
            this.contactStatus.ReadOnly = true;
            this.contactStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.contactStatus.Width = 24;
            // 
            // contactName
            // 
            this.contactName.FillWeight = 84F;
            this.contactName.HeaderText = "Contact Name";
            this.contactName.Name = "contactName";
            this.contactName.ReadOnly = true;
            // 
            // tpCall
            // 
            this.tpCall.Controls.Add(this.label2);
            this.tpCall.Controls.Add(this.lCallTarget);
            this.tpCall.Controls.Add(this.pictureBox2);
            this.tpCall.ImageKey = "RESUMECALL.png";
            this.tpCall.Location = new System.Drawing.Point(4, 23);
            this.tpCall.Name = "tpCall";
            this.tpCall.Padding = new System.Windows.Forms.Padding(3);
            this.tpCall.Size = new System.Drawing.Size(258, 243);
            this.tpCall.TabIndex = 3;
            this.tpCall.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Location = new System.Drawing.Point(46, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Call duration 0:00";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lCallTarget
            // 
            this.lCallTarget.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lCallTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCallTarget.Location = new System.Drawing.Point(45, 139);
            this.lCallTarget.Name = "lCallTarget";
            this.lCallTarget.Size = new System.Drawing.Size(176, 23);
            this.lCallTarget.TabIndex = 1;
            this.lCallTarget.Text = "In call with";
            this.lCallTarget.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = global::PintoNS.Logo.LOGO;
            this.pictureBox2.Location = new System.Drawing.Point(83, 24);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 100);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // ilTabImages
            // 
            this.ilTabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTabImages.ImageStream")));
            this.ilTabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTabImages.Images.SetKeyName(0, "HOUSE.png");
            this.ilTabImages.Images.SetKeyName(1, "LOCK_ENABLED.png");
            this.ilTabImages.Images.SetKeyName(2, "CONTACT.png");
            this.ilTabImages.Images.SetKeyName(3, "HISTORY_ENABLED.png");
            this.ilTabImages.Images.SetKeyName(4, "RESUMECALL.png");
            // 
            // ssStatusBar
            // 
            this.ssStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbStatusBarStatus,
            this.tsslStatusBarStatusText});
            this.ssStatusBar.Location = new System.Drawing.Point(0, 394);
            this.ssStatusBar.Name = "ssStatusBar";
            this.ssStatusBar.Size = new System.Drawing.Size(269, 22);
            this.ssStatusBar.TabIndex = 3;
            this.ssStatusBar.Text = "statusStrip1";
            // 
            // tsddbStatusBarStatus
            // 
            this.tsddbStatusBarStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsddbStatusBarStatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStatusBarStatusOnline,
            this.tsmiStatusBarStatusAway,
            this.tsmiStatusBarStatusBusy,
            this.tsmiStatusBarStatusInvisible});
            this.tsddbStatusBarStatus.Enabled = false;
            this.tsddbStatusBarStatus.Image = global::PintoNS.Statuses.OFFLINE;
            this.tsddbStatusBarStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbStatusBarStatus.Name = "tsddbStatusBarStatus";
            this.tsddbStatusBarStatus.ShowDropDownArrow = false;
            this.tsddbStatusBarStatus.Size = new System.Drawing.Size(20, 20);
            this.tsddbStatusBarStatus.Text = "Status";
            // 
            // tsmiStatusBarStatusOnline
            // 
            this.tsmiStatusBarStatusOnline.Image = global::PintoNS.Statuses.ONLINE;
            this.tsmiStatusBarStatusOnline.Name = "tsmiStatusBarStatusOnline";
            this.tsmiStatusBarStatusOnline.Size = new System.Drawing.Size(117, 22);
            this.tsmiStatusBarStatusOnline.Text = "Online";
            this.tsmiStatusBarStatusOnline.Click += new System.EventHandler(this.tsmiStatusBarStatusOnline_Click);
            // 
            // tsmiStatusBarStatusAway
            // 
            this.tsmiStatusBarStatusAway.Image = global::PintoNS.Statuses.AWAY;
            this.tsmiStatusBarStatusAway.Name = "tsmiStatusBarStatusAway";
            this.tsmiStatusBarStatusAway.Size = new System.Drawing.Size(117, 22);
            this.tsmiStatusBarStatusAway.Text = "Away";
            this.tsmiStatusBarStatusAway.Click += new System.EventHandler(this.tsmiStatusBarStatusAway_Click);
            // 
            // tsmiStatusBarStatusBusy
            // 
            this.tsmiStatusBarStatusBusy.Image = global::PintoNS.Statuses.BUSY;
            this.tsmiStatusBarStatusBusy.Name = "tsmiStatusBarStatusBusy";
            this.tsmiStatusBarStatusBusy.Size = new System.Drawing.Size(117, 22);
            this.tsmiStatusBarStatusBusy.Text = "Busy";
            this.tsmiStatusBarStatusBusy.Click += new System.EventHandler(this.tsmiStatusBarStatusBusy_Click);
            // 
            // tsmiStatusBarStatusInvisible
            // 
            this.tsmiStatusBarStatusInvisible.Image = global::PintoNS.Statuses.INVISIBLE;
            this.tsmiStatusBarStatusInvisible.Name = "tsmiStatusBarStatusInvisible";
            this.tsmiStatusBarStatusInvisible.Size = new System.Drawing.Size(117, 22);
            this.tsmiStatusBarStatusInvisible.Text = "Invisible";
            this.tsmiStatusBarStatusInvisible.Click += new System.EventHandler(this.tsmiStatusBarStatusInvisible_Click);
            // 
            // tsslStatusBarStatusText
            // 
            this.tsslStatusBarStatusText.Name = "tsslStatusBarStatusText";
            this.tsslStatusBarStatusText.Size = new System.Drawing.Size(80, 17);
            this.tsslStatusBarStatusText.Text = "Not logged in";
            // 
            // tsMenuBar
            // 
            this.tsMenuBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbMenuBarFile,
            this.tsddbMenuBarTools,
            this.tsddbMenuBarHelp});
            this.tsMenuBar.Location = new System.Drawing.Point(0, 0);
            this.tsMenuBar.Name = "tsMenuBar";
            this.tsMenuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsMenuBar.Size = new System.Drawing.Size(269, 25);
            this.tsMenuBar.TabIndex = 0;
            this.tsMenuBar.Text = "toolStrip1";
            // 
            // tsddbMenuBarFile
            // 
            this.tsddbMenuBarFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuBarFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuBarFileAddContact,
            this.tsmiMenuBarFileRemoveContact,
            this.tsmiMenuBarFileLogOut});
            this.tsddbMenuBarFile.Image = ((System.Drawing.Image)(resources.GetObject("tsddbMenuBarFile.Image")));
            this.tsddbMenuBarFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuBarFile.Name = "tsddbMenuBarFile";
            this.tsddbMenuBarFile.ShowDropDownArrow = false;
            this.tsddbMenuBarFile.Size = new System.Drawing.Size(29, 22);
            this.tsddbMenuBarFile.Text = "File";
            // 
            // tsmiMenuBarFileAddContact
            // 
            this.tsmiMenuBarFileAddContact.Name = "tsmiMenuBarFileAddContact";
            this.tsmiMenuBarFileAddContact.Size = new System.Drawing.Size(160, 22);
            this.tsmiMenuBarFileAddContact.Text = "Add contact";
            this.tsmiMenuBarFileAddContact.Click += new System.EventHandler(this.tsmiMenuBarFileAddContact_Click);
            // 
            // tsmiMenuBarFileRemoveContact
            // 
            this.tsmiMenuBarFileRemoveContact.Name = "tsmiMenuBarFileRemoveContact";
            this.tsmiMenuBarFileRemoveContact.Size = new System.Drawing.Size(160, 22);
            this.tsmiMenuBarFileRemoveContact.Text = "Remove contact";
            this.tsmiMenuBarFileRemoveContact.Click += new System.EventHandler(this.tsmiMenuBarFileRemoveContact_Click);
            // 
            // tsmiMenuBarFileLogOut
            // 
            this.tsmiMenuBarFileLogOut.Name = "tsmiMenuBarFileLogOut";
            this.tsmiMenuBarFileLogOut.Size = new System.Drawing.Size(160, 22);
            this.tsmiMenuBarFileLogOut.Text = "Log out";
            this.tsmiMenuBarFileLogOut.Click += new System.EventHandler(this.tsmiMenuBarFileLogOut_Click);
            // 
            // tsddbMenuBarTools
            // 
            this.tsddbMenuBarTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuBarTools.Image = ((System.Drawing.Image)(resources.GetObject("tsddbMenuBarTools.Image")));
            this.tsddbMenuBarTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuBarTools.Name = "tsddbMenuBarTools";
            this.tsddbMenuBarTools.ShowDropDownArrow = false;
            this.tsddbMenuBarTools.Size = new System.Drawing.Size(38, 22);
            this.tsddbMenuBarTools.Text = "Tools";
            // 
            // tsddbMenuBarHelp
            // 
            this.tsddbMenuBarHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuBarHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuBarHelpToggleConsole,
            this.tsmiMenuBarHelpAbout});
            this.tsddbMenuBarHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsddbMenuBarHelp.Image")));
            this.tsddbMenuBarHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuBarHelp.Name = "tsddbMenuBarHelp";
            this.tsddbMenuBarHelp.ShowDropDownArrow = false;
            this.tsddbMenuBarHelp.Size = new System.Drawing.Size(36, 22);
            this.tsddbMenuBarHelp.Text = "Help";
            // 
            // tsmiMenuBarHelpToggleConsole
            // 
            this.tsmiMenuBarHelpToggleConsole.Name = "tsmiMenuBarHelpToggleConsole";
            this.tsmiMenuBarHelpToggleConsole.Size = new System.Drawing.Size(153, 22);
            this.tsmiMenuBarHelpToggleConsole.Text = "Toggle console";
            this.tsmiMenuBarHelpToggleConsole.Click += new System.EventHandler(this.tsmiMenuBarHelpToggleConsole_Click);
            // 
            // tsmiMenuBarHelpAbout
            // 
            this.tsmiMenuBarHelpAbout.Name = "tsmiMenuBarHelpAbout";
            this.tsmiMenuBarHelpAbout.Size = new System.Drawing.Size(153, 22);
            this.tsmiMenuBarHelpAbout.Text = "About";
            this.tsmiMenuBarHelpAbout.Click += new System.EventHandler(this.tsmiMenuBarHelpAbout_Click);
            // 
            // niTray
            // 
            this.niTray.ContextMenuStrip = this.cmsTray;
            this.niTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.niTray_MouseClick);
            // 
            // btnEndCall
            // 
            this.btnEndCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEndCall.Enabled = false;
            this.btnEndCall.Image = global::PintoNS.Assets.ENDCALL_DISABLED;
            this.btnEndCall.Location = new System.Drawing.Point(164, 351);
            this.btnEndCall.Name = "btnEndCall";
            this.btnEndCall.Size = new System.Drawing.Size(32, 32);
            this.btnEndCall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnEndCall.TabIndex = 6;
            this.btnEndCall.TabStop = false;
            this.btnEndCall.Click += new System.EventHandler(this.btnEndCall_Click);
            // 
            // btnStartCall
            // 
            this.btnStartCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnStartCall.Enabled = false;
            this.btnStartCall.Image = global::PintoNS.Assets.STARTCALL_DISABLED;
            this.btnStartCall.Location = new System.Drawing.Point(68, 351);
            this.btnStartCall.Name = "btnStartCall";
            this.btnStartCall.Size = new System.Drawing.Size(32, 32);
            this.btnStartCall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnStartCall.TabIndex = 5;
            this.btnStartCall.TabStop = false;
            this.btnStartCall.Click += new System.EventHandler(this.btnStartCall_Click);
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayExit});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.Size = new System.Drawing.Size(94, 26);
            // 
            // tsmiTrayExit
            // 
            this.tsmiTrayExit.Name = "tsmiTrayExit";
            this.tsmiTrayExit.Size = new System.Drawing.Size(93, 22);
            this.tsmiTrayExit.Text = "Exit";
            this.tsmiTrayExit.Click += new System.EventHandler(this.tsmiTrayExit_Click);
            // 
            // txtSearchBox
            // 
            this.txtSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtSearchBox.BackColor = System.Drawing.Color.White;
            this.txtSearchBox.Enabled = false;
            this.txtSearchBox.ForeColor = System.Drawing.Color.DimGray;
            this.txtSearchBox.Location = new System.Drawing.Point(3, 315);
            this.txtSearchBox.Name = "txtSearchBox";
            this.txtSearchBox.PlaceholderText = "Type the username you would like to search for";
            this.txtSearchBox.PlaceholderTextForeColor = System.Drawing.Color.DimGray;
            this.txtSearchBox.Size = new System.Drawing.Size(264, 20);
            this.txtSearchBox.TabIndex = 4;
            this.txtSearchBox.Text = "Type the username you would like to search for";
            this.txtSearchBox.TextForeColor = System.Drawing.Color.Black;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 416);
            this.Controls.Add(this.btnEndCall);
            this.Controls.Add(this.btnStartCall);
            this.Controls.Add(this.txtSearchBox);
            this.Controls.Add(this.ssStatusBar);
            this.Controls.Add(this.tcTabs);
            this.Controls.Add(this.pQuickActionBar);
            this.Controls.Add(this.tsMenuBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Pinto!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.pQuickActionBar.ResumeLayout(false);
            this.pQuickActionBar.PerformLayout();
            this.tcTabs.ResumeLayout(false);
            this.tpLogin.ResumeLayout(false);
            this.tpLogin.PerformLayout();
            this.tpConnecting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tpContacts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).EndInit();
            this.tpCall.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ssStatusBar.ResumeLayout(false);
            this.ssStatusBar.PerformLayout();
            this.tsMenuBar.ResumeLayout(false);
            this.tsMenuBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEndCall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStartCall)).EndInit();
            this.cmsTray.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pQuickActionBar;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tcTabs;
        private System.Windows.Forms.TabPage tpLogin;
        private System.Windows.Forms.ImageList ilTabImages;
        private System.Windows.Forms.StatusStrip ssStatusBar;
        private Controls.TextBoxWithPlaceholderSupport txtSearchBox;
        private System.Windows.Forms.PictureBox btnStartCall;
        private System.Windows.Forms.PictureBox btnEndCall;
        private System.Windows.Forms.ToolStripDropDownButton tsddbStatusBarStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusBarStatusText;
        private System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarFile;
        private System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarTools;
        private System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarHelp;
        private System.Windows.Forms.ToolStrip tsMenuBar;
        private System.Windows.Forms.TabPage tpContacts;
        public System.Windows.Forms.DataGridView dgvContacts;
        private System.Windows.Forms.LinkLabel llLogin;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileLogOut;
        private System.Windows.Forms.TabPage tpConnecting;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lConnectingStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuBarHelpAbout;
        private System.Windows.Forms.DataGridViewImageColumn contactStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn contactName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem tsmiStatusBarStatusOnline;
        private System.Windows.Forms.ToolStripMenuItem tsmiStatusBarStatusAway;
        private System.Windows.Forms.ToolStripMenuItem tsmiStatusBarStatusBusy;
        private System.Windows.Forms.ToolStripMenuItem tsmiStatusBarStatusInvisible;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileAddContact;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileRemoveContact;
        private System.Windows.Forms.TabPage tpCall;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lCallTarget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuBarHelpToggleConsole;
        private System.Windows.Forms.NotifyIcon niTray;
        private System.Windows.Forms.ContextMenuStrip cmsTray;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayExit;
    }
}