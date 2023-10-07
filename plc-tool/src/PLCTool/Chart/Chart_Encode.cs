using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCTool.Chart
{
    public partial class Chart_Encode : UserControl
    {
        public Chart_Encode()
        {
            InitializeComponent();
        }

        private void Chart_Encode_Load(object sender,EventArgs e)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                    chart1.Series[0].LegendText = "编码器值";
                    break;
                case "zh-TW":
                    chart1.Series[0].LegendText = "編碼器值";
                    break;
                case "en":
                    chart1.Series[0].LegendText = "Encoder Value";
                    break;
                default:
                    chart1.Series[0].LegendText = "编码器值";
                    break;
            }
        }

        public void BindData(PLCLogData[]data)
        {
            chart1.Series[0].Points.Clear();
            foreach(var k in data)
            {
                chart1.Series[0].Points.AddXY(k.time, k.Encoder);
            }
        }
    }
}
