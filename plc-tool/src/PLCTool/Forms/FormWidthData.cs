using FrameworkCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PLCTool.Forms
{
    public partial class FormWidthData : BaseForm
    {
        List<double> listX = new List<double>();
        List<double> listY = new List<double>();
        List<double> RicelistX = new List<double>();
        List<double> RicelistY = new List<double>();
        public FormWidthData()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string offSet = this.txtOffset.Text.Trim();
                if (string.IsNullOrEmpty(offSet))
                {
                    MessageBox.Show(NotCodeLengthOffsetMsg);
                    this.txtOffset.Focus();
                    return;
                }
                double offSetInt = 0;
                if (!double.TryParse(offSet, out offSetInt))
                {
                    MessageBox.Show(NumbersCodeLengthOffsetMsg);
                    this.txtOffset.Focus();
                    return;
                }
                OpenFileDialog fileDialog = new OpenFileDialog();
                DialogResult diaResult = fileDialog.ShowDialog();
                if (diaResult == DialogResult.OK)
                {
                    var data = ReadClothWidthTxt(fileDialog.FileName);
                    lbFileName.Text = $"{FileNameTitle}:  {Path.GetFileName(fileDialog.FileName)}"; 
                    StringBuilder sb = new StringBuilder();
                    List<ClothWidthModel> listClothWidthModel = new List<ClothWidthModel>(); 
                    double minY = 200d;
                    double maxY = 0d;
                    double sum = 0d; 
                    listX = new List<double>();
                    listY = new List<double>();
                    int num = 1;
                    foreach (var item in data)
                    {
                        double length = item.Key / 1000 + offSetInt / 100;
                        listX.Add(length);
                        listY.Add(item.Value);
                        if (item.Value < minY)
                        {
                            minY = item.Value; 
                        }
                        if (item.Value > maxY)
                        {
                            maxY = item.Value; 
                        }
                        sum += item.Value;
                        listClothWidthModel.Add(new ClothWidthModel() { ID = num++, Length = (length).ToString("f2"), Width = item.Value.ToString("f2"), LengthInt = Math.Floor(decimal.Parse(length.ToString("f2"))) + 1 });
                    }
                    
                    this.lbMin.Text = $"{minY:f2} cm";
                    this.lbMax.Text = $"{maxY:f2} cm";
                    this.lbClothWidth.Text = $"{sum / data.Count:f2} cm";
                     
                    this.dataGridView1.DataSource = listClothWidthModel; 
                    SetMaxMinColor(dataGridView1, minY.ToString("f2"), maxY.ToString("f2"));
                    BindRiceWidth(listClothWidthModel);
                    rdoAll_CheckedChanged(null, null);
                }
            }
            catch (Exception ex)
            { 
                LogHelper.Default.Error("查看宽幅数据错误", ex);
            }
        }
        private void rdoAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.DataSource == null) return;
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            Series series1 = new Series("Series1");
            chart1.Series.Add(series1);
            ChartArea chartArea1 = new ChartArea("ChartArea1");
            chart1.ChartAreas.Add(chartArea1);

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.ChartAreas[0].AxisY.Title = $"（{YardLengthTitle}(m)）";
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Interval = 10;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart1.ChartAreas[0].AxisX.Title = $"（{WidthTitle}(cm)）";
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 50;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDot;
            if (rdoAll.Checked)
            {
                chart1.Series[0].Points.DataBindXY(listY, listX);
                chart1.ChartAreas[0].AxisY.Maximum = CalculationMaximum(listX.Max(), chart1.ChartAreas[0].AxisY.Interval);
                chart1.ChartAreas[0].AxisX.Maximum = CalculationMaximum(listY.Max(), chart1.ChartAreas[0].AxisX.Interval);
            }
            else
            {
                chart1.Series[0].Points.DataBindXY(RicelistY, RicelistX);
                chart1.ChartAreas[0].AxisY.Maximum = CalculationMaximum(RicelistX.Max(), chart1.ChartAreas[0].AxisY.Interval);
                chart1.ChartAreas[0].AxisX.Maximum = CalculationMaximum(RicelistY.Max(), chart1.ChartAreas[0].AxisX.Interval);
            } 
            chart1.Series[0].Color = Color.Blue;
        }
        /// <summary>
        /// 计算克重
        /// </summary> 
        private void btnCalculation_Click(object sender, EventArgs e)
        {
            string NetWeight = this.txtNetWeight.Text.Trim();
            if (string.IsNullOrEmpty(NetWeight))
            {
                MessageBox.Show(NotNetWeightMsg);
                this.txtNetWeight.Focus();
                return;
            }
            double NetWeightInt = 0;
            if (!double.TryParse(NetWeight, out NetWeightInt))
            {
                MessageBox.Show(NumbersNetWeightMsg);
                this.txtNetWeight.Focus();
                return;
            }
            if (double.Parse(txtAreaRice.Text) == 0) return;
            double Weight = NetWeightInt * 1000; 
            txtWeight.Text = (Weight / double.Parse(txtAreaRice.Text)).ToString("f2");
        }
        /// <summary>
        /// 绑定每米平均幅宽
        /// </summary>
        private void BindRiceWidth(List<ClothWidthModel> listClothWidthModel)
        {
            List<ClothWidthModel> list = new List<ClothWidthModel>();
            List<decimal> listLengthInt = listClothWidthModel.Select(t => t.LengthInt).Distinct().OrderBy(t => t).ToList();
            double minY = 200d;
            double maxY = 0d;
            double sum = 0d;
            double areasum = 0d; 
            double maxLength = double.Parse(listClothWidthModel.Max(t=> double.Parse(t.Length)).ToString());
            RicelistX = new List<double>();
            RicelistY = new List<double>(); 
            for (int i = 0; i < listLengthInt.Count; i++)
            {
                var data = listClothWidthModel.Where(t => t.LengthInt == listLengthInt[i]).ToList();
                double width = data.Sum(t => double.Parse(t.Width)) / data.Count;
                RicelistX.Add(double.Parse(listLengthInt[i].ToString()));
                RicelistY.Add(width);
                if (width < minY)
                {
                    minY = width;
                }
                if (width > maxY)
                {
                    maxY = width;
                }
                sum += width;
                double Area = 0;
                double result = maxLength - (1 * double.Parse(listLengthInt[i].ToString()) - 1);
                if (result >= 1)
                {
                    Area = 1 * width / 100;
                }
                else
                {
                    Area = result * width / 100;
                }
                areasum += Area;
                list.Add(new ClothWidthModel() { ID = (i + 1), Length = listLengthInt[i].ToString(), Width = width.ToString("f2"), Area = Area.ToString("f2") });
            }
            this.lbMinRice.Text = $"{minY:f2} cm";
            this.lbMaxRice.Text = $"{maxY:f2} cm";
            this.lbClothWidthRice.Text = $"{sum / listLengthInt.Count:f2} cm";
            this.txtAreaRice.Text = $"{areasum:f2}";
            this.dataGridView2.DataSource = list;
            SetMaxMinColor(dataGridView2, minY.ToString("f2"), maxY.ToString("f2"));
        }
        /// <summary>
        /// 设置所有最大、最小单元格字体颜色
        /// </summary>
        private void SetMaxMinColor(DataGridView dgv, string minWidth, string maxWidth)
        {
            var listClothWidthModel = (List<ClothWidthModel>)dgv.DataSource;
            var minList = listClothWidthModel.Where(t => t.Width == minWidth).ToList();
            var maxList = listClothWidthModel.Where(t => t.Width == maxWidth).ToList();
            foreach (var item in minList)
            {
                dgv.Rows[(item.ID - 1)].Cells[2].Style.ForeColor = Color.Red;
            }
            foreach (var item in maxList)
            {
                dgv.Rows[(item.ID - 1)].Cells[2].Style.ForeColor = Color.Blue;
            }
        }
        /// <summary>
        /// 读取幅宽文本记录  文本格式：位置，幅宽
        /// </summary>
        /// <param name="txtPath"></param>
        /// <returns></returns>
        public Dictionary<double, double> ReadClothWidthTxt(string txtPath)
        {
            Dictionary<double, double> dic = new Dictionary<double, double>();

            try
            {
                string line = string.Empty;
                using (StreamReader reader = new StreamReader(txtPath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] strs = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (strs.Length == 2)
                        {
                            dic.Add(double.Parse(strs[0]), double.Parse(strs[1]));
                        }
                    }
                }
                //return true;
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error("ReadClothWidthTxt Error!", ex); 
                //return false;
            }
            return dic;
        }
        /// <summary>
        /// 算轴最大值
        /// </summary>
        private double CalculationMaximum(double value, double interval)
        {
            value = double.Parse(Math.Ceiling(decimal.Parse(value.ToString())).ToString());
            double valueNew = value;
            while (valueNew > interval)
            {
                valueNew = valueNew - interval;
            }
            value = value - valueNew + interval;
            return value;
        }
        #region 定位
        private void btnMinLocate_Click(object sender, EventArgs e)
        {
            SetLocate(dataGridView1, lbMin.Text);
        }

        private void btnMaxLocate_Click(object sender, EventArgs e)
        { 
            SetLocate(dataGridView1, lbMax.Text);
        }

        private void btnMinRiceLocate_Click(object sender, EventArgs e)
        {
            SetLocate(dataGridView2, lbMinRice.Text);
        }

        private void btnMaxRiceLocate_Click(object sender, EventArgs e)
        {
            SetLocate(dataGridView2, lbMaxRice.Text);
        }
        /// <summary>
        /// 设置定位
        /// </summary>
        private void SetLocate(DataGridView dgv, string widthLocate)
        {
            if (dgv.Rows.Count <= 0) return;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string width = dgv.Rows[i].Cells[2].Value.ToString();
                if (width.IndexOf(widthLocate.Replace(" cm","")) != -1)
                {
                    int currentIndex = dgv.FirstDisplayedScrollingRowIndex;
                    int lastIndex = i;
                    if (lastIndex > currentIndex || currentIndex==0)
                    {
                        dgv.FirstDisplayedScrollingRowIndex = lastIndex;
                        return;
                    } 
                }
            }
            dgv.FirstDisplayedScrollingRowIndex = 0;
            SetLocate(dgv, widthLocate);
        }
        #endregion
        #region 多语言
        private string FileNameTitle => LanguageResource.FormWidthData_FileNameTitle;
        private string YardLengthTitle => LanguageResource.FormWidthData_YardLengthTitle;
        private string WidthTitle => LanguageResource.FormWidthData_WidthTitle;
        private string NotCodeLengthOffsetMsg => LanguageResource.FormWidthData_NotCodeLengthOffsetMsg;
        private string NumbersCodeLengthOffsetMsg => LanguageResource.FormWidthData_NumbersCodeLengthOffsetMsg;
        private string NotNetWeightMsg => LanguageResource.FormWidthData_NotNetWeightMsg;
        private string NumbersNetWeightMsg => LanguageResource.FormWidthData_NumbersNetWeightMsg;
        #endregion 
    }
    public class ClothWidthModel
    {
        public int ID { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public decimal LengthInt { get; set; }
        public string Area { get; set; }
    }
}
