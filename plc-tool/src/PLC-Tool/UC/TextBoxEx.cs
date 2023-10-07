using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PLCTool.UC
{
    public partial class TextBoxEx : TextBox
    {
        #region Windows消息API
        //设置Rect消息
        private const int EM_SETRECT = 179;
        //获取Rect消息
        private const int EM_GETRECT = 178;
        //private const int WM_GETTEXT = 0x000d;
        //private const int WM_COPY = 0x0301;
        //粘贴消息
        private const int WM_PASTE = 0x0302;
        //绘制消息
        private const int WM_PAINT = 0xF;
        //控件颜色编辑消息
        private const int WM_CTLCOLOREDIT = 0x0133;
        //private const int WM_CONTEXTMENU = 0x007B;
        //private const int WM_RBUTTONDOWN = 0x0204;
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, ref Rectangle lParam);
        #endregion

        public TextBoxEx()
        {
            InitializeComponent();
            //this.SetStyle(ControlStyles.UserPaint
            //    | ControlStyles.DoubleBuffer
            //    | ControlStyles.ResizeRedraw  //调整大小时重绘
            //    | ControlStyles.AllPaintingInWmPaint // 禁止擦除背景.
            //    | ControlStyles.OptimizedDoubleBuffer // 双缓冲
            //    | ControlStyles.SupportsTransparentBackColor //透明效果
            //    , true);            
            this.Multiline = true;//多行显示 只有多行显示才能设置Rect有效
            AllowReturn = false;//不允许回车
            BorderStyle = System.Windows.Forms.BorderStyle.None;//关闭默认的边框            
            WordWrap = false;//禁止折行
            _textMargin = new Padding(1);
        }

        /// <summary>
        /// 是否允许有回车
        /// </summary>
        public bool AllowReturn { get; set; }

        public string KeyPressChar;

        /// <summary>
        /// 边框样式
        /// </summary>
        /// <remarks>获取或设置边框样式.</remarks>
        [Category("Appearance"),
         Description("边框样式"),
         DefaultValue(null)]
        public virtual TTextBoxBorderRenderStyle BorderRenderStyle
        {
            get { return borderRenderStyle; }
            set { borderRenderStyle = value; }
        }
        private TTextBoxBorderRenderStyle borderRenderStyle = new TTextBoxBorderRenderStyle();
        

        private Padding _textMargin;
        /// <summary>
        /// Text Padding值
        /// </summary>
        public Padding TextMargin { get { return _textMargin; } set { _textMargin = value; SetTextDispLayout(); } }

        protected override void OnResize(EventArgs e)
        {            
            base.OnResize(e);
            SetTextDispLayout();
        }

        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            SetTextDispLayout();
        }
        
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //如果不允许回车 屏蔽回车 换行键值
            if (!AllowReturn
                && ((int)e.KeyChar == (int)Keys.Return || (int)e.KeyChar == (int)Keys.LineFeed))
            {
                e.Handled = true;
            }
            KeyPressChar = e.KeyChar.ToString();
            base.OnKeyPress(e);
        }

        /// <summary>
        /// 绘制消息进行绘制边框
        /// </summary>
        /// <param name="m"></param>
        private void WmPaint(ref System.Windows.Forms.Message m)
        {
            if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
            {
                Rectangle clientRect = this.ClientRectangle;

                Color lineColor = borderRenderStyle.LineColor;
                if (Focused)
                    lineColor = borderRenderStyle.ActiveLineColor;
                using (Graphics g = this.CreateGraphics())
                {
                    using (Pen pen = new Pen(lineColor, borderRenderStyle.LineWidth))
                    {
                        pen.DashStyle = borderRenderStyle.LineDashStyle;
                        if (borderRenderStyle.ShowLeftLine)
                        {
                            g.DrawLine(pen, new Point(clientRect.Left, clientRect.Top + borderRenderStyle.Fillet), new Point(clientRect.Left, clientRect.Bottom - borderRenderStyle.Fillet - 1));
                        }

                        if (borderRenderStyle.ShowTopLine)
                        {
                            g.DrawLine(pen, new Point(clientRect.Left + borderRenderStyle.Fillet), new Point(clientRect.Right - borderRenderStyle.Fillet - 1, clientRect.Top));
                        }

                        if (borderRenderStyle.ShowRightLine)
                        {
                            g.DrawLine(pen, new Point(clientRect.Right - 1, clientRect.Top + borderRenderStyle.Fillet), new Point(clientRect.Right - 1, clientRect.Bottom - borderRenderStyle.Fillet - 1));
                        }

                        if (borderRenderStyle.ShowBottomLine)
                        {
                            g.DrawLine(pen, new Point(clientRect.Left + borderRenderStyle.Fillet, clientRect.Bottom - 1), new Point(clientRect.Right - borderRenderStyle.Fillet - 1, clientRect.Bottom - 1));
                        }
                        if (borderRenderStyle.Fillet > 0)
                        {
                            if (borderRenderStyle.ShowLeftLine && borderRenderStyle.ShowTopLine)
                            {
                                g.DrawArc(pen, clientRect.Left, clientRect.Top, borderRenderStyle.Fillet * 2 - 1, borderRenderStyle.Fillet * 2 - 1, 180, 90);
                            }
                            if (borderRenderStyle.ShowRightLine && borderRenderStyle.ShowTopLine)
                            {
                                g.DrawArc(pen, clientRect.Right - borderRenderStyle.Fillet * 2, clientRect.Top, borderRenderStyle.Fillet * 2 - 1, borderRenderStyle.Fillet * 2 - 1, 270, 90);
                            }
                            if (borderRenderStyle.ShowLeftLine && borderRenderStyle.ShowBottomLine)
                            {
                                g.DrawArc(pen, clientRect.Left, clientRect.Bottom - borderRenderStyle.Fillet * 2, borderRenderStyle.Fillet * 2 - 1, borderRenderStyle.Fillet * 2 - 1, 90, 90);
                            }
                            if (borderRenderStyle.ShowRightLine && borderRenderStyle.ShowBottomLine)
                            {
                                g.DrawArc(pen, clientRect.Right - borderRenderStyle.Fillet * 2, clientRect.Bottom - borderRenderStyle.Fillet * 2, borderRenderStyle.Fillet * 2 - 1, borderRenderStyle.Fillet * 2 - 1, 0, 90);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 窗体处理消息主函数 处理粘贴及绘制消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            string str = "";
            bool flag = false;
            int i = 0;
            if (m.Msg == 0x0204)
                i++;
            if (!AllowReturn
                && m.Msg == WM_PASTE
                && System.Windows.Forms.Clipboard.ContainsText())
            {
                str = System.Windows.Forms.Clipboard.GetText();
                System.Windows.Forms.Clipboard.Clear();
                string nstr = str.Replace(char.ConvertFromUtf32((int)Keys.Return), "").Replace(char.ConvertFromUtf32((int)Keys.LineFeed), "");
                System.Windows.Forms.Clipboard.SetText(nstr);
                if (str.Length > 0) flag = true;
            }

            base.WndProc(ref m);
            if (flag)
            {
                flag = false;
                System.Windows.Forms.Clipboard.SetText(str);
                str = "";
            }

            if (m.Msg == WM_PAINT || m.Msg == WM_CTLCOLOREDIT)
            {
                WmPaint(ref m);
            }
        }        

        /// <summary>
        /// 尺寸变化时重新设置字体的显示位置居中
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetTextDispLayout();
        }

        /// <summary>
        /// 设置文本显示布局位置
        /// </summary>
        public void SetTextDispLayout()
        {            
            //if (Text == "")
            //    return;
            Rectangle rect = new Rectangle();
            SendMessage(this.Handle, EM_GETRECT, (IntPtr)0, ref rect);
            SizeF size = CreateGraphics().MeasureString(Text, Font);
            rect.Y = (int)(Height - size.Height) / 2 + TextMargin.Top;
            rect.X = 1 + TextMargin.Left;
            rect.Height = Height - 2;
            rect.Width = Width - TextMargin.Right - TextMargin.Left - 2;
            SendMessage(this.Handle, EM_SETRECT, IntPtr.Zero, ref rect);
        }       

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            SetTextDispLayout();
        }        

        private void TextBoxEx_Click(object sender, EventArgs e)
        {
            //打开系统软键盘
            //System.Diagnostics.Process.Start(@"C:\Program Files\Common Files\microsoft shared\ink\TabTip.exe");
        }

        private void TextBoxEx_Enter(object sender, EventArgs e)
        {
            SetTextDispLayout();
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TTextBoxBorderRenderStyle
    {
        /// <summary>
        /// 边线颜色
        /// </summary>
        /// <remarks>获取或设置边线颜色</remarks>
        [Category("Appearance"),
         Description("获取或设置边线颜色"),
         DefaultValue(typeof(Color), "Gray")]
        public virtual Color LineColor
        {
            get { return gridLineColor; }
            set { gridLineColor = value; }
        }
        private Color gridLineColor = Color.LightGray;

        /// <summary>
        /// 激活状态时的边线颜色
        /// </summary>
        /// <remarks>获取或设置激活状态时的边线颜色.</remarks>
        [Category("Appearance"),
         Description("激活状态时的边线颜色"),
         DefaultValue(typeof(Color), "RoyalBlue")]
        public virtual Color ActiveLineColor
        {
            get { return activeGridLineColor; }
            set { activeGridLineColor = value; }
        }
        private Color activeGridLineColor = Color.RoyalBlue;

        [Category("Appearance"),
         Description("线宽度"),
         DefaultValue(1)]
        public virtual float LineWidth
        {
            get { return lineWidth; }
            set { lineWidth = value; }
        }
        private float lineWidth = 1;

        /// <summary>
        ///线样式
        /// </summary>
        /// <remarks>获取或设置线样式.</remarks>
        [Category("Appearance"),
         Description("获取或设置线样式"),
         DefaultValue(typeof(DashStyle), "Solid")]
        public virtual DashStyle LineDashStyle
        {
            get { return lineDashStyle; }
            set { lineDashStyle = value; }
        }
        private DashStyle lineDashStyle = DashStyle.Solid;

        /// <summary>
        /// 左边线是否显示
        /// </summary>
        /// <remarks>获取或设置左线是否显示.</remarks>
        [Category("Appearance"),
         Description("左边网格线是否显示"),
        DefaultValue(true)
        ]
        public virtual bool ShowLeftLine
        {
            get { return showLeftLine; }
            set { showLeftLine = value; }
        }
        private bool showLeftLine = true;

        /// <summary>
        /// 上边线是否显示
        /// </summary>
        /// <remarks>获取或设置上边线是否显示.</remarks>
        [Category("Appearance"),
         Description("上边线是否显示"),
        DefaultValue(true)
        ]
        public virtual bool ShowTopLine
        {
            get { return showTopLine; }
            set { showTopLine = value; }
        }
        private bool showTopLine = true;

        /// <summary>
        /// 右边线是否显示
        /// </summary>
        /// <remarks>获取或设置右边线是否显示.</remarks>
        [Category("Appearance"),
         Description("右边线是否显示"),
        DefaultValue(true)
        ]
        public virtual bool ShowRightLine
        {
            get { return showRightLine; }
            set { showRightLine = value; }
        }
        private bool showRightLine = true;

        /// <summary>
        /// 底边线是否显
        /// </summary>
        /// <remarks>获取或设置底边线是否显示.</remarks>
        [Category("Appearance"),
         Description("底边线是否显示"),
        DefaultValue(true)
        ]
        public virtual bool ShowBottomLine
        {
            get { return showBottomLine; }
            set { showBottomLine = value; }
        }
        private bool showBottomLine = true;

        [Category("Appearance"),
         Description("圆角像素点"),
        DefaultValue(5)
        ]
        public int Fillet
        {
            get { return fillet; }
            set { fillet = value; }
        }
        private int fillet = 5;
    }
}
