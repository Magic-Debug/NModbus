namespace PLCTool
{
    partial class FormMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMsg));
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle1 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle2 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle3 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle4 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle5 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            this.rtbMsg = new System.Windows.Forms.RichTextBox();
            this.btnClear = new PLCTool.UC.ButtonRadiusEx();
            this.btnMoniter = new PLCTool.UC.ButtonRadiusEx();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSend = new PLCTool.UC.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReceive = new PLCTool.UC.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtError = new PLCTool.UC.TextBoxEx();
            this.btnReset = new PLCTool.UC.ButtonRadiusEx();
            this.btnCount = new PLCTool.UC.ButtonRadiusEx();
            this.txtSendCommand = new PLCTool.UC.TextBoxEx();
            this.btnSend = new PLCTool.UC.ButtonRadiusEx();
            this.chkSplitPackage = new System.Windows.Forms.CheckBox();
            this.chkDisplaySpecialAddress = new System.Windows.Forms.CheckBox();
            this.txtDisplaySpecialAddress = new PLCTool.UC.TextBoxEx();
            this.chkDisplaySent = new System.Windows.Forms.CheckBox();
            this.chkDisplayReceived = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rtbMsg
            // 
            resources.ApplyResources(this.rtbMsg, "rtbMsg");
            this.rtbMsg.BackColor = System.Drawing.SystemColors.Window;
            this.rtbMsg.Name = "rtbMsg";
            this.rtbMsg.ReadOnly = true;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BorderColor = System.Drawing.Color.White;
            this.btnClear.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnClear.Name = "btnClear";
            this.btnClear.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnMoniter
            // 
            resources.ApplyResources(this.btnMoniter, "btnMoniter");
            this.btnMoniter.BorderColor = System.Drawing.Color.White;
            this.btnMoniter.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnMoniter.Name = "btnMoniter";
            this.btnMoniter.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnMoniter.UseVisualStyleBackColor = true;
            this.btnMoniter.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtSend
            // 
            resources.ApplyResources(this.txtSend, "txtSend");
            this.txtSend.AllowReturn = false;
            this.txtSend.BackColor = System.Drawing.SystemColors.Window;
            tTextBoxBorderRenderStyle1.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle1.LineWidth = 1F;
            this.txtSend.BorderRenderStyle = tTextBoxBorderRenderStyle1;
            this.txtSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSend.Name = "txtSend";
            this.txtSend.ReadOnly = true;
            this.txtSend.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtReceive
            // 
            resources.ApplyResources(this.txtReceive, "txtReceive");
            this.txtReceive.AllowReturn = false;
            this.txtReceive.BackColor = System.Drawing.SystemColors.Window;
            tTextBoxBorderRenderStyle2.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle2.LineWidth = 1F;
            this.txtReceive.BorderRenderStyle = tTextBoxBorderRenderStyle2;
            this.txtReceive.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ReadOnly = true;
            this.txtReceive.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtError
            // 
            resources.ApplyResources(this.txtError, "txtError");
            this.txtError.AllowReturn = false;
            this.txtError.BackColor = System.Drawing.SystemColors.Window;
            tTextBoxBorderRenderStyle3.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle3.LineWidth = 1F;
            this.txtError.BorderRenderStyle = tTextBoxBorderRenderStyle3;
            this.txtError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.BorderColor = System.Drawing.Color.White;
            this.btnReset.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnReset.Name = "btnReset";
            this.btnReset.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCount
            // 
            resources.ApplyResources(this.btnCount, "btnCount");
            this.btnCount.BorderColor = System.Drawing.Color.White;
            this.btnCount.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnCount.Name = "btnCount";
            this.btnCount.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnCount.UseVisualStyleBackColor = true;
            this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
            // 
            // txtSendCommand
            // 
            resources.ApplyResources(this.txtSendCommand, "txtSendCommand");
            this.txtSendCommand.AllowReturn = false;
            tTextBoxBorderRenderStyle4.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle4.LineWidth = 1F;
            this.txtSendCommand.BorderRenderStyle = tTextBoxBorderRenderStyle4;
            this.txtSendCommand.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSendCommand.Name = "txtSendCommand";
            this.txtSendCommand.TextMargin = new System.Windows.Forms.Padding(1);
            this.txtSendCommand.TextChanged += new System.EventHandler(this.txtSendCommand_TextChanged);
            this.txtSendCommand.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSendCommand_KeyUp);
            // 
            // btnSend
            // 
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.BorderColor = System.Drawing.Color.White;
            this.btnSend.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnSend.Name = "btnSend";
            this.btnSend.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // chkSplitPackage
            // 
            resources.ApplyResources(this.chkSplitPackage, "chkSplitPackage");
            this.chkSplitPackage.Checked = true;
            this.chkSplitPackage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSplitPackage.Name = "chkSplitPackage";
            this.chkSplitPackage.UseVisualStyleBackColor = true;
            // 
            // chkDisplaySpecialAddress
            // 
            resources.ApplyResources(this.chkDisplaySpecialAddress, "chkDisplaySpecialAddress");
            this.chkDisplaySpecialAddress.Name = "chkDisplaySpecialAddress";
            this.chkDisplaySpecialAddress.UseVisualStyleBackColor = true;
            // 
            // txtDisplaySpecialAddress
            // 
            resources.ApplyResources(this.txtDisplaySpecialAddress, "txtDisplaySpecialAddress");
            this.txtDisplaySpecialAddress.AllowReturn = false;
            this.txtDisplaySpecialAddress.BackColor = System.Drawing.SystemColors.Window;
            tTextBoxBorderRenderStyle5.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle5.LineWidth = 1F;
            this.txtDisplaySpecialAddress.BorderRenderStyle = tTextBoxBorderRenderStyle5;
            this.txtDisplaySpecialAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDisplaySpecialAddress.Name = "txtDisplaySpecialAddress";
            this.txtDisplaySpecialAddress.TextMargin = new System.Windows.Forms.Padding(1);
            this.txtDisplaySpecialAddress.TextChanged += new System.EventHandler(this.txtDisplaySpecialAddress_TextChanged);
            // 
            // chkDisplaySent
            // 
            resources.ApplyResources(this.chkDisplaySent, "chkDisplaySent");
            this.chkDisplaySent.Checked = true;
            this.chkDisplaySent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplaySent.Name = "chkDisplaySent";
            this.chkDisplaySent.UseVisualStyleBackColor = true;
            this.chkDisplaySent.CheckedChanged += new System.EventHandler(this.chkDisplaySent_CheckedChanged);
            // 
            // chkDisplayReceived
            // 
            resources.ApplyResources(this.chkDisplayReceived, "chkDisplayReceived");
            this.chkDisplayReceived.Checked = true;
            this.chkDisplayReceived.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayReceived.Name = "chkDisplayReceived";
            this.chkDisplayReceived.UseVisualStyleBackColor = true;
            this.chkDisplayReceived.CheckedChanged += new System.EventHandler(this.chkDisplayReceived_CheckedChanged);
            // 
            // FormMsg
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chkDisplayReceived);
            this.Controls.Add(this.chkDisplaySent);
            this.Controls.Add(this.txtDisplaySpecialAddress);
            this.Controls.Add(this.chkDisplaySpecialAddress);
            this.Controls.Add(this.chkSplitPackage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSendCommand);
            this.Controls.Add(this.btnCount);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtReceive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMoniter);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.rtbMsg);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormMsg";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMsg_FormClosing);
            this.Load += new System.EventHandler(this.FormMsg_Load);
            this.Controls.SetChildIndex(this.rtbMsg, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnMoniter, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtSend, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtReceive, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtError, 0);
            this.Controls.SetChildIndex(this.btnReset, 0);
            this.Controls.SetChildIndex(this.btnCount, 0);
            this.Controls.SetChildIndex(this.txtSendCommand, 0);
            this.Controls.SetChildIndex(this.btnSend, 0);
            this.Controls.SetChildIndex(this.chkSplitPackage, 0);
            this.Controls.SetChildIndex(this.chkDisplaySpecialAddress, 0);
            this.Controls.SetChildIndex(this.txtDisplaySpecialAddress, 0);
            this.Controls.SetChildIndex(this.chkDisplaySent, 0);
            this.Controls.SetChildIndex(this.chkDisplayReceived, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMsg;
        private PLCTool.UC.ButtonRadiusEx btnClear;
        private PLCTool.UC.ButtonRadiusEx btnMoniter;
        private System.Windows.Forms.Label label1;
        private PLCTool.UC.TextBoxEx txtSend;
        private System.Windows.Forms.Label label2;
        private PLCTool.UC.TextBoxEx txtReceive;
        private System.Windows.Forms.Label label3;
        private PLCTool.UC.TextBoxEx txtError;
        private PLCTool.UC.ButtonRadiusEx btnReset;
        private PLCTool.UC.ButtonRadiusEx btnCount;
        private PLCTool.UC.TextBoxEx txtSendCommand;
        private PLCTool.UC.ButtonRadiusEx btnSend;
        private System.Windows.Forms.CheckBox chkSplitPackage;
        private System.Windows.Forms.CheckBox chkDisplaySpecialAddress;
        private UC.TextBoxEx txtDisplaySpecialAddress;
        private System.Windows.Forms.CheckBox chkDisplaySent;
        private System.Windows.Forms.CheckBox chkDisplayReceived;
    }
}