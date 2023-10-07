using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLCTool.Properties;

namespace PLCTool.UC
{
    public partial class UC_OnOffButton : UserControl
    {
        public bool ButtonChecked
        {
            get
            {
                return buttonChecked;
            }
            set
            {
                buttonChecked = value;
                if (buttonChecked)
                {
                    pictureBox1.Image = Resources.BtnOn;
                }
                else
                {
                    pictureBox1.Image = Resources.BtnOff;
                }
            }
        }
        private bool buttonChecked;
        public UC_OnOffButton()
        {
            InitializeComponent();
        }
    }
}
