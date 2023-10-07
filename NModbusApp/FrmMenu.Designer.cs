namespace NModbusApp
{
    partial class FrmMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            this.btnSerialAscii = new System.Windows.Forms.Button();
            this.btnTCP = new System.Windows.Forms.Button();
            this.btnSerialRtu = new System.Windows.Forms.Button();
            this.btnSocketSerial = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSerialAscii
            // 
            this.btnSerialAscii.Location = new System.Drawing.Point(12, 30);
            this.btnSerialAscii.Name = "btnSerialAscii";
            this.btnSerialAscii.Size = new System.Drawing.Size(112, 30);
            this.btnSerialAscii.TabIndex = 0;
            this.btnSerialAscii.Text = "SerialAscii";
            this.btnSerialAscii.UseVisualStyleBackColor = true;
            this.btnSerialAscii.Click += new System.EventHandler(this.btnSerialAscii_Click);
            // 
            // btnTCP
            // 
            this.btnTCP.Location = new System.Drawing.Point(12, 79);
            this.btnTCP.Name = "btnTCP";
            this.btnTCP.Size = new System.Drawing.Size(75, 23);
            this.btnTCP.TabIndex = 1;
            this.btnTCP.Text = "TCP/IP";
            this.btnTCP.UseVisualStyleBackColor = true;
            this.btnTCP.Click += new System.EventHandler(this.btnTCP_Click);
            // 
            // btnSerialRtu
            // 
            this.btnSerialRtu.Location = new System.Drawing.Point(12, 131);
            this.btnSerialRtu.Name = "btnSerialRtu";
            this.btnSerialRtu.Size = new System.Drawing.Size(75, 23);
            this.btnSerialRtu.TabIndex = 2;
            this.btnSerialRtu.Text = "SerialRtu";
            this.btnSerialRtu.UseVisualStyleBackColor = true;
            this.btnSerialRtu.Click += new System.EventHandler(this.btnSerialRtu_Click);
            // 
            // btnSocketSerial
            // 
            this.btnSocketSerial.Location = new System.Drawing.Point(12, 179);
            this.btnSocketSerial.Name = "btnSocketSerial";
            this.btnSocketSerial.Size = new System.Drawing.Size(75, 23);
            this.btnSocketSerial.TabIndex = 3;
            this.btnSocketSerial.Text = "SocketSerial";
            this.btnSocketSerial.UseVisualStyleBackColor = true;
            this.btnSocketSerial.Click += new System.EventHandler(this.btnSocketSerial_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 230);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 290);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnSocketSerial);
            this.Controls.Add(this.btnSerialRtu);
            this.Controls.Add(this.btnTCP);
            this.Controls.Add(this.btnSerialAscii);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMenu";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnSerialAscii;
        private Button btnTCP;
        private Button btnSerialRtu;
        private Button btnSocketSerial;
        private Button button5;
    }
}