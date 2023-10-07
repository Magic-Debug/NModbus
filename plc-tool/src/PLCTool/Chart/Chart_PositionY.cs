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
    public partial class Chart_PositionY : UserControl
    {
        public Chart_PositionY()
        {
            InitializeComponent();
        }
        public void BindData(PLCLogData[] data)
        {
            chart1.Series[0].Points.Clear();
            foreach (var k in data)
                chart1.Series[0].Points.AddXY(k.time, k.CurrentPositionY);
        }

        private void Chart_PositionY_Load(object sender, EventArgs e)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                    chart1.Series[0].LegendText = "Y坐标";
                    break;
                case "zh-TW":
                    chart1.Series[0].LegendText = "Y座標";
                    break;
                case "en":
                    chart1.Series[0].LegendText = "Y coodinate";
                    break;
                default:
                    chart1.Series[0].LegendText = "Y坐标";
                    break;
            }
        }
    }
}
