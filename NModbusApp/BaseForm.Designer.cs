namespace NModbusApp
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFunctionCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rTextBoxHoldingRegisters32 = new System.Windows.Forms.RichTextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.txtNumberOfPoints = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRegister = new System.Windows.Forms.TextBox();
            this.btnReadCoils = new System.Windows.Forms.Button();
            this.btnWriteSingleCoil = new System.Windows.Forms.Button();
            this.btnWriteSingleRegister = new System.Windows.Forms.Button();
            this.btnWriteMultipleRegisters = new System.Windows.Forms.Button();
            this.btnWriteMultipleCoils = new System.Windows.Forms.Button();
            this.btnWriteFileRecord = new System.Windows.Forms.Button();
            this.btnReadInputs = new System.Windows.Forms.Button();
            this.btnReadHoldingRegisters = new System.Windows.Forms.Button();
            this.btnReadInputRegisters = new System.Windows.Forms.Button();
            this.btnReadWriteMultipleRegisters = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(302, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "btnOK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "FunctionCode:";
            // 
            // txtFunctionCode
            // 
            this.txtFunctionCode.Location = new System.Drawing.Point(165, 22);
            this.txtFunctionCode.Name = "txtFunctionCode";
            this.txtFunctionCode.Size = new System.Drawing.Size(100, 23);
            this.txtFunctionCode.TabIndex = 2;
            this.txtFunctionCode.Text = "4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "SlaveAddress:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "ByteCount:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "StartAddress:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "NumberOfPoints:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "SubFunctionCode:";
            // 
            // rTextBoxHoldingRegisters32
            // 
            this.rTextBoxHoldingRegisters32.Location = new System.Drawing.Point(30, 291);
            this.rTextBoxHoldingRegisters32.Name = "rTextBoxHoldingRegisters32";
            this.rTextBoxHoldingRegisters32.Size = new System.Drawing.Size(524, 96);
            this.rTextBoxHoldingRegisters32.TabIndex = 8;
            this.rTextBoxHoldingRegisters32.Text = "";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(165, 51);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 23);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "7";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(165, 78);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 23);
            this.textBox3.TabIndex = 10;
            this.textBox3.Text = "5";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(165, 115);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 23);
            this.textBox4.TabIndex = 11;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(165, 144);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 23);
            this.textBox5.TabIndex = 12;
            this.textBox5.Text = "12";
            // 
            // txtNumberOfPoints
            // 
            this.txtNumberOfPoints.Location = new System.Drawing.Point(165, 168);
            this.txtNumberOfPoints.Name = "txtNumberOfPoints";
            this.txtNumberOfPoints.Size = new System.Drawing.Size(100, 23);
            this.txtNumberOfPoints.TabIndex = 13;
            this.txtNumberOfPoints.Text = "6";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(83, 207);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(43, 17);
            this.lblValue.TabIndex = 15;
            this.lblValue.Text = "Value:";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(165, 201);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 23);
            this.txtValue.TabIndex = 14;
            this.txtValue.Text = "36";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(76, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Register:";
            // 
            // txtRegister
            // 
            this.txtRegister.Location = new System.Drawing.Point(165, 245);
            this.txtRegister.Name = "txtRegister";
            this.txtRegister.Size = new System.Drawing.Size(100, 23);
            this.txtRegister.TabIndex = 16;
            this.txtRegister.Text = "2";
            // 
            // btnReadCoils
            // 
            this.btnReadCoils.Location = new System.Drawing.Point(302, 66);
            this.btnReadCoils.Name = "btnReadCoils";
            this.btnReadCoils.Size = new System.Drawing.Size(75, 23);
            this.btnReadCoils.TabIndex = 18;
            this.btnReadCoils.Text = "ReadCoils";
            this.btnReadCoils.UseVisualStyleBackColor = true;
            this.btnReadCoils.Click += new System.EventHandler(this.btnReadCoils_Click);
            // 
            // btnWriteSingleCoil
            // 
            this.btnWriteSingleCoil.ForeColor = System.Drawing.Color.Fuchsia;
            this.btnWriteSingleCoil.Location = new System.Drawing.Point(516, 146);
            this.btnWriteSingleCoil.Name = "btnWriteSingleCoil";
            this.btnWriteSingleCoil.Size = new System.Drawing.Size(119, 26);
            this.btnWriteSingleCoil.TabIndex = 19;
            this.btnWriteSingleCoil.Text = "WriteSingleCoil";
            this.btnWriteSingleCoil.UseVisualStyleBackColor = true;
            this.btnWriteSingleCoil.Click += new System.EventHandler(this.btnWriteSingleCoil_Click);
            // 
            // btnWriteSingleRegister
            // 
            this.btnWriteSingleRegister.ForeColor = System.Drawing.Color.Red;
            this.btnWriteSingleRegister.Location = new System.Drawing.Point(516, 19);
            this.btnWriteSingleRegister.Name = "btnWriteSingleRegister";
            this.btnWriteSingleRegister.Size = new System.Drawing.Size(150, 23);
            this.btnWriteSingleRegister.TabIndex = 20;
            this.btnWriteSingleRegister.Text = "WriteSingleRegister";
            this.btnWriteSingleRegister.UseVisualStyleBackColor = true;
            this.btnWriteSingleRegister.Click += new System.EventHandler(this.btnWriteSingleRegister_Click);
            // 
            // btnWriteMultipleRegisters
            // 
            this.btnWriteMultipleRegisters.Location = new System.Drawing.Point(516, 66);
            this.btnWriteMultipleRegisters.Name = "btnWriteMultipleRegisters";
            this.btnWriteMultipleRegisters.Size = new System.Drawing.Size(153, 23);
            this.btnWriteMultipleRegisters.TabIndex = 21;
            this.btnWriteMultipleRegisters.Text = "WriteMultipleRegisters";
            this.btnWriteMultipleRegisters.UseVisualStyleBackColor = true;
            this.btnWriteMultipleRegisters.Click += new System.EventHandler(this.btnWriteMultipleRegisters_Click);
            // 
            // btnWriteMultipleCoils
            // 
            this.btnWriteMultipleCoils.Location = new System.Drawing.Point(516, 106);
            this.btnWriteMultipleCoils.Name = "btnWriteMultipleCoils";
            this.btnWriteMultipleCoils.Size = new System.Drawing.Size(177, 26);
            this.btnWriteMultipleCoils.TabIndex = 22;
            this.btnWriteMultipleCoils.Text = "WriteMultipleCoils";
            this.btnWriteMultipleCoils.UseVisualStyleBackColor = true;
            this.btnWriteMultipleCoils.Click += new System.EventHandler(this.btnWriteMultipleCoils_Click);
            // 
            // btnWriteFileRecord
            // 
            this.btnWriteFileRecord.Location = new System.Drawing.Point(516, 192);
            this.btnWriteFileRecord.Name = "btnWriteFileRecord";
            this.btnWriteFileRecord.Size = new System.Drawing.Size(119, 32);
            this.btnWriteFileRecord.TabIndex = 23;
            this.btnWriteFileRecord.Text = "WriteFileRecord";
            this.btnWriteFileRecord.UseVisualStyleBackColor = true;
            this.btnWriteFileRecord.Click += new System.EventHandler(this.btnWriteFileRecord_Click);
            // 
            // btnReadInputs
            // 
            this.btnReadInputs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnReadInputs.Location = new System.Drawing.Point(302, 103);
            this.btnReadInputs.Name = "btnReadInputs";
            this.btnReadInputs.Size = new System.Drawing.Size(89, 32);
            this.btnReadInputs.TabIndex = 24;
            this.btnReadInputs.Text = "ReadInputs";
            this.btnReadInputs.UseVisualStyleBackColor = false;
            this.btnReadInputs.Click += new System.EventHandler(this.btnReadInputs_Click);
            // 
            // btnReadHoldingRegisters
            // 
            this.btnReadHoldingRegisters.Location = new System.Drawing.Point(283, 144);
            this.btnReadHoldingRegisters.Name = "btnReadHoldingRegisters";
            this.btnReadHoldingRegisters.Size = new System.Drawing.Size(170, 23);
            this.btnReadHoldingRegisters.TabIndex = 25;
            this.btnReadHoldingRegisters.Text = "ReadHoldingRegisters";
            this.btnReadHoldingRegisters.UseVisualStyleBackColor = true;
            this.btnReadHoldingRegisters.Click += new System.EventHandler(this.btnReadHoldingRegisters_Click);
            // 
            // btnReadInputRegisters
            // 
            this.btnReadInputRegisters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnReadInputRegisters.Location = new System.Drawing.Point(283, 178);
            this.btnReadInputRegisters.Name = "btnReadInputRegisters";
            this.btnReadInputRegisters.Size = new System.Drawing.Size(147, 33);
            this.btnReadInputRegisters.TabIndex = 26;
            this.btnReadInputRegisters.Text = "ReadInputRegisters";
            this.btnReadInputRegisters.UseVisualStyleBackColor = false;
            this.btnReadInputRegisters.Click += new System.EventHandler(this.btnReadInputRegisters_Click);
            // 
            // btnReadWriteMultipleRegisters
            // 
            this.btnReadWriteMultipleRegisters.Location = new System.Drawing.Point(283, 236);
            this.btnReadWriteMultipleRegisters.Name = "btnReadWriteMultipleRegisters";
            this.btnReadWriteMultipleRegisters.Size = new System.Drawing.Size(183, 32);
            this.btnReadWriteMultipleRegisters.TabIndex = 27;
            this.btnReadWriteMultipleRegisters.Text = "ReadWriteMultipleRegisters";
            this.btnReadWriteMultipleRegisters.UseVisualStyleBackColor = true;
            this.btnReadWriteMultipleRegisters.Click += new System.EventHandler(this.btnReadWriteMultipleRegisters_Click);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 509);
            this.Controls.Add(this.btnReadWriteMultipleRegisters);
            this.Controls.Add(this.btnReadInputRegisters);
            this.Controls.Add(this.btnReadHoldingRegisters);
            this.Controls.Add(this.btnReadInputs);
            this.Controls.Add(this.btnWriteFileRecord);
            this.Controls.Add(this.btnWriteMultipleCoils);
            this.Controls.Add(this.btnWriteMultipleRegisters);
            this.Controls.Add(this.btnWriteSingleRegister);
            this.Controls.Add(this.btnWriteSingleCoil);
            this.Controls.Add(this.btnReadCoils);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRegister);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtNumberOfPoints);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.rTextBoxHoldingRegisters32);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFunctionCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Label label1;
        private TextBox txtFunctionCode;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private RichTextBox rTextBoxHoldingRegisters32;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox txtNumberOfPoints;
        private Label lblValue;
        private TextBox txtValue;
        private Label label7;
        private TextBox txtRegister;
        private Button btnReadCoils;
        private Button btnWriteSingleCoil;
        private Button btnWriteSingleRegister;
        private Button btnWriteMultipleRegisters;
        private Button btnWriteMultipleCoils;
        private Button btnWriteFileRecord;
        private Button btnReadInputs;
        private Button btnReadHoldingRegisters;
        private Button btnReadInputRegisters;
        private Button btnReadWriteMultipleRegisters;
    }
}