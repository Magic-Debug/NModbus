using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCTool.Chart
{
    public partial class Chart_Displacement : UserControl
    {
        public Chart_Displacement()
        {
            InitializeComponent();
        }

        private void Chart_Displacement_Load(object sender, EventArgs e)
        {

        }

        public void BindData(DateTime dateTime, double dou, int index)
        {
            if (chart1.Series[index].Points.Count > 150)
            {
                chart1.Series[index].Points.RemoveAt(0);
            }
            chart1.Series[index].Points.AddXY(dateTime, dou);
        }

        public void ClearData(int index)
        {
            chart1.Series[index].Points.Clear();
        }
    }
}
