namespace PLCTool
{
    partial class FormChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChangePassword));
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle1 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle2 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle3 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.lblNewPassword1 = new System.Windows.Forms.Label();
            this.lblNewPassword2 = new System.Windows.Forms.Label();
            this.txtOldPassword = new PLCTool.UC.TextBoxEx();
            this.txtNewPassword1 = new PLCTool.UC.TextBoxEx();
            this.txtNewPassword2 = new PLCTool.UC.TextBoxEx();
            this.btnOK = new PLCTool.UC.ButtonRadiusEx();
            this.SuspendLayout();
            // 
            // lblOldPassword
            // 
            resources.ApplyResources(this.lblOldPassword, "lblOldPassword");
            this.lblOldPassword.Name = "lblOldPassword";
            // 
            // lblNewPassword1
            // 
            resources.ApplyResources(this.lblNewPassword1, "lblNewPassword1");
            this.lblNewPassword1.Name = "lblNewPassword1";
            // 
            // lblNewPassword2
            // 
            resources.ApplyResources(this.lblNewPassword2, "lblNewPassword2");
            this.lblNewPassword2.Name = "lblNewPassword2";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.AllowReturn = false;
            tTextBoxBorderRenderStyle1.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle1.LineWidth = 1F;
            this.txtOldPassword.BorderRenderStyle = tTextBoxBorderRenderStyle1;
            this.txtOldPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtOldPassword, "txtOldPassword");
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.TextMargin = new System.Windows.Forms.Padding(1);
            this.txtOldPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_KeyUP);
            // 
            // txtNewPassword1
            // 
            this.txtNewPassword1.AllowReturn = false;
            tTextBoxBorderRenderStyle2.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle2.LineWidth = 1F;
            this.txtNewPassword1.BorderRenderStyle = tTextBoxBorderRenderStyle2;
            this.txtNewPassword1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtNewPassword1, "txtNewPassword1");
            this.txtNewPassword1.Name = "txtNewPassword1";
            this.txtNewPassword1.TextMargin = new System.Windows.Forms.Padding(1);
            this.txtNewPassword1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_KeyUP);
            // 
            // txtNewPassword2
            // 
            this.txtNewPassword2.AllowReturn = false;
            tTextBoxBorderRenderStyle3.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle3.LineWidth = 1F;
            this.txtNewPassword2.BorderRenderStyle = tTextBoxBorderRenderStyle3;
            this.txtNewPassword2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtNewPassword2, "txtNewPassword2");
            this.txtNewPassword2.Name = "txtNewPassword2";
            this.txtNewPassword2.TextMargin = new System.Windows.Forms.Padding(1);
            this.txtNewPassword2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_KeyUP);
            // 
            // btnOK
            // 
            this.btnOK.BorderColor = System.Drawing.Color.White;
            this.btnOK.ControlState = PLCTool.UC.ControlState.Normal;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormChangePassword
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtNewPassword2);
            this.Controls.Add(this.txtNewPassword1);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblNewPassword2);
            this.Controls.Add(this.lblNewPassword1);
            this.Controls.Add(this.lblOldPassword);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChangePassword";
            this.Controls.SetChildIndex(this.lblOldPassword, 0);
            this.Controls.SetChildIndex(this.lblNewPassword1, 0);
            this.Controls.SetChildIndex(this.lblNewPassword2, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.txtOldPassword, 0);
            this.Controls.SetChildIndex(this.txtNewPassword1, 0);
            this.Controls.SetChildIndex(this.txtNewPassword2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.Label lblNewPassword1;
        private System.Windows.Forms.Label lblNewPassword2;
        private PLCTool.UC.ButtonRadiusEx btnOK;
        private PLCTool.UC.TextBoxEx txtOldPassword;
        private PLCTool.UC.TextBoxEx txtNewPassword1;
        private PLCTool.UC.TextBoxEx txtNewPassword2;
    }
}