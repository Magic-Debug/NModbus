namespace PLCTool
{
    partial class FormAddlogColumn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddlogColumn));
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle1 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            PLCTool.UC.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle2 = new PLCTool.UC.TTextBoxBorderRenderStyle();
            this.txtName = new PLCTool.UC.TextBoxEx();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDataType = new System.Windows.Forms.Label();
            this.cbbDataType = new System.Windows.Forms.ComboBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new PLCTool.UC.TextBoxEx();
            this.chkIsShow = new System.Windows.Forms.CheckBox();
            this.btnSave = new PLCTool.UC.ButtonRadiusEx();
            this.SuspendLayout();
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.AllowReturn = false;
            tTextBoxBorderRenderStyle1.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle1.LineWidth = 1F;
            this.txtName.BorderRenderStyle = tTextBoxBorderRenderStyle1;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Name = "txtName";
            this.txtName.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // lblDataType
            // 
            resources.ApplyResources(this.lblDataType, "lblDataType");
            this.lblDataType.Name = "lblDataType";
            // 
            // cbbDataType
            // 
            resources.ApplyResources(this.cbbDataType, "cbbDataType");
            this.cbbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDataType.FormattingEnabled = true;
            this.cbbDataType.Items.AddRange(new object[] {
            resources.GetString("cbbDataType.Items"),
            resources.GetString("cbbDataType.Items1"),
            resources.GetString("cbbDataType.Items2"),
            resources.GetString("cbbDataType.Items3"),
            resources.GetString("cbbDataType.Items4"),
            resources.GetString("cbbDataType.Items5"),
            resources.GetString("cbbDataType.Items6"),
            resources.GetString("cbbDataType.Items7")});
            this.cbbDataType.Name = "cbbDataType";
            this.cbbDataType.SelectedIndexChanged += new System.EventHandler(this.cbbDataType_SelectedIndexChanged);
            // 
            // lblAddress
            // 
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Name = "lblAddress";
            // 
            // txtAddress
            // 
            resources.ApplyResources(this.txtAddress, "txtAddress");
            this.txtAddress.AllowReturn = false;
            tTextBoxBorderRenderStyle2.LineColor = System.Drawing.Color.Black;
            tTextBoxBorderRenderStyle2.LineWidth = 1F;
            this.txtAddress.BorderRenderStyle = tTextBoxBorderRenderStyle2;
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.TextMargin = new System.Windows.Forms.Padding(1);
            // 
            // chkIsShow
            // 
            resources.ApplyResources(this.chkIsShow, "chkIsShow");
            this.chkIsShow.Name = "chkIsShow";
            this.chkIsShow.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BorderColor = System.Drawing.Color.White;
            this.btnSave.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnSave.Name = "btnSave";
            this.btnSave.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FormAddlogColumn
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkIsShow);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.cbbDataType);
            this.Controls.Add(this.lblDataType);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddlogColumn";
            this.Load += new System.EventHandler(this.FormAddlogColumn_Load);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.lblDataType, 0);
            this.Controls.SetChildIndex(this.cbbDataType, 0);
            this.Controls.SetChildIndex(this.lblAddress, 0);
            this.Controls.SetChildIndex(this.txtAddress, 0);
            this.Controls.SetChildIndex(this.chkIsShow, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PLCTool.UC.TextBoxEx txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.ComboBox cbbDataType;
        private System.Windows.Forms.Label lblAddress;
        private PLCTool.UC.TextBoxEx txtAddress;
        private System.Windows.Forms.CheckBox chkIsShow;
        private PLCTool.UC.ButtonRadiusEx btnSave;
    }
}