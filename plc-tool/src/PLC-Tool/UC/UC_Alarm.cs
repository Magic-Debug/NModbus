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
    public partial class UC_Alarm : UserControl
    {
        public UC_Alarm()
        {
            InitializeComponent();
        }

        private DateTime dtLastUpDateListTime = DateTime.Now.AddSeconds(-10);

        public void SetErrors(List<string> errors)
        {
            if ((DateTime.Now - dtLastUpDateListTime).TotalMilliseconds < 500)
                return;
            else
                dtLastUpDateListTime = DateTime.Now;

            timer1.Enabled = false;
            if (errors == null)
            {
                ErrorList = new List<string>();
            }
            else
            {
                ErrorList = new List<string>(errors);
            }
            timer1.Enabled = true;
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        public string NormalText
        {
            get
            {
                return normaltext;
            }
            set
            {
                normaltext = value;
                if (ErrorList.Count == 0)
                {
                    label2.Text = normaltext;
                }
            }
        }

        private List<string> ErrorList = new List<string>();
        private string normaltext;

        private int errorIndex;
        private int cycle;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ErrorList.Count == 0)
            {
                label2.Text = normaltext;
                label2.ForeColor = Color.Black;
                errorIndex = 0;
                cycle = 0;
            }
            else
            {
                label2.ForeColor = Color.Red;
                if (cycle == 0)
                {
                    label2.Text = "";
                }
                else if (errorIndex < ErrorList.Count)
                {
                    label2.Text = ErrorList[errorIndex];
                }

                cycle++;
                if (cycle >= 10)
                {
                    cycle = 0;
                    errorIndex++;
                    if (errorIndex >= ErrorList.Count)
                    {
                        errorIndex = 0;
                    }
                }
            }
        }
    }
}
