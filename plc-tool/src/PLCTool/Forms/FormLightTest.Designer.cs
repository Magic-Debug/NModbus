namespace PLCTool.Forms
{
    partial class FormLightTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLightTest));
            this.pnlPorts = new System.Windows.Forms.Panel();
            this.btnOpen = new PLCTool.UC.ButtonRadiusEx();
            this.gbxPortsScan = new System.Windows.Forms.GroupBox();
            this.btnRefreshPortsList = new PLCTool.UC.ButtonRadiusEx();
            this.gbxPortList = new System.Windows.Forms.GroupBox();
            this.lbxPorts = new System.Windows.Forms.ListBox();
            this.nudDeviceIdOrAddressTo = new System.Windows.Forms.NumericUpDown();
            this.lblDeviceIdOrAddressTo = new System.Windows.Forms.Label();
            this.lblDeviceIdOrAddressFrom = new System.Windows.Forms.Label();
            this.nudDeviceIdOrAddressFrom = new System.Windows.Forms.NumericUpDown();
            this.chkOnlyDisplayMayConnectCom = new System.Windows.Forms.CheckBox();
            this.cbxDeviceType = new System.Windows.Forms.ComboBox();
            this.nudDelayTimeForReceive = new System.Windows.Forms.NumericUpDown();
            this.lblDelayTimeForReceive = new System.Windows.Forms.Label();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.pbRefreshProgress = new System.Windows.Forms.ProgressBar();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.lblDeviceType = new System.Windows.Forms.Label();
            this.gbxLightOperate = new System.Windows.Forms.GroupBox();
            this.lblBrightnessValue = new System.Windows.Forms.Label();
            this.pnlChannel = new System.Windows.Forms.Panel();
            this.nudChannel = new System.Windows.Forms.NumericUpDown();
            this.cmbKSDChannel = new System.Windows.Forms.ComboBox();
            this.lblChannel = new System.Windows.Forms.Label();
            this.cmbChannelOperateWide = new System.Windows.Forms.ComboBox();
            this.pnlKangShiDa = new System.Windows.Forms.Panel();
            this.btnSetChannelAlwaysStatus = new PLCTool.UC.ButtonRadiusEx();
            this.cmbChannelAwalysStatus = new System.Windows.Forms.ComboBox();
            this.lblOpenOrCloseStatus = new System.Windows.Forms.Label();
            this.btnReadChannelAlwaysStatus = new PLCTool.UC.ButtonRadiusEx();
            this.lblChannelOperateWide = new System.Windows.Forms.Label();
            this.pnlOperate = new System.Windows.Forms.Panel();
            this.btnReadChannelInfo = new PLCTool.UC.ButtonRadiusEx();
            this.lblNewIDOrAddress = new System.Windows.Forms.Label();
            this.nudNewIDOrAddress = new System.Windows.Forms.NumericUpDown();
            this.btnNewIDOrAddress = new PLCTool.UC.ButtonRadiusEx();
            this.lblSetBrightness = new System.Windows.Forms.Label();
            this.tbSetBrightness = new System.Windows.Forms.TrackBar();
            this.pnlLogs = new System.Windows.Forms.Panel();
            this.btnClearLog = new PLCTool.UC.ButtonRadiusEx();
            this.lbxLog = new System.Windows.Forms.ListBox();
            this.pnlPorts.SuspendLayout();
            this.gbxPortsScan.SuspendLayout();
            this.gbxPortList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeviceIdOrAddressTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeviceIdOrAddressFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayTimeForReceive)).BeginInit();
            this.gbxLightOperate.SuspendLayout();
            this.pnlChannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannel)).BeginInit();
            this.pnlKangShiDa.SuspendLayout();
            this.pnlOperate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewIDOrAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSetBrightness)).BeginInit();
            this.pnlLogs.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPorts
            // 
            this.pnlPorts.Controls.Add(this.btnOpen);
            this.pnlPorts.Controls.Add(this.gbxPortsScan);
            this.pnlPorts.Controls.Add(this.gbxLightOperate);
            resources.ApplyResources(this.pnlPorts, "pnlPorts");
            this.pnlPorts.Name = "pnlPorts";
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.BorderColor = System.Drawing.Color.White;
            this.btnOpen.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnOpen.DrawBorder = true;
            this.btnOpen.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOpen.ForeColor = System.Drawing.Color.White;
            this.btnOpen.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnOpen.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // gbxPortsScan
            // 
            resources.ApplyResources(this.gbxPortsScan, "gbxPortsScan");
            this.gbxPortsScan.Controls.Add(this.btnRefreshPortsList);
            this.gbxPortsScan.Controls.Add(this.gbxPortList);
            this.gbxPortsScan.Controls.Add(this.nudDeviceIdOrAddressTo);
            this.gbxPortsScan.Controls.Add(this.lblDeviceIdOrAddressTo);
            this.gbxPortsScan.Controls.Add(this.lblDeviceIdOrAddressFrom);
            this.gbxPortsScan.Controls.Add(this.nudDeviceIdOrAddressFrom);
            this.gbxPortsScan.Controls.Add(this.chkOnlyDisplayMayConnectCom);
            this.gbxPortsScan.Controls.Add(this.cbxDeviceType);
            this.gbxPortsScan.Controls.Add(this.nudDelayTimeForReceive);
            this.gbxPortsScan.Controls.Add(this.lblDelayTimeForReceive);
            this.gbxPortsScan.Controls.Add(this.cbxBaudRate);
            this.gbxPortsScan.Controls.Add(this.pbRefreshProgress);
            this.gbxPortsScan.Controls.Add(this.lblBaudRate);
            this.gbxPortsScan.Controls.Add(this.lblDeviceType);
            this.gbxPortsScan.Name = "gbxPortsScan";
            this.gbxPortsScan.TabStop = false;
            // 
            // btnRefreshPortsList
            // 
            this.btnRefreshPortsList.BorderColor = System.Drawing.Color.White;
            this.btnRefreshPortsList.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnRefreshPortsList.DrawBorder = true;
            this.btnRefreshPortsList.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnRefreshPortsList, "btnRefreshPortsList");
            this.btnRefreshPortsList.ForeColor = System.Drawing.Color.White;
            this.btnRefreshPortsList.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnRefreshPortsList.Name = "btnRefreshPortsList";
            this.btnRefreshPortsList.PressColor = System.Drawing.Color.Black;
            this.btnRefreshPortsList.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnRefreshPortsList.UseVisualStyleBackColor = true;
            this.btnRefreshPortsList.Click += new System.EventHandler(this.btnRefreshPortsList_Click);
            // 
            // gbxPortList
            // 
            resources.ApplyResources(this.gbxPortList, "gbxPortList");
            this.gbxPortList.Controls.Add(this.lbxPorts);
            this.gbxPortList.Name = "gbxPortList";
            this.gbxPortList.TabStop = false;
            // 
            // lbxPorts
            // 
            resources.ApplyResources(this.lbxPorts, "lbxPorts");
            this.lbxPorts.BackColor = System.Drawing.SystemColors.Window;
            this.lbxPorts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbxPorts.FormattingEnabled = true;
            this.lbxPorts.Name = "lbxPorts";
            // 
            // nudDeviceIdOrAddressTo
            // 
            resources.ApplyResources(this.nudDeviceIdOrAddressTo, "nudDeviceIdOrAddressTo");
            this.nudDeviceIdOrAddressTo.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudDeviceIdOrAddressTo.Name = "nudDeviceIdOrAddressTo";
            this.nudDeviceIdOrAddressTo.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblDeviceIdOrAddressTo
            // 
            resources.ApplyResources(this.lblDeviceIdOrAddressTo, "lblDeviceIdOrAddressTo");
            this.lblDeviceIdOrAddressTo.Name = "lblDeviceIdOrAddressTo";
            // 
            // lblDeviceIdOrAddressFrom
            // 
            resources.ApplyResources(this.lblDeviceIdOrAddressFrom, "lblDeviceIdOrAddressFrom");
            this.lblDeviceIdOrAddressFrom.Name = "lblDeviceIdOrAddressFrom";
            // 
            // nudDeviceIdOrAddressFrom
            // 
            resources.ApplyResources(this.nudDeviceIdOrAddressFrom, "nudDeviceIdOrAddressFrom");
            this.nudDeviceIdOrAddressFrom.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudDeviceIdOrAddressFrom.Name = "nudDeviceIdOrAddressFrom";
            // 
            // chkOnlyDisplayMayConnectCom
            // 
            resources.ApplyResources(this.chkOnlyDisplayMayConnectCom, "chkOnlyDisplayMayConnectCom");
            this.chkOnlyDisplayMayConnectCom.Checked = true;
            this.chkOnlyDisplayMayConnectCom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnlyDisplayMayConnectCom.Name = "chkOnlyDisplayMayConnectCom";
            this.chkOnlyDisplayMayConnectCom.UseVisualStyleBackColor = true;
            // 
            // cbxDeviceType
            // 
            this.cbxDeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDeviceType.FormattingEnabled = true;
            this.cbxDeviceType.Items.AddRange(new object[] {
            resources.GetString("cbxDeviceType.Items"),
            resources.GetString("cbxDeviceType.Items1"),
            resources.GetString("cbxDeviceType.Items2"),
            resources.GetString("cbxDeviceType.Items3")});
            resources.ApplyResources(this.cbxDeviceType, "cbxDeviceType");
            this.cbxDeviceType.Name = "cbxDeviceType";
            this.cbxDeviceType.SelectedIndexChanged += new System.EventHandler(this.cbxDeviceType_SelectedIndexChanged);
            // 
            // nudDelayTimeForReceive
            // 
            resources.ApplyResources(this.nudDelayTimeForReceive, "nudDelayTimeForReceive");
            this.nudDelayTimeForReceive.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudDelayTimeForReceive.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDelayTimeForReceive.Name = "nudDelayTimeForReceive";
            this.nudDelayTimeForReceive.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lblDelayTimeForReceive
            // 
            resources.ApplyResources(this.lblDelayTimeForReceive, "lblDelayTimeForReceive");
            this.lblDelayTimeForReceive.Name = "lblDelayTimeForReceive";
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Items.AddRange(new object[] {
            resources.GetString("cbxBaudRate.Items"),
            resources.GetString("cbxBaudRate.Items1"),
            resources.GetString("cbxBaudRate.Items2"),
            resources.GetString("cbxBaudRate.Items3"),
            resources.GetString("cbxBaudRate.Items4"),
            resources.GetString("cbxBaudRate.Items5")});
            resources.ApplyResources(this.cbxBaudRate, "cbxBaudRate");
            this.cbxBaudRate.Name = "cbxBaudRate";
            // 
            // pbRefreshProgress
            // 
            resources.ApplyResources(this.pbRefreshProgress, "pbRefreshProgress");
            this.pbRefreshProgress.Name = "pbRefreshProgress";
            // 
            // lblBaudRate
            // 
            resources.ApplyResources(this.lblBaudRate, "lblBaudRate");
            this.lblBaudRate.Name = "lblBaudRate";
            // 
            // lblDeviceType
            // 
            resources.ApplyResources(this.lblDeviceType, "lblDeviceType");
            this.lblDeviceType.Name = "lblDeviceType";
            // 
            // gbxLightOperate
            // 
            resources.ApplyResources(this.gbxLightOperate, "gbxLightOperate");
            this.gbxLightOperate.Controls.Add(this.lblBrightnessValue);
            this.gbxLightOperate.Controls.Add(this.pnlChannel);
            this.gbxLightOperate.Controls.Add(this.cmbChannelOperateWide);
            this.gbxLightOperate.Controls.Add(this.pnlKangShiDa);
            this.gbxLightOperate.Controls.Add(this.lblChannelOperateWide);
            this.gbxLightOperate.Controls.Add(this.pnlOperate);
            this.gbxLightOperate.Controls.Add(this.lblSetBrightness);
            this.gbxLightOperate.Controls.Add(this.tbSetBrightness);
            this.gbxLightOperate.Name = "gbxLightOperate";
            this.gbxLightOperate.TabStop = false;
            // 
            // lblBrightnessValue
            // 
            resources.ApplyResources(this.lblBrightnessValue, "lblBrightnessValue");
            this.lblBrightnessValue.Name = "lblBrightnessValue";
            // 
            // pnlChannel
            // 
            this.pnlChannel.Controls.Add(this.nudChannel);
            this.pnlChannel.Controls.Add(this.cmbKSDChannel);
            this.pnlChannel.Controls.Add(this.lblChannel);
            resources.ApplyResources(this.pnlChannel, "pnlChannel");
            this.pnlChannel.Name = "pnlChannel";
            // 
            // nudChannel
            // 
            resources.ApplyResources(this.nudChannel, "nudChannel");
            this.nudChannel.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudChannel.Name = "nudChannel";
            // 
            // cmbKSDChannel
            // 
            this.cmbKSDChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKSDChannel.FormattingEnabled = true;
            this.cmbKSDChannel.Items.AddRange(new object[] {
            resources.GetString("cmbKSDChannel.Items"),
            resources.GetString("cmbKSDChannel.Items1")});
            resources.ApplyResources(this.cmbKSDChannel, "cmbKSDChannel");
            this.cmbKSDChannel.Name = "cmbKSDChannel";
            this.cmbKSDChannel.SelectedIndexChanged += new System.EventHandler(this.cmbKSDChannel_SelectedIndexChanged);
            // 
            // lblChannel
            // 
            resources.ApplyResources(this.lblChannel, "lblChannel");
            this.lblChannel.Name = "lblChannel";
            // 
            // cmbChannelOperateWide
            // 
            this.cmbChannelOperateWide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChannelOperateWide.FormattingEnabled = true;
            this.cmbChannelOperateWide.Items.AddRange(new object[] {
            resources.GetString("cmbChannelOperateWide.Items"),
            resources.GetString("cmbChannelOperateWide.Items1")});
            resources.ApplyResources(this.cmbChannelOperateWide, "cmbChannelOperateWide");
            this.cmbChannelOperateWide.Name = "cmbChannelOperateWide";
            this.cmbChannelOperateWide.SelectedIndexChanged += new System.EventHandler(this.cmbChannelOperateWide_SelectedIndexChanged);
            // 
            // pnlKangShiDa
            // 
            this.pnlKangShiDa.Controls.Add(this.btnSetChannelAlwaysStatus);
            this.pnlKangShiDa.Controls.Add(this.cmbChannelAwalysStatus);
            this.pnlKangShiDa.Controls.Add(this.lblOpenOrCloseStatus);
            this.pnlKangShiDa.Controls.Add(this.btnReadChannelAlwaysStatus);
            resources.ApplyResources(this.pnlKangShiDa, "pnlKangShiDa");
            this.pnlKangShiDa.Name = "pnlKangShiDa";
            // 
            // btnSetChannelAlwaysStatus
            // 
            this.btnSetChannelAlwaysStatus.BorderColor = System.Drawing.Color.White;
            this.btnSetChannelAlwaysStatus.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnSetChannelAlwaysStatus.DrawBorder = true;
            this.btnSetChannelAlwaysStatus.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnSetChannelAlwaysStatus, "btnSetChannelAlwaysStatus");
            this.btnSetChannelAlwaysStatus.ForeColor = System.Drawing.Color.White;
            this.btnSetChannelAlwaysStatus.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnSetChannelAlwaysStatus.Name = "btnSetChannelAlwaysStatus";
            this.btnSetChannelAlwaysStatus.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnSetChannelAlwaysStatus.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnSetChannelAlwaysStatus.UseVisualStyleBackColor = true;
            this.btnSetChannelAlwaysStatus.Click += new System.EventHandler(this.btnSetChannelAlwaysStatus_Click);
            // 
            // cmbChannelAwalysStatus
            // 
            this.cmbChannelAwalysStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChannelAwalysStatus.FormattingEnabled = true;
            this.cmbChannelAwalysStatus.Items.AddRange(new object[] {
            resources.GetString("cmbChannelAwalysStatus.Items"),
            resources.GetString("cmbChannelAwalysStatus.Items1")});
            resources.ApplyResources(this.cmbChannelAwalysStatus, "cmbChannelAwalysStatus");
            this.cmbChannelAwalysStatus.Name = "cmbChannelAwalysStatus";
            // 
            // lblOpenOrCloseStatus
            // 
            resources.ApplyResources(this.lblOpenOrCloseStatus, "lblOpenOrCloseStatus");
            this.lblOpenOrCloseStatus.Name = "lblOpenOrCloseStatus";
            // 
            // btnReadChannelAlwaysStatus
            // 
            this.btnReadChannelAlwaysStatus.BorderColor = System.Drawing.Color.White;
            this.btnReadChannelAlwaysStatus.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnReadChannelAlwaysStatus.DrawBorder = true;
            this.btnReadChannelAlwaysStatus.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnReadChannelAlwaysStatus, "btnReadChannelAlwaysStatus");
            this.btnReadChannelAlwaysStatus.ForeColor = System.Drawing.Color.White;
            this.btnReadChannelAlwaysStatus.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnReadChannelAlwaysStatus.Name = "btnReadChannelAlwaysStatus";
            this.btnReadChannelAlwaysStatus.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnReadChannelAlwaysStatus.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnReadChannelAlwaysStatus.UseVisualStyleBackColor = true;
            this.btnReadChannelAlwaysStatus.Click += new System.EventHandler(this.btnReadChannelAlwaysStatus_Click);
            // 
            // lblChannelOperateWide
            // 
            resources.ApplyResources(this.lblChannelOperateWide, "lblChannelOperateWide");
            this.lblChannelOperateWide.Name = "lblChannelOperateWide";
            // 
            // pnlOperate
            // 
            this.pnlOperate.Controls.Add(this.btnReadChannelInfo);
            this.pnlOperate.Controls.Add(this.lblNewIDOrAddress);
            this.pnlOperate.Controls.Add(this.nudNewIDOrAddress);
            this.pnlOperate.Controls.Add(this.btnNewIDOrAddress);
            resources.ApplyResources(this.pnlOperate, "pnlOperate");
            this.pnlOperate.Name = "pnlOperate";
            // 
            // btnReadChannelInfo
            // 
            this.btnReadChannelInfo.BorderColor = System.Drawing.Color.White;
            this.btnReadChannelInfo.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnReadChannelInfo.DrawBorder = true;
            this.btnReadChannelInfo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnReadChannelInfo, "btnReadChannelInfo");
            this.btnReadChannelInfo.ForeColor = System.Drawing.Color.White;
            this.btnReadChannelInfo.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnReadChannelInfo.Name = "btnReadChannelInfo";
            this.btnReadChannelInfo.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnReadChannelInfo.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnReadChannelInfo.UseVisualStyleBackColor = true;
            this.btnReadChannelInfo.Click += new System.EventHandler(this.btnReadChannelInfo_Click);
            // 
            // lblNewIDOrAddress
            // 
            resources.ApplyResources(this.lblNewIDOrAddress, "lblNewIDOrAddress");
            this.lblNewIDOrAddress.Name = "lblNewIDOrAddress";
            // 
            // nudNewIDOrAddress
            // 
            this.nudNewIDOrAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.nudNewIDOrAddress, "nudNewIDOrAddress");
            this.nudNewIDOrAddress.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudNewIDOrAddress.Name = "nudNewIDOrAddress";
            // 
            // btnNewIDOrAddress
            // 
            this.btnNewIDOrAddress.BorderColor = System.Drawing.Color.White;
            this.btnNewIDOrAddress.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnNewIDOrAddress.DrawBorder = true;
            this.btnNewIDOrAddress.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnNewIDOrAddress, "btnNewIDOrAddress");
            this.btnNewIDOrAddress.ForeColor = System.Drawing.Color.White;
            this.btnNewIDOrAddress.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnNewIDOrAddress.Name = "btnNewIDOrAddress";
            this.btnNewIDOrAddress.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnNewIDOrAddress.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnNewIDOrAddress.UseVisualStyleBackColor = true;
            this.btnNewIDOrAddress.Click += new System.EventHandler(this.btnNewIDOrAddress_Click);
            // 
            // lblSetBrightness
            // 
            resources.ApplyResources(this.lblSetBrightness, "lblSetBrightness");
            this.lblSetBrightness.Name = "lblSetBrightness";
            // 
            // tbSetBrightness
            // 
            resources.ApplyResources(this.tbSetBrightness, "tbSetBrightness");
            this.tbSetBrightness.LargeChange = 10;
            this.tbSetBrightness.Maximum = 255;
            this.tbSetBrightness.Name = "tbSetBrightness";
            this.tbSetBrightness.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbSetBrightness.Scroll += new System.EventHandler(this.tbSetBrightness_Scroll);
            this.tbSetBrightness.ValueChanged += new System.EventHandler(this.tbSetBrightness_ValueChanged);
            // 
            // pnlLogs
            // 
            this.pnlLogs.Controls.Add(this.btnClearLog);
            this.pnlLogs.Controls.Add(this.lbxLog);
            resources.ApplyResources(this.pnlLogs, "pnlLogs");
            this.pnlLogs.Name = "pnlLogs";
            // 
            // btnClearLog
            // 
            resources.ApplyResources(this.btnClearLog, "btnClearLog");
            this.btnClearLog.BorderColor = System.Drawing.Color.White;
            this.btnClearLog.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnClearLog.DrawBorder = true;
            this.btnClearLog.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClearLog.ForeColor = System.Drawing.Color.White;
            this.btnClearLog.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnClearLog.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // lbxLog
            // 
            resources.ApplyResources(this.lbxLog, "lbxLog");
            this.lbxLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.lbxLog.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbxLog.ForeColor = System.Drawing.Color.Lime;
            this.lbxLog.FormattingEnabled = true;
            this.lbxLog.Name = "lbxLog";
            this.lbxLog.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxLog_DrawItem);
            // 
            // FormLightTest
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLogs);
            this.Controls.Add(this.pnlPorts);
            this.Name = "FormLightTest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLightTest_FormClosing);
            this.Load += new System.EventHandler(this.FormLightTest_Load);
            this.Controls.SetChildIndex(this.pnlPorts, 0);
            this.Controls.SetChildIndex(this.pnlLogs, 0);
            this.pnlPorts.ResumeLayout(false);
            this.gbxPortsScan.ResumeLayout(false);
            this.gbxPortsScan.PerformLayout();
            this.gbxPortList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDeviceIdOrAddressTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeviceIdOrAddressFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayTimeForReceive)).EndInit();
            this.gbxLightOperate.ResumeLayout(false);
            this.gbxLightOperate.PerformLayout();
            this.pnlChannel.ResumeLayout(false);
            this.pnlChannel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannel)).EndInit();
            this.pnlKangShiDa.ResumeLayout(false);
            this.pnlKangShiDa.PerformLayout();
            this.pnlOperate.ResumeLayout(false);
            this.pnlOperate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewIDOrAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSetBrightness)).EndInit();
            this.pnlLogs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPorts;
        private System.Windows.Forms.GroupBox gbxPortsScan;
        private System.Windows.Forms.NumericUpDown nudDeviceIdOrAddressTo;
        private System.Windows.Forms.Label lblDeviceIdOrAddressTo;
        private System.Windows.Forms.Label lblDeviceIdOrAddressFrom;
        private System.Windows.Forms.NumericUpDown nudDeviceIdOrAddressFrom;
        private System.Windows.Forms.CheckBox chkOnlyDisplayMayConnectCom;
        private System.Windows.Forms.ComboBox cbxDeviceType;
        private System.Windows.Forms.NumericUpDown nudDelayTimeForReceive;
        private System.Windows.Forms.ListBox lbxPorts;
        private System.Windows.Forms.Label lblDelayTimeForReceive;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.ProgressBar pbRefreshProgress;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Label lblDeviceType;
        private System.Windows.Forms.GroupBox gbxLightOperate;
        private System.Windows.Forms.Label lblNewIDOrAddress;
        private System.Windows.Forms.NumericUpDown nudNewIDOrAddress;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Label lblSetBrightness;
        private System.Windows.Forms.TrackBar tbSetBrightness;
        private System.Windows.Forms.Panel pnlLogs;
        private System.Windows.Forms.ListBox lbxLog;
        private System.Windows.Forms.GroupBox gbxPortList;
        private UC.ButtonRadiusEx btnOpen;
        private UC.ButtonRadiusEx btnRefreshPortsList;
        private UC.ButtonRadiusEx btnNewIDOrAddress;
        private UC.ButtonRadiusEx btnReadChannelInfo;
        private UC.ButtonRadiusEx btnClearLog;
        private System.Windows.Forms.Panel pnlKangShiDa;
        private UC.ButtonRadiusEx btnSetChannelAlwaysStatus;
        private System.Windows.Forms.ComboBox cmbChannelAwalysStatus;
        private System.Windows.Forms.Label lblOpenOrCloseStatus;
        private UC.ButtonRadiusEx btnReadChannelAlwaysStatus;
        private System.Windows.Forms.Panel pnlOperate;
        private System.Windows.Forms.ComboBox cmbChannelOperateWide;
        private System.Windows.Forms.Label lblChannelOperateWide;
        private System.Windows.Forms.NumericUpDown nudChannel;
        private System.Windows.Forms.ComboBox cmbKSDChannel;
        private System.Windows.Forms.Panel pnlChannel;
        private System.Windows.Forms.Label lblBrightnessValue;
    }
}