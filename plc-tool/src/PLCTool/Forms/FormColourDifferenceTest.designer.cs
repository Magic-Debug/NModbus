
namespace PLCTool.Forms
{
    partial class FormColourDifferenceTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormColourDifferenceTest));
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle16 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle17 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle18 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            this.buttonConnect = new PLCTool.UC.ButtonRadiusEx();
            this.txtName = new PLCTool.UC.TextBoxEx();
            this.label0 = new System.Windows.Forms.Label();
            this.txtPort = new PLCTool.UC.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxEx1 = new PLCTool.UC.GroupBoxEx(this.components);
            this.lbConnectState = new System.Windows.Forms.Label();
            this.dgvSample = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClear = new PLCTool.UC.ButtonRadiusEx();
            this.groupBoxEx4 = new PLCTool.UC.GroupBoxEx(this.components);
            this.btnMeasureSample = new PLCTool.UC.ButtonRadiusEx();
            this.btnMeasureStandard = new PLCTool.UC.ButtonRadiusEx();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgvStandard = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPositionX = new PLCTool.UC.TextBoxEx();
            this.rdoFinish = new System.Windows.Forms.RadioButton();
            this.rdoNotFinish = new System.Windows.Forms.RadioButton();
            this.lblFinishTicket = new System.Windows.Forms.Label();
            this.btnReturnZero = new PLCTool.UC.ButtonRadiusEx();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timerSetValue = new System.Windows.Forms.Timer(this.components);
            this.groupBoxEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSample)).BeginInit();
            this.groupBoxEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStandard)).BeginInit();
            this.SuspendLayout();
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
            // txtName
            // 
            this.txtName.AllowReturn = false;
            tTextBoxBorderRenderStyle16.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle16.LineWidth = 1F;
            this.txtName.BorderRenderStyle = tTextBoxBorderRenderStyle16;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // label0
            // 
            resources.ApplyResources(this.label0, "label0");
            this.label0.Name = "label0";
            // 
            // txtPort
            // 
            this.txtPort.AllowReturn = false;
            tTextBoxBorderRenderStyle17.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle17.LineWidth = 1F;
            this.txtPort.BorderRenderStyle = tTextBoxBorderRenderStyle17;
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtPort, "txtPort");
            this.txtPort.Name = "txtPort";
            this.txtPort.ReadOnly = true;
            this.txtPort.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx1.Controls.Add(this.buttonConnect);
            this.groupBoxEx1.Controls.Add(this.txtName);
            this.groupBoxEx1.Controls.Add(this.lbConnectState);
            this.groupBoxEx1.Controls.Add(this.label2);
            this.groupBoxEx1.Controls.Add(this.label0);
            this.groupBoxEx1.Controls.Add(this.txtPort);
            this.groupBoxEx1.Controls.Add(this.label1);
            this.groupBoxEx1.Fillet = 0;
            resources.ApplyResources(this.groupBoxEx1, "groupBoxEx1");
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.TabStop = false;
            // 
            // lbConnectState
            // 
            resources.ApplyResources(this.lbConnectState, "lbConnectState");
            this.lbConnectState.ForeColor = System.Drawing.Color.Red;
            this.lbConnectState.Name = "lbConnectState";
            // 
            // dgvSample
            // 
            this.dgvSample.AllowUserToAddRows = false;
            this.dgvSample.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgvSample, "dgvSample");
            this.dgvSample.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSample.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column5,
            this.Column10,
            this.Column6});
            this.dgvSample.Name = "dgvSample";
            this.dgvSample.ReadOnly = true;
            this.dgvSample.RowHeadersVisible = false;
            this.dgvSample.RowTemplate.Height = 23;
            // 
            // Column1
            // 
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column7
            // 
            resources.ApplyResources(this.Column7, "Column7");
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            resources.ApplyResources(this.Column8, "Column8");
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            resources.ApplyResources(this.Column9, "Column9");
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column5
            // 
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column10
            // 
            resources.ApplyResources(this.Column10, "Column10");
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column6
            // 
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // btnClear
            // 
            this.btnClear.BorderColor = System.Drawing.Color.White;
            this.btnClear.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnClear.DrawBorder = true;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnClear.Name = "btnClear";
            this.btnClear.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnClear.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupBoxEx4
            // 
            this.groupBoxEx4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx4.Controls.Add(this.rdoFinish);
            this.groupBoxEx4.Controls.Add(this.rdoNotFinish);
            this.groupBoxEx4.Controls.Add(this.lblFinishTicket);
            this.groupBoxEx4.Controls.Add(this.btnMeasureSample);
            this.groupBoxEx4.Controls.Add(this.btnMeasureStandard);
            this.groupBoxEx4.Controls.Add(this.btnReturnZero);
            this.groupBoxEx4.Controls.Add(this.btnClear);
            this.groupBoxEx4.Controls.Add(this.txtPositionX);
            this.groupBoxEx4.Controls.Add(this.label3);
            this.groupBoxEx4.Fillet = 0;
            resources.ApplyResources(this.groupBoxEx4, "groupBoxEx4");
            this.groupBoxEx4.Name = "groupBoxEx4";
            this.groupBoxEx4.TabStop = false;
            // 
            // btnMeasureSample
            // 
            this.btnMeasureSample.BorderColor = System.Drawing.Color.White;
            this.btnMeasureSample.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnMeasureSample.DrawBorder = true;
            this.btnMeasureSample.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnMeasureSample, "btnMeasureSample");
            this.btnMeasureSample.ForeColor = System.Drawing.Color.White;
            this.btnMeasureSample.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnMeasureSample.Name = "btnMeasureSample";
            this.btnMeasureSample.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnMeasureSample.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnMeasureSample.UseVisualStyleBackColor = true;
            this.btnMeasureSample.Click += new System.EventHandler(this.btnMeasureSample_Click);
            // 
            // btnMeasureStandard
            // 
            this.btnMeasureStandard.BorderColor = System.Drawing.Color.White;
            this.btnMeasureStandard.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnMeasureStandard.DrawBorder = true;
            this.btnMeasureStandard.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnMeasureStandard, "btnMeasureStandard");
            this.btnMeasureStandard.ForeColor = System.Drawing.Color.White;
            this.btnMeasureStandard.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnMeasureStandard.Name = "btnMeasureStandard";
            this.btnMeasureStandard.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnMeasureStandard.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnMeasureStandard.UseVisualStyleBackColor = true;
            this.btnMeasureStandard.Click += new System.EventHandler(this.btnMeasureStandard_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dgvStandard
            // 
            this.dgvStandard.AllowUserToAddRows = false;
            this.dgvStandard.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgvStandard, "dgvStandard");
            this.dgvStandard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStandard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvStandard.Name = "dgvStandard";
            this.dgvStandard.ReadOnly = true;
            this.dgvStandard.RowHeadersVisible = false;
            this.dgvStandard.RowTemplate.Height = 23;
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtPositionX
            // 
            this.txtPositionX.AllowReturn = false;
            tTextBoxBorderRenderStyle18.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle18.LineWidth = 1F;
            this.txtPositionX.BorderRenderStyle = tTextBoxBorderRenderStyle18;
            this.txtPositionX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtPositionX, "txtPositionX");
            this.txtPositionX.Name = "txtPositionX";
            this.txtPositionX.TextMargin = new System.Windows.Forms.Padding(1);
            this.txtPositionX.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPositionX_KeyUp);
            this.txtPositionX.Leave += new System.EventHandler(this.txtPositionX_Leave);
            // 
            // rdoFinish
            // 
            resources.ApplyResources(this.rdoFinish, "rdoFinish");
            this.rdoFinish.Name = "rdoFinish";
            this.rdoFinish.TabStop = true;
            this.rdoFinish.UseVisualStyleBackColor = true;
            // 
            // rdoNotFinish
            // 
            resources.ApplyResources(this.rdoNotFinish, "rdoNotFinish");
            this.rdoNotFinish.Name = "rdoNotFinish";
            this.rdoNotFinish.TabStop = true;
            this.rdoNotFinish.UseVisualStyleBackColor = true;
            // 
            // lblFinishTicket
            // 
            resources.ApplyResources(this.lblFinishTicket, "lblFinishTicket");
            this.lblFinishTicket.Name = "lblFinishTicket";
            // 
            // btnReturnZero
            // 
            this.btnReturnZero.BorderColor = System.Drawing.Color.White;
            this.btnReturnZero.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnReturnZero.DrawBorder = true;
            this.btnReturnZero.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnReturnZero, "btnReturnZero");
            this.btnReturnZero.ForeColor = System.Drawing.Color.White;
            this.btnReturnZero.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnReturnZero.Name = "btnReturnZero";
            this.btnReturnZero.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnReturnZero.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnReturnZero.UseVisualStyleBackColor = true;
            this.btnReturnZero.Click += new System.EventHandler(this.btnReturnZero_Click);
            // 
            // timerSetValue
            // 
            this.timerSetValue.Tick += new System.EventHandler(this.timerSetValue_Tick);
            // 
            // FormColourDifferenceTest
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvStandard);
            this.Controls.Add(this.dgvSample);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBoxEx4);
            this.Controls.Add(this.groupBoxEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormColourDifferenceTest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormColourDifferenceTest_FormClosing);
            this.Controls.SetChildIndex(this.groupBoxEx1, 0);
            this.Controls.SetChildIndex(this.groupBoxEx4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.dgvSample, 0);
            this.Controls.SetChildIndex(this.dgvStandard, 0);
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSample)).EndInit();
            this.groupBoxEx4.ResumeLayout(false);
            this.groupBoxEx4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStandard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private UC.ButtonRadiusEx buttonConnect;
        private UC.TextBoxEx txtName;
        private System.Windows.Forms.Label label0;
        private UC.TextBoxEx txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private UC.GroupBoxEx groupBoxEx1;
        private System.Windows.Forms.DataGridView dgvSample;
        private UC.ButtonRadiusEx btnClear;
        private UC.GroupBoxEx groupBoxEx4;
        private System.Windows.Forms.Label lbConnectState;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridView dgvStandard;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private UC.ButtonRadiusEx btnMeasureSample;
        private UC.ButtonRadiusEx btnMeasureStandard;
        private UC.TextBoxEx txtPositionX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdoFinish;
        private System.Windows.Forms.RadioButton rdoNotFinish;
        private System.Windows.Forms.Label lblFinishTicket;
        private UC.ButtonRadiusEx btnReturnZero;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timerSetValue;
    }
}