namespace PLCTool
{
    partial class BaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.pnlPaddingLeft = new System.Windows.Forms.Panel();
            this.pnlPadingRight = new System.Windows.Forms.Panel();
            this.pnlPaddingBottom = new System.Windows.Forms.Panel();
            this.titleControl1 = new PLCTool.UC.TitleControl();
            this.SuspendLayout();
            // 
            // pnlPaddingLeft
            // 
            this.pnlPaddingLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.pnlPaddingLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPaddingLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlPaddingLeft.Name = "pnlPaddingLeft";
            this.pnlPaddingLeft.Size = new System.Drawing.Size(7, 689);
            this.pnlPaddingLeft.TabIndex = 0;
            // 
            // pnlPadingRight
            // 
            this.pnlPadingRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.pnlPadingRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPadingRight.Location = new System.Drawing.Point(1121, 0);
            this.pnlPadingRight.Name = "pnlPadingRight";
            this.pnlPadingRight.Size = new System.Drawing.Size(7, 689);
            this.pnlPadingRight.TabIndex = 1;
            // 
            // pnlPaddingBottom
            // 
            this.pnlPaddingBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.pnlPaddingBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPaddingBottom.Location = new System.Drawing.Point(7, 682);
            this.pnlPaddingBottom.Name = "pnlPaddingBottom";
            this.pnlPaddingBottom.Size = new System.Drawing.Size(1114, 7);
            this.pnlPaddingBottom.TabIndex = 2;
            // 
            // titleControl1
            // 
            this.titleControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.titleControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleControl1.ForeColor = System.Drawing.Color.White;
            this.titleControl1.Headline = "";
            this.titleControl1.HeadlineColor = System.Drawing.Color.White;
            this.titleControl1.Location = new System.Drawing.Point(7, 0);
            this.titleControl1.MaximizeBox = true;
            this.titleControl1.MinimizeBox = true;
            this.titleControl1.Name = "titleControl1";
            this.titleControl1.SelectSkin = PLCTool.UC.TitleControl.Skin.Auto;
            this.titleControl1.ShowTitleIcon = true;
            this.titleControl1.Size = new System.Drawing.Size(1114, 26);
            this.titleControl1.TabIndex = 3;
            this.titleControl1.TitleIcon = global::PLCTool.Properties.Resources.tool;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1128, 689);
            this.Controls.Add(this.titleControl1);
            this.Controls.Add(this.pnlPaddingBottom);
            this.Controls.Add(this.pnlPadingRight);
            this.Controls.Add(this.pnlPaddingLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPaddingLeft;
        private System.Windows.Forms.Panel pnlPadingRight;
        private System.Windows.Forms.Panel pnlPaddingBottom;
        private UC.TitleControl titleControl1;
    }
}