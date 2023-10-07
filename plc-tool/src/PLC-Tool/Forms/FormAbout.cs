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
    public partial class FormAbout : BaseForm
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.lintsense.com");
        }

        private void FormAbout_Load(object sender,EventArgs e)
        {
            label4.Text = $"Version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        }
    }
}
