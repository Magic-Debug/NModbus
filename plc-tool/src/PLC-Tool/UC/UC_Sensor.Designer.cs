namespace PLCTool.UC
{
    partial class UC_Sensor
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.pbxStatusImage = new System.Windows.Forms.PictureBox();
            this.lblGroupSubName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxStatusImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(36, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(77, 14);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "SensorName";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;          
            this.lblName.DoubleClick += new System.EventHandler(this.lbl_DoubleClick);
            // 
            // pbxStatusImage
            // 
            this.pbxStatusImage.Location = new System.Drawing.Point(0, 0);
            this.pbxStatusImage.Name = "pbxStatusImage";
            this.pbxStatusImage.Size = new System.Drawing.Size(32, 32);
            this.pbxStatusImage.TabIndex = 2;
            this.pbxStatusImage.TabStop = false;          
            this.pbxStatusImage.DoubleClick += new System.EventHandler(this.lbl_DoubleClick);
            // 
            // lblGroupSubName
            // 
            this.lblGroupSubName.AutoSize = true;
            this.lblGroupSubName.BackColor = System.Drawing.Color.Transparent;
            this.lblGroupSubName.Font = new System.Drawing.Font("微软雅黑", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGroupSubName.Location = new System.Drawing.Point(8, 12);
            this.lblGroupSubName.Name = "lblGroupSubName";
            this.lblGroupSubName.Size = new System.Drawing.Size(13, 9);
            this.lblGroupSubName.TabIndex = 4;
            this.lblGroupSubName.Text = "a0";
            this.lblGroupSubName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_Sensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblGroupSubName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbxStatusImage);
            this.Name = "UC_Sensor";
            this.Size = new System.Drawing.Size(240, 32);
            ((System.ComponentModel.ISupportInitialize)(this.pbxStatusImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pbxStatusImage;
        private System.Windows.Forms.Label lblGroupSubName;
    }
}
