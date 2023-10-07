namespace PLCTool
{
    partial class FormSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetting));
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle1 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle2 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle3 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle4 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle5 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle6 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtIP = new PLCTool.UC.TextBoxEx();
            this.lblPort1 = new System.Windows.Forms.Label();
            this.txtPort1 = new PLCTool.UC.TextBoxEx();
            this.chkAutoLog = new System.Windows.Forms.CheckBox();
            this.chkCheckError = new System.Windows.Forms.CheckBox();
            this.lblScanRate = new System.Windows.Forms.Label();
            this.txtScanRate = new PLCTool.UC.TextBoxEx();
            this.btnSave = new PLCTool.UC.ButtonRadiusEx();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chkIsClothOutShelf = new System.Windows.Forms.CheckBox();
            this.chkLeather = new System.Windows.Forms.CheckBox();
            this.chkWeijin = new System.Windows.Forms.CheckBox();
            this.chkShowWeight = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOnePlusLength = new PLCTool.UC.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaxSpeed = new PLCTool.UC.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDoubleLine = new PLCTool.UC.TextBoxEx();
            this.lblDoubleLine = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblIP
            // 
            resources.ApplyResources(this.lblIP, "lblIP");
            this.lblIP.Name = "lblIP";
            // 
            // txtIP
            // 
            this.txtIP.AllowReturn = false;
            tTextBoxBorderRenderStyle1.LineColor = System.Drawing.Color.LightGray;
            tTextBoxBorderRenderStyle1.LineWidth = 1F;
            this.txtIP.BorderRenderStyle = tTextBoxBorderRenderStyle1;
            this.txtIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtIP, "txtIP");
            this.txtIP.Name = "txtIP";
            this.txtIP.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // lblPort1
            // 
            resources.ApplyResources(this.lblPort1, "lblPort1");
            this.lblPort1.Name = "lblPort1";
            // 
            // txtPort1
            // 
            this.txtPort1.AllowReturn = false;
            tTextBoxBorderRenderStyle2.LineColor = System.Drawing.Color.LightGray;
            tTextBoxBorderRenderStyle2.LineWidth = 1F;
            this.txtPort1.BorderRenderStyle = tTextBoxBorderRenderStyle2;
            this.txtPort1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtPort1, "txtPort1");
            this.txtPort1.Name = "txtPort1";
            this.txtPort1.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // chkAutoLog
            // 
            resources.ApplyResources(this.chkAutoLog, "chkAutoLog");
            this.chkAutoLog.Name = "chkAutoLog";
            this.chkAutoLog.UseVisualStyleBackColor = true;
            // 
            // chkCheckError
            // 
            resources.ApplyResources(this.chkCheckError, "chkCheckError");
            this.chkCheckError.Name = "chkCheckError";
            this.chkCheckError.UseVisualStyleBackColor = true;
            // 
            // lblScanRate
            // 
            resources.ApplyResources(this.lblScanRate, "lblScanRate");
            this.lblScanRate.Name = "lblScanRate";
            // 
            // txtScanRate
            // 
            this.txtScanRate.AllowReturn = false;
            tTextBoxBorderRenderStyle3.LineColor = System.Drawing.Color.LightGray;
            tTextBoxBorderRenderStyle3.LineWidth = 1F;
            this.txtScanRate.BorderRenderStyle = tTextBoxBorderRenderStyle3;
            this.txtScanRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtScanRate, "txtScanRate");
            this.txtScanRate.Name = "txtScanRate";
            this.txtScanRate.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // btnSave
            // 
            this.btnSave.BorderColor = System.Drawing.Color.White;
            this.btnSave.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1")});
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            // 
            // chkIsClothOutShelf
            // 
            resources.ApplyResources(this.chkIsClothOutShelf, "chkIsClothOutShelf");
            this.chkIsClothOutShelf.Name = "chkIsClothOutShelf";
            this.chkIsClothOutShelf.UseVisualStyleBackColor = true;
            // 
            // chkLeather
            // 
            resources.ApplyResources(this.chkLeather, "chkLeather");
            this.chkLeather.Name = "chkLeather";
            this.chkLeather.UseVisualStyleBackColor = true;
            // 
            // chkWeijin
            // 
            resources.ApplyResources(this.chkWeijin, "chkWeijin");
            this.chkWeijin.Name = "chkWeijin";
            this.chkWeijin.UseVisualStyleBackColor = true;
            // 
            // chkShowWeight
            // 
            resources.ApplyResources(this.chkShowWeight, "chkShowWeight");
            this.chkShowWeight.Name = "chkShowWeight";
            this.chkShowWeight.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtOnePlusLength
            // 
            this.txtOnePlusLength.AllowReturn = false;
            tTextBoxBorderRenderStyle4.LineColor = System.Drawing.Color.LightGray;
            tTextBoxBorderRenderStyle4.LineWidth = 1F;
            this.txtOnePlusLength.BorderRenderStyle = tTextBoxBorderRenderStyle4;
            this.txtOnePlusLength.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtOnePlusLength, "txtOnePlusLength");
            this.txtOnePlusLength.Name = "txtOnePlusLength";
            this.txtOnePlusLength.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtMaxSpeed
            // 
            this.txtMaxSpeed.AllowReturn = false;
            tTextBoxBorderRenderStyle5.LineColor = System.Drawing.Color.LightGray;
            tTextBoxBorderRenderStyle5.LineWidth = 1F;
            this.txtMaxSpeed.BorderRenderStyle = tTextBoxBorderRenderStyle5;
            this.txtMaxSpeed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtMaxSpeed, "txtMaxSpeed");
            this.txtMaxSpeed.Name = "txtMaxSpeed";
            this.txtMaxSpeed.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtDoubleLine
            // 
            this.txtDoubleLine.AllowReturn = false;
            tTextBoxBorderRenderStyle6.LineColor = System.Drawing.Color.LightGray;
            tTextBoxBorderRenderStyle6.LineWidth = 1F;
            this.txtDoubleLine.BorderRenderStyle = tTextBoxBorderRenderStyle6;
            this.txtDoubleLine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtDoubleLine, "txtDoubleLine");
            this.txtDoubleLine.Name = "txtDoubleLine";
            this.txtDoubleLine.TextMargin = new System.Windows.Forms.Padding(1);
            this.txtDoubleLine.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxFloat_KeyUp);
            this.txtDoubleLine.Leave += new System.EventHandler(this.textBoxFloat_Leave);
            // 
            // lblDoubleLine
            // 
            resources.ApplyResources(this.lblDoubleLine, "lblDoubleLine");
            this.lblDoubleLine.Name = "lblDoubleLine";
            // 
            // FormSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtDoubleLine);
            this.Controls.Add(this.lblDoubleLine);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMaxSpeed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOnePlusLength);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkShowWeight);
            this.Controls.Add(this.chkWeijin);
            this.Controls.Add(this.chkLeather);
            this.Controls.Add(this.chkIsClothOutShelf);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtScanRate);
            this.Controls.Add(this.lblScanRate);
            this.Controls.Add(this.chkCheckError);
            this.Controls.Add(this.chkAutoLog);
            this.Controls.Add(this.txtPort1);
            this.Controls.Add(this.lblPort1);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.lblIP);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetting";
            this.Load += new System.EventHandler(this.FormSetting_Load);
            this.Controls.SetChildIndex(this.lblIP, 0);
            this.Controls.SetChildIndex(this.txtIP, 0);
            this.Controls.SetChildIndex(this.lblPort1, 0);
            this.Controls.SetChildIndex(this.txtPort1, 0);
            this.Controls.SetChildIndex(this.chkAutoLog, 0);
            this.Controls.SetChildIndex(this.chkCheckError, 0);
            this.Controls.SetChildIndex(this.lblScanRate, 0);
            this.Controls.SetChildIndex(this.txtScanRate, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.chkIsClothOutShelf, 0);
            this.Controls.SetChildIndex(this.chkLeather, 0);
            this.Controls.SetChildIndex(this.chkWeijin, 0);
            this.Controls.SetChildIndex(this.chkShowWeight, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtOnePlusLength, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtMaxSpeed, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.lblDoubleLine, 0);
            this.Controls.SetChildIndex(this.txtDoubleLine, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIP;
        private UC.TextBoxEx txtIP;
        private System.Windows.Forms.Label lblPort1;
        private UC.TextBoxEx txtPort1;
        private System.Windows.Forms.CheckBox chkAutoLog;
        private System.Windows.Forms.CheckBox chkCheckError;
        private System.Windows.Forms.Label lblScanRate;
        private UC.TextBoxEx txtScanRate;
        private UC.ButtonRadiusEx btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chkIsClothOutShelf;
        private System.Windows.Forms.CheckBox chkLeather;
        private System.Windows.Forms.CheckBox chkWeijin;
        private System.Windows.Forms.CheckBox chkShowWeight;
        private System.Windows.Forms.Label label2;
        private UC.TextBoxEx txtOnePlusLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private UC.TextBoxEx txtMaxSpeed;
        private System.Windows.Forms.Label label5;
        private UC.TextBoxEx txtDoubleLine;
        private System.Windows.Forms.Label lblDoubleLine;
    }
}