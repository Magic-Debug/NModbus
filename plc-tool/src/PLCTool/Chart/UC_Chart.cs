using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PLCTool.Chart
{
    public partial class UC_Chart : UserControl
    {
        public UC_Chart()
        {
            InitializeComponent();
        }

        public void UC_Chart_Load(object sender,EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 2;
            comboBox3.SelectedIndex = 3;
            GetChart();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChart();
        }

        private Legend GetLegend(string option,int index)
        {
            Legend legend = new Legend();
            legend.Name = "Leagend" + index;
            legend.Alignment = StringAlignment.Far;
            legend.Docking = Docking.Top;
            return legend;
        }

        private Series GetSeries(string option,int index)
        {
            Series series = new Series();
            series.ChartArea = "ChartArea1";
            series.ChartType = SeriesChartType.Line;
            series.Legend = "Leagend" + index;
            series.Name = "Series" + index;
            series.LegendText = option;
            series.ToolTip = "#VALX{HH:mm:ss} , #VALY";
            series.XValueType = ChartValueType.DateTime;
            series.YValueType = ChartValueType.Single;
            return series;
        }

        private void GetChart()
        {
            ChartArea chartArea1 = chart1.ChartAreas["ChartArea1"];
            chartArea1.AxisX.Interval = 60D;
            chartArea1.AxisX.IntervalOffsetType = DateTimeIntervalType.Seconds;
            chartArea1.AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea1.AxisX.LabelStyle.Interval = 60D;
            chartArea1.AxisX.LabelStyle.IntervalOffset = 0D;
            chartArea1.AxisX.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Seconds;
            chartArea1.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Seconds;
            chartArea1.AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Seconds;
            chartArea1.AxisX.ScrollBar.IsPositionedInside = false;
            chartArea1.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IntervalOffsetType = DateTimeIntervalType.Number;
            chartArea1.AxisY.IntervalType = DateTimeIntervalType.Number;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 91.06426F;
            chartArea1.InnerPlotPosition.Width = 92.28775F;
            chartArea1.InnerPlotPosition.X = 7.71225F;
            chartArea1.InnerPlotPosition.Y = 2.23404F;

            chart1.Legends.Clear();
            chart1.Series.Clear();
            Legend legend1 = GetLegend(comboBox1.Text, 1);
            Legend legend2 = GetLegend(comboBox2.Text, 2);
            Legend legend3 = GetLegend(comboBox3.Text, 3);
            Series series1 = GetSeries(comboBox1.Text, 1);
            Series series2 = GetSeries(comboBox2.Text, 2);
            Series series3 = GetSeries(comboBox3.Text, 3);
            chart1.Legends.Add(legend1);
            chart1.Legends.Add(legend2);
            chart1.Legends.Add(legend3);
            chart1.Series.Add(series1);
            chart1.Series.Add(series2);
            chart1.Series.Add(series3);
        }

        private void SetData()
        {

        }
    }
}
