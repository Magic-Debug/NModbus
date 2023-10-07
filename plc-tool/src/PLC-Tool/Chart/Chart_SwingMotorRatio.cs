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
    public partial class Chart_SwingMotorRatio : UserControl
    {
        public Chart_SwingMotorRatio()
        {
            InitializeComponent();
        }

        public void BindData(PLCLogData[]data)
        {
            chart1.Series[0].Points.Clear();
            foreach (var k in data)
                chart1.Series[0].Points.AddXY(k.time, k.SwingMotorRatio);
        }

        private void Chart_SwingMotorRatio_Load(object sender, EventArgs e)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                    chart1.Series[0].LegendText = "摆布电机速度系数";
                    break;
                case "zh-TW":
                    chart1.Series[0].LegendText = "擺布電機速度系數";
                    break;
                case "en":
                    chart1.Series[0].LegendText = "Motor speed coefficient";
                    break;
                default:
                    chart1.Series[0].LegendText = "摆布电机速度系数";
                    break;
            }
        }
    }
}
