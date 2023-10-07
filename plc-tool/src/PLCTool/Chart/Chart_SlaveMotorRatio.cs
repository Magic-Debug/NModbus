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
    public partial class Chart_SlaveMotorRatio : UserControl
    {
        public Chart_SlaveMotorRatio()
        {
            InitializeComponent();
        }

        public void BindData(PLCLogData[] data)
        {
            chart1.Series[0].Points.Clear();
            foreach (var k in data)
                chart1.Series[0].Points.AddXY(k.time, k.SlaveMotorRatio);
        }

        private void Chart_SlaveMotorRatio_Load(object sender, EventArgs e)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                    chart1.Series[0].LegendText = "张力电机速度系数";
                    break;
                case "zh-TW":
                    chart1.Series[0].LegendText = "張力電機速度系數";
                    break;
                case "en":
                    chart1.Series[0].LegendText = "Tension motor speed coefficient";
                    break;
                default:
                    chart1.Series[0].LegendText = "张力电机速度系数";
                    break;
            }
        }
    }
}
