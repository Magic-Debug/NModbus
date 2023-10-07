namespace PLCTool.Forms
{
    partial class FormColourDifference
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormColourDifference));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.standardsListView = new System.Windows.Forms.ListView();
            this.standardsListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uploadSampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStandardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllStandardsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearStandardListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.samplesListView = new System.Windows.Forms.ListView();
            this.samplesListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSelectedSampleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllSamplesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearSampleListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devInfoListView = new System.Windows.Forms.ListView();
            this.itemColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBoxConnectionMethods = new System.Windows.Forms.ComboBox();
            this.buttonConnect = new PLCTool.UC.ButtonRadiusEx();
            this.buttonGetDevInfo = new PLCTool.UC.ButtonRadiusEx();
            this.buttonGetStatus = new PLCTool.UC.ButtonRadiusEx();
            this.btnTest = new PLCTool.UC.ButtonRadiusEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.instrumentStatusListView = new System.Windows.Forms.ListView();
            this.columnHeaderItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonMeasureSample = new PLCTool.UC.ButtonRadiusEx();
            this.buttonMeasureStandard = new PLCTool.UC.ButtonRadiusEx();
            this.buttonCalibrateWhite = new PLCTool.UC.ButtonRadiusEx();
            this.measureResultListView = new System.Windows.Forms.ListView();
            this.measureResultListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standardObserverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standardIlluminantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearResultListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonCalibrateBlack = new PLCTool.UC.ButtonRadiusEx();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.standardIndeicesComboBox = new System.Windows.Forms.ComboBox();
            this.buttonUploadStandard = new PLCTool.UC.ButtonRadiusEx();
            this.buttonWriteStandard = new PLCTool.UC.ButtonRadiusEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.standardsListViewContextMenuStrip.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.samplesListViewContextMenuStrip.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.measureResultListContextMenuStrip.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox5);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.standardsListView);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // standardsListView
            // 
            this.standardsListView.ContextMenuStrip = this.standardsListViewContextMenuStrip;
            resources.ApplyResources(this.standardsListView, "standardsListView");
            this.standardsListView.FullRowSelect = true;
            this.standardsListView.GridLines = true;
            this.standardsListView.HideSelection = false;
            this.standardsListView.MultiSelect = false;
            this.standardsListView.Name = "standardsListView";
            this.standardsListView.UseCompatibleStateImageBehavior = false;
            this.standardsListView.View = System.Windows.Forms.View.Details;
            this.standardsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.standardsListView_MouseDoubleClick);
            // 
            // standardsListViewContextMenuStrip
            // 
            this.standardsListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadSampleToolStripMenuItem,
            this.deleteStandardMenuItem,
            this.deleteAllStandardsMenuItem,
            this.toolStripSeparator1,
            this.clearStandardListMenuItem});
            this.standardsListViewContextMenuStrip.Name = "standardsListViewContextMenuStrip";
            resources.ApplyResources(this.standardsListViewContextMenuStrip, "standardsListViewContextMenuStrip");
            this.standardsListViewContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.standardsListViewContextMenuStrip_Opening);
            // 
            // uploadSampleToolStripMenuItem
            // 
            this.uploadSampleToolStripMenuItem.Name = "uploadSampleToolStripMenuItem";
            resources.ApplyResources(this.uploadSampleToolStripMenuItem, "uploadSampleToolStripMenuItem");
            // 
            // deleteStandardMenuItem
            // 
            this.deleteStandardMenuItem.Name = "deleteStandardMenuItem";
            resources.ApplyResources(this.deleteStandardMenuItem, "deleteStandardMenuItem");
            this.deleteStandardMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // deleteAllStandardsMenuItem
            // 
            this.deleteAllStandardsMenuItem.Name = "deleteAllStandardsMenuItem";
            resources.ApplyResources(this.deleteAllStandardsMenuItem, "deleteAllStandardsMenuItem");
            this.deleteAllStandardsMenuItem.Click += new System.EventHandler(this.deleteAllRecordsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // clearStandardListMenuItem
            // 
            this.clearStandardListMenuItem.Name = "clearStandardListMenuItem";
            resources.ApplyResources(this.clearStandardListMenuItem, "clearStandardListMenuItem");
            this.clearStandardListMenuItem.Click += new System.EventHandler(this.clearListToolStripMenuItem_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.samplesListView);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // samplesListView
            // 
            this.samplesListView.ContextMenuStrip = this.samplesListViewContextMenuStrip;
            resources.ApplyResources(this.samplesListView, "samplesListView");
            this.samplesListView.FullRowSelect = true;
            this.samplesListView.GridLines = true;
            this.samplesListView.HideSelection = false;
            this.samplesListView.Name = "samplesListView";
            this.samplesListView.UseCompatibleStateImageBehavior = false;
            this.samplesListView.View = System.Windows.Forms.View.Details;
            // 
            // samplesListViewContextMenuStrip
            // 
            this.samplesListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSelectedSampleMenuItem,
            this.deleteAllSamplesMenuItem,
            this.toolStripSeparator2,
            this.clearSampleListMenuItem});
            this.samplesListViewContextMenuStrip.Name = "samplesListViewContextMenuStrip";
            resources.ApplyResources(this.samplesListViewContextMenuStrip, "samplesListViewContextMenuStrip");
            this.samplesListViewContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.samplesListViewContextMenuStrip_Opening);
            // 
            // deleteSelectedSampleMenuItem
            // 
            this.deleteSelectedSampleMenuItem.Name = "deleteSelectedSampleMenuItem";
            resources.ApplyResources(this.deleteSelectedSampleMenuItem, "deleteSelectedSampleMenuItem");
            // 
            // deleteAllSamplesMenuItem
            // 
            this.deleteAllSamplesMenuItem.Name = "deleteAllSamplesMenuItem";
            resources.ApplyResources(this.deleteAllSamplesMenuItem, "deleteAllSamplesMenuItem");
            this.deleteAllSamplesMenuItem.Click += new System.EventHandler(this.deleteAllSamplesFromInstrumentToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // clearSampleListMenuItem
            // 
            this.clearSampleListMenuItem.Name = "clearSampleListMenuItem";
            resources.ApplyResources(this.clearSampleListMenuItem, "clearSampleListMenuItem");
            this.clearSampleListMenuItem.Click += new System.EventHandler(this.clearSamplesListViewMenuItem_Click);
            // 
            // devInfoListView
            // 
            this.devInfoListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemColumn,
            this.columnValue});
            resources.ApplyResources(this.devInfoListView, "devInfoListView");
            this.devInfoListView.FullRowSelect = true;
            this.devInfoListView.GridLines = true;
            this.devInfoListView.HideSelection = false;
            this.devInfoListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("devInfoListView.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("devInfoListView.Items1"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("devInfoListView.Items2"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("devInfoListView.Items3"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("devInfoListView.Items4"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("devInfoListView.Items5")))});
            this.devInfoListView.Name = "devInfoListView";
            this.devInfoListView.UseCompatibleStateImageBehavior = false;
            this.devInfoListView.View = System.Windows.Forms.View.Details;
            // 
            // itemColumn
            // 
            resources.ApplyResources(this.itemColumn, "itemColumn");
            // 
            // columnValue
            // 
            resources.ApplyResources(this.columnValue, "columnValue");
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.flowLayoutPanel1.Controls.Add(this.comboBoxConnectionMethods);
            this.flowLayoutPanel1.Controls.Add(this.buttonConnect);
            this.flowLayoutPanel1.Controls.Add(this.buttonGetDevInfo);
            this.flowLayoutPanel1.Controls.Add(this.buttonGetStatus);
            this.flowLayoutPanel1.Controls.Add(this.btnTest);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // comboBoxConnectionMethods
            // 
            this.comboBoxConnectionMethods.FormattingEnabled = true;
            this.comboBoxConnectionMethods.Items.AddRange(new object[] {
            resources.GetString("comboBoxConnectionMethods.Items"),
            resources.GetString("comboBoxConnectionMethods.Items1")});
            resources.ApplyResources(this.comboBoxConnectionMethods, "comboBoxConnectionMethods");
            this.comboBoxConnectionMethods.Name = "comboBoxConnectionMethods";
            // 
            // buttonConnect
            // 
            this.buttonConnect.BorderColor = System.Drawing.Color.White;
            this.buttonConnect.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonConnect.DrawBorder = true;
            this.buttonConnect.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonConnect, "buttonConnect");
            this.buttonConnect.ForeColor = System.Drawing.Color.White;
            this.buttonConnect.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonConnect.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonGetDevInfo
            // 
            this.buttonGetDevInfo.BorderColor = System.Drawing.Color.White;
            this.buttonGetDevInfo.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonGetDevInfo.DrawBorder = true;
            this.buttonGetDevInfo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonGetDevInfo, "buttonGetDevInfo");
            this.buttonGetDevInfo.ForeColor = System.Drawing.Color.White;
            this.buttonGetDevInfo.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonGetDevInfo.Name = "buttonGetDevInfo";
            this.buttonGetDevInfo.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonGetDevInfo.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonGetDevInfo.UseVisualStyleBackColor = true;
            this.buttonGetDevInfo.Click += new System.EventHandler(this.buttonGetDevInfo_Click);
            // 
            // buttonGetStatus
            // 
            this.buttonGetStatus.BorderColor = System.Drawing.Color.White;
            this.buttonGetStatus.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonGetStatus.DrawBorder = true;
            this.buttonGetStatus.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonGetStatus, "buttonGetStatus");
            this.buttonGetStatus.ForeColor = System.Drawing.Color.White;
            this.buttonGetStatus.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonGetStatus.Name = "buttonGetStatus";
            this.buttonGetStatus.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonGetStatus.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonGetStatus.UseVisualStyleBackColor = true;
            this.buttonGetStatus.Click += new System.EventHandler(this.buttonGetStatus_Click);
            // 
            // btnTest
            // 
            this.btnTest.BorderColor = System.Drawing.Color.White;
            this.btnTest.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnTest.DrawBorder = true;
            this.btnTest.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.ForeColor = System.Drawing.Color.White;
            this.btnTest.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnTest.Name = "btnTest";
            this.btnTest.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnTest.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.devInfoListView);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.instrumentStatusListView);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // instrumentStatusListView
            // 
            this.instrumentStatusListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderItem,
            this.columnHeaderValue});
            resources.ApplyResources(this.instrumentStatusListView, "instrumentStatusListView");
            this.instrumentStatusListView.FullRowSelect = true;
            this.instrumentStatusListView.GridLines = true;
            this.instrumentStatusListView.HideSelection = false;
            this.instrumentStatusListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items1"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items2"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items3"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items4"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items5"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items6"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items7"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items8"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items9"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("instrumentStatusListView.Items10")))});
            this.instrumentStatusListView.Name = "instrumentStatusListView";
            this.instrumentStatusListView.UseCompatibleStateImageBehavior = false;
            this.instrumentStatusListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderItem
            // 
            resources.ApplyResources(this.columnHeaderItem, "columnHeaderItem");
            // 
            // columnHeaderValue
            // 
            resources.ApplyResources(this.columnHeaderValue, "columnHeaderValue");
            // 
            // groupBox6
            // 
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Controls.Add(this.tableLayoutPanel4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.buttonMeasureSample, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.buttonMeasureStandard, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.buttonCalibrateWhite, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.measureResultListView, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.buttonCalibrateBlack, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // buttonMeasureSample
            // 
            this.buttonMeasureSample.BorderColor = System.Drawing.Color.White;
            this.buttonMeasureSample.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonMeasureSample.DrawBorder = true;
            this.buttonMeasureSample.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonMeasureSample, "buttonMeasureSample");
            this.buttonMeasureSample.ForeColor = System.Drawing.Color.White;
            this.buttonMeasureSample.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonMeasureSample.Name = "buttonMeasureSample";
            this.buttonMeasureSample.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonMeasureSample.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonMeasureSample.UseVisualStyleBackColor = true;
            this.buttonMeasureSample.Click += new System.EventHandler(this.buttonMeasureSample_Click);
            // 
            // buttonMeasureStandard
            // 
            this.buttonMeasureStandard.BorderColor = System.Drawing.Color.White;
            this.buttonMeasureStandard.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonMeasureStandard.DrawBorder = true;
            this.buttonMeasureStandard.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonMeasureStandard, "buttonMeasureStandard");
            this.buttonMeasureStandard.ForeColor = System.Drawing.Color.White;
            this.buttonMeasureStandard.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonMeasureStandard.Name = "buttonMeasureStandard";
            this.buttonMeasureStandard.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonMeasureStandard.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonMeasureStandard.UseVisualStyleBackColor = true;
            this.buttonMeasureStandard.Click += new System.EventHandler(this.buttonMeasureStandard_Click);
            // 
            // buttonCalibrateWhite
            // 
            this.buttonCalibrateWhite.BorderColor = System.Drawing.Color.White;
            this.buttonCalibrateWhite.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonCalibrateWhite.DrawBorder = true;
            this.buttonCalibrateWhite.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonCalibrateWhite, "buttonCalibrateWhite");
            this.buttonCalibrateWhite.ForeColor = System.Drawing.Color.White;
            this.buttonCalibrateWhite.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonCalibrateWhite.Name = "buttonCalibrateWhite";
            this.buttonCalibrateWhite.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonCalibrateWhite.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonCalibrateWhite.UseVisualStyleBackColor = true;
            this.buttonCalibrateWhite.Click += new System.EventHandler(this.buttonCalibrateWhite_Click);
            // 
            // measureResultListView
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.measureResultListView, 2);
            this.measureResultListView.ContextMenuStrip = this.measureResultListContextMenuStrip;
            resources.ApplyResources(this.measureResultListView, "measureResultListView");
            this.measureResultListView.FullRowSelect = true;
            this.measureResultListView.GridLines = true;
            this.measureResultListView.HideSelection = false;
            this.measureResultListView.Name = "measureResultListView";
            this.measureResultListView.UseCompatibleStateImageBehavior = false;
            this.measureResultListView.View = System.Windows.Forms.View.Details;
            // 
            // measureResultListContextMenuStrip
            // 
            this.measureResultListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorSpaceToolStripMenuItem,
            this.standardObserverToolStripMenuItem,
            this.standardIlluminantToolStripMenuItem,
            this.clearResultListMenuItem,
            this.toolStripSeparator3});
            this.measureResultListContextMenuStrip.Name = "measureResultListContextMenuStrip";
            resources.ApplyResources(this.measureResultListContextMenuStrip, "measureResultListContextMenuStrip");
            // 
            // colorSpaceToolStripMenuItem
            // 
            this.colorSpaceToolStripMenuItem.Name = "colorSpaceToolStripMenuItem";
            resources.ApplyResources(this.colorSpaceToolStripMenuItem, "colorSpaceToolStripMenuItem");
            // 
            // standardObserverToolStripMenuItem
            // 
            this.standardObserverToolStripMenuItem.Name = "standardObserverToolStripMenuItem";
            resources.ApplyResources(this.standardObserverToolStripMenuItem, "standardObserverToolStripMenuItem");
            // 
            // standardIlluminantToolStripMenuItem
            // 
            this.standardIlluminantToolStripMenuItem.Name = "standardIlluminantToolStripMenuItem";
            resources.ApplyResources(this.standardIlluminantToolStripMenuItem, "standardIlluminantToolStripMenuItem");
            // 
            // clearResultListMenuItem
            // 
            this.clearResultListMenuItem.Name = "clearResultListMenuItem";
            resources.ApplyResources(this.clearResultListMenuItem, "clearResultListMenuItem");
            this.clearResultListMenuItem.Click += new System.EventHandler(this.clearResultListMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // buttonCalibrateBlack
            // 
            this.buttonCalibrateBlack.BorderColor = System.Drawing.Color.White;
            this.buttonCalibrateBlack.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonCalibrateBlack.DrawBorder = true;
            this.buttonCalibrateBlack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonCalibrateBlack, "buttonCalibrateBlack");
            this.buttonCalibrateBlack.ForeColor = System.Drawing.Color.White;
            this.buttonCalibrateBlack.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonCalibrateBlack.Name = "buttonCalibrateBlack";
            this.buttonCalibrateBlack.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonCalibrateBlack.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonCalibrateBlack.UseVisualStyleBackColor = true;
            this.buttonCalibrateBlack.Click += new System.EventHandler(this.buttonCalibrateBlack_Click);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.label1);
            this.flowLayoutPanel4.Controls.Add(this.standardIndeicesComboBox);
            this.flowLayoutPanel4.Controls.Add(this.buttonUploadStandard);
            this.flowLayoutPanel4.Controls.Add(this.buttonWriteStandard);
            resources.ApplyResources(this.flowLayoutPanel4, "flowLayoutPanel4");
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // standardIndeicesComboBox
            // 
            resources.ApplyResources(this.standardIndeicesComboBox, "standardIndeicesComboBox");
            this.standardIndeicesComboBox.FormattingEnabled = true;
            this.standardIndeicesComboBox.Name = "standardIndeicesComboBox";
            this.standardIndeicesComboBox.DropDown += new System.EventHandler(this.standardIndeicesComboBox_DropDown);
            // 
            // buttonUploadStandard
            // 
            this.buttonUploadStandard.BorderColor = System.Drawing.Color.White;
            this.buttonUploadStandard.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonUploadStandard.DrawBorder = true;
            this.buttonUploadStandard.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonUploadStandard, "buttonUploadStandard");
            this.buttonUploadStandard.ForeColor = System.Drawing.Color.White;
            this.buttonUploadStandard.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonUploadStandard.Name = "buttonUploadStandard";
            this.buttonUploadStandard.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonUploadStandard.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonUploadStandard.UseVisualStyleBackColor = true;
            this.buttonUploadStandard.Click += new System.EventHandler(this.buttonUploadStandard_Click);
            // 
            // buttonWriteStandard
            // 
            this.buttonWriteStandard.BorderColor = System.Drawing.Color.White;
            this.buttonWriteStandard.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonWriteStandard.DrawBorder = true;
            this.buttonWriteStandard.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonWriteStandard, "buttonWriteStandard");
            this.buttonWriteStandard.ForeColor = System.Drawing.Color.White;
            this.buttonWriteStandard.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonWriteStandard.Name = "buttonWriteStandard";
            this.buttonWriteStandard.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonWriteStandard.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonWriteStandard.UseVisualStyleBackColor = true;
            this.buttonWriteStandard.Click += new System.EventHandler(this.buttonWriteStandard_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.groupBox6, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // FormColourDifference
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormColourDifference";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.standardsListViewContextMenuStrip.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.samplesListViewContextMenuStrip.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.measureResultListContextMenuStrip.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView devInfoListView;
        private System.Windows.Forms.ColumnHeader itemColumn;
        private System.Windows.Forms.ColumnHeader columnValue;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView instrumentStatusListView;
        private System.Windows.Forms.ColumnHeader columnHeaderItem;
        private System.Windows.Forms.ColumnHeader columnHeaderValue;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox standardIndeicesComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView standardsListView;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListView samplesListView;
        private System.Windows.Forms.ContextMenuStrip standardsListViewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem uploadSampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteStandardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllStandardsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearStandardListMenuItem;
        private System.Windows.Forms.ContextMenuStrip samplesListViewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedSampleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllSamplesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem clearSampleListMenuItem;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListView measureResultListView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ContextMenuStrip measureResultListContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem colorSpaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem standardObserverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem standardIlluminantToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ComboBox comboBoxConnectionMethods;
        private UC.ButtonRadiusEx buttonConnect;
        private UC.ButtonRadiusEx buttonGetDevInfo;
        private UC.ButtonRadiusEx buttonGetStatus;
        private UC.ButtonRadiusEx buttonCalibrateWhite;
        private UC.ButtonRadiusEx buttonCalibrateBlack;
        private UC.ButtonRadiusEx buttonUploadStandard;
        private UC.ButtonRadiusEx buttonWriteStandard;
        private UC.ButtonRadiusEx buttonMeasureSample;
        private UC.ButtonRadiusEx buttonMeasureStandard;
        private UC.ButtonRadiusEx btnTest;
        private System.Windows.Forms.ToolStripMenuItem clearResultListMenuItem;
    }
}
