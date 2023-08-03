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
            this.tsddbMenuBarPinto = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiMenuBarPintoChangeStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileChangeStatusOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileChangeStatusAway = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileChangeStatusBusy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileChangeStatusInvisible = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarFileChangeStatusOffline = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMenuBarPintoLogOff = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMenuBarPintoOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuBarPintoExit = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsmiTrayChangeStatusOffline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.scSections = new System.Windows.Forms.SplitContainer();
            this.pUserInfo = new System.Windows.Forms.Panel();
            this.lUserInfoName = new System.Windows.Forms.Label();
            this.cmsUserInfoStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiUserInfoStatusOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserInfoStatusAway = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserInfoStatusBusy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserInfoStatusInvisible = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserInfoStatusOffline = new System.Windows.Forms.ToolStripMenuItem();
            this.pMessaging = new System.Windows.Forms.Panel();
            this.pbMessagingContactStatus = new System.Windows.Forms.PictureBox();
            this.lMessagingContactName = new System.Windows.Forms.Label();
            this.pPintoNews = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.wbPintoNews = new System.Windows.Forms.WebBrowser();
            this.tConnectingTray = new System.Windows.Forms.Timer(this.components);
            this.separator2 = new PintoNS.Controls.Separator();
            this.txtSearchBox = new PintoNS.Controls.ModernTextBoxWithPlaceholderSupport();
            this.tcLeftSections = new PintoNS.JacksonTabControl.CustomTabControl();
            this.tpLeftSectionsContacts = new System.Windows.Forms.TabPage();
            this.dgvContacts = new System.Windows.Forms.DataGridView();
            this.mbUserInfoStatus = new PintoNS.Controls.MenuButton();
            this.separator3 = new PintoNS.Controls.Separator();
            this.separator1 = new PintoNS.Controls.Separator();
            this.tsMenuBar.SuspendLayout();
            this.cmsTray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSections)).BeginInit();
            this.scSections.Panel1.SuspendLayout();
            this.scSections.Panel2.SuspendLayout();
            this.scSections.SuspendLayout();
            this.pUserInfo.SuspendLayout();
            this.cmsUserInfoStatus.SuspendLayout();
            this.pMessaging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMessagingContactStatus)).BeginInit();
            this.pPintoNews.SuspendLayout();
            this.tcLeftSections.SuspendLayout();
            this.tpLeftSectionsContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).BeginInit();
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
            this.tsddbMenuBarPinto,
            this.tsddbMenuBarTools,
            this.tsddbMenuBarHelp});
            this.tsMenuBar.Location = new System.Drawing.Point(0, 0);
            this.tsMenuBar.Name = "tsMenuBar";
            this.tsMenuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsMenuBar.Size = new System.Drawing.Size(899, 25);
            this.tsMenuBar.TabIndex = 0;
            this.tsMenuBar.Text = "toolStrip1";
            // 
            // tsddbMenuBarPinto
            // 
            this.tsddbMenuBarPinto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbMenuBarPinto.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuBarPintoChangeStatus,
            this.toolStripSeparator3,
            this.tsmiMenuBarPintoLogOff,
            this.toolStripSeparator1,
            this.tsmiMenuBarPintoOptions,
            this.tsmiMenuBarPintoExit});
            this.tsddbMenuBarPinto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbMenuBarPinto.Name = "tsddbMenuBarPinto";
            this.tsddbMenuBarPinto.ShowDropDownArrow = false;
            this.tsddbMenuBarPinto.Size = new System.Drawing.Size(39, 22);
            this.tsddbMenuBarPinto.Text = "Pinto";
            // 
            // tsmiMenuBarPintoChangeStatus
            // 
            this.tsmiMenuBarPintoChangeStatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuBarFileChangeStatusOnline,
            this.tsmiMenuBarFileChangeStatusAway,
            this.tsmiMenuBarFileChangeStatusBusy,
            this.tsmiMenuBarFileChangeStatusInvisible,
            this.tsmiMenuBarFileChangeStatusOffline});
            this.tsmiMenuBarPintoChangeStatus.Name = "tsmiMenuBarPintoChangeStatus";
            this.tsmiMenuBarPintoChangeStatus.Size = new System.Drawing.Size(150, 22);
            this.tsmiMenuBarPintoChangeStatus.Text = "Change Status";
            // 
            // tsmiMenuBarFileChangeStatusOnline
            // 
            this.tsmiMenuBarFileChangeStatusOnline.Image = global::PintoNS.Statuses.ONLINE;
            this.tsmiMenuBarFileChangeStatusOnline.Name = "tsmiMenuBarFileChangeStatusOnline";
            this.tsmiMenuBarFileChangeStatusOnline.Size = new System.Drawing.Size(117, 22);
            this.tsmiMenuBarFileChangeStatusOnline.Text = "Online";
            this.tsmiMenuBarFileChangeStatusOnline.Click += new System.EventHandler(this.tsmiUserInfoStatusOnline_Click);
            // 
            // tsmiMenuBarFileChangeStatusAway
            // 
            this.tsmiMenuBarFileChangeStatusAway.Image = global::PintoNS.Statuses.AWAY;
            this.tsmiMenuBarFileChangeStatusAway.Name = "tsmiMenuBarFileChangeStatusAway";
            this.tsmiMenuBarFileChangeStatusAway.Size = new System.Drawing.Size(117, 22);
            this.tsmiMenuBarFileChangeStatusAway.Text = "Away";
            this.tsmiMenuBarFileChangeStatusAway.Click += new System.EventHandler(this.tsmiUserInfoStatusAway_Click);
            // 
            // tsmiMenuBarFileChangeStatusBusy
            // 
            this.tsmiMenuBarFileChangeStatusBusy.Image = global::PintoNS.Statuses.BUSY;
            this.tsmiMenuBarFileChangeStatusBusy.Name = "tsmiMenuBarFileChangeStatusBusy";
            this.tsmiMenuBarFileChangeStatusBusy.Size = new System.Drawing.Size(117, 22);
            this.tsmiMenuBarFileChangeStatusBusy.Text = "Busy";
            this.tsmiMenuBarFileChangeStatusBusy.Click += new System.EventHandler(this.tsmiUserInfoStatusBusy_Click);
            // 
            // tsmiMenuBarFileChangeStatusInvisible
            // 
            this.tsmiMenuBarFileChangeStatusInvisible.Image = global::PintoNS.Statuses.INVISIBLE;
            this.tsmiMenuBarFileChangeStatusInvisible.Name = "tsmiMenuBarFileChangeStatusInvisible";
            this.tsmiMenuBarFileChangeStatusInvisible.Size = new System.Drawing.Size(117, 22);
            this.tsmiMenuBarFileChangeStatusInvisible.Text = "Invisible";
            this.tsmiMenuBarFileChangeStatusInvisible.Click += new System.EventHandler(this.tsmiUserInfoStatusInvisible_Click);
            // 
            // tsmiMenuBarFileChangeStatusOffline
            // 
            this.tsmiMenuBarFileChangeStatusOffline.Image = global::PintoNS.Statuses.INVISIBLE;
            this.tsmiMenuBarFileChangeStatusOffline.Name = "tsmiMenuBarFileChangeStatusOffline";
            this.tsmiMenuBarFileChangeStatusOffline.Size = new System.Drawing.Size(117, 22);
            this.tsmiMenuBarFileChangeStatusOffline.Text = "Offline";
            this.tsmiMenuBarFileChangeStatusOffline.Click += new System.EventHandler(this.tsmiUserInfoStatusOffline_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(147, 6);
            // 
            // tsmiMenuBarPintoLogOff
            // 
            this.tsmiMenuBarPintoLogOff.Name = "tsmiMenuBarPintoLogOff";
            this.tsmiMenuBarPintoLogOff.Size = new System.Drawing.Size(150, 22);
            this.tsmiMenuBarPintoLogOff.Text = "Log Off";
            this.tsmiMenuBarPintoLogOff.Click += new System.EventHandler(this.tsmiMenuBarPintoLogOut_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // tsmiMenuBarPintoOptions
            // 
            this.tsmiMenuBarPintoOptions.Name = "tsmiMenuBarPintoOptions";
            this.tsmiMenuBarPintoOptions.Size = new System.Drawing.Size(150, 22);
            this.tsmiMenuBarPintoOptions.Text = "Options...";
            this.tsmiMenuBarPintoOptions.Click += new System.EventHandler(this.tsmiMenuBarPintoOptions_Click);
            // 
            // tsmiMenuBarPintoExit
            // 
            this.tsmiMenuBarPintoExit.Name = "tsmiMenuBarPintoExit";
            this.tsmiMenuBarPintoExit.Size = new System.Drawing.Size(150, 22);
            this.tsmiMenuBarPintoExit.Text = "Exit";
            this.tsmiMenuBarPintoExit.Click += new System.EventHandler(this.tsmiMenuBarPintoExit_Click);
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
            this.tsmiTrayChangeStatusInvisible,
            this.tsmiTrayChangeStatusOffline});
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
            this.tsmiTrayChangeStatusOnline.Click += new System.EventHandler(this.tsmiUserInfoStatusOnline_Click);
            // 
            // tsmiTrayChangeStatusAway
            // 
            this.tsmiTrayChangeStatusAway.Image = global::PintoNS.Statuses.AWAY;
            this.tsmiTrayChangeStatusAway.Name = "tsmiTrayChangeStatusAway";
            this.tsmiTrayChangeStatusAway.Size = new System.Drawing.Size(117, 22);
            this.tsmiTrayChangeStatusAway.Text = "Away";
            this.tsmiTrayChangeStatusAway.Click += new System.EventHandler(this.tsmiUserInfoStatusAway_Click);
            // 
            // tsmiTrayChangeStatusBusy
            // 
            this.tsmiTrayChangeStatusBusy.Image = global::PintoNS.Statuses.BUSY;
            this.tsmiTrayChangeStatusBusy.Name = "tsmiTrayChangeStatusBusy";
            this.tsmiTrayChangeStatusBusy.Size = new System.Drawing.Size(117, 22);
            this.tsmiTrayChangeStatusBusy.Text = "Busy";
            this.tsmiTrayChangeStatusBusy.Click += new System.EventHandler(this.tsmiUserInfoStatusBusy_Click);
            // 
            // tsmiTrayChangeStatusInvisible
            // 
            this.tsmiTrayChangeStatusInvisible.Image = global::PintoNS.Statuses.INVISIBLE;
            this.tsmiTrayChangeStatusInvisible.Name = "tsmiTrayChangeStatusInvisible";
            this.tsmiTrayChangeStatusInvisible.Size = new System.Drawing.Size(117, 22);
            this.tsmiTrayChangeStatusInvisible.Text = "Invisible";
            this.tsmiTrayChangeStatusInvisible.Click += new System.EventHandler(this.tsmiUserInfoStatusInvisible_Click);
            // 
            // tsmiTrayChangeStatusOffline
            // 
            this.tsmiTrayChangeStatusOffline.Image = global::PintoNS.Statuses.INVISIBLE;
            this.tsmiTrayChangeStatusOffline.Name = "tsmiTrayChangeStatusOffline";
            this.tsmiTrayChangeStatusOffline.Size = new System.Drawing.Size(117, 22);
            this.tsmiTrayChangeStatusOffline.Text = "Offline";
            this.tsmiTrayChangeStatusOffline.Click += new System.EventHandler(this.tsmiUserInfoStatusOffline_Click);
            // 
            // tsmiTrayExit
            // 
            this.tsmiTrayExit.Name = "tsmiTrayExit";
            this.tsmiTrayExit.Size = new System.Drawing.Size(125, 22);
            this.tsmiTrayExit.Text = "Exit";
            this.tsmiTrayExit.Click += new System.EventHandler(this.tsmiMenuBarPintoExit_Click);
            // 
            // scSections
            // 
            this.scSections.BackColor = System.Drawing.Color.LightGray;
            this.scSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSections.Location = new System.Drawing.Point(0, 25);
            this.scSections.Name = "scSections";
            // 
            // scSections.Panel1
            // 
            this.scSections.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.scSections.Panel1.Controls.Add(this.separator2);
            this.scSections.Panel1.Controls.Add(this.txtSearchBox);
            this.scSections.Panel1.Controls.Add(this.tcLeftSections);
            this.scSections.Panel1.Controls.Add(this.pUserInfo);
            this.scSections.Panel1MinSize = 250;
            // 
            // scSections.Panel2
            // 
            this.scSections.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.scSections.Panel2.Controls.Add(this.pMessaging);
            this.scSections.Panel2.Controls.Add(this.pPintoNews);
            this.scSections.Panel2MinSize = 500;
            this.scSections.Size = new System.Drawing.Size(899, 554);
            this.scSections.SplitterDistance = 250;
            this.scSections.SplitterWidth = 2;
            this.scSections.TabIndex = 9;
            // 
            // pUserInfo
            // 
            this.pUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pUserInfo.Controls.Add(this.lUserInfoName);
            this.pUserInfo.Controls.Add(this.mbUserInfoStatus);
            this.pUserInfo.Location = new System.Drawing.Point(0, 0);
            this.pUserInfo.Name = "pUserInfo";
            this.pUserInfo.Size = new System.Drawing.Size(250, 26);
            this.pUserInfo.TabIndex = 9;
            this.pUserInfo.Click += new System.EventHandler(this.pUserInfo_Click);
            this.pUserInfo.MouseEnter += new System.EventHandler(this.pUserInfo_MouseEnter);
            this.pUserInfo.MouseLeave += new System.EventHandler(this.pUserInfo_MouseLeave);
            // 
            // lUserInfoName
            // 
            this.lUserInfoName.AutoSize = true;
            this.lUserInfoName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lUserInfoName.Location = new System.Drawing.Point(46, 7);
            this.lUserInfoName.Name = "lUserInfoName";
            this.lUserInfoName.Size = new System.Drawing.Size(62, 13);
            this.lUserInfoName.TabIndex = 9;
            this.lUserInfoName.Text = "PintoUser";
            // 
            // cmsUserInfoStatus
            // 
            this.cmsUserInfoStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUserInfoStatusOnline,
            this.tsmiUserInfoStatusAway,
            this.tsmiUserInfoStatusBusy,
            this.tsmiUserInfoStatusInvisible,
            this.tsmiUserInfoStatusOffline});
            this.cmsUserInfoStatus.Name = "cmsUserInfoStatus";
            this.cmsUserInfoStatus.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsUserInfoStatus.Size = new System.Drawing.Size(118, 114);
            // 
            // tsmiUserInfoStatusOnline
            // 
            this.tsmiUserInfoStatusOnline.Image = global::PintoNS.Statuses.ONLINE;
            this.tsmiUserInfoStatusOnline.Name = "tsmiUserInfoStatusOnline";
            this.tsmiUserInfoStatusOnline.Size = new System.Drawing.Size(117, 22);
            this.tsmiUserInfoStatusOnline.Text = "Online";
            this.tsmiUserInfoStatusOnline.Click += new System.EventHandler(this.tsmiUserInfoStatusOnline_Click);
            // 
            // tsmiUserInfoStatusAway
            // 
            this.tsmiUserInfoStatusAway.Image = global::PintoNS.Statuses.AWAY;
            this.tsmiUserInfoStatusAway.Name = "tsmiUserInfoStatusAway";
            this.tsmiUserInfoStatusAway.Size = new System.Drawing.Size(117, 22);
            this.tsmiUserInfoStatusAway.Text = "Away";
            this.tsmiUserInfoStatusAway.Click += new System.EventHandler(this.tsmiUserInfoStatusAway_Click);
            // 
            // tsmiUserInfoStatusBusy
            // 
            this.tsmiUserInfoStatusBusy.Image = global::PintoNS.Statuses.BUSY;
            this.tsmiUserInfoStatusBusy.Name = "tsmiUserInfoStatusBusy";
            this.tsmiUserInfoStatusBusy.Size = new System.Drawing.Size(117, 22);
            this.tsmiUserInfoStatusBusy.Text = "Busy";
            this.tsmiUserInfoStatusBusy.Click += new System.EventHandler(this.tsmiUserInfoStatusBusy_Click);
            // 
            // tsmiUserInfoStatusInvisible
            // 
            this.tsmiUserInfoStatusInvisible.Image = global::PintoNS.Statuses.INVISIBLE;
            this.tsmiUserInfoStatusInvisible.Name = "tsmiUserInfoStatusInvisible";
            this.tsmiUserInfoStatusInvisible.Size = new System.Drawing.Size(117, 22);
            this.tsmiUserInfoStatusInvisible.Text = "Invisible";
            this.tsmiUserInfoStatusInvisible.Click += new System.EventHandler(this.tsmiUserInfoStatusInvisible_Click);
            // 
            // tsmiUserInfoStatusOffline
            // 
            this.tsmiUserInfoStatusOffline.Image = global::PintoNS.Statuses.OFFLINE;
            this.tsmiUserInfoStatusOffline.Name = "tsmiUserInfoStatusOffline";
            this.tsmiUserInfoStatusOffline.Size = new System.Drawing.Size(117, 22);
            this.tsmiUserInfoStatusOffline.Text = "Offline";
            this.tsmiUserInfoStatusOffline.Click += new System.EventHandler(this.tsmiUserInfoStatusOffline_Click);
            // 
            // pMessaging
            // 
            this.pMessaging.Controls.Add(this.pbMessagingContactStatus);
            this.pMessaging.Controls.Add(this.separator3);
            this.pMessaging.Controls.Add(this.lMessagingContactName);
            this.pMessaging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMessaging.Location = new System.Drawing.Point(0, 0);
            this.pMessaging.Name = "pMessaging";
            this.pMessaging.Size = new System.Drawing.Size(647, 554);
            this.pMessaging.TabIndex = 3;
            this.pMessaging.Visible = false;
            // 
            // pbMessagingContactStatus
            // 
            this.pbMessagingContactStatus.Image = global::PintoNS.Statuses.OFFLINE;
            this.pbMessagingContactStatus.Location = new System.Drawing.Point(14, 9);
            this.pbMessagingContactStatus.Name = "pbMessagingContactStatus";
            this.pbMessagingContactStatus.Size = new System.Drawing.Size(16, 16);
            this.pbMessagingContactStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMessagingContactStatus.TabIndex = 6;
            this.pbMessagingContactStatus.TabStop = false;
            // 
            // lMessagingContactName
            // 
            this.lMessagingContactName.AutoSize = true;
            this.lMessagingContactName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lMessagingContactName.Location = new System.Drawing.Point(33, 7);
            this.lMessagingContactName.Name = "lMessagingContactName";
            this.lMessagingContactName.Size = new System.Drawing.Size(72, 19);
            this.lMessagingContactName.TabIndex = 4;
            this.lMessagingContactName.Text = "Contact";
            // 
            // pPintoNews
            // 
            this.pPintoNews.Controls.Add(this.separator1);
            this.pPintoNews.Controls.Add(this.label1);
            this.pPintoNews.Controls.Add(this.wbPintoNews);
            this.pPintoNews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pPintoNews.Location = new System.Drawing.Point(0, 0);
            this.pPintoNews.Name = "pPintoNews";
            this.pPintoNews.Size = new System.Drawing.Size(647, 554);
            this.pPintoNews.TabIndex = 2;
            this.pPintoNews.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pinto! News";
            // 
            // wbPintoNews
            // 
            this.wbPintoNews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbPintoNews.IsWebBrowserContextMenuEnabled = false;
            this.wbPintoNews.Location = new System.Drawing.Point(8, 36);
            this.wbPintoNews.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbPintoNews.Name = "wbPintoNews";
            this.wbPintoNews.Size = new System.Drawing.Size(630, 517);
            this.wbPintoNews.TabIndex = 3;
            this.wbPintoNews.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            this.wbPintoNews.WebBrowserShortcutsEnabled = false;
            // 
            // tConnectingTray
            // 
            this.tConnectingTray.Interval = 350;
            this.tConnectingTray.Tick += new System.EventHandler(this.tConnectingTray_Tick);
            // 
            // separator2
            // 
            this.separator2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separator2.Location = new System.Drawing.Point(8, 28);
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(238, 2);
            this.separator2.TabIndex = 2;
            // 
            // txtSearchBox
            // 
            this.txtSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchBox.BackColor = System.Drawing.Color.White;
            this.txtSearchBox.BorderColor = System.Drawing.Color.DarkGray;
            this.txtSearchBox.Image = global::PintoNS.Assets._20859;
            this.txtSearchBox.Location = new System.Drawing.Point(8, 32);
            this.txtSearchBox.Name = "txtSearchBox";
            this.txtSearchBox.PlaceholderText = "Search";
            this.txtSearchBox.PlaceholderTextForeColor = System.Drawing.Color.DimGray;
            this.txtSearchBox.Size = new System.Drawing.Size(238, 20);
            this.txtSearchBox.TabIndex = 9;
            this.txtSearchBox.TextForeColor = System.Drawing.Color.Black;
            this.txtSearchBox.TextChanged2 += new System.EventHandler(this.txtSearchBox_TextChanged2);
            // 
            // tcLeftSections
            // 
            this.tcLeftSections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcLeftSections.Controls.Add(this.tpLeftSectionsContacts);
            // 
            // 
            // 
            this.tcLeftSections.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.tcLeftSections.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            this.tcLeftSections.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tcLeftSections.DisplayStyleProvider.CloserColor = System.Drawing.Color.Empty;
            this.tcLeftSections.DisplayStyleProvider.FocusTrack = true;
            this.tcLeftSections.DisplayStyleProvider.HotTrack = true;
            this.tcLeftSections.DisplayStyleProvider.IgnoreImageOnTextDraw = false;
            this.tcLeftSections.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tcLeftSections.DisplayStyleProvider.Opacity = 1F;
            this.tcLeftSections.DisplayStyleProvider.Overlap = 0;
            this.tcLeftSections.DisplayStyleProvider.Padding = new System.Drawing.Point(6, 3);
            this.tcLeftSections.DisplayStyleProvider.ShowTabCloser = false;
            this.tcLeftSections.DisplayStyleProvider.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tcLeftSections.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.tcLeftSections.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.tcLeftSections.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tcLeftSections.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcLeftSections.HotTrack = true;
            this.tcLeftSections.ImageList = this.ilTabImages;
            this.tcLeftSections.Location = new System.Drawing.Point(4, 59);
            this.tcLeftSections.Name = "tcLeftSections";
            this.tcLeftSections.SelectedIndex = 0;
            this.tcLeftSections.Size = new System.Drawing.Size(246, 494);
            this.tcLeftSections.TabIndex = 2;
            // 
            // tpLeftSectionsContacts
            // 
            this.tpLeftSectionsContacts.BackColor = System.Drawing.SystemColors.Window;
            this.tpLeftSectionsContacts.Controls.Add(this.dgvContacts);
            this.tpLeftSectionsContacts.ImageIndex = 1;
            this.tpLeftSectionsContacts.Location = new System.Drawing.Point(4, 40);
            this.tpLeftSectionsContacts.Name = "tpLeftSectionsContacts";
            this.tpLeftSectionsContacts.Padding = new System.Windows.Forms.Padding(3);
            this.tpLeftSectionsContacts.Size = new System.Drawing.Size(238, 450);
            this.tpLeftSectionsContacts.TabIndex = 1;
            this.tpLeftSectionsContacts.Text = "Contacts";
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
            this.dgvContacts.Size = new System.Drawing.Size(232, 444);
            this.dgvContacts.TabIndex = 0;
            this.dgvContacts.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvContacts_CellContextMenuStripNeeded);
            this.dgvContacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContacts_CellDoubleClick);
            this.dgvContacts.SelectionChanged += new System.EventHandler(this.dgvContacts_SelectionChanged);
            // 
            // mbUserInfoStatus
            // 
            this.mbUserInfoStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
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
            // separator3
            // 
            this.separator3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separator3.Location = new System.Drawing.Point(11, 28);
            this.separator3.Name = "separator3";
            this.separator3.Size = new System.Drawing.Size(627, 2);
            this.separator3.TabIndex = 5;
            // 
            // separator1
            // 
            this.separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separator1.Location = new System.Drawing.Point(11, 28);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(627, 2);
            this.separator1.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(899, 579);
            this.Controls.Add(this.scSections);
            this.Controls.Add(this.tsMenuBar);
            this.MinimumSize = new System.Drawing.Size(816, 463);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tsMenuBar.ResumeLayout(false);
            this.tsMenuBar.PerformLayout();
            this.cmsTray.ResumeLayout(false);
            this.scSections.Panel1.ResumeLayout(false);
            this.scSections.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scSections)).EndInit();
            this.scSections.ResumeLayout(false);
            this.pUserInfo.ResumeLayout(false);
            this.pUserInfo.PerformLayout();
            this.cmsUserInfoStatus.ResumeLayout(false);
            this.pMessaging.ResumeLayout(false);
            this.pMessaging.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMessagingContactStatus)).EndInit();
            this.pPintoNews.ResumeLayout(false);
            this.pPintoNews.PerformLayout();
            this.tcLeftSections.ResumeLayout(false);
            this.tpLeftSectionsContacts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public PintoNS.JacksonTabControl.CustomTabControl tcLeftSections;
        public System.Windows.Forms.ImageList ilTabImages;
        public System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarPinto;
        public System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarHelp;
        public System.Windows.Forms.TabPage tpLeftSectionsContacts;
        public System.Windows.Forms.DataGridView dgvContacts;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarPintoLogOff;
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
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarPintoExit;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarHelpCheckForUpdates;
        public System.Windows.Forms.ToolStripDropDownButton tsddbMenuBarTools;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarToolsAddContact;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarToolsRemoveContact;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarPintoOptions;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarPintoChangeStatus;
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
        private System.Windows.Forms.Timer tConnectingTray;
        private System.Windows.Forms.ToolStripMenuItem tsmiUserInfoStatusOffline;
        public System.Windows.Forms.ToolStripMenuItem tsmiTrayChangeStatusOffline;
        public System.Windows.Forms.ToolStripMenuItem tsmiMenuBarFileChangeStatusOffline;
        private Separator separator2;
        private System.Windows.Forms.Panel pPintoNews;
        private Separator separator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser wbPintoNews;
        private System.Windows.Forms.Panel pMessaging;
        private Separator separator3;
        private System.Windows.Forms.Label lMessagingContactName;
        private System.Windows.Forms.PictureBox pbMessagingContactStatus;
    }
}