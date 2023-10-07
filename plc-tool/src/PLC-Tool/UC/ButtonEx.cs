using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PLCTool.UC
{
    public partial class ButtonEx : Button
    {
        public ButtonEx()
        {
            InitializeComponent();
        }

        public ButtonEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Pen pen = new Pen(this.BackColor, 3);
            pevent.Graphics.DrawRectangle(pen, 0, 0, this.Width, this.Height);//填充
            pen.Dispose();
        }
        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
    }
}
