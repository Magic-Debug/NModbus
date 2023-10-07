namespace PLCTool.Forms
{
    partial class FormIOStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIOStatus));
            this.groupBoxEx1 = new PLCTool.UC.GroupBoxEx(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxEx2 = new PLCTool.UC.GroupBoxEx(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSave = new PLCTool.UC.ButtonRadiusEx();
            this.btnImport = new PLCTool.UC.ButtonRadiusEx();
            this.btnExport = new PLCTool.UC.ButtonRadiusEx();
            this.btnDefault = new PLCTool.UC.ButtonRadiusEx();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxEx1.SuspendLayout();
            this.groupBoxEx2.SuspendLayout();
            this.SuspendLayout();
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
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
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
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            // btnImport
            // 
            this.btnImport.BorderColor = System.Drawing.Color.White;
            this.btnImport.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnImport, "btnImport");
            this.btnImport.Name = "btnImport";
            this.btnImport.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.BorderColor = System.Drawing.Color.White;
            this.btnExport.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            this.btnExport.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.BorderColor = System.Drawing.Color.White;
            this.btnDefault.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnDefault, "btnDefault");
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // openFileDialog1
            // 
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // saveFileDialog1
            // 
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // FormIOStatus
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxEx2);
            this.Controls.Add(this.groupBoxEx1);
            this.MaximizeBox = false;
            this.Name = "FormIOStatus";
            this.Load += new System.EventHandler(this.FormIOStatus_Load);
            this.Controls.SetChildIndex(this.groupBoxEx1, 0);
            this.Controls.SetChildIndex(this.groupBoxEx2, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnImport, 0);
            this.Controls.SetChildIndex(this.btnExport, 0);
            this.Controls.SetChildIndex(this.btnDefault, 0);
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UC.GroupBoxEx groupBoxEx1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private UC.GroupBoxEx groupBoxEx2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Timer timer1;
        private UC.ButtonRadiusEx btnSave;
        private UC.ButtonRadiusEx btnImport;
        private UC.ButtonRadiusEx btnExport;
        private UC.ButtonRadiusEx btnDefault;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}