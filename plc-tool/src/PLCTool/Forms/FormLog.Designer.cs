namespace PLCTool
{
    partial class FormLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLog));
            this.pnlRead = new System.Windows.Forms.Panel();
            this.btnSetting = new PLCTool.UC.ButtonRadiusEx();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new PLCTool.UC.ButtonRadiusEx();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chart_Encode1 = new PLCTool.Chart.Chart_Encode();
            this.chart_PositionX1 = new PLCTool.Chart.Chart_PositionX();
            this.chart_PositionY1 = new PLCTool.Chart.Chart_PositionY();
            this.chart_PositionZ1 = new PLCTool.Chart.Chart_PositionZ();
            this.chart_Weight1 = new PLCTool.Chart.Chart_Weight();
            this.chart_WindingTension1 = new PLCTool.Chart.Chart_WindingTension();
            this.chart_InspectionTension1 = new PLCTool.Chart.Chart_InspectionTension();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnNext3 = new PLCTool.UC.ButtonRadiusEx();
            this.btnLast3 = new PLCTool.UC.ButtonRadiusEx();
            this.cbbStatus = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnNext2 = new PLCTool.UC.ButtonRadiusEx();
            this.btnLast2 = new PLCTool.UC.ButtonRadiusEx();
            this.cbbTicketAlarm = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNext1 = new PLCTool.UC.ButtonRadiusEx();
            this.btnLast1 = new PLCTool.UC.ButtonRadiusEx();
            this.cbbAlarm = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlRead.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRead
            // 
            this.pnlRead.Controls.Add(this.btnSetting);
            this.pnlRead.Controls.Add(this.label3);
            this.pnlRead.Controls.Add(this.dateTimePicker2);
            this.pnlRead.Controls.Add(this.dateTimePicker1);
            this.pnlRead.Controls.Add(this.label2);
            this.pnlRead.Controls.Add(this.button1);
            resources.ApplyResources(this.pnlRead, "pnlRead");
            this.pnlRead.Name = "pnlRead";
            // 
            // btnSetting
            // 
            this.btnSetting.BorderColor = System.Drawing.Color.White;
            this.btnSetting.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnSetting, "btnSetting");
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dateTimePicker2
            // 
            resources.ApplyResources(this.dateTimePicker2, "dateTimePicker2");
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker2_KeyUp);
            this.dateTimePicker2.Leave += new System.EventHandler(this.dateTimePicker2_Leave);
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyUp);
            this.dateTimePicker1.Leave += new System.EventHandler(this.dateTimePicker1_Leave);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button1
            // 
            this.button1.BorderColor = System.Drawing.Color.White;
            this.button1.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.chart_Encode1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chart_PositionX1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chart_PositionY1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.chart_PositionZ1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.chart_Weight1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chart_WindingTension1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chart_InspectionTension1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 1, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // chart_Encode1
            // 
            resources.ApplyResources(this.chart_Encode1, "chart_Encode1");
            this.chart_Encode1.Name = "chart_Encode1";
            // 
            // chart_PositionX1
            // 
            resources.ApplyResources(this.chart_PositionX1, "chart_PositionX1");
            this.chart_PositionX1.Name = "chart_PositionX1";
            // 
            // chart_PositionY1
            // 
            resources.ApplyResources(this.chart_PositionY1, "chart_PositionY1");
            this.chart_PositionY1.Name = "chart_PositionY1";
            // 
            // chart_PositionZ1
            // 
            resources.ApplyResources(this.chart_PositionZ1, "chart_PositionZ1");
            this.chart_PositionZ1.Name = "chart_PositionZ1";
            // 
            // chart_Weight1
            // 
            resources.ApplyResources(this.chart_Weight1, "chart_Weight1");
            this.chart_Weight1.Name = "chart_Weight1";
            // 
            // chart_WindingTension1
            // 
            resources.ApplyResources(this.chart_WindingTension1, "chart_WindingTension1");
            this.chart_WindingTension1.Name = "chart_WindingTension1";
            // 
            // chart_InspectionTension1
            // 
            resources.ApplyResources(this.chart_InspectionTension1, "chart_InspectionTension1");
            this.chart_InspectionTension1.Name = "chart_InspectionTension1";
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            // 
            // openFileDialog1
            // 
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.btnNext3);
            this.pnlSearch.Controls.Add(this.btnLast3);
            this.pnlSearch.Controls.Add(this.cbbStatus);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.btnNext2);
            this.pnlSearch.Controls.Add(this.btnLast2);
            this.pnlSearch.Controls.Add(this.cbbTicketAlarm);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.btnNext1);
            this.pnlSearch.Controls.Add(this.btnLast1);
            this.pnlSearch.Controls.Add(this.cbbAlarm);
            this.pnlSearch.Controls.Add(this.label4);
            resources.ApplyResources(this.pnlSearch, "pnlSearch");
            this.pnlSearch.Name = "pnlSearch";
            // 
            // btnNext3
            // 
            this.btnNext3.BorderColor = System.Drawing.Color.White;
            this.btnNext3.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnNext3, "btnNext3");
            this.btnNext3.Name = "btnNext3";
            this.btnNext3.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnNext3.UseVisualStyleBackColor = true;
            this.btnNext3.Click += new System.EventHandler(this.btnNext3_Click);
            // 
            // btnLast3
            // 
            this.btnLast3.BorderColor = System.Drawing.Color.White;
            this.btnLast3.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnLast3, "btnLast3");
            this.btnLast3.Name = "btnLast3";
            this.btnLast3.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnLast3.UseVisualStyleBackColor = true;
            this.btnLast3.Click += new System.EventHandler(this.btnLast3_Click);
            // 
            // cbbStatus
            // 
            this.cbbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbStatus.FormattingEnabled = true;
            this.cbbStatus.Items.AddRange(new object[] {
            resources.GetString("cbbStatus.Items"),
            resources.GetString("cbbStatus.Items1"),
            resources.GetString("cbbStatus.Items2"),
            resources.GetString("cbbStatus.Items3"),
            resources.GetString("cbbStatus.Items4"),
            resources.GetString("cbbStatus.Items5"),
            resources.GetString("cbbStatus.Items6"),
            resources.GetString("cbbStatus.Items7"),
            resources.GetString("cbbStatus.Items8"),
            resources.GetString("cbbStatus.Items9"),
            resources.GetString("cbbStatus.Items10"),
            resources.GetString("cbbStatus.Items11"),
            resources.GetString("cbbStatus.Items12"),
            resources.GetString("cbbStatus.Items13")});
            resources.ApplyResources(this.cbbStatus, "cbbStatus");
            this.cbbStatus.Name = "cbbStatus";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btnNext2
            // 
            this.btnNext2.BorderColor = System.Drawing.Color.White;
            this.btnNext2.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnNext2, "btnNext2");
            this.btnNext2.Name = "btnNext2";
            this.btnNext2.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnNext2.UseVisualStyleBackColor = true;
            this.btnNext2.Click += new System.EventHandler(this.btnNext2_Click);
            // 
            // btnLast2
            // 
            this.btnLast2.BorderColor = System.Drawing.Color.White;
            this.btnLast2.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnLast2, "btnLast2");
            this.btnLast2.Name = "btnLast2";
            this.btnLast2.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnLast2.UseVisualStyleBackColor = true;
            this.btnLast2.Click += new System.EventHandler(this.btnLast2_Click);
            // 
            // cbbTicketAlarm
            // 
            this.cbbTicketAlarm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTicketAlarm.FormattingEnabled = true;
            resources.ApplyResources(this.cbbTicketAlarm, "cbbTicketAlarm");
            this.cbbTicketAlarm.Name = "cbbTicketAlarm";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btnNext1
            // 
            this.btnNext1.BorderColor = System.Drawing.Color.White;
            this.btnNext1.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnNext1, "btnNext1");
            this.btnNext1.Name = "btnNext1";
            this.btnNext1.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnNext1.UseVisualStyleBackColor = true;
            this.btnNext1.Click += new System.EventHandler(this.btnNext1_Click);
            // 
            // btnLast1
            // 
            this.btnLast1.BorderColor = System.Drawing.Color.White;
            this.btnLast1.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnLast1, "btnLast1");
            this.btnLast1.Name = "btnLast1";
            this.btnLast1.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnLast1.UseVisualStyleBackColor = true;
            this.btnLast1.Click += new System.EventHandler(this.btnLast1_Click);
            // 
            // cbbAlarm
            // 
            this.cbbAlarm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAlarm.FormattingEnabled = true;
            resources.ApplyResources(this.cbbAlarm, "cbbAlarm");
            this.cbbAlarm.Name = "cbbAlarm";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.tabControl1);
            this.pnlBody.Controls.Add(this.pnlSearch);
            resources.ApplyResources(this.pnlBody, "pnlBody");
            this.pnlBody.Name = "pnlBody";
            // 
            // FormLog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlRead);
            this.MaximizeBox = false;
            this.Name = "FormLog";
            this.Load += new System.EventHandler(this.FormLog_Load);
            this.Controls.SetChildIndex(this.pnlRead, 0);
            this.Controls.SetChildIndex(this.pnlBody, 0);
            this.pnlRead.ResumeLayout(false);
            this.pnlRead.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRead;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private PLCTool.UC.ButtonRadiusEx button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private Chart.Chart_Encode chart_Encode1;
        private Chart.Chart_PositionX chart_PositionX1;
        private Chart.Chart_PositionY chart_PositionY1;
        private Chart.Chart_PositionZ chart_PositionZ1;
        private Chart.Chart_Weight chart_Weight1;
        private Chart.Chart_WindingTension chart_WindingTension1;
        private Chart.Chart_InspectionTension chart_InspectionTension1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private PLCTool.UC.ButtonRadiusEx btnSetting;
        private System.Windows.Forms.Panel pnlSearch;
        private UC.ButtonRadiusEx btnNext2;
        private UC.ButtonRadiusEx btnLast2;
        private System.Windows.Forms.ComboBox cbbTicketAlarm;
        private System.Windows.Forms.Label label5;
        private UC.ButtonRadiusEx btnNext1;
        private UC.ButtonRadiusEx btnLast1;
        private System.Windows.Forms.ComboBox cbbAlarm;
        private System.Windows.Forms.Label label4;
        private UC.ButtonRadiusEx btnNext3;
        private UC.ButtonRadiusEx btnLast3;
        private System.Windows.Forms.ComboBox cbbStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlBody;
    }
}