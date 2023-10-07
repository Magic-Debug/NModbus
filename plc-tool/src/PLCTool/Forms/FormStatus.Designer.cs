namespace PLCTool
{
    partial class FormStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStatus));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbbIColor = new System.Windows.Forms.ComboBox();
            this.cbbQColor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkRandom = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxEx1 = new PLCTool.UC.GroupBoxEx(this.components);
            this.groupBoxEx2 = new PLCTool.UC.GroupBoxEx(this.components);
            this.buttonRadiusEx1 = new PLCTool.UC.ButtonRadiusEx();
            this.buttonRadiusEx2 = new PLCTool.UC.ButtonRadiusEx();
            this.buttonRadiusEx3 = new PLCTool.UC.ButtonRadiusEx();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxEx1.SuspendLayout();
            this.groupBoxEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbbIColor
            // 
            this.cbbIColor.FormattingEnabled = true;
            this.cbbIColor.Items.AddRange(new object[] {
            resources.GetString("cbbIColor.Items"),
            resources.GetString("cbbIColor.Items1"),
            resources.GetString("cbbIColor.Items2"),
            resources.GetString("cbbIColor.Items3")});
            resources.ApplyResources(this.cbbIColor, "cbbIColor");
            this.cbbIColor.Name = "cbbIColor";
            // 
            // cbbQColor
            // 
            this.cbbQColor.FormattingEnabled = true;
            this.cbbQColor.Items.AddRange(new object[] {
            resources.GetString("cbbQColor.Items"),
            resources.GetString("cbbQColor.Items1"),
            resources.GetString("cbbQColor.Items2"),
            resources.GetString("cbbQColor.Items3")});
            resources.ApplyResources(this.cbbQColor, "cbbQColor");
            this.cbbQColor.Name = "cbbQColor";
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
            // chkRandom
            // 
            resources.ApplyResources(this.chkRandom, "chkRandom");
            this.chkRandom.Name = "chkRandom";
            this.chkRandom.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx1.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxEx1.Fillet = 0;
            resources.ApplyResources(this.groupBoxEx1, "groupBoxEx1");
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.TabStop = false;
            // 
            // groupBoxEx2
            // 
            this.groupBoxEx2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx2.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxEx2.Fillet = 0;
            resources.ApplyResources(this.groupBoxEx2, "groupBoxEx2");
            this.groupBoxEx2.Name = "groupBoxEx2";
            this.groupBoxEx2.TabStop = false;
            // 
            // buttonRadiusEx1
            // 
            this.buttonRadiusEx1.BorderColor = System.Drawing.Color.White;
            this.buttonRadiusEx1.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.buttonRadiusEx1, "buttonRadiusEx1");
            this.buttonRadiusEx1.Name = "buttonRadiusEx1";
            this.buttonRadiusEx1.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonRadiusEx1.UseVisualStyleBackColor = true;
            // 
            // buttonRadiusEx2
            // 
            this.buttonRadiusEx2.BorderColor = System.Drawing.Color.White;
            this.buttonRadiusEx2.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.buttonRadiusEx2, "buttonRadiusEx2");
            this.buttonRadiusEx2.Name = "buttonRadiusEx2";
            this.buttonRadiusEx2.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonRadiusEx2.UseVisualStyleBackColor = true;
            // 
            // buttonRadiusEx3
            // 
            this.buttonRadiusEx3.BorderColor = System.Drawing.Color.White;
            this.buttonRadiusEx3.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.buttonRadiusEx3, "buttonRadiusEx3");
            this.buttonRadiusEx3.Name = "buttonRadiusEx3";
            this.buttonRadiusEx3.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonRadiusEx3.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormStatus
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.buttonRadiusEx3);
            this.Controls.Add(this.buttonRadiusEx2);
            this.Controls.Add(this.buttonRadiusEx1);
            this.Controls.Add(this.groupBoxEx2);
            this.Controls.Add(this.groupBoxEx1);
            this.Controls.Add(this.chkRandom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbQColor);
            this.Controls.Add(this.cbbIColor);
            this.MinimizeBox = false;
            this.Name = "FormStatus";
            this.Load += new System.EventHandler(this.FormmStatus_Load);
            this.Controls.SetChildIndex(this.cbbIColor, 0);
            this.Controls.SetChildIndex(this.cbbQColor, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.chkRandom, 0);
            this.Controls.SetChildIndex(this.groupBoxEx1, 0);
            this.Controls.SetChildIndex(this.groupBoxEx2, 0);
            this.Controls.SetChildIndex(this.buttonRadiusEx1, 0);
            this.Controls.SetChildIndex(this.buttonRadiusEx2, 0);
            this.Controls.SetChildIndex(this.buttonRadiusEx3, 0);
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cbbIColor;
        private System.Windows.Forms.ComboBox cbbQColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkRandom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private UC.GroupBoxEx groupBoxEx1;
        private UC.GroupBoxEx groupBoxEx2;
        private UC.ButtonRadiusEx buttonRadiusEx1;
        private UC.ButtonRadiusEx buttonRadiusEx2;
        private UC.ButtonRadiusEx buttonRadiusEx3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}