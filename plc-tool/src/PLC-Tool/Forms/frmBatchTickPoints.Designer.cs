namespace PLCTool.Forms
{
    partial class frmBatchTickPoints
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
            this.pnlOperate = new System.Windows.Forms.Panel();
            this.btnCancel = new PLCTool.UC.ButtonRadiusEx();
            this.btnOK = new PLCTool.UC.ButtonRadiusEx();
            this.lbxTicketPoints = new System.Windows.Forms.ListBox();
            this.txtNewPointX = new System.Windows.Forms.TextBox();
            this.btnAdd = new PLCTool.UC.ButtonRadiusEx();
            this.btnDel = new PLCTool.UC.ButtonRadiusEx();
            this.lblNewPointX = new System.Windows.Forms.Label();
            this.lblNewPointY = new System.Windows.Forms.Label();
            this.txtNewPointY = new System.Windows.Forms.TextBox();
            this.pnlOperate.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOperate
            // 
            this.pnlOperate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOperate.Controls.Add(this.btnCancel);
            this.pnlOperate.Controls.Add(this.btnOK);
            this.pnlOperate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOperate.Location = new System.Drawing.Point(7, 225);
            this.pnlOperate.Name = "pnlOperate";
            this.pnlOperate.Size = new System.Drawing.Size(296, 49);
            this.pnlOperate.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.BaseColor = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnCancel.DrawBorder = true;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCancel.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(164, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnCancel.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnCancel.Size = new System.Drawing.Size(70, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BaseColor = System.Drawing.Color.White;
            this.btnOK.BorderColor = System.Drawing.Color.Black;
            this.btnOK.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnOK.DrawBorder = true;
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnOK.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(62, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnOK.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnOK.Size = new System.Drawing.Size(70, 28);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbxTicketPoints
            // 
            this.lbxTicketPoints.FormattingEnabled = true;
            this.lbxTicketPoints.ItemHeight = 12;
            this.lbxTicketPoints.Location = new System.Drawing.Point(13, 29);
            this.lbxTicketPoints.Name = "lbxTicketPoints";
            this.lbxTicketPoints.Size = new System.Drawing.Size(177, 160);
            this.lbxTicketPoints.TabIndex = 0;
            // 
            // txtNewPointX
            // 
            this.txtNewPointX.Location = new System.Drawing.Point(30, 197);
            this.txtNewPointX.Name = "txtNewPointX";
            this.txtNewPointX.Size = new System.Drawing.Size(65, 21);
            this.txtNewPointX.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.BaseColor = System.Drawing.Color.White;
            this.btnAdd.BorderColor = System.Drawing.Color.Black;
            this.btnAdd.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnAdd.DrawBorder = true;
            this.btnAdd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAdd.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(210, 193);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnAdd.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnAdd.Size = new System.Drawing.Size(70, 28);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.BaseColor = System.Drawing.Color.White;
            this.btnDel.BorderColor = System.Drawing.Color.Black;
            this.btnDel.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnDel.DrawBorder = true;
            this.btnDel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.btnDel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnDel.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.btnDel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDel.Location = new System.Drawing.Point(210, 95);
            this.btnDel.Name = "btnDel";
            this.btnDel.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.btnDel.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnDel.Size = new System.Drawing.Size(70, 28);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // lblNewPointX
            // 
            this.lblNewPointX.AutoSize = true;
            this.lblNewPointX.Location = new System.Drawing.Point(13, 201);
            this.lblNewPointX.Name = "lblNewPointX";
            this.lblNewPointX.Size = new System.Drawing.Size(11, 12);
            this.lblNewPointX.TabIndex = 17;
            this.lblNewPointX.Text = "X";
            // 
            // lblNewPointY
            // 
            this.lblNewPointY.AutoSize = true;
            this.lblNewPointY.Location = new System.Drawing.Point(108, 201);
            this.lblNewPointY.Name = "lblNewPointY";
            this.lblNewPointY.Size = new System.Drawing.Size(11, 12);
            this.lblNewPointY.TabIndex = 19;
            this.lblNewPointY.Text = "Y";
            // 
            // txtNewPointY
            // 
            this.txtNewPointY.Location = new System.Drawing.Point(125, 197);
            this.txtNewPointY.Name = "txtNewPointY";
            this.txtNewPointY.Size = new System.Drawing.Size(65, 21);
            this.txtNewPointY.TabIndex = 3;
            // 
            // frmBatchTickPoints
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 281);
            this.Controls.Add(this.lblNewPointY);
            this.Controls.Add(this.txtNewPointY);
            this.Controls.Add(this.lblNewPointX);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtNewPointX);
            this.Controls.Add(this.lbxTicketPoints);
            this.Controls.Add(this.pnlOperate);
            this.MaximizeBox = false;
            this.Name = "frmBatchTickPoints";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "批量贴标坐标管理";
            this.Controls.SetChildIndex(this.pnlOperate, 0);
            this.Controls.SetChildIndex(this.lbxTicketPoints, 0);
            this.Controls.SetChildIndex(this.txtNewPointX, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnDel, 0);
            this.Controls.SetChildIndex(this.lblNewPointX, 0);
            this.Controls.SetChildIndex(this.txtNewPointY, 0);
            this.Controls.SetChildIndex(this.lblNewPointY, 0);
            this.pnlOperate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlOperate;
        private UC.ButtonRadiusEx btnCancel;
        private UC.ButtonRadiusEx btnOK;
        private System.Windows.Forms.ListBox lbxTicketPoints;
        private System.Windows.Forms.TextBox txtNewPointX;
        private UC.ButtonRadiusEx btnAdd;
        private UC.ButtonRadiusEx btnDel;
        private System.Windows.Forms.Label lblNewPointX;
        private System.Windows.Forms.Label lblNewPointY;
        private System.Windows.Forms.TextBox txtNewPointY;
    }
}