using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCTool.UC
{
    public partial class GroupBoxEx : GroupBox
    {
        public GroupBoxEx()
        {
            InitializeComponent();
        }

        public GroupBoxEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        [Browsable(true)]
        public Color BaseColor
        {
            get { return _basecolor; }
            set { _basecolor = value; }
        }
        private Color _basecolor = Color.FromArgb(0, 0, 0);

        public int Fillet
        {
            get { return _fillet; }
        set { _fillet = value; }
        }
        private int _fillet;

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle clientRect = this.ClientRectangle;
            Color lineColor = this.BaseColor;
            using (Graphics g = this.CreateGraphics())
            {
                if (clientRect.Width > 0 && clientRect.Height > 0)
                {
                    using (Pen pen = new Pen(lineColor, 1))
                    {
                        g.DrawLine(pen, new Point(clientRect.Left, clientRect.Top + 16), new Point(clientRect.Left, clientRect.Bottom - 11));
                        g.DrawLine(pen, new Point(clientRect.Left + 8 + (int)g.MeasureString(this.Text, this.Font).Width, clientRect.Top + 6), new Point(clientRect.Right - 11, clientRect.Top + 6));
                        g.DrawLine(pen, new Point(clientRect.Right - 1, clientRect.Top + 16), new Point(clientRect.Right - 1, clientRect.Bottom - 11));
                        g.DrawLine(pen, new Point(clientRect.Left + 10, clientRect.Bottom - 1), new Point(clientRect.Right - 11, clientRect.Bottom - 1));
                        g.DrawArc(pen, clientRect.Left, clientRect.Top + 6, 19, 19, 180, 90);
                        g.DrawArc(pen, clientRect.Right - 20, clientRect.Top + 6, 19, 19, 270, 90);
                        g.DrawArc(pen, clientRect.Left, clientRect.Bottom - 20, 19, 19, 90, 90);
                        g.DrawArc(pen, clientRect.Right - 20, clientRect.Bottom - 20, 19, 19, 0, 90);
                        g.DrawString(this.Text, this.Font, Brushes.Black, 10, 1);
                    }
                }
            }
        }
    }
}
