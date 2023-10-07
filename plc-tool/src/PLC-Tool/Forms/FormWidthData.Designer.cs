namespace PLCTool.Forms
{
    partial class FormWidthData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWidthData));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnLoad = new PLCTool.UC.ButtonRadiusEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbFileName = new System.Windows.Forms.Label();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.lbMax = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbMin = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbClothWidth = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.码长m = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grbDetect = new PLCTool.UC.GroupBoxEx(this.components);
            this.btnMaxLocate = new PLCTool.UC.ButtonRadiusEx();
            this.btnMinLocate = new PLCTool.UC.ButtonRadiusEx();
            this.groupBoxEx1 = new PLCTool.UC.GroupBoxEx(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.btnMaxRiceLocate = new PLCTool.UC.ButtonRadiusEx();
            this.btnMinRiceLocate = new PLCTool.UC.ButtonRadiusEx();
            this.label8 = new System.Windows.Forms.Label();
            this.lbClothWidthRice = new System.Windows.Forms.Label();
            this.lbMinRice = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbMaxRice = new System.Windows.Forms.Label();
            this.rdoRice = new System.Windows.Forms.RadioButton();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.lblFinishTicket = new System.Windows.Forms.Label();
            this.txtNetWeight = new System.Windows.Forms.TextBox();
            this.btnCalculation = new PLCTool.UC.ButtonRadiusEx();
            this.groupBoxEx3 = new PLCTool.UC.GroupBoxEx(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.txtAreaRice = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.grbDetect.SuspendLayout();
            this.groupBoxEx1.SuspendLayout();
            this.groupBoxEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            resources.ApplyResources(this.btnLoad, "btnLoad");
            this.btnLoad.BorderColor = System.Drawing.Color.White;
            this.btnLoad.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.lbFileName);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Name = "panel1";
            // 
            // lbFileName
            // 
            resources.ApplyResources(this.lbFileName, "lbFileName");
            this.lbFileName.Name = "lbFileName";
            // 
            // txtOffset
            // 
            resources.ApplyResources(this.txtOffset, "txtOffset");
            this.txtOffset.Name = "txtOffset";
            // 
            // lbMax
            // 
            resources.ApplyResources(this.lbMax, "lbMax");
            this.lbMax.ForeColor = System.Drawing.Color.Blue;
            this.lbMax.Name = "lbMax";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // lbMin
            // 
            resources.ApplyResources(this.lbMin, "lbMin");
            this.lbMin.ForeColor = System.Drawing.Color.Red;
            this.lbMin.Name = "lbMin";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lbClothWidth
            // 
            resources.ApplyResources(this.lbClothWidth, "lbClothWidth");
            this.lbClothWidth.ForeColor = System.Drawing.Color.Purple;
            this.lbClothWidth.Name = "lbClothWidth";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chart1
            // 
            resources.ApplyResources(this.chart1, "chart1");
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.码长m,
            this.Width});
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            resources.ApplyResources(this.ID, "ID");
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // 码长m
            // 
            this.码长m.DataPropertyName = "Length";
            resources.ApplyResources(this.码长m, "码长m");
            this.码长m.Name = "码长m";
            this.码长m.ReadOnly = true;
            // 
            // Width
            // 
            this.Width.DataPropertyName = "Width";
            resources.ApplyResources(this.Width, "Width");
            this.Width.Name = "Width";
            this.Width.ReadOnly = true;
            // 
            // dataGridView2
            // 
            resources.ApplyResources(this.dataGridView2, "dataGridView2");
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column1});
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ID";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Length";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Width";
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Area";
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // grbDetect
            // 
            resources.ApplyResources(this.grbDetect, "grbDetect");
            this.grbDetect.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.grbDetect.Controls.Add(this.label3);
            this.grbDetect.Controls.Add(this.btnMaxLocate);
            this.grbDetect.Controls.Add(this.btnMinLocate);
            this.grbDetect.Controls.Add(this.label1);
            this.grbDetect.Controls.Add(this.lbClothWidth);
            this.grbDetect.Controls.Add(this.lbMin);
            this.grbDetect.Controls.Add(this.label5);
            this.grbDetect.Controls.Add(this.lbMax);
            this.grbDetect.Fillet = 0;
            this.grbDetect.Name = "grbDetect";
            this.grbDetect.TabStop = false;
            // 
            // btnMaxLocate
            // 
            resources.ApplyResources(this.btnMaxLocate, "btnMaxLocate");
            this.btnMaxLocate.BorderColor = System.Drawing.Color.White;
            this.btnMaxLocate.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnMaxLocate.Name = "btnMaxLocate";
            this.btnMaxLocate.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnMaxLocate.UseVisualStyleBackColor = true;
            this.btnMaxLocate.Click += new System.EventHandler(this.btnMaxLocate_Click);
            // 
            // btnMinLocate
            // 
            resources.ApplyResources(this.btnMinLocate, "btnMinLocate");
            this.btnMinLocate.BorderColor = System.Drawing.Color.White;
            this.btnMinLocate.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnMinLocate.Name = "btnMinLocate";
            this.btnMinLocate.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnMinLocate.UseVisualStyleBackColor = true;
            this.btnMinLocate.Click += new System.EventHandler(this.btnMinLocate_Click);
            // 
            // groupBoxEx1
            // 
            resources.ApplyResources(this.groupBoxEx1, "groupBoxEx1");
            this.groupBoxEx1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx1.Controls.Add(this.label7);
            this.groupBoxEx1.Controls.Add(this.btnMaxRiceLocate);
            this.groupBoxEx1.Controls.Add(this.btnMinRiceLocate);
            this.groupBoxEx1.Controls.Add(this.label8);
            this.groupBoxEx1.Controls.Add(this.lbClothWidthRice);
            this.groupBoxEx1.Controls.Add(this.lbMinRice);
            this.groupBoxEx1.Controls.Add(this.label11);
            this.groupBoxEx1.Controls.Add(this.lbMaxRice);
            this.groupBoxEx1.Fillet = 0;
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.TabStop = false;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // btnMaxRiceLocate
            // 
            resources.ApplyResources(this.btnMaxRiceLocate, "btnMaxRiceLocate");
            this.btnMaxRiceLocate.BorderColor = System.Drawing.Color.White;
            this.btnMaxRiceLocate.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnMaxRiceLocate.Name = "btnMaxRiceLocate";
            this.btnMaxRiceLocate.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnMaxRiceLocate.UseVisualStyleBackColor = true;
            this.btnMaxRiceLocate.Click += new System.EventHandler(this.btnMaxRiceLocate_Click);
            // 
            // btnMinRiceLocate
            // 
            resources.ApplyResources(this.btnMinRiceLocate, "btnMinRiceLocate");
            this.btnMinRiceLocate.BorderColor = System.Drawing.Color.White;
            this.btnMinRiceLocate.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnMinRiceLocate.Name = "btnMinRiceLocate";
            this.btnMinRiceLocate.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnMinRiceLocate.UseVisualStyleBackColor = true;
            this.btnMinRiceLocate.Click += new System.EventHandler(this.btnMinRiceLocate_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // lbClothWidthRice
            // 
            resources.ApplyResources(this.lbClothWidthRice, "lbClothWidthRice");
            this.lbClothWidthRice.ForeColor = System.Drawing.Color.Purple;
            this.lbClothWidthRice.Name = "lbClothWidthRice";
            // 
            // lbMinRice
            // 
            resources.ApplyResources(this.lbMinRice, "lbMinRice");
            this.lbMinRice.ForeColor = System.Drawing.Color.Red;
            this.lbMinRice.Name = "lbMinRice";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // lbMaxRice
            // 
            resources.ApplyResources(this.lbMaxRice, "lbMaxRice");
            this.lbMaxRice.ForeColor = System.Drawing.Color.Blue;
            this.lbMaxRice.Name = "lbMaxRice";
            // 
            // rdoRice
            // 
            resources.ApplyResources(this.rdoRice, "rdoRice");
            this.rdoRice.Name = "rdoRice";
            this.rdoRice.UseVisualStyleBackColor = true;
            // 
            // rdoAll
            // 
            resources.ApplyResources(this.rdoAll, "rdoAll");
            this.rdoAll.Checked = true;
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.TabStop = true;
            this.rdoAll.UseVisualStyleBackColor = true;
            this.rdoAll.CheckedChanged += new System.EventHandler(this.rdoAll_CheckedChanged);
            // 
            // lblFinishTicket
            // 
            resources.ApplyResources(this.lblFinishTicket, "lblFinishTicket");
            this.lblFinishTicket.Name = "lblFinishTicket";
            // 
            // txtNetWeight
            // 
            resources.ApplyResources(this.txtNetWeight, "txtNetWeight");
            this.txtNetWeight.Name = "txtNetWeight";
            // 
            // btnCalculation
            // 
            resources.ApplyResources(this.btnCalculation, "btnCalculation");
            this.btnCalculation.BorderColor = System.Drawing.Color.White;
            this.btnCalculation.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnCalculation.Name = "btnCalculation";
            this.btnCalculation.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnCalculation.UseVisualStyleBackColor = true;
            this.btnCalculation.Click += new System.EventHandler(this.btnCalculation_Click);
            // 
            // groupBoxEx3
            // 
            resources.ApplyResources(this.groupBoxEx3, "groupBoxEx3");
            this.groupBoxEx3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx3.Controls.Add(this.label15);
            this.groupBoxEx3.Controls.Add(this.btnCalculation);
            this.groupBoxEx3.Controls.Add(this.label16);
            this.groupBoxEx3.Controls.Add(this.txtWeight);
            this.groupBoxEx3.Controls.Add(this.txtAreaRice);
            this.groupBoxEx3.Controls.Add(this.txtNetWeight);
            this.groupBoxEx3.Controls.Add(this.label19);
            this.groupBoxEx3.Fillet = 0;
            this.groupBoxEx3.Name = "groupBoxEx3";
            this.groupBoxEx3.TabStop = false;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // txtWeight
            // 
            resources.ApplyResources(this.txtWeight, "txtWeight");
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            // 
            // txtAreaRice
            // 
            resources.ApplyResources(this.txtAreaRice, "txtAreaRice");
            this.txtAreaRice.Name = "txtAreaRice";
            this.txtAreaRice.ReadOnly = true;
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // FormWidthData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxEx3);
            this.Controls.Add(this.rdoRice);
            this.Controls.Add(this.rdoAll);
            this.Controls.Add(this.lblFinishTicket);
            this.Controls.Add(this.groupBoxEx1);
            this.Controls.Add(this.grbDetect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.txtOffset);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWidthData";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtOffset, 0);
            this.Controls.SetChildIndex(this.chart1, 0);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.dataGridView2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.grbDetect, 0);
            this.Controls.SetChildIndex(this.groupBoxEx1, 0);
            this.Controls.SetChildIndex(this.lblFinishTicket, 0);
            this.Controls.SetChildIndex(this.rdoAll, 0);
            this.Controls.SetChildIndex(this.rdoRice, 0);
            this.Controls.SetChildIndex(this.groupBoxEx3, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.grbDetect.ResumeLayout(false);
            this.grbDetect.PerformLayout();
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx1.PerformLayout();
            this.groupBoxEx3.ResumeLayout(false);
            this.groupBoxEx3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UC.ButtonRadiusEx btnLoad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbFileName;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Label lbMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbMin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbClothWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private UC.GroupBoxEx grbDetect;
        private UC.GroupBoxEx groupBoxEx1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbClothWidthRice;
        private System.Windows.Forms.Label lbMinRice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbMaxRice;
        private System.Windows.Forms.RadioButton rdoRice;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.Label lblFinishTicket;
        private System.Windows.Forms.TextBox txtNetWeight;
        private UC.ButtonRadiusEx btnCalculation;
        private UC.GroupBoxEx groupBoxEx3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.TextBox txtAreaRice;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 码长m;
        private System.Windows.Forms.DataGridViewTextBoxColumn Width;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private UC.ButtonRadiusEx btnMaxLocate;
        private UC.ButtonRadiusEx btnMinLocate;
        private UC.ButtonRadiusEx btnMaxRiceLocate;
        private UC.ButtonRadiusEx btnMinRiceLocate;
    }
}