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
    public partial class Chart_WindingTension : UserControl
    {
        public Chart_WindingTension()
        {
            InitializeComponent();
        }

        public void BindData(PLCLogData[] data)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            foreach (var k in data)
            {
                chart1.Series[0].Points.AddXY(k.time, k.WindingTensionGetValue);
                chart1.Series[1].Points.AddXY(k.time, k.WindingTensionSetValue);
            }
        }

        private void Chart_WindingTension_Load(object sender, EventArgs e)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                    chart1.Series[0].LegendText = "收卷张力反馈值";
                    chart1.Series[1].LegendText = "收卷张力设定值";
                    break;
                case "zh-TW":
                    chart1.Series[0].LegendText = "收卷張力反饋值";
                    chart1.Series[1].LegendText = "收卷張力反饋值";
                    break;
                case "en":
                    chart1.Series[0].LegendText = "Winding tension feedback value";
                    chart1.Series[1].LegendText = "Winding tension setting value";
                    break;
                default:
                    chart1.Series[0].LegendText = "收卷张力反馈值";
                    chart1.Series[1].LegendText = "收卷张力设定值";
                    break;
            }
        }
    }
}
