using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PLCTool.UC
{
    public partial class TitleControl : UserControl
    {
        public TitleControl()
        {
            InitializeComponent();
        }

        private Image titleIcon;
        /// <summary>
        /// 标题栏控件的左上角图标
        /// </summary>
        public Image TitleIcon
        {
            get { return titleIcon; }
            set
            {
                titleIcon = value;
                picicon.Image = titleIcon;
            }
        }

        private bool _ShowTitleIcon = true;//是否有标题图案

        public bool ShowTitleIcon
        {
            get { return _ShowTitleIcon; }
            set
            {
                _ShowTitleIcon = value;
                if (_ShowTitleIcon == false)
                {
                    picicon.Visible = false;

                    lbswname.Location = new Point(picicon.Location.X, lbswname.Location.Y);
                }
                else
                {
                    picicon.Visible = true;
                    lbswname.Location = new Point(picicon.Location.X + picicon.Width + 1, lbswname.Location.Y);
                }
            }
        }

        public enum Skin//皮肤枚举
        {
            Blue,
            White,
            Auto
        }

        private Skin _SelectSkin = Skin.Auto;//选择皮肤样式

        public Skin SelectSkin
        {
            get { return _SelectSkin; }
            set
            {
                _SelectSkin = value;
                if (_SelectSkin == Skin.Blue)
                {
                    picClose.BackgroundImage = TitleControlResource.CloseBlue;
                    picMax.BackgroundImage = TitleControlResource.MaxBlue;
                    picMin.BackgroundImage = TitleControlResource.MinBlue;
                }
                else if (_SelectSkin == Skin.White)
                {
                    picClose.BackgroundImage = TitleControlResource.CloseWrite;
                    picMax.BackgroundImage = TitleControlResource.MaxWhite;
                    picMin.BackgroundImage = TitleControlResource.MinWhite;
                }
                else if (_SelectSkin == Skin.Auto)
                {
                    picClose.BackgroundImage = TitleControlResource.CloseTransparent;
                    picMax.BackgroundImage = TitleControlResource.MaxTransparent;
                    picMin.BackgroundImage = TitleControlResource.MinTransparent;
                }
            }
        }

        private Color _HeadlineColor = Color.Black;

        public Color HeadlineColor
        {
            get { return _HeadlineColor; }
            set
            {
                _HeadlineColor = value;
                lbswname.ForeColor = _HeadlineColor;
            }
        }
        
        public string Headline
        {
            get { return lbswname.Text; }
            set { lbswname.Text = value; }
        }

        private bool _MaximizeBox = true;//是否有放大标题栏

        public bool MaximizeBox
        {
            get { return _MaximizeBox; }
            set
            {
                _MaximizeBox = value;
                if (_MaximizeBox == false)
                {
                    picMax.Visible = false;
                    picMin.Location = picMax.Location;
                }
                else
                {
                    picMax.Visible = true;
                    picMin.Location = new Point(picMax.Location.X - picMax.Width, 0);
                }
            }
        }

        private bool _MinimizeBox = true;//是否有最小化框

        public bool MinimizeBox
        {
            get { return _MinimizeBox; }
            set
            {
                _MinimizeBox = value;
                //if (_MaximizeBox == false)
                //{
                picMin.Visible = _MinimizeBox;
                //}
            }
        }

        private void picClose_Click(object sender, EventArgs e)//关闭窗体
        {
            this.ParentForm.Close();
        }

        private void picMax_Click(object sender, EventArgs e)//放大按钮点击事件
        {
            if (this.ParentForm.WindowState == FormWindowState.Normal)
            {
                this.ParentForm.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.ParentForm.WindowState = FormWindowState.Maximized;
                if (_SelectSkin == Skin.Blue)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxBlue;
                }
                else if (_SelectSkin == Skin.White)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxWhite;
                }
                else if (_SelectSkin == Skin.Auto)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxTransparent;
                }
            }
            else if (this.ParentForm.WindowState == FormWindowState.Maximized)
            {
                this.ParentForm.WindowState = FormWindowState.Normal;
                if (_SelectSkin == Skin.Blue)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxSizeBlue;
                }
                else if (_SelectSkin == Skin.White)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxSizeWhite;
                }
                else if (_SelectSkin == Skin.Auto)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxSizeTransparent;
                }
            }
        }

        private void picMin_Click(object sender, EventArgs e)//缩小窗体点击事件
        {
            this.ParentForm.WindowState = FormWindowState.Minimized;
        }

        private void picClose_MouseMove(object sender, MouseEventArgs e)//关闭按钮得到焦点事件
        {
            if (_SelectSkin == Skin.Blue)
            {
                picClose.BackgroundImage = TitleControlResource.CloseBlueLeave;
            }
            else if (_SelectSkin == Skin.White)
            {
                picClose.BackgroundImage = TitleControlResource.CloseWhiteLeave;
            }
            else if (_SelectSkin == Skin.Auto)
            {
                picClose.BackgroundImage = TitleControlResource.CloseTransparentLeave;
            }
        }

        private void picClose_MouseLeave(object sender, EventArgs e)//关闭按钮失去焦点事件
        {
            if (_SelectSkin == Skin.Blue)
            {
                picClose.BackgroundImage = TitleControlResource.CloseBlue;
            }
            else if (_SelectSkin == Skin.White)
            {
                picClose.BackgroundImage = TitleControlResource.CloseWrite;
            }
            else if (_SelectSkin == Skin.Auto)
            {
                picClose.BackgroundImage = TitleControlResource.CloseTransparent;
            }
        }

        private void picMax_MouseMove(object sender, MouseEventArgs e)//放大按钮得到焦点事件
        {
            if (this.ParentForm.WindowState == FormWindowState.Normal)
            {
                if (_SelectSkin == Skin.Blue)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxBlueLeave;
                }
                else if (_SelectSkin == Skin.White)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxWhiteLeave;
                }
                else if (_SelectSkin == Skin.Auto)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxTransparentLeave;
                }
            }
            else if (this.ParentForm.WindowState == FormWindowState.Maximized)
            {
                if (_SelectSkin == Skin.Blue)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxSizeBlueLeave;
                }
                else if (_SelectSkin == Skin.White)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxSizeWhiteLeave;
                }
                else if (_SelectSkin == Skin.Auto)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxSizeTransparentLeave;
                }
            }
        }

        private void picMax_MouseLeave(object sender, EventArgs e)//放大按钮失去焦点事件
        {
            if (this.ParentForm.WindowState == FormWindowState.Normal)
            {
                if (_SelectSkin == Skin.Blue)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxBlue;
                }
                else if (_SelectSkin == Skin.White)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxWhite;
                }
                else if (_SelectSkin == Skin.Auto)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxTransparent;
                }
            }
            else if (this.ParentForm.WindowState == FormWindowState.Maximized)
            {
                if (_SelectSkin == Skin.Blue)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxSizeBlue;
                }
                else if (_SelectSkin == Skin.White)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxSizeWhite;
                }
                else if (_SelectSkin == Skin.Auto)
                {
                    picMax.BackgroundImage = TitleControlResource.MaxSizeTransparent;
                }
            }
        }

        private void picMin_MouseMove(object sender, MouseEventArgs e)//最小化按钮得到焦点事件
        {
            if (_SelectSkin == Skin.Blue)
            {
                picMin.BackgroundImage = TitleControlResource.MinblueLeave;
            }
            else if (_SelectSkin == Skin.White)
            {
                picMin.BackgroundImage = TitleControlResource.MinWhiteLeave;
            }
            else if (_SelectSkin == Skin.Auto)
            {
                picMin.BackgroundImage = TitleControlResource.MinTransparentLeave;
            }
        }

        private void picMin_MouseLeave(object sender, EventArgs e)//最小化按钮失去焦点事件
        {
            if (_SelectSkin == Skin.Blue)
            {
                picMin.BackgroundImage = TitleControlResource.MinBlue;
            }
            else if (_SelectSkin == Skin.White)
            {
                picMin.BackgroundImage = TitleControlResource.MinWhite;
            }
            else if (_SelectSkin == Skin.Auto)
            {
                picMin.BackgroundImage = TitleControlResource.MinTransparent;
            }
        }

        //移动窗体
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void TitleControl_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.ParentForm.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void TitleControl_SizeChanged(object sender, EventArgs e)
        {
            picClose.Location = new Point(this.Width - picClose.Width, 0);
            picMax.Location = new Point(this.Width - picClose.Width * 2, 0);
            picMin.Location = new Point(this.Width - picClose.Width * 3, 0);
            if (_MaximizeBox == false)
            {
                picMax.Visible = false;
                picMin.Location = picMax.Location;
            }
            else
            {
                picMax.Visible = true;
                picMin.Location = new Point(picMax.Location.X - picMax.Width, 0);
            }
        }
    }
}
