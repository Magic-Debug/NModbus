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
    public partial class Chart_Weight : UserControl
    {
        public Chart_Weight()
        {
            InitializeComponent();
        }

        public void BindData(PLCLogData[]data)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            foreach (var k in data)
            {
                chart1.Series[0].Points.AddXY(k.time, k.RealtimeWeight);
                chart1.Series[1].Points.AddXY(k.time, k.FinalWeight);
            }
        }

        private void Chart_Weight_Load(object sender, EventArgs e)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                    chart1.Series[0].LegendText = "称重实时值";
                    chart1.Series[1].LegendText = "称重变送值";
                    break;
                case "zh-TW":
                    chart1.Series[0].LegendText = "稱重實時值";
                    chart1.Series[1].LegendText = "穩重變送值";
                    break;
                case "en":
                    chart1.Series[0].LegendText = "Real time weight";
                    chart1.Series[1].LegendText = "Final weight";
                    break;
                default:
                    chart1.Series[0].LegendText = "称重实时值";
                    chart1.Series[1].LegendText = "称重变送值";
                    break;
            }
        }
    }
}
