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
            this.pQA = new System.Windows.Forms.Panel();
            this.btnQAAddContact = new PintoNS.Controls.NoFocusQueButton();
            this.tcTabs = new System.Windows.Forms.TabControl();
            this.tpLogin = new System.Windows.Forms.TabPage();
            this.llLogin = new System.Windows.Forms.LinkLabel();
            this.tpConnecting = new System.Windows.Forms.TabPage();
            this.lrConnectingLoader = new PintoNS.Controls.Loader();
            this.lConnectingStatus = new System.Windows.Forms.Label();
            this.tpStart = new System.Windows.Forms.TabPage();
            this.llStartContacts = new System.Windows.Forms.LinkLabel();
            this.pbStartContacts = new System.Windows.Forms.PictureBox();
            this.lStartTitle = new System.Windows.Forms.Label();
            this.tpContacts = new System.Windows.Forms.TabPage();
            this.lContactsNoContacts = new System.Windows.Forms.Label();
            this.dgvContacts = new System.Windows.Forms.DataGridView();
            this.tpCall = new System.Windows.Forms.TabPage();
            this.lCallDuration = new System.Windows.Forms.Label();
            this.lCallTarget = new System.Windows.Forms.Label();
            this.pbCallPicture = new System.Windows.Forms.PictureBox();
            this.ilTabImages = new System.Windows.Forms.ImageList(this.components);
            this.ssStatusBar = new System.Windows.Forms.StatusStrip();
            this.tsddbStatusBarStatus = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiStatusBarStatusOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStatusBarStatusAway = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStatusBarStatusBusy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStatusBarStatusInvisible = new System.Windows.Forms.ToolStripMenuItem();
            this.tsslStatusBarStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsddbStatusBarMOTD = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsMenuBar = new System.Windows.Forms.ToolStrip();
            this.tsddbMenuBarFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiMenuBarFileChangeStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileChangeStatusOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileChangeStatusAway = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileChangeStatusBusy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileChangeStatusInvisible = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMenuBarFileLogOff = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMenuBarFileOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddbMenuBarTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiMenuBarToolsAddContact = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarToolsRemoveContact = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddbMenuBarHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiMenuBarHelpCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMenuBarHelpReportAProblem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMenuBarHelpToggleConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTrayChangeStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayChangeStatusOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayChangeStatusAway = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayChangeStatusBusy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayChangeStatusInvisible = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tContactsOnlineUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnStartCall = new System.Windows.Forms.Button();
            this.btnEndCall = new System.Windows.Forms.Button();
            this.txtSearchBox = new PintoNS.Controls.TextBoxWithPlaceholderSupport();
            this.pQA.SuspendLayout();
            this.tcTabs.SuspendLayout();
            this.tpLogin.SuspendLayout();
            this.tpConnecting.SuspendLayout();
            this.tpStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStartContacts)).BeginInit();
            this.tpContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).BeginInit();
            this.tpCall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCallPicture)).BeginInit();
            this.ssStatusBar.SuspendLayout();
            this.tsMenuBar.SuspendLayout();
            this.cmsTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // pQA
            // 
            this.pQA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pQA.Controls.Add(this.btnQAAddContact);
            this.pQA.Location = new System.Drawing.Point(0, 24);
            this.pQA.Name = "pQA";
            this.pQA.Size = new System.Drawing.Size(269, 24);
            this.pQA.TabIndex = 1;
            // 
            // btnQAAddContact
            // 
            this.btnQAAddContact.BackColor = System.Drawing.Color.Transparent;
            this.btnQAAddContact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQAAddContact.FlatAppearance.BorderSize = 0;
            this.btnQAAddContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQAAddContact.Image = global::PintoNS.Assets.ADDCONTACT_DISABLED;
            this.btnQAAddContact.Location = new System.Drawing.Point(3, 4);
            this.btnQAAddContact.Name = "btnQAAddContact";
            this.btnQAAddContact.Size = new System.Drawing.Size(16, 16);
            this.btnQAAddContact.TabIndex = 9;
            this.btnQAAddContact.UseVisualStyleBackColor = false;
            this.btnQAAddContact.Click += new System.EventHandler(this.tsmiMenuBarToolsAddContact_Click);
            // 
            // tcTabs
            // 
            this.tcTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcTabs.Controls.Add(this.tpLogin);
            this.tcTabs.Controls.Add(this.tpConnecting);
            this.tcTabs.Controls.Add(this.tpStart);
            this.tcTabs.Controls.Add(this.tpContacts);
            this.tcTabs.Controls.Add(this.tpCall);
            this.tcTabs.ImageList = this.ilTabImages;
            this.tcTabs.Location = new System.Drawing.Point(3, 47);
            this.tcTabs.Name = "tcTabs";
            this.tcTabs.SelectedIndex = 0;
            this.tcTabs.Size = new System.Drawing.Size(266, 265);
            this.tcTabs.TabIndex = 2;
            // 
            // tpLogin
            // 
            this.tpLogin.BackColor = System.Drawing.SystemColors.Window;
            this.tpLogin.Controls.Add(this.llLogin);
            this.tpLogin.ImageKey = "HOUSE.png";
            this.tpLogin.Location = new System.Drawing.Point(4, 23);
            this.tpLogin.Name = "tpLogin";
            this.tpLogin.Size = new System.Drawing.Size(258, 238);
            this.tpLogin.TabIndex = 0;
            this.tpLogin.Text = "Login";
            // 
            // llLogin
            // 
            this.llLogin.AutoSize = true;
            this.llLogin.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.tpConnecting.Controls.Add(this.lrConnectingLoader);
            this.tpConnecting.Controls.Add(this.lConnectingStatus);
            this.tpConnecting.ImageKey = "LOCK_ENABLED.png";
            this.tpConnecting.Location = new System.Drawing.Point(4, 23);
            this.tpConnecting.Name = "tpConnecting";
            this.tpConnecting.Padding = new System.Windows.Forms.Padding(3);
            this.tpConnecting.Size = new System.Drawing.Size(258, 238);
            this.tpConnecting.TabIndex = 2;
            this.tpConnecting.Text = "Connecting";
            // 
            // lrConnectingLoader
            // 
            this.lrConnectingLoader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lrConnectingLoader.Location = new System.Drawing.Point(71, 69);
            this.lrConnectingLoader.MaximumSize = new System.Drawing.Size(122, 107);
            this.lrConnectingLoader.MinimumSize = new System.Drawing.Size(122, 107);
            this.lrConnectingLoader.Name = "lrConnectingLoader";
            this.lrConnectingLoader.Size = new System.Drawing.Size(122, 107);
            this.lrConnectingLoader.TabIndex = 2;
            // 
            // lConnectingStatus
            // 
            this.lConnectingStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lConnectingStatus.Location = new System.Drawing.Point(68, 44);
            this.lConnectingStatus.Name = "lConnectingStatus";
            this.lConnectingStatus.Size = new System.Drawing.Size(128, 16);
            this.lConnectingStatus.TabIndex = 1;
            this.lConnectingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpStart
            // 
            this.tpStart.BackColor = System.Drawing.SystemColors.Window;
            this.tpStart.Controls.Add(this.llStartContacts);
            this.tpStart.Controls.Add(this.pbStartContacts);
            this.tpStart.Controls.Add(this.lStartTitle);
            this.tpStart.ImageKey = "HOUSE.png";
            this.tpStart.Location = new System.Drawing.Point(4, 23);
            this.tpStart.Name = "tpStart";
            this.tpStart.Padding = new System.Windows.Forms.Padding(3);
            this.tpStart.Size = new System.Drawing.Size(258, 238);
            this.tpStart.TabIndex = 4;
            this.tpStart.Text = "Start";
            // 
            // llStartContacts
            // 
            this.llStartContacts.AutoSize = true;
            this.llStartContacts.Location = new System.Drawing.Point(43, 37);
            this.llStartContacts.Name = "llStartContacts";
            this.llStartContacts.Size = new System.Drawing.Size(91, 13);
            this.llStartContacts.TabIndex = 3;
            this.llStartContacts.TabStop = true;
            this.llStartContacts.Text = "0 Contacts Online";
            this.llStartContacts.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llStartContacts_LinkClicked);
            // 
            // pbStartContacts
            // 
            this.pbStartContacts.Image = global::PintoNS.Assets.CONTACT_BIG;
            this.pbStartContacts.Location = new System.Drawing.Point(14, 32);
            this.pbStartContacts.Name = "pbStartContacts";
            this.pbStartContacts.Size = new System.Drawing.Size(24, 24);
            this.pbStartContacts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbStartContacts.TabIndex = 2;
            this.pbStartContacts.TabStop = false;
            // 
            // lStartTitle
            // 
            this.lStartTitle.AutoSize = true;
            this.lStartTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lStartTitle.Location = new System.Drawing.Point(10, 9);
            this.lStartTitle.Name = "lStartTitle";
            this.lStartTitle.Size = new System.Drawing.Size(76, 20);
            this.lStartTitle.TabIndex = 0;
            this.lStartTitle.Text = "You have";
            // 
            // tpContacts
            // 
            this.tpContacts.BackColor = System.Drawing.SystemColors.Window;
            this.tpContacts.Controls.Add(this.lContactsNoContacts);
            this.tpContacts.Controls.Add(this.dgvContacts);
            this.tpContacts.ImageKey = "CONTACT.png";
            this.tpContacts.Location = new System.Drawing.Point(4, 23);
            this.tpContacts.Name = "tpContacts";
            this.tpContacts.Padding = new System.Windows.Forms.Padding(3);
            this.tpContacts.Size = new System.Drawing.Size(258, 238);
            this.tpContacts.TabIndex = 1;
            this.tpContacts.Text = "Contacts";
            // 
            // lContactsNoContacts
            // 
            this.lContactsNoContacts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lContactsNoContacts.AutoSize = true;
            this.lContactsNoContacts.Location = new System.Drawing.Point(68, 14);
            this.lContactsNoContacts.Name = "lContactsNoContacts";
            this.lContactsNoContacts.Size = new System.Drawing.Size(112, 13);
            this.lContactsNoContacts.TabIndex = 1;
            this.lContactsNoContacts.Text = "You have no contacts";
            this.lContactsNoContacts.Visible = false;
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
            this.dgvContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContacts.Location = new System.Drawing.Point(3, 3);
            this.dgvContacts.MultiSelect = false;
            this.dgvContacts.Name = "dgvContacts";
            this.dgvContacts.ReadOnly = true;
            this.dgvContacts.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvContacts.RowHeadersVisible = false;
            this.dgvContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContacts.Size = new System.Drawing.Size(252, 232);
            this.dgvContacts.TabIndex = 0;
            this.dgvContacts.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvContacts_CellContextMenuStripNeeded);
            this.dgvContacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContacts_CellDoubleClick);
            this.dgvContacts.SelectionChanged += new System.EventHandler(this.dgvContacts_SelectionChanged);
            // 
            // tpCall
            // 
            this.tpCall.BackColor = System.Drawing.SystemColors.Window;
            this.tpCall.Controls.Add(this.lCallDuration);
            this.tpCall.Controls.Add(this.lCallTarget);
            this.tpCall.Controls.Add(this.pbCallPicture);
            this.tpCall.ImageKey = "RESUMECALL.png";
            this.tpCall.Location = new System.Drawing.Point(4, 23);
            this.tpCall.Name = "tpCall";
            this.tpCall.Padding = new System.Windows.Forms.Padding(3);
            this.tpCall.Size = new System.Drawing.Size(258, 238);
            this.tpCall.TabIndex = 3;
            // 
            // lCallDuration
            // 
            this.lCallDuration.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lCallDuration.Location = new System.Drawing.Point(46, 164);
            this.lCallDuration.Name = "lCallDuration";
            this.lCallDuration.Size = new System.Drawing.Size(175, 23);
            this.lCallDuration.TabIndex = 2;
            this.lCallDuration.Text = "Call duration 0:00";
            this.lCallDuration.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lCallTarget
            // 
            this.lCallTarget.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lCallTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCallTarget.Location = new System.Drawing.Point(45, 137);
            this.lCallTarget.Name = "lCallTarget";
            this.lCallTarget.Size = new System.Drawing.Size(176, 23);
            this.lCallTarget.TabIndex = 1;
            this.lCallTarget.Text = "In call with";
            this.lCallTarget.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbCallPicture
            // 
            this.pbCallPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbCallPicture.Image = global::PintoNS.Logo.LOGO;
            this.pbCallPicture.Location = new System.Drawing.Point(83, 22);
            this.pbCallPicture.Name = "pbCallPicture";
            this.pbCallPicture.Size = new System.Drawing.Size(100, 100);
            this.pbCallPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCallPicture.TabIndex = 0;
            this.pbCallPicture.TabStop = false;
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
            this.tsslStatusBarStatusText,
            this.tsddbStatusBarMOTD});
            this.ssStatusBar.Location = new System.Drawing.Point(0, 389);
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
            // tsddbStatusBarMOTD
            // 
            this.tsddbStatusBarMOTD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbStatusBarMOTD.Enabled = false;
            this.tsddbStatusBarMOTD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbStatusBarMOTD.Name = "tsddbStatusBarMOTD";
            this.tsddbStatusBarMOTD.ShowDropDownArrow = false;
            this.tsddbStatusBarMOTD.Size = new System.Drawing.Size(87, 20);
            this.tsddbStatusBarMOTD.Text = "(no MOTD set)";
            this.tsddbStatusBarMOTD.Click += new System.EventHandler(this.tsddbStatusBarMOTD_Click);
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
            this.tsmiMenuBarFileChangeStatus,
            this.toolStripSeparator3,
            this.tsmiMenuBarFileLogOff,
            this.toolStripSeparator1,
            this.tsmiMenuBarFileOptions,
            this.tsmiMenuBarFileExit});
            this.tsddbMenuBarFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuBarFile.Name = "tsddbMenuBarFile";
            this.tsddbMenuBarFile.ShowDropDownArrow = false;
            this.tsddbMenuBarFile.Size = new System.Drawing.Size(29, 22);
            this.tsddbMenuBarFile.Text = "File";
            // 
            // tsmiMenuBarFileChangeStatus
            // 
            this.tsmiMenuBarFileChangeStatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuBarFileChangeStatusOnline,
            this.tsmiMenuBarFileChangeStatusAway,
            this.tsmiMenuBarFileChangeStatusBusy,
            this.tsmiMenuBarFileChangeStatusInvisible});
            this.tsmiMenuBarFileChangeStatus.Name = "tsmiMenuBarFileChangeStatus";
            this.tsmiMenuBarFileChangeStatus.Size = new System.Drawing.Size(150, 22);
            this.tsmiMenuBarFileChangeStatus.Text = "Change Status";
            // 
            // tsmiMenuBarFileChangeStatusOnline
            // 
            this.tsmiMenuBarFileChangeStatusOnline.Image = global::PintoNS.Statuses.ONLINE;
            this.tsmiMenuBarFileChangeStatusOnline.Name = "tsmiMenuBarFileChangeStatusOnline";
            this.tsmiMenuBarFileChangeStatusOnline.Size = new System.Drawing.Size(117, 22);
            this.tsmiMenuBarFileChangeStatusOnline.Text = "Online";
            this.tsmiMenuBarFileChangeStatusOnline.Click += new System.EventHandler(this.tsmiStatusBarStatusOnline_Click);
            // 
            // tsmiMenuBarFileChangeStatusAway
            // 
            this.tsmiMenuBarFileChangeStatusAway.Image = global::PintoNS.Statuses.AWAY;
            this.tsmiMenuBarFileChangeStatusAway.Name = "tsmiMenuBarFileChangeStatusAway";
            this.tsmiMenuBarFileChangeStatusAway.Size = new System.Drawing.Size(117, 22);
            this.tsmiMenuBarFileChangeStatusAway.Text = "Away";
            this.tsmiMenuBarFileChangeStatusAway.Click += new System.EventHandler(this.tsmiStatusBarStatusAway_Click);
            // 
            // tsmiMenuBarFileChangeStatusBusy
            // 
            this.tsmiMenuBarFileChangeStatusBusy.Image = global::PintoNS.Statuses.BUSY;
            this.tsmiMenuBarFileChangeStatusBusy.Name = "tsmiMenuBarFileChangeStatusBusy";
            this.tsmiMenuBarFileChangeStatusBusy.Size = new System.Drawing.Size(117, 22);
            this.tsmiMenuBarFileChangeStatusBusy.Text = "Busy";
            this.tsmiMenuBarFileChangeStatusBusy.Click += new System.EventHandler(this.tsmiStatusBarStatusBusy_Click);
            // 
            // tsmiMenuBarFileChangeStatusInvisible
            // 
            this.tsmiMenuBarFileChangeStatusInvisible.Image = global::PintoNS.Statuses.INVISIBLE;
            this.tsmiMenuBarFileChangeStatusInvisible.Name = "tsmiMenuBarFileChangeStatusInvisible";
            this.tsmiMenuBarFileChangeStatusInvisible.Size = new System.Drawing.Size(117, 22);
            this.tsmiMenuBarFileChangeStatusInvisible.Text = "Invisible";
            this.tsmiMenuBarFileChangeStatusInvisible.Click += new System.EventHandler(this.tsmiStatusBarStatusInvisible_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(147, 6);
            // 
            // tsmiMenuBarFileLogOff
            // 
            this.tsmiMenuBarFileLogOff.Name = "tsmiMenuBarFileLogOff";
            this.tsmiMenuBarFileLogOff.Size = new System.Drawing.Size(150, 22);
            this.tsmiMenuBarFileLogOff.Text = "Log Off";
            this.tsmiMenuBarFileLogOff.Click += new System.EventHandler(this.tsmiMenuBarFileLogOut_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // tsmiMenuBarFileOptions
            // 
            this.tsmiMenuBarFileOptions.Name = "tsmiMenuBarFileOptions";
            this.tsmiMenuBarFileOptions.Size = new System.Drawing.Size(150, 22);
            this.tsmiMenuBarFileOptions.Text = "Options...";
            this.tsmiMenuBarFileOptions.Click += new System.EventHandler(this.tsmiMenuBarFileOptions_Click);
            // 
            // tsmiMenuBarFileExit
            // 
            this.tsmiMenuBarFileExit.Name = "tsmiMenuBarFileExit";
            this.tsmiMenuBarFileExit.Size = new System.Drawing.Size(150, 22);
            this.tsmiMenuBarFileExit.Text = "Exit";
            this.tsmiMenuBarFileExit.Click += new System.EventHandler(this.tsmiMenuBarFileExit_Click);
            // 
            // tsddbMenuBarTools
            // 
            this.tsddbMenuBarTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuBarTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuBarToolsAddContact,
            this.tsmiMenuBarToolsRemoveContact});
            this.tsddbMenuBarTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuBarTools.Name = "tsddbMenuBarTools";
            this.tsddbMenuBarTools.ShowDropDownArrow = false;
            this.tsddbMenuBarTools.Size = new System.Drawing.Size(38, 22);
            this.tsddbMenuBarTools.Text = "Tools";
            // 
            // tsmiMenuBarToolsAddContact
            // 
            this.tsmiMenuBarToolsAddContact.Name = "tsmiMenuBarToolsAddContact";
            this.tsmiMenuBarToolsAddContact.Size = new System.Drawing.Size(171, 22);
            this.tsmiMenuBarToolsAddContact.Text = "Add a Contact";
            this.tsmiMenuBarToolsAddContact.Click += new System.EventHandler(this.tsmiMenuBarToolsAddContact_Click);
            // 
            // tsmiMenuBarToolsRemoveContact
            // 
            this.tsmiMenuBarToolsRemoveContact.Name = "tsmiMenuBarToolsRemoveContact";
            this.tsmiMenuBarToolsRemoveContact.Size = new System.Drawing.Size(171, 22);
            this.tsmiMenuBarToolsRemoveContact.Text = "Remove a Contact";
            this.tsmiMenuBarToolsRemoveContact.Click += new System.EventHandler(this.tsmiMenuBarToolsRemoveContact_Click);
            // 
            // tsddbMenuBarHelp
            // 
            this.tsddbMenuBarHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuBarHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuBarHelpCheckForUpdates,
            this.toolStripSeparator4,
            this.tsmiMenuBarHelpReportAProblem,
            this.toolStripSeparator2,
            this.tsmiMenuBarHelpToggleConsole,
            this.tsmiMenuBarHelpAbout});
            this.tsddbMenuBarHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuBarHelp.Name = "tsddbMenuBarHelp";
            this.tsddbMenuBarHelp.ShowDropDownArrow = false;
            this.tsddbMenuBarHelp.Size = new System.Drawing.Size(36, 22);
            this.tsddbMenuBarHelp.Text = "Help";
            // 
            // tsmiMenuBarHelpCheckForUpdates
            // 
            this.tsmiMenuBarHelpCheckForUpdates.Name = "tsmiMenuBarHelpCheckForUpdates";
            this.tsmiMenuBarHelpCheckForUpdates.Size = new System.Drawing.Size(171, 22);
            this.tsmiMenuBarHelpCheckForUpdates.Text = "Check for Updates";
            this.tsmiMenuBarHelpCheckForUpdates.Click += new System.EventHandler(this.tsmiMenuBarHelpCheckForUpdates_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(168, 6);
            // 
            // tsmiMenuBarHelpReportAProblem
            // 
            this.tsmiMenuBarHelpReportAProblem.Name = "tsmiMenuBarHelpReportAProblem";
            this.tsmiMenuBarHelpReportAProblem.Size = new System.Drawing.Size(171, 22);
            this.tsmiMenuBarHelpReportAProblem.Text = "Report an Issue";
            this.tsmiMenuBarHelpReportAProblem.Click += new System.EventHandler(this.tsmiMenuBarHelpReportAProblem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(168, 6);
            // 
            // tsmiMenuBarHelpToggleConsole
            // 
            this.tsmiMenuBarHelpToggleConsole.Name = "tsmiMenuBarHelpToggleConsole";
            this.tsmiMenuBarHelpToggleConsole.Size = new System.Drawing.Size(171, 22);
            this.tsmiMenuBarHelpToggleConsole.Text = "Toggle Console";
            this.tsmiMenuBarHelpToggleConsole.Click += new System.EventHandler(this.tsmiMenuBarHelpToggleConsole_Click);
            // 
            // tsmiMenuBarHelpAbout
            // 
            this.tsmiMenuBarHelpAbout.Name = "tsmiMenuBarHelpAbout";
            this.tsmiMenuBarHelpAbout.Size = new System.Drawing.Size(171, 22);
            this.tsmiMenuBarHelpAbout.Text = "About";
            this.tsmiMenuBarHelpAbout.Click += new System.EventHandler(this.tsmiMenuBarHelpAbout_Click);
            // 
            // niTray
            // 
            this.niTray.ContextMenuStrip = this.cmsTray;
            this.niTray.DoubleClick += new System.EventHandler(this.niTray_DoubleClick);
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayChangeStatus,
            this.tsmiTrayExit});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.Size = new System.Drawing.Size(151, 48);
            // 
            // tsmiTrayChangeStatus
            // 
            this.tsmiTrayChangeStatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayChangeStatusOnline,
            this.tsmiTrayChangeStatusAway,
            this.tsmiTrayChangeStatusBusy,
            this.tsmiTrayChangeStatusInvisible});
            this.tsmiTrayChangeStatus.Image = global::PintoNS.Statuses.ONLINE;
            this.tsmiTrayChangeStatus.Name = "tsmiTrayChangeStatus";
            this.tsmiTrayChangeStatus.Size = new System.Drawing.Size(150, 22);
            this.tsmiTrayChangeStatus.Text = "Change Status";
            // 
            // tsmiTrayChangeStatusOnline
            // 
            this.tsmiTrayChangeStatusOnline.Image = global::PintoNS.Statuses.ONLINE;
            this.tsmiTrayChangeStatusOnline.Name = "tsmiTrayChangeStatusOnline";
            this.tsmiTrayChangeStatusOnline.Size = new System.Drawing.Size(117, 22);
            this.tsmiTrayChangeStatusOnline.Text = "Online";
            this.tsmiTrayChangeStatusOnline.Click += new System.EventHandler(this.tsmiStatusBarStatusOnline_Click);
            // 
            // tsmiTrayChangeStatusAway
            // 
            this.tsmiTrayChangeStatusAway.Image = global::PintoNS.Statuses.AWAY;
            this.tsmiTrayChangeStatusAway.Name = "tsmiTrayChangeStatusAway";
            this.tsmiTrayChangeStatusAway.Size = new System.Drawing.Size(117, 22);
            this.tsmiTrayChangeStatusAway.Text = "Away";
            this.tsmiTrayChangeStatusAway.Click += new System.EventHandler(this.tsmiStatusBarStatusAway_Click);
            // 
            // tsmiTrayChangeStatusBusy
            // 
            this.tsmiTrayChangeStatusBusy.Image = global::PintoNS.Statuses.BUSY;
            this.tsmiTrayChangeStatusBusy.Name = "tsmiTrayChangeStatusBusy";
            this.tsmiTrayChangeStatusBusy.Size = new System.Drawing.Size(117, 22);
            this.tsmiTrayChangeStatusBusy.Text = "Busy";
            this.tsmiTrayChangeStatusBusy.Click += new System.EventHandler(this.tsmiStatusBarStatusBusy_Click);
            // 
            // tsmiTrayChangeStatusInvisible
            // 
            this.tsmiTrayChangeStatusInvisible.Image = global::PintoNS.Statuses.INVISIBLE;
            this.tsmiTrayChangeStatusInvisible.Name = "tsmiTrayChangeStatusInvisible";
            this.tsmiTrayChangeStatusInvisible.Size = new System.Drawing.Size(117, 22);
            this.tsmiTrayChangeStatusInvisible.Text = "Invisible";
            this.tsmiTrayChangeStatusInvisible.Click += new System.EventHandler(this.tsmiStatusBarStatusInvisible_Click);
            // 
            // tsmiTrayExit
            // 
            this.tsmiTrayExit.Image = global::PintoNS.Assets.WARNING;
            this.tsmiTrayExit.Name = "tsmiTrayExit";
            this.tsmiTrayExit.Size = new System.Drawing.Size(150, 22);
            this.tsmiTrayExit.Text = "Exit";
            this.tsmiTrayExit.Click += new System.EventHandler(this.tsmiMenuBarFileExit_Click);
            // 
            // tContactsOnlineUpdate
            // 
            this.tContactsOnlineUpdate.Enabled = true;
            this.tContactsOnlineUpdate.Interval = 1000;
            this.tContactsOnlineUpdate.Tick += new System.EventHandler(this.tContactsOnlineUpdate_Tick);
            // 
            // btnStartCall
            // 
            this.btnStartCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnStartCall.BackColor = System.Drawing.Color.Transparent;
            this.btnStartCall.FlatAppearance.BorderSize = 0;
            this.btnStartCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartCall.Image = global::PintoNS.Assets.STARTCALL_DISABLED;
            this.btnStartCall.Location = new System.Drawing.Point(68, 346);
            this.btnStartCall.Name = "btnStartCall";
            this.btnStartCall.Size = new System.Drawing.Size(32, 32);
            this.btnStartCall.TabIndex = 7;
            this.btnStartCall.UseVisualStyleBackColor = false;
            this.btnStartCall.Click += new System.EventHandler(this.btnStartCall_Click);
            // 
            // btnEndCall
            // 
            this.btnEndCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEndCall.BackColor = System.Drawing.Color.Transparent;
            this.btnEndCall.FlatAppearance.BorderSize = 0;
            this.btnEndCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEndCall.Image = global::PintoNS.Assets.ENDCALL_DISABLED;
            this.btnEndCall.Location = new System.Drawing.Point(164, 346);
            this.btnEndCall.Name = "btnEndCall";
            this.btnEndCall.Size = new System.Drawing.Size(32, 32);
            this.btnEndCall.TabIndex = 8;
            this.btnEndCall.UseVisualStyleBackColor = false;
            this.btnEndCall.Click += new System.EventHandler(this.btnEndCall_Click);
            // 
            // txtSearchBox
            // 
            this.txtSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtSearchBox.BackColor = System.Drawing.Color.White;
            this.txtSearchBox.Enabled = false;
            this.txtSearchBox.ForeColor = System.Drawing.Color.DimGray;
            this.txtSearchBox.Location = new System.Drawing.Point(3, 310);
            this.txtSearchBox.Name = "txtSearchBox";
            this.txtSearchBox.PlaceholderText = "Type the username you would like to search for";
            this.txtSearchBox.PlaceholderTextForeColor = System.Drawing.Color.DimGray;
            this.txtSearchBox.Size = new System.Drawing.Size(264, 20);
            this.txtSearchBox.TabIndex = 4;
            this.txtSearchBox.Text = "Type the username you would like to search for";
            this.txtSearchBox.TextForeColor = System.Drawing.Color.Black;
            this.txtSearchBox.TextChanged2 += new System.EventHandler(this.txtSearchBox_TextChanged2);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 411);
            this.Controls.Add(this.btnEndCall);
            this.Controls.Add(this.btnStartCall);
            this.Controls.Add(this.txtSearchBox);
            this.Controls.Add(this.ssStatusBar);
            this.Controls.Add(this.tcTabs);
            this.Controls.Add(this.pQA);
            this.Controls.Add(this.tsMenuBar);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(163, 237);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pQA.ResumeLayout(false);
            this.tcTabs.ResumeLayout(false);
            this.tpLogin.ResumeLayout(false);
            this.tpLogin.PerformLayout();
            this.tpConnecting.ResumeLayout(false);
            this.tpStart.ResumeLayout(false);
            this.tpStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStartContacts)).EndInit();
            this.tpContacts.ResumeLayout(false);
            this.tpContacts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).EndInit();
            this.tpCall.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCallPicture)).EndInit();
            this.ssStatusBar.ResumeLayout(false);
            this.ssStatusBar.PerformLayout();
            this.tsMenuBar.ResumeLayout(false);
            this.tsMenuBar.PerformLayout();
            this.cmsTray.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Panel pQA;
        public System.Windows.Forms.TabControl tcTabs;
        public System.Windows.Forms.TabPage tpLogin;
        public System.Windows.Forms.ImageList ilTabImages;
        public System.Windows.Forms.StatusStrip ssStatusBar;
        public Controls.TextBoxWithPlaceholderSupport txtSearchBox;
        public System.Windows.Forms.ToolStripDropDownButton tsddbStatusBarStatus;
        public System.Windows.Forms.ToolStripStatusLabel tsslStatusBarStatusText;
        public System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarFile;
        public System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarHelp;
        public System.Windows.Forms.TabPage tpContacts;
        public System.Windows.Forms.DataGridView dgvContacts;
        public System.Windows.Forms.LinkLabel llLogin;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileLogOff;
        public System.Windows.Forms.TabPage tpConnecting;
        public System.Windows.Forms.Label lConnectingStatus;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarHelpAbout;
        public System.Windows.Forms.ToolStripMenuItem tsmiStatusBarStatusOnline;
        public System.Windows.Forms.ToolStripMenuItem tsmiStatusBarStatusAway;
        public System.Windows.Forms.ToolStripMenuItem tsmiStatusBarStatusBusy;
        public System.Windows.Forms.ToolStripMenuItem tsmiStatusBarStatusInvisible;
        public System.Windows.Forms.TabPage tpCall;
        public System.Windows.Forms.PictureBox pbCallPicture;
        public System.Windows.Forms.Label lCallTarget;
        public System.Windows.Forms.Label lCallDuration;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarHelpToggleConsole;
        public System.Windows.Forms.NotifyIcon niTray;
        public System.Windows.Forms.ContextMenuStrip cmsTray;
        public System.Windows.Forms.ToolStripMenuItem tsmiTrayExit;
        public System.Windows.Forms.ToolStripMenuItem tsmiTrayChangeStatus;
        public System.Windows.Forms.ToolStripMenuItem tsmiTrayChangeStatusOnline;
        public System.Windows.Forms.ToolStripMenuItem tsmiTrayChangeStatusAway;
        public System.Windows.Forms.ToolStripMenuItem tsmiTrayChangeStatusBusy;
        public System.Windows.Forms.ToolStripMenuItem tsmiTrayChangeStatusInvisible;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileExit;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarHelpCheckForUpdates;
        public Loader lrConnectingLoader;
        public System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarTools;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarToolsAddContact;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarToolsRemoveContact;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileOptions;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileChangeStatus;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileChangeStatusOnline;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileChangeStatusAway;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileChangeStatusBusy;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileChangeStatusInvisible;
        public System.Windows.Forms.TabPage tpStart;
        public System.Windows.Forms.Label lStartTitle;
        public System.Windows.Forms.PictureBox pbStartContacts;
        public System.Windows.Forms.LinkLabel llStartContacts;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarHelpReportAProblem;
        public System.Windows.Forms.Timer tContactsOnlineUpdate;
        public System.Windows.Forms.Button btnStartCall;
        public System.Windows.Forms.Button btnEndCall;
        public PintoNS.Controls.NoFocusQueButton btnQAAddContact;
        public System.Windows.Forms.ToolStripDropDownButton tsddbStatusBarMOTD;
        public System.Windows.Forms.Label lContactsNoContacts;
        public System.Windows.Forms.ToolStrip tsMenuBar;
    }
}