using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCTool
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        [DefaultValue(typeof(Color),"80, 80, 80")]
        public Color BoderColor
        {
            get { return _bodercolor; }
            set
            {
                _bodercolor = value;
                pnlPaddingLeft.BackColor = _bodercolor;
                pnlPadingRight.BackColor = _bodercolor;
                pnlPaddingBottom.BackColor = _bodercolor;
                titleControl1.BackColor = _bodercolor;
            }
        }
        private Color _bodercolor = Color.FromArgb(80, 80, 80);

        [Localizable(true)]
        public new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                titleControl1.Headline = value;
            }
        }
        
        public new bool MaximizeBox
        {
            get { return titleControl1.MaximizeBox; }
            set { titleControl1.MaximizeBox = value; }
        }

        public new bool MinimizeBox
        {
            get { return titleControl1.MinimizeBox; }
            set { titleControl1.MinimizeBox = value; }
        }
    }
}
