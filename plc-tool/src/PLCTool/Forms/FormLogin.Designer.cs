namespace PLCTool
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle1 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new PLCTool.UC.ButtonRadiusEx();
            this.textBox1 = new PLCTool.UC.TextBoxEx();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnOk
            // 
            this.btnOk.BorderColor = System.Drawing.Color.White;
            this.btnOk.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // textBox1
            // 
            this.textBox1.AllowReturn = false;
            tTextBoxBorderRenderStyle1.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle1.LineWidth = 1F;
            this.textBox1.BorderRenderStyle = tTextBoxBorderRenderStyle1;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextMargin = new System.Windows.Forms.Padding(1);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // FormLogin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PLCTool.UC.TextBoxEx textBox1;
        private System.Windows.Forms.Label label1;
        private UC.ButtonRadiusEx btnOk;
    }
}