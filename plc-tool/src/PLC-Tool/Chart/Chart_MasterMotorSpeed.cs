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
    public partial class Chart_MasterMotorSpeed : UserControl
    {
        public Chart_MasterMotorSpeed()
        {
            InitializeComponent();
        }

        public void BindData(PLCLogData[] data)
        {
            chart1.Series[0].Points.Clear();
            foreach (var k in data)
                chart1.Series[0].Points.AddXY(k.time, k.MasterMotorSpeed);
        }

        private void Chart_MasterMotorSpeed_Load(object sender, EventArgs e)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                    chart1.Series[0].LegendText = "牵引电机速度";
                    break;
                case "zh-TW":
                    chart1.Series[0].LegendText = "牽引電機速度";
                    break;
                case "en":
                    chart1.Series[0].LegendText = "Traction motor speed";
                    break;
                default:
                    chart1.Series[0].LegendText = "牵引电机速度";
                    break;
            }
        }
    }
}
