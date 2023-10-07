using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCTool.Forms
{
    public partial class FormEditIOName : BaseForm
    {
        public FormEditIOName()
        {
            InitializeComponent();
        }

        public string IOName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        private void buttonRadiusEx1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
