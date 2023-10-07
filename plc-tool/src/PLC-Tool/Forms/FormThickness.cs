using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrameworkCommon;
using PLCTool.UC;

namespace PLCTool.Forms
{
    public partial class FormThickness : BaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        public MainFrom.TcpCommunication communication = MainFrom.TcpCommunication.GetInstance();
        /// <summary>
        /// 
        /// </summary>
        private Common common = Common.GetInstance();
        /// <summary>
        /// 传感器数据
        /// </summary>
        BindingList<DataSensor> dataSensors = new BindingList<DataSensor>();

        public FormThickness()
        {
            InitializeComponent();

            textBoxEx14.LostFocus += TextBoxEx14_LostFocus;
        }

        private void FormThickness_Load(object sender, EventArgs e)
        {
            buttonRadiusEx2.Enabled = false;
            RadioButton1.Checked = true;
            textBoxEx14.Text = SystemConfig.GetConfigValues("ThicknessDisplacement");

            dataGridView1.AutoGenerateColumns = false;
            dataSensors.Add(new DataSensor() { Code = "3", Address = "0", DualChannel = "0", LowerLimit = "820", UpperLimit = "4095", DisplacementLowerLimit = "25.000", DisplacementUpperLimit = "35.000" });
            dataSensors.Add(new DataSensor() { Code = "3", Address = "1", DualChannel = "0", LowerLimit = "820", UpperLimit = "4095", DisplacementLowerLimit = "25.000", DisplacementUpperLimit = "35.000" });
            dataGridView1.DataSource = dataSensors;
        }

        /// <summary>
        /// 文本框输入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxEx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBoxEx)
            {
                var textBoxEx = sender as TextBoxEx;

                //判断按键是不是要输入的类型。
                if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                {
                    e.Handled = true;
                }

                //小数点的处理。
                //小数点
                if ((int)e.KeyChar == 46)
                {
                    if (textBoxEx.Text.Length <= 0)
                    {
                        //小数点不能在第一位
                        e.Handled = true;
                    }
                    else
                    {
                        //输入小数点前文本框的值
                        bool b1 = float.TryParse(textBoxEx.Text, out float oldf);
                        //输入小数点后文本框的值
                        bool b2 = float.TryParse(textBoxEx.Text + e.KeyChar.ToString(), out float f);

                        if (b2 == false)
                        {
                            if (b1 == true)
                            {
                                e.Handled = true;
                            }
                            else
                            {
                                e.Handled = false;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 单元格值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
        }
        /// <summary>
        /// 启动采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRadiusEx1_Click(object sender, EventArgs e)
        {
            if (communication.IsConnected)
            {
                DateTime dateTime = DateTime.Now;
                DataFileName = dateTime.ToString("yyyy-MM-dd-HH-mm");
                //清除数据
                chart_Displacement1.ClearData(0);
                chart_Displacement1.ClearData(1);
                timer1.Enabled = true;
                buttonRadiusEx1.Enabled = false;
                buttonRadiusEx2.Enabled = true;
            }
        }
        /// <summary>
        /// 停止采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRadiusEx2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            buttonRadiusEx1.Enabled = true;
            buttonRadiusEx2.Enabled = false;
        }
        /// <summary>
        /// 保存文件名
        /// </summary>
        string DataFileName = "";
        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (communication.IsConnected)
            {
                common.modbusStatus.SetValues(common.modbusValues);
                ModbusStatus states = common.modbusStatus;
                //设备1位移
                var value1 = states.ThicknessDisplacement.ToString("f3");
                //设备2位移
                var value2 = states.ThicknessDisplacement2.ToString("f3");
                //差值
                var value3 = (float.Parse(textBoxEx14.Text) - float.Parse(textBoxEx1.Text) - float.Parse(textBoxEx2.Text)).ToString("f3");
                //设备1
                textBoxEx1.Text = value1;
                //设备2
                textBoxEx2.Text = value2;
                //差值
                textBoxEx3.Text = value3;

                var dateTime = DateTime.Now;
                //位移一图表数据
                chart_Displacement1.BindData(dateTime, Convert.ToDouble(value1), 0);
                //位移二图标数据
                chart_Displacement1.BindData(dateTime, Convert.ToDouble(value2), 1);

                //保存历史文件
                string DataFileRootPath = System.Environment.CurrentDirectory + "\\历史报表";
                WriteData(DataFileRootPath, DataFileName, dateTime, Convert.ToDouble(value1), Convert.ToDouble(value2), Convert.ToDouble(value3));
            }
            else
            {
                timer1.Enabled = false;
                buttonRadiusEx1.Enabled = true;
                buttonRadiusEx2.Enabled = false;
            }
        }
        /// <summary>
        /// 读取起始位移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRadiusEx5_Click(object sender, EventArgs e)
        {
            common.modbusStatus.SetValues(common.modbusValues);
            ModbusStatus states = common.modbusStatus;
            textBoxEx14.Text = states.ThicknessDisplacement.ToString("f3");
            SystemConfig.SaveConfigValue("ThicknessDisplacement", textBoxEx14.Text);
        }
        /// <summary>
        /// 起始位移失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxEx14_LostFocus(object sender, EventArgs e)
        {
            SystemConfig.SaveConfigValue("ThicknessDisplacement", textBoxEx14.Text);
        }
        /// <summary>
        /// 起始位移回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxEx14_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBoxEx14_LostFocus(sender, e);
            }
        }
        /// <summary>
        /// 保存历史报表
        /// </summary>
        /// <param name="DataFileRootPath">文件保存路径</param>
        /// <param name="FileName">文件名不带后缀</param>
        /// <param name="dateTime">日期时间</param>
        /// <param name="value1">位移一</param>
        /// <param name="value2">位移二</param>
        /// <param name="value3">差值</param>
        private void WriteData(string DataFileRootPath, string FileName, DateTime dateTime, double value1, double value2, double value3)
        {
            try
            {
                if (System.IO.Directory.Exists(DataFileRootPath) == false)
                {
                    System.IO.Directory.CreateDirectory(DataFileRootPath);
                }

                StringBuilder DataColumn = new StringBuilder();
                StringBuilder DataLine = new StringBuilder();

                //列标题
                DataColumn.Append("时间,位移一,位移二,差值");
                //行数据
                DataLine.Append(dateTime.ToString("yyyy-MM-dd HH-mm-ss"));
                DataLine.Append(",");
                DataLine.Append(value1);
                DataLine.Append(",");
                DataLine.Append(value2);
                DataLine.Append(",");
                DataLine.Append(value3);

                string FilePath = DataFileRootPath + "\\" + FileName + ".csv";

                if (System.IO.File.Exists(FilePath) == false)
                {
                    System.IO.StreamWriter stream = new System.IO.StreamWriter(FilePath, false, Encoding.UTF8);
                    stream.WriteLine(DataColumn);
                    stream.WriteLine(DataLine);
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
                else
                {
                    System.IO.StreamWriter stream = new System.IO.StreamWriter(FilePath, true, Encoding.UTF8);
                    stream.WriteLine(DataLine);
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 历史文件查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRadiusEx4_Click(object sender, EventArgs e)
        {
            string DataFileRootPath = System.Environment.CurrentDirectory + "\\历史报表";
            if (System.IO.Directory.Exists(DataFileRootPath) == false)
            {
                System.IO.Directory.CreateDirectory(DataFileRootPath);
            }
            System.Diagnostics.Process.Start("explorer.exe", DataFileRootPath);
        }

        private void FormThickness_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }
    }

    /// <summary>
    /// 传感器设置
    /// </summary>
    public class DataSensor
    {
        /// <summary>
        /// 功能码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 单0/双1
        /// </summary>
        public string DualChannel { get; set; }
        /// <summary>
        /// 原下限
        /// </summary>
        public string LowerLimit { get; set; }
        /// <summary>
        /// 原上限
        /// </summary>
        public string UpperLimit { get; set; }
        /// <summary>
        /// 位移上限
        /// </summary>
        public string DisplacementLowerLimit { get; set; }
        /// <summary>
        /// 位移下限
        /// </summary>
        public string DisplacementUpperLimit { get; set; }
    }
}
