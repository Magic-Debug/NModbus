namespace PLCTool.UC
{
    partial class TitleControl
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitleControl));
            this.lbswname = new System.Windows.Forms.Label();
            this.picMin = new System.Windows.Forms.PictureBox();
            this.picMax = new System.Windows.Forms.PictureBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picicon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picicon)).BeginInit();
            this.SuspendLayout();
            // 
            // lbswname
            // 
            this.lbswname.AutoSize = true;
            this.lbswname.Font = new System.Drawing.Font("宋体", 9F);
            this.lbswname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbswname.Location = new System.Drawing.Point(34, 7);
            this.lbswname.Name = "lbswname";
            this.lbswname.Size = new System.Drawing.Size(53, 12);
            this.lbswname.TabIndex = 51;
            this.lbswname.Text = "标题文字";
            // 
            // picMin
            // 
            this.picMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMin.Location = new System.Drawing.Point(410, 0);
            this.picMin.Name = "picMin";
            this.picMin.Size = new System.Drawing.Size(34, 26);
            this.picMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMin.TabIndex = 55;
            this.picMin.TabStop = false;
            this.picMin.Click += new System.EventHandler(this.picMin_Click);
            this.picMin.MouseLeave += new System.EventHandler(this.picMin_MouseLeave);
            this.picMin.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMin_MouseMove);
            // 
            // picMax
            // 
            this.picMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMax.Location = new System.Drawing.Point(444, 0);
            this.picMax.Name = "picMax";
            this.picMax.Size = new System.Drawing.Size(34, 26);
            this.picMax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMax.TabIndex = 54;
            this.picMax.TabStop = false;
            this.picMax.Click += new System.EventHandler(this.picMax_Click);
            this.picMax.MouseLeave += new System.EventHandler(this.picMax_MouseLeave);
            this.picMax.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMax_MouseMove);
            // 
            // picClose
            // 
            this.picClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClose.BackgroundImage = global::PLCTool.UC.TitleControlResource.CloseTransparent;
            this.picClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picClose.Location = new System.Drawing.Point(478, 0);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(34, 26);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picClose.TabIndex = 53;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            this.picClose.MouseLeave += new System.EventHandler(this.picClose_MouseLeave);
            this.picClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picClose_MouseMove);
            // 
            // picicon
            // 
            this.picicon.Image = ((System.Drawing.Image)(resources.GetObject("picicon.Image")));
            this.picicon.Location = new System.Drawing.Point(10, 5);
            this.picicon.Margin = new System.Windows.Forms.Padding(0);
            this.picicon.Name = "picicon";
            this.picicon.Size = new System.Drawing.Size(16, 16);
            this.picicon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picicon.TabIndex = 52;
            this.picicon.TabStop = false;
            // 
            // TitleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.picMin);
            this.Controls.Add(this.picMax);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.lbswname);
            this.Controls.Add(this.picicon);
            this.Name = "TitleControl";
            this.Size = new System.Drawing.Size(512, 26);
            this.SizeChanged += new System.EventHandler(this.TitleControl_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleControl_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picicon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picMin;
        private System.Windows.Forms.PictureBox picMax;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Label lbswname;
        private System.Windows.Forms.PictureBox picicon;


    }
}
