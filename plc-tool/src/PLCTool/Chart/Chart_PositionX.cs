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
    public partial class Chart_PositionX : UserControl
    {
        public Chart_PositionX()
        {
            InitializeComponent();
        }

        public void BindData(PLCLogData[]data)
        {
            chart1.Series[0].Points.Clear();
            foreach (var k in data)
                chart1.Series[0].Points.AddXY(k.time, k.CurrentPositionX);
        }

        private void Chart_PositionX_Load(object sender, EventArgs e)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                    chart1.Series[0].LegendText = "X坐标";
                    break;
                case "zh-TW":
                    chart1.Series[0].LegendText = "X座標";
                    break;
                case "en":
                    chart1.Series[0].LegendText = "X coodinate";
                    break;
                default:
                    chart1.Series[0].LegendText = "X坐标";
                    break;
            }
        }
    }
}
