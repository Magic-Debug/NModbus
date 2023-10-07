namespace PLCTool.Forms
{
    partial class FormManual
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormManual));
            this.lblManual = new System.Windows.Forms.Label();
            this.pcbManual = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBoxEx6 = new PLCTool.UC.GroupBoxEx(this.components);
            this.btnZdjForward = new PLCTool.UC.ButtonRadiusEx();
            this.btnZdjStop = new PLCTool.UC.ButtonRadiusEx();
            this.btnZdjBack = new PLCTool.UC.ButtonRadiusEx();
            this.groupBoxEx5 = new PLCTool.UC.GroupBoxEx(this.components);
            this.btnZldjStop = new PLCTool.UC.ButtonRadiusEx();
            this.pcbZldjForward = new System.Windows.Forms.PictureBox();
            this.pcbZldjBack = new System.Windows.Forms.PictureBox();
            this.groupBoxEx4 = new PLCTool.UC.GroupBoxEx(this.components);
            this.btnFjdjStop = new PLCTool.UC.ButtonRadiusEx();
            this.pcbFjdjForward = new System.Windows.Forms.PictureBox();
            this.pcbFjdjBack = new System.Windows.Forms.PictureBox();
            this.groupBoxEx3 = new PLCTool.UC.GroupBoxEx(this.components);
            this.btnZReturn = new PLCTool.UC.ButtonRadiusEx();
            this.btnZstop = new PLCTool.UC.ButtonRadiusEx();
            this.btnZDown = new PLCTool.UC.ButtonRadiusEx();
            this.btnZUp = new PLCTool.UC.ButtonRadiusEx();
            this.groupBoxEx2 = new PLCTool.UC.GroupBoxEx(this.components);
            this.btnXReturn = new PLCTool.UC.ButtonRadiusEx();
            this.btnXStop = new PLCTool.UC.ButtonRadiusEx();
            this.btnXRight = new PLCTool.UC.ButtonRadiusEx();
            this.btnXLeft = new PLCTool.UC.ButtonRadiusEx();
            this.groupBoxEx1 = new PLCTool.UC.GroupBoxEx(this.components);
            this.lblZlfbdj = new System.Windows.Forms.Label();
            this.lblQg = new System.Windows.Forms.Label();
            this.pcbZlfbdj = new System.Windows.Forms.PictureBox();
            this.lblZkf = new System.Windows.Forms.Label();
            this.pcbJbdj = new System.Windows.Forms.PictureBox();
            this.lblJbdj = new System.Windows.Forms.Label();
            this.pcbZkf = new System.Windows.Forms.PictureBox();
            this.pcbQg = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pcbManual)).BeginInit();
            this.groupBoxEx6.SuspendLayout();
            this.groupBoxEx5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbZldjForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbZldjBack)).BeginInit();
            this.groupBoxEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFjdjForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFjdjBack)).BeginInit();
            this.groupBoxEx3.SuspendLayout();
            this.groupBoxEx2.SuspendLayout();
            this.groupBoxEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbZlfbdj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbJbdj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbZkf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbQg)).BeginInit();
            this.SuspendLayout();
            // 
            // lblManual
            // 
            resources.ApplyResources(this.lblManual, "lblManual");
            this.lblManual.Name = "lblManual";
            this.toolTip1.SetToolTip(this.lblManual, resources.GetString("lblManual.ToolTip"));
            // 
            // pcbManual
            // 
            this.pcbManual.Image = global::PLCTool.Properties.Resources.BtnOff2;
            resources.ApplyResources(this.pcbManual, "pcbManual");
            this.pcbManual.Name = "pcbManual";
            this.pcbManual.TabStop = false;
            this.toolTip1.SetToolTip(this.pcbManual, resources.GetString("pcbManual.ToolTip"));
            this.pcbManual.Click += new System.EventHandler(this.pcbManual_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBoxEx6
            // 
            this.groupBoxEx6.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx6.Controls.Add(this.btnZdjForward);
            this.groupBoxEx6.Controls.Add(this.btnZdjStop);
            this.groupBoxEx6.Controls.Add(this.btnZdjBack);
            this.groupBoxEx6.Fillet = 0;
            resources.ApplyResources(this.groupBoxEx6, "groupBoxEx6");
            this.groupBoxEx6.Name = "groupBoxEx6";
            this.groupBoxEx6.TabStop = false;
            // 
            // btnZdjForward
            // 
            this.btnZdjForward.BaseColor = System.Drawing.Color.White;
            this.btnZdjForward.BorderColor = System.Drawing.Color.Black;
            this.btnZdjForward.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnZdjForward.DrawBorder = true;
            resources.ApplyResources(this.btnZdjForward, "btnZdjForward");
            this.btnZdjForward.Name = "btnZdjForward";
            this.btnZdjForward.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.toolTip1.SetToolTip(this.btnZdjForward, resources.GetString("btnZdjForward.ToolTip"));
            this.btnZdjForward.UseVisualStyleBackColor = true;
            this.btnZdjForward.Click += new System.EventHandler(this.uC_ForwardBackControl3_ForwardClick);
            // 
            // btnZdjStop
            // 
            this.btnZdjStop.BaseColor = System.Drawing.Color.White;
            this.btnZdjStop.BorderColor = System.Drawing.Color.Black;
            this.btnZdjStop.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnZdjStop.DrawBorder = true;
            resources.ApplyResources(this.btnZdjStop, "btnZdjStop");
            this.btnZdjStop.Name = "btnZdjStop";
            this.btnZdjStop.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnZdjStop.UseVisualStyleBackColor = true;
            this.btnZdjStop.Click += new System.EventHandler(this.uC_ForwardBackControl3_StopClick);
            // 
            // btnZdjBack
            // 
            this.btnZdjBack.BaseColor = System.Drawing.Color.White;
            this.btnZdjBack.BorderColor = System.Drawing.Color.Black;
            this.btnZdjBack.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnZdjBack.DrawBorder = true;
            resources.ApplyResources(this.btnZdjBack, "btnZdjBack");
            this.btnZdjBack.Name = "btnZdjBack";
            this.btnZdjBack.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.toolTip1.SetToolTip(this.btnZdjBack, resources.GetString("btnZdjBack.ToolTip"));
            this.btnZdjBack.UseVisualStyleBackColor = true;
            this.btnZdjBack.Click += new System.EventHandler(this.uC_ForwardBackControl3_BackClick);
            // 
            // groupBoxEx5
            // 
            this.groupBoxEx5.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx5.Controls.Add(this.btnZldjStop);
            this.groupBoxEx5.Controls.Add(this.pcbZldjForward);
            this.groupBoxEx5.Controls.Add(this.pcbZldjBack);
            this.groupBoxEx5.Fillet = 0;
            resources.ApplyResources(this.groupBoxEx5, "groupBoxEx5");
            this.groupBoxEx5.Name = "groupBoxEx5";
            this.groupBoxEx5.TabStop = false;
            // 
            // btnZldjStop
            // 
            this.btnZldjStop.BaseColor = System.Drawing.Color.White;
            this.btnZldjStop.BorderColor = System.Drawing.Color.Black;
            this.btnZldjStop.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnZldjStop.DrawBorder = true;
            resources.ApplyResources(this.btnZldjStop, "btnZldjStop");
            this.btnZldjStop.Name = "btnZldjStop";
            this.btnZldjStop.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnZldjStop.UseVisualStyleBackColor = true;
            this.btnZldjStop.Click += new System.EventHandler(this.uC_ForwardBackControl2_StopClick);
            // 
            // pcbZldjForward
            // 
            this.pcbZldjForward.Image = global::PLCTool.Properties.Resources.Forward2;
            resources.ApplyResources(this.pcbZldjForward, "pcbZldjForward");
            this.pcbZldjForward.Name = "pcbZldjForward";
            this.pcbZldjForward.TabStop = false;
            this.pcbZldjForward.Click += new System.EventHandler(this.uC_ForwardBackControl2_ForwardClick);
            // 
            // pcbZldjBack
            // 
            this.pcbZldjBack.Image = global::PLCTool.Properties.Resources.Back2;
            resources.ApplyResources(this.pcbZldjBack, "pcbZldjBack");
            this.pcbZldjBack.Name = "pcbZldjBack";
            this.pcbZldjBack.TabStop = false;
            this.pcbZldjBack.Click += new System.EventHandler(this.uC_ForwardBackControl2_BackClick);
            // 
            // groupBoxEx4
            // 
            this.groupBoxEx4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx4.Controls.Add(this.btnFjdjStop);
            this.groupBoxEx4.Controls.Add(this.pcbFjdjForward);
            this.groupBoxEx4.Controls.Add(this.pcbFjdjBack);
            this.groupBoxEx4.Fillet = 0;
            resources.ApplyResources(this.groupBoxEx4, "groupBoxEx4");
            this.groupBoxEx4.Name = "groupBoxEx4";
            this.groupBoxEx4.TabStop = false;
            // 
            // btnFjdjStop
            // 
            this.btnFjdjStop.BaseColor = System.Drawing.Color.White;
            this.btnFjdjStop.BorderColor = System.Drawing.Color.Black;
            this.btnFjdjStop.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnFjdjStop.DrawBorder = true;
            resources.ApplyResources(this.btnFjdjStop, "btnFjdjStop");
            this.btnFjdjStop.Name = "btnFjdjStop";
            this.btnFjdjStop.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.btnFjdjStop.UseVisualStyleBackColor = true;
            this.btnFjdjStop.Click += new System.EventHandler(this.uC_ForwardBackControl1_StopClick);
            // 
            // pcbFjdjForward
            // 
            this.pcbFjdjForward.Image = global::PLCTool.Properties.Resources.Forward2;
            resources.ApplyResources(this.pcbFjdjForward, "pcbFjdjForward");
            this.pcbFjdjForward.Name = "pcbFjdjForward";
            this.pcbFjdjForward.TabStop = false;
            this.pcbFjdjForward.Click += new System.EventHandler(this.uC_ForwardBackControl1_ForwardClick);
            // 
            // pcbFjdjBack
            // 
            this.pcbFjdjBack.Image = global::PLCTool.Properties.Resources.Back2;
            resources.ApplyResources(this.pcbFjdjBack, "pcbFjdjBack");
            this.pcbFjdjBack.Name = "pcbFjdjBack";
            this.pcbFjdjBack.TabStop = false;
            this.pcbFjdjBack.Click += new System.EventHandler(this.uC_ForwardBackControl1_BackClick);
            // 
            // groupBoxEx3
            // 
            this.groupBoxEx3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx3.Controls.Add(this.btnZReturn);
            this.groupBoxEx3.Controls.Add(this.btnZstop);
            this.groupBoxEx3.Controls.Add(this.btnZDown);
            this.groupBoxEx3.Controls.Add(this.btnZUp);
            this.groupBoxEx3.Fillet = 0;
            resources.ApplyResources(this.groupBoxEx3, "groupBoxEx3");
            this.groupBoxEx3.Name = "groupBoxEx3";
            this.groupBoxEx3.TabStop = false;
            // 
            // btnZReturn
            // 
            this.btnZReturn.BaseColor = System.Drawing.Color.White;
            this.btnZReturn.BorderColor = System.Drawing.Color.Black;
            this.btnZReturn.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnZReturn.DrawBorder = true;
            resources.ApplyResources(this.btnZReturn, "btnZReturn");
            this.btnZReturn.Name = "btnZReturn";
            this.btnZReturn.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.toolTip1.SetToolTip(this.btnZReturn, resources.GetString("btnZReturn.ToolTip"));
            this.btnZReturn.UseVisualStyleBackColor = true;
            this.btnZReturn.Click += new System.EventHandler(this.chkZreturn_Click);
            // 
            // btnZstop
            // 
            this.btnZstop.BaseColor = System.Drawing.Color.White;
            this.btnZstop.BorderColor = System.Drawing.Color.Black;
            this.btnZstop.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnZstop.DrawBorder = true;
            resources.ApplyResources(this.btnZstop, "btnZstop");
            this.btnZstop.Name = "btnZstop";
            this.btnZstop.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.toolTip1.SetToolTip(this.btnZstop, resources.GetString("btnZstop.ToolTip"));
            this.btnZstop.UseVisualStyleBackColor = true;
            this.btnZstop.Click += new System.EventHandler(this.chkZstop_Click);
            // 
            // btnZDown
            // 
            this.btnZDown.BaseColor = System.Drawing.Color.White;
            this.btnZDown.BorderColor = System.Drawing.Color.Black;
            this.btnZDown.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnZDown.DrawBorder = true;
            resources.ApplyResources(this.btnZDown, "btnZDown");
            this.btnZDown.Name = "btnZDown";
            this.btnZDown.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.toolTip1.SetToolTip(this.btnZDown, resources.GetString("btnZDown.ToolTip"));
            this.btnZDown.UseVisualStyleBackColor = true;
            this.btnZDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pcbZdown_MouseDown);
            this.btnZDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pcbZdown_MouseUp);
            // 
            // btnZUp
            // 
            this.btnZUp.BaseColor = System.Drawing.Color.White;
            this.btnZUp.BorderColor = System.Drawing.Color.Black;
            this.btnZUp.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnZUp.DrawBorder = true;
            resources.ApplyResources(this.btnZUp, "btnZUp");
            this.btnZUp.Name = "btnZUp";
            this.btnZUp.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.toolTip1.SetToolTip(this.btnZUp, resources.GetString("btnZUp.ToolTip"));
            this.btnZUp.UseVisualStyleBackColor = true;
            this.btnZUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pcbZup_MouseDown);
            this.btnZUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pcbZup_MouseUp);
            // 
            // groupBoxEx2
            // 
            this.groupBoxEx2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx2.Controls.Add(this.btnXReturn);
            this.groupBoxEx2.Controls.Add(this.btnXStop);
            this.groupBoxEx2.Controls.Add(this.btnXRight);
            this.groupBoxEx2.Controls.Add(this.btnXLeft);
            this.groupBoxEx2.Fillet = 0;
            resources.ApplyResources(this.groupBoxEx2, "groupBoxEx2");
            this.groupBoxEx2.Name = "groupBoxEx2";
            this.groupBoxEx2.TabStop = false;
            // 
            // btnXReturn
            // 
            this.btnXReturn.BaseColor = System.Drawing.Color.White;
            this.btnXReturn.BorderColor = System.Drawing.Color.Black;
            this.btnXReturn.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnXReturn.DrawBorder = true;
            resources.ApplyResources(this.btnXReturn, "btnXReturn");
            this.btnXReturn.Name = "btnXReturn";
            this.btnXReturn.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.toolTip1.SetToolTip(this.btnXReturn, resources.GetString("btnXReturn.ToolTip"));
            this.btnXReturn.UseVisualStyleBackColor = true;
            this.btnXReturn.Click += new System.EventHandler(this.chkXreturn_Click);
            // 
            // btnXStop
            // 
            this.btnXStop.BaseColor = System.Drawing.Color.White;
            this.btnXStop.BorderColor = System.Drawing.Color.Black;
            this.btnXStop.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnXStop.DrawBorder = true;
            resources.ApplyResources(this.btnXStop, "btnXStop");
            this.btnXStop.Name = "btnXStop";
            this.btnXStop.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.toolTip1.SetToolTip(this.btnXStop, resources.GetString("btnXStop.ToolTip"));
            this.btnXStop.UseVisualStyleBackColor = true;
            this.btnXStop.Click += new System.EventHandler(this.chkXstop_Click);
            // 
            // btnXRight
            // 
            this.btnXRight.BaseColor = System.Drawing.Color.White;
            this.btnXRight.BorderColor = System.Drawing.Color.Black;
            this.btnXRight.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnXRight.DrawBorder = true;
            resources.ApplyResources(this.btnXRight, "btnXRight");
            this.btnXRight.Name = "btnXRight";
            this.btnXRight.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.toolTip1.SetToolTip(this.btnXRight, resources.GetString("btnXRight.ToolTip"));
            this.btnXRight.UseVisualStyleBackColor = true;
            this.btnXRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pcbXright_MouseDown);
            this.btnXRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pcbXright_MouseUp);
            // 
            // btnXLeft
            // 
            this.btnXLeft.BaseColor = System.Drawing.Color.White;
            this.btnXLeft.BorderColor = System.Drawing.Color.Black;
            this.btnXLeft.ControlState = PLCTool.UC.ControlState.Normal;
            this.btnXLeft.DrawBorder = true;
            resources.ApplyResources(this.btnXLeft, "btnXLeft");
            this.btnXLeft.Name = "btnXLeft";
            this.btnXLeft.RoundStyle = PLCTool.UC.RoundStyle.All;
            this.toolTip1.SetToolTip(this.btnXLeft, resources.GetString("btnXLeft.ToolTip"));
            this.btnXLeft.UseVisualStyleBackColor = true;
            this.btnXLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pcbXleft_MouseDown);
            this.btnXLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pcbXleft_MouseUp);
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBoxEx1.Controls.Add(this.lblZlfbdj);
            this.groupBoxEx1.Controls.Add(this.lblQg);
            this.groupBoxEx1.Controls.Add(this.pcbZlfbdj);
            this.groupBoxEx1.Controls.Add(this.lblZkf);
            this.groupBoxEx1.Controls.Add(this.pcbJbdj);
            this.groupBoxEx1.Controls.Add(this.lblJbdj);
            this.groupBoxEx1.Controls.Add(this.pcbZkf);
            this.groupBoxEx1.Controls.Add(this.pcbQg);
            this.groupBoxEx1.Fillet = 0;
            resources.ApplyResources(this.groupBoxEx1, "groupBoxEx1");
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.TabStop = false;
            // 
            // lblZlfbdj
            // 
            resources.ApplyResources(this.lblZlfbdj, "lblZlfbdj");
            this.lblZlfbdj.Name = "lblZlfbdj";
            this.toolTip1.SetToolTip(this.lblZlfbdj, resources.GetString("lblZlfbdj.ToolTip"));
            // 
            // lblQg
            // 
            resources.ApplyResources(this.lblQg, "lblQg");
            this.lblQg.Name = "lblQg";
            this.toolTip1.SetToolTip(this.lblQg, resources.GetString("lblQg.ToolTip"));
            // 
            // pcbZlfbdj
            // 
            this.pcbZlfbdj.Image = global::PLCTool.Properties.Resources.BtnOff2;
            resources.ApplyResources(this.pcbZlfbdj, "pcbZlfbdj");
            this.pcbZlfbdj.Name = "pcbZlfbdj";
            this.pcbZlfbdj.TabStop = false;
            this.toolTip1.SetToolTip(this.pcbZlfbdj, resources.GetString("pcbZlfbdj.ToolTip"));
            this.pcbZlfbdj.Click += new System.EventHandler(this.pcbZlfbdj_Click);
            // 
            // lblZkf
            // 
            resources.ApplyResources(this.lblZkf, "lblZkf");
            this.lblZkf.Name = "lblZkf";
            this.toolTip1.SetToolTip(this.lblZkf, resources.GetString("lblZkf.ToolTip"));
            // 
            // pcbJbdj
            // 
            this.pcbJbdj.Image = global::PLCTool.Properties.Resources.BtnOff2;
            resources.ApplyResources(this.pcbJbdj, "pcbJbdj");
            this.pcbJbdj.Name = "pcbJbdj";
            this.pcbJbdj.TabStop = false;
            this.toolTip1.SetToolTip(this.pcbJbdj, resources.GetString("pcbJbdj.ToolTip"));
            this.pcbJbdj.Click += new System.EventHandler(this.pcbJbdj_Click);
            // 
            // lblJbdj
            // 
            resources.ApplyResources(this.lblJbdj, "lblJbdj");
            this.lblJbdj.Name = "lblJbdj";
            this.toolTip1.SetToolTip(this.lblJbdj, resources.GetString("lblJbdj.ToolTip"));
            // 
            // pcbZkf
            // 
            this.pcbZkf.Image = global::PLCTool.Properties.Resources.BtnOff2;
            resources.ApplyResources(this.pcbZkf, "pcbZkf");
            this.pcbZkf.Name = "pcbZkf";
            this.pcbZkf.TabStop = false;
            this.toolTip1.SetToolTip(this.pcbZkf, resources.GetString("pcbZkf.ToolTip"));
            this.pcbZkf.Click += new System.EventHandler(this.pcbZkf_Click);
            // 
            // pcbQg
            // 
            this.pcbQg.Image = global::PLCTool.Properties.Resources.BtnOff2;
            resources.ApplyResources(this.pcbQg, "pcbQg");
            this.pcbQg.Name = "pcbQg";
            this.pcbQg.TabStop = false;
            this.toolTip1.SetToolTip(this.pcbQg, resources.GetString("pcbQg.ToolTip"));
            this.pcbQg.Click += new System.EventHandler(this.pcbQg_Click);
            // 
            // FormManual
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBoxEx6);
            this.Controls.Add(this.groupBoxEx5);
            this.Controls.Add(this.groupBoxEx4);
            this.Controls.Add(this.groupBoxEx3);
            this.Controls.Add(this.groupBoxEx2);
            this.Controls.Add(this.groupBoxEx1);
            this.Controls.Add(this.pcbManual);
            this.Controls.Add(this.lblManual);
            this.MaximizeBox = false;
            this.Name = "FormManual";
            this.Controls.SetChildIndex(this.lblManual, 0);
            this.Controls.SetChildIndex(this.pcbManual, 0);
            this.Controls.SetChildIndex(this.groupBoxEx1, 0);
            this.Controls.SetChildIndex(this.groupBoxEx2, 0);
            this.Controls.SetChildIndex(this.groupBoxEx3, 0);
            this.Controls.SetChildIndex(this.groupBoxEx4, 0);
            this.Controls.SetChildIndex(this.groupBoxEx5, 0);
            this.Controls.SetChildIndex(this.groupBoxEx6, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcbManual)).EndInit();
            this.groupBoxEx6.ResumeLayout(false);
            this.groupBoxEx5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbZldjForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbZldjBack)).EndInit();
            this.groupBoxEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbFjdjForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFjdjBack)).EndInit();
            this.groupBoxEx3.ResumeLayout(false);
            this.groupBoxEx2.ResumeLayout(false);
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbZlfbdj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbJbdj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbZkf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbQg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UC.GroupBoxEx groupBoxEx3;
        private UC.ButtonRadiusEx btnZReturn;
        private UC.ButtonRadiusEx btnZstop;
        private UC.ButtonRadiusEx btnZDown;
        private UC.ButtonRadiusEx btnZUp;
        private UC.GroupBoxEx groupBoxEx2;
        private UC.ButtonRadiusEx btnXReturn;
        private UC.ButtonRadiusEx btnXStop;
        private UC.ButtonRadiusEx btnXRight;
        private UC.ButtonRadiusEx btnXLeft;
        private UC.GroupBoxEx groupBoxEx1;
        private System.Windows.Forms.Label lblZlfbdj;
        private System.Windows.Forms.Label lblQg;
        private System.Windows.Forms.PictureBox pcbZlfbdj;
        private System.Windows.Forms.Label lblZkf;
        private System.Windows.Forms.PictureBox pcbJbdj;
        private System.Windows.Forms.Label lblJbdj;
        private System.Windows.Forms.PictureBox pcbZkf;
        private System.Windows.Forms.PictureBox pcbQg;
        private System.Windows.Forms.PictureBox pcbManual;
        private System.Windows.Forms.Label lblManual;
        private UC.GroupBoxEx groupBoxEx4;
        private UC.GroupBoxEx groupBoxEx5;
        private UC.GroupBoxEx groupBoxEx6;
        private UC.ButtonRadiusEx btnFjdjStop;
        private System.Windows.Forms.PictureBox pcbFjdjForward;
        private System.Windows.Forms.PictureBox pcbFjdjBack;
        private UC.ButtonRadiusEx btnZldjStop;
        private System.Windows.Forms.PictureBox pcbZldjForward;
        private System.Windows.Forms.PictureBox pcbZldjBack;
        private UC.ButtonRadiusEx btnZdjStop;
        private System.Windows.Forms.Timer timer1;
        private UC.ButtonRadiusEx btnZdjForward;
        private UC.ButtonRadiusEx btnZdjBack;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}