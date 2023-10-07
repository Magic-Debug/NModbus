namespace NModbusApp
{
    partial class BaeSlaveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaeSlaveForm));
            this.btnStartSlave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartSlave
            // 
            this.btnStartSlave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnStartSlave.Location = new System.Drawing.Point(52, 21);
            this.btnStartSlave.Name = "btnStartSlave";
            this.btnStartSlave.Size = new System.Drawing.Size(103, 41);
            this.btnStartSlave.TabIndex = 0;
            this.btnStartSlave.Text = "启动PLC-Slave";
            this.btnStartSlave.UseVisualStyleBackColor = false;
            this.btnStartSlave.Click += new System.EventHandler(this.btnStartSlave_Click);
            // 
            // BaeSlaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 192);
            this.Controls.Add(this.btnStartSlave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaeSlaveForm";
            this.Text = "PLC-Slave";
            this.Load += new System.EventHandler(this.FrmTcpSlave_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnStartSlave;
    }
}