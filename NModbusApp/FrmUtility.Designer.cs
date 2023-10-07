namespace NModbusApp
{
    partial class FrmUtility
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBitConverter = new System.Windows.Forms.TabPage();
            this.btnConvert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabRegisterFunctions = new System.Windows.Forms.TabPage();
            this.tabCRC = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabBitConverter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabBitConverter);
            this.tabControl1.Controls.Add(this.tabRegisterFunctions);
            this.tabControl1.Controls.Add(this.tabCRC);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 323);
            this.tabControl1.TabIndex = 0;
            // 
            // tabBitConverter
            // 
            this.tabBitConverter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabBitConverter.Controls.Add(this.btnConvert);
            this.tabBitConverter.Controls.Add(this.label1);
            this.tabBitConverter.Location = new System.Drawing.Point(4, 26);
            this.tabBitConverter.Name = "tabBitConverter";
            this.tabBitConverter.Padding = new System.Windows.Forms.Padding(3);
            this.tabBitConverter.Size = new System.Drawing.Size(768, 293);
            this.tabBitConverter.TabIndex = 0;
            this.tabBitConverter.Text = "BitConverter";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(69, 22);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // tabRegisterFunctions
            // 
            this.tabRegisterFunctions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tabRegisterFunctions.Location = new System.Drawing.Point(4, 26);
            this.tabRegisterFunctions.Name = "tabRegisterFunctions";
            this.tabRegisterFunctions.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegisterFunctions.Size = new System.Drawing.Size(675, 327);
            this.tabRegisterFunctions.TabIndex = 1;
            this.tabRegisterFunctions.Text = "RegisterFunctions";
            // 
            // tabCRC
            // 
            this.tabCRC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabCRC.Location = new System.Drawing.Point(4, 26);
            this.tabCRC.Name = "tabCRC";
            this.tabCRC.Padding = new System.Windows.Forms.Padding(3);
            this.tabCRC.Size = new System.Drawing.Size(675, 327);
            this.tabCRC.TabIndex = 2;
            this.tabCRC.Text = "CRC";
            // 
            // FrmUtility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmUtility";
            this.Text = "FrmUtility";
            this.tabControl1.ResumeLayout(false);
            this.tabBitConverter.ResumeLayout(false);
            this.tabBitConverter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabBitConverter;
        private TabPage tabRegisterFunctions;
        private Label label1;
        private TabPage tabCRC;
        private Button btnConvert;
    }
}