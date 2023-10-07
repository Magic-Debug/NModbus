
namespace PLCTool.Forms
{
    partial class FormColourDifferenceDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormColourDifferenceDlg));
            this.comboBoxPortNames = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSignal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonScan = new PLCTool.UC.ButtonRadiusEx();
            this.buttonOK = new PLCTool.UC.ButtonRadiusEx();
            this.buttonCancle = new PLCTool.UC.ButtonRadiusEx();
            this.SuspendLayout();
            // 
            // comboBoxPortNames
            // 
            this.comboBoxPortNames.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxPortNames, "comboBoxPortNames");
            this.comboBoxPortNames.Name = "comboBoxPortNames";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnMAC,
            this.columnSignal});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnName
            // 
            resources.ApplyResources(this.columnName, "columnName");
            // 
            // columnMAC
            // 
            resources.ApplyResources(this.columnMAC, "columnMAC");
            // 
            // columnSignal
            // 
            resources.ApplyResources(this.columnSignal, "columnSignal");
            // 
            // buttonScan
            // 
            this.buttonScan.BorderColor = System.Drawing.Color.White;
            this.buttonScan.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonScan.DrawBorder = true;
            this.buttonScan.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonScan, "buttonScan");
            this.buttonScan.ForeColor = System.Drawing.Color.White;
            this.buttonScan.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonScan.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonScan.UseVisualStyleBackColor = true;
            this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.BorderColor = System.Drawing.Color.White;
            this.buttonOK.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonOK.DrawBorder = true;
            this.buttonOK.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.ForeColor = System.Drawing.Color.White;
            this.buttonOK.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonOK.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancle
            // 
            this.buttonCancle.BorderColor = System.Drawing.Color.White;
            this.buttonCancle.ControlState = PLCTool.UC.ControlState.Normal;
            this.buttonCancle.DrawBorder = true;
            this.buttonCancle.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonCancle, "buttonCancle");
            this.buttonCancle.ForeColor = System.Drawing.Color.White;
            this.buttonCancle.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(182)))), ((int)(((byte)(230)))));
            this.buttonCancle.Name = "buttonCancle";
            this.buttonCancle.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(116)))), ((int)(((byte)(173)))));
            this.buttonCancle.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.buttonCancle.UseVisualStyleBackColor = true;
            this.buttonCancle.Click += new System.EventHandler(this.buttonCancle_Click);
            // 
            // FormColourDifferenceDlg
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCancle);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonScan);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.comboBoxPortNames);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormColourDifferenceDlg";
            this.Controls.SetChildIndex(this.comboBoxPortNames, 0);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.Controls.SetChildIndex(this.buttonScan, 0);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.buttonCancle, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPortNames;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnMAC;
        private System.Windows.Forms.ColumnHeader columnSignal;
        private UC.ButtonRadiusEx buttonScan;
        private UC.ButtonRadiusEx buttonOK;
        private UC.ButtonRadiusEx buttonCancle;
    }
}