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
            this.ilTabImages = new System.Windows.Forms.ImageList(this.components);
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
            this.scSections = new System.Windows.Forms.SplitContainer();
            this.txtSearchBox = new PintoNS.Controls.ModernTextBoxWithPlaceholderSupport();
            this.tcTabs = new PintoNS.JacksonTabControl.CustomTabControl();
            this.tpConnecting = new System.Windows.Forms.TabPage();
            this.lrConnectingLoader = new PintoNS.Controls.Loader();
            this.lConnectingStatus = new System.Windows.Forms.Label();
            this.tpContacts = new System.Windows.Forms.TabPage();
            this.dgvContacts = new System.Windows.Forms.DataGridView();
            this.pUserInfo = new System.Windows.Forms.Panel();
            this.lUserInfoName = new System.Windows.Forms.Label();
            this.mbUserInfoStatus = new PintoNS.Controls.MenuButton();
            this.cmsUserInfoStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiUserInfoStatusOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserInfoStatusAway = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserInfoStatusBusy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserInfoStatusInvisible = new System.Windows.Forms.ToolStripMenuItem();
            this.lContactsNoContacts = new System.Windows.Forms.Label();
            this.tsMenuBar.SuspendLayout();
            this.cmsTray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSections)).BeginInit();
            this.scSections.Panel1.SuspendLayout();
            this.scSections.SuspendLayout();
            this.tcTabs.SuspendLayout();
            this.tpConnecting.SuspendLayout();
            this.tpContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).BeginInit();
            this.pUserInfo.SuspendLayout();
            this.cmsUserInfoStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilTabImages
            // 
            this.ilTabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTabImages.ImageStream")));
            this.ilTabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTabImages.Images.SetKeyName(0, "21591.png");
            this.ilTabImages.Images.SetKeyName(1, "20659.png");
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
            this.tsMenuBar.Size = new System.Drawing.Size(1064, 25);
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
            this.cmsTray.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsTray.ShowImageMargin = false;
            this.cmsTray.Size = new System.Drawing.Size(126, 48);
            // 
            // tsmiTrayChangeStatus
            // 
            this.tsmiTrayChangeStatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayChangeStatusOnline,
            this.tsmiTrayChangeStatusAway,
            this.tsmiTrayChangeStatusBusy,
            this.tsmiTrayChangeStatusInvisible});
            this.tsmiTrayChangeStatus.Name = "tsmiTrayChangeStatus";
            this.tsmiTrayChangeStatus.Size = new System.Drawing.Size(125, 22);
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
            this.tsmiTrayExit.Name = "tsmiTrayExit";
            this.tsmiTrayExit.Size = new System.Drawing.Size(125, 22);
            this.tsmiTrayExit.Text = "Exit";
            this.tsmiTrayExit.Click += new System.EventHandler(this.tsmiMenuBarFileExit_Click);
            // 
            // scSections
            // 
            this.scSections.BackColor = System.Drawing.Color.DarkGray;
            this.scSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSections.Location = new System.Drawing.Point(0, 25);
            this.scSections.Name = "scSections";
            // 
            // scSections.Panel1
            // 
            this.scSections.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.scSections.Panel1.Controls.Add(this.txtSearchBox);
            this.scSections.Panel1.Controls.Add(this.tcTabs);
            this.scSections.Panel1.Controls.Add(this.pUserInfo);
            this.scSections.Panel1MinSize = 247;
            // 
            // scSections.Panel2
            // 
            this.scSections.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.scSections.Panel2MinSize = 648;
            this.scSections.Size = new System.Drawing.Size(1064, 554);
            this.scSections.SplitterDistance = 272;
            this.scSections.SplitterWidth = 2;
            this.scSections.TabIndex = 9;
            this.scSections.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scSections_SplitterMoved);
            // 
            // txtSearchBox
            // 
            this.txtSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchBox.BackColor = System.Drawing.Color.White;
            this.txtSearchBox.BorderColor = System.Drawing.Color.DarkGray;
            this.txtSearchBox.Image = global::PintoNS.Assets._20859;
            this.txtSearchBox.Location = new System.Drawing.Point(6, 32);
            this.txtSearchBox.Name = "txtSearchBox";
            this.txtSearchBox.PlaceholderText = "Search";
            this.txtSearchBox.PlaceholderTextForeColor = System.Drawing.Color.DimGray;
            this.txtSearchBox.Size = new System.Drawing.Size(260, 20);
            this.txtSearchBox.TabIndex = 9;
            this.txtSearchBox.TextForeColor = System.Drawing.Color.Black;
            this.txtSearchBox.TextChanged2 += new System.EventHandler(this.txtSearchBox_TextChanged2);
            // 
            // tcTabs
            // 
            this.tcTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcTabs.Controls.Add(this.tpConnecting);
            this.tcTabs.Controls.Add(this.tpContacts);
            // 
            // 
            // 
            this.tcTabs.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.tcTabs.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            this.tcTabs.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tcTabs.DisplayStyleProvider.CloserColor = System.Drawing.Color.Empty;
            this.tcTabs.DisplayStyleProvider.FocusTrack = true;
            this.tcTabs.DisplayStyleProvider.HotTrack = true;
            this.tcTabs.DisplayStyleProvider.IgnoreImageOnTextDraw = false;
            this.tcTabs.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tcTabs.DisplayStyleProvider.Opacity = 1F;
            this.tcTabs.DisplayStyleProvider.Overlap = 0;
            this.tcTabs.DisplayStyleProvider.Padding = new System.Drawing.Point(6, 3);
            this.tcTabs.DisplayStyleProvider.ShowTabCloser = false;
            this.tcTabs.DisplayStyleProvider.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tcTabs.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.tcTabs.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.tcTabs.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tcTabs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcTabs.HotTrack = true;
            this.tcTabs.ImageList = this.ilTabImages;
            this.tcTabs.Location = new System.Drawing.Point(4, 59);
            this.tcTabs.Name = "tcTabs";
            this.tcTabs.SelectedIndex = 0;
            this.tcTabs.Size = new System.Drawing.Size(268, 494);
            this.tcTabs.TabIndex = 2;
            // 
            // tpConnecting
            // 
            this.tpConnecting.BackColor = System.Drawing.SystemColors.Window;
            this.tpConnecting.Controls.Add(this.lrConnectingLoader);
            this.tpConnecting.Controls.Add(this.lConnectingStatus);
            this.tpConnecting.ImageIndex = 0;
            this.tpConnecting.Location = new System.Drawing.Point(4, 40);
            this.tpConnecting.Name = "tpConnecting";
            this.tpConnecting.Padding = new System.Windows.Forms.Padding(3);
            this.tpConnecting.Size = new System.Drawing.Size(260, 450);
            this.tpConnecting.TabIndex = 2;
            this.tpConnecting.Text = "Connecting";
            // 
            // lrConnectingLoader
            // 
            this.lrConnectingLoader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lrConnectingLoader.Location = new System.Drawing.Point(72, 175);
            this.lrConnectingLoader.MaximumSize = new System.Drawing.Size(122, 107);
            this.lrConnectingLoader.MinimumSize = new System.Drawing.Size(122, 107);
            this.lrConnectingLoader.Name = "lrConnectingLoader";
            this.lrConnectingLoader.Size = new System.Drawing.Size(122, 107);
            this.lrConnectingLoader.TabIndex = 2;
            // 
            // lConnectingStatus
            // 
            this.lConnectingStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lConnectingStatus.Location = new System.Drawing.Point(69, 150);
            this.lConnectingStatus.Name = "lConnectingStatus";
            this.lConnectingStatus.Size = new System.Drawing.Size(128, 16);
            this.lConnectingStatus.TabIndex = 1;
            this.lConnectingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpContacts
            // 
            this.tpContacts.BackColor = System.Drawing.SystemColors.Window;
            this.tpContacts.Controls.Add(this.lContactsNoContacts);
            this.tpContacts.Controls.Add(this.dgvContacts);
            this.tpContacts.ImageIndex = 1;
            this.tpContacts.Location = new System.Drawing.Point(4, 40);
            this.tpContacts.Name = "tpContacts";
            this.tpContacts.Padding = new System.Windows.Forms.Padding(3);
            this.tpContacts.Size = new System.Drawing.Size(260, 450);
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
            this.dgvContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContacts.Location = new System.Drawing.Point(3, 3);
            this.dgvContacts.MultiSelect = false;
            this.dgvContacts.Name = "dgvContacts";
            this.dgvContacts.ReadOnly = true;
            this.dgvContacts.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvContacts.RowHeadersVisible = false;
            this.dgvContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContacts.Size = new System.Drawing.Size(254, 444);
            this.dgvContacts.TabIndex = 0;
            this.dgvContacts.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvContacts_CellContextMenuStripNeeded);
            this.dgvContacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContacts_CellDoubleClick);
            this.dgvContacts.SelectionChanged += new System.EventHandler(this.dgvContacts_SelectionChanged);
            // 
            // pUserInfo
            // 
            this.pUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pUserInfo.Controls.Add(this.lUserInfoName);
            this.pUserInfo.Controls.Add(this.mbUserInfoStatus);
            this.pUserInfo.Location = new System.Drawing.Point(0, 0);
            this.pUserInfo.Name = "pUserInfo";
            this.pUserInfo.Size = new System.Drawing.Size(272, 26);
            this.pUserInfo.TabIndex = 9;
            // 
            // lUserInfoName
            // 
            this.lUserInfoName.AutoSize = true;
            this.lUserInfoName.Location = new System.Drawing.Point(46, 7);
            this.lUserInfoName.Name = "lUserInfoName";
            this.lUserInfoName.Size = new System.Drawing.Size(53, 13);
            this.lUserInfoName.TabIndex = 9;
            this.lUserInfoName.Text = "PintoUser";
            // 
            // mbUserInfoStatus
            // 
            this.mbUserInfoStatus.FlatAppearance.BorderSize = 0;
            this.mbUserInfoStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbUserInfoStatus.ForeColor = System.Drawing.Color.DimGray;
            this.mbUserInfoStatus.Image = global::PintoNS.Statuses.OFFLINE;
            this.mbUserInfoStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mbUserInfoStatus.Location = new System.Drawing.Point(3, 1);
            this.mbUserInfoStatus.Menu = this.cmsUserInfoStatus;
            this.mbUserInfoStatus.Name = "mbUserInfoStatus";
            this.mbUserInfoStatus.Size = new System.Drawing.Size(37, 24);
            this.mbUserInfoStatus.TabIndex = 9;
            this.mbUserInfoStatus.UseVisualStyleBackColor = true;
            // 
            // cmsUserInfoStatus
            // 
            this.cmsUserInfoStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUserInfoStatusOnline,
            this.tsmiUserInfoStatusAway,
            this.tsmiUserInfoStatusBusy,
            this.tsmiUserInfoStatusInvisible});
            this.cmsUserInfoStatus.Name = "cmsUserInfoStatus";
            this.cmsUserInfoStatus.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsUserInfoStatus.Size = new System.Drawing.Size(118, 92);
            // 
            // tsmiUserInfoStatusOnline
            // 
            this.tsmiUserInfoStatusOnline.Image = global::PintoNS.Statuses.ONLINE;
            this.tsmiUserInfoStatusOnline.Name = "tsmiUserInfoStatusOnline";
            this.tsmiUserInfoStatusOnline.Size = new System.Drawing.Size(117, 22);
            this.tsmiUserInfoStatusOnline.Text = "Online";
            this.tsmiUserInfoStatusOnline.Click += new System.EventHandler(this.tsmiStatusBarStatusOnline_Click);
            // 
            // tsmiUserInfoStatusAway
            // 
            this.tsmiUserInfoStatusAway.Image = global::PintoNS.Statuses.AWAY;
            this.tsmiUserInfoStatusAway.Name = "tsmiUserInfoStatusAway";
            this.tsmiUserInfoStatusAway.Size = new System.Drawing.Size(117, 22);
            this.tsmiUserInfoStatusAway.Text = "Away";
            this.tsmiUserInfoStatusAway.Click += new System.EventHandler(this.tsmiStatusBarStatusAway_Click);
            // 
            // tsmiUserInfoStatusBusy
            // 
            this.tsmiUserInfoStatusBusy.Image = global::PintoNS.Statuses.BUSY;
            this.tsmiUserInfoStatusBusy.Name = "tsmiUserInfoStatusBusy";
            this.tsmiUserInfoStatusBusy.Size = new System.Drawing.Size(117, 22);
            this.tsmiUserInfoStatusBusy.Text = "Busy";
            this.tsmiUserInfoStatusBusy.Click += new System.EventHandler(this.tsmiStatusBarStatusBusy_Click);
            // 
            // tsmiUserInfoStatusInvisible
            // 
            this.tsmiUserInfoStatusInvisible.Image = global::PintoNS.Statuses.INVISIBLE;
            this.tsmiUserInfoStatusInvisible.Name = "tsmiUserInfoStatusInvisible";
            this.tsmiUserInfoStatusInvisible.Size = new System.Drawing.Size(117, 22);
            this.tsmiUserInfoStatusInvisible.Text = "Invisible";
            this.tsmiUserInfoStatusInvisible.Click += new System.EventHandler(this.tsmiStatusBarStatusInvisible_Click);
            // 
            // lContactsNoContacts
            // 
            this.lContactsNoContacts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lContactsNoContacts.AutoSize = true;
            this.lContactsNoContacts.Location = new System.Drawing.Point(75, 20);
            this.lContactsNoContacts.Name = "lContactsNoContacts";
            this.lContactsNoContacts.Size = new System.Drawing.Size(111, 13);
            this.lContactsNoContacts.TabIndex = 1;
            this.lContactsNoContacts.Text = "You have no contacts";
            this.lContactsNoContacts.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 579);
            this.Controls.Add(this.scSections);
            this.Controls.Add(this.tsMenuBar);
            this.MinimumSize = new System.Drawing.Size(163, 237);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.tsMenuBar.ResumeLayout(false);
            this.tsMenuBar.PerformLayout();
            this.cmsTray.ResumeLayout(false);
            this.scSections.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scSections)).EndInit();
            this.scSections.ResumeLayout(false);
            this.tcTabs.ResumeLayout(false);
            this.tpConnecting.ResumeLayout(false);
            this.tpContacts.ResumeLayout(false);
            this.tpContacts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).EndInit();
            this.pUserInfo.ResumeLayout(false);
            this.pUserInfo.PerformLayout();
            this.cmsUserInfoStatus.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public PintoNS.JacksonTabControl.CustomTabControl tcTabs;
        public System.Windows.Forms.ImageList ilTabImages;
        public System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarFile;
        public System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarHelp;
        public System.Windows.Forms.TabPage tpContacts;
        public System.Windows.Forms.DataGridView dgvContacts;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileLogOff;
        public System.Windows.Forms.TabPage tpConnecting;
        public System.Windows.Forms.Label lConnectingStatus;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarHelpAbout;
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
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarHelpReportAProblem;
        public System.Windows.Forms.ToolStrip tsMenuBar;
        private System.Windows.Forms.SplitContainer scSections;
        private System.Windows.Forms.Panel pUserInfo;
        private MenuButton mbUserInfoStatus;
        private System.Windows.Forms.Label lUserInfoName;
        private System.Windows.Forms.ContextMenuStrip cmsUserInfoStatus;
        public System.Windows.Forms.ToolStripMenuItem tsmiUserInfoStatusOnline;
        public System.Windows.Forms.ToolStripMenuItem tsmiUserInfoStatusAway;
        public System.Windows.Forms.ToolStripMenuItem tsmiUserInfoStatusBusy;
        public System.Windows.Forms.ToolStripMenuItem tsmiUserInfoStatusInvisible;
        private ModernTextBoxWithPlaceholderSupport txtSearchBox;
        public System.Windows.Forms.Label lContactsNoContacts;
    }
}