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
    public partial class Chart_InspectionTension : UserControl
    {
        public Chart_InspectionTension()
        {
            InitializeComponent();
        }

        public void BindData(PLCLogData[] data)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            foreach (var k in data)
            {
                chart1.Series[0].Points.AddXY(k.time, k.InspectionTensionGetValue);
                chart1.Series[1].Points.AddXY(k.time, k.InspectionTensionSetValue);
            }
        }

        private void Chart_InspectionTension_Load(object sender, EventArgs e)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                    chart1.Series[0].LegendText = "验布张力反馈值";
                    chart1.Series[1].LegendText = "验布张力设定值";
                    break;
                case "zh-TW":
                    chart1.Series[0].LegendText = "驗布張力反饋值";
                    chart1.Series[1].LegendText = "驗布張力反饋值";
                    break;
                case "en":
                    chart1.Series[0].LegendText = "Inspection tension feedback value";
                    chart1.Series[1].LegendText = "Inspection tension setting value";
                    break;
                default:
                    chart1.Series[0].LegendText = "验布张力反馈值";
                    chart1.Series[1].LegendText = "验布张力设定值";
                    break;
            }
        }
    }
}
