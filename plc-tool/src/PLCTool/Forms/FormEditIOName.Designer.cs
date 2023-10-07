namespace PLCTool.Forms
{
    partial class FormEditIOName
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonRadiusEx1 = new PLCTool.UC.ButtonRadiusEx();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(134, 21);
            this.textBox1.TabIndex = 0;
            // 
            // buttonRadiusEx1
            // 
            this.buttonRadiusEx1.BorderColor = System.Drawing.Color.White;
            this.buttonRadiusEx1.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonRadiusEx1.Location = new System.Drawing.Point(175, 36);
            this.buttonRadiusEx1.Name = "buttonRadiusEx1";
            this.buttonRadiusEx1.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonRadiusEx1.Size = new System.Drawing.Size(75, 23);
            this.buttonRadiusEx1.TabIndex = 1;
            this.buttonRadiusEx1.Text = "确定";
            this.buttonRadiusEx1.UseVisualStyleBackColor = true;
            this.buttonRadiusEx1.Click += new System.EventHandler(this.buttonRadiusEx1_Click);
            // 
            // FormEditIOName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 76);
            this.Controls.Add(this.buttonRadiusEx1);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditIOName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑传感器名称";
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.buttonRadiusEx1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private UC.ButtonRadiusEx buttonRadiusEx1;
    }
}