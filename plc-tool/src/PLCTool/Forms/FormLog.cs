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
using System.Text.RegularExpressions;
using MainFrom;

namespace PLCTool
{
    public partial class FormLog : BaseForm
    {
        private PLCLogData[] logdata;
        private DateTime maxtime = DateTime.Now;
        private DateTime mintime = DateTime.Now;
        public FormLog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filename = openFileDialog1.FileName;
            try
            {
                logdata = PLCLog.ReadLog(filename);
                mintime = logdata[0].time;
                maxtime = logdata[logdata.Length - 1].time;
                dateTimePicker1.Value = mintime;
                dateTimePicker2.Value = maxtime;
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void BindData()
        {
            PLCLogData[] binddata = GetBindData();
            DataTable dt;
            dt = PLCLog.GetTable(binddata);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.AutoGenerateColumns = true;
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].Width = 80;
            }
            SetDataColumnWidth();
            SetDataGridViewColor();
            chart_Encode1.BindData(binddata);
            chart_Weight1.BindData(binddata);
            chart_WindingTension1.BindData(binddata);
            chart_InspectionTension1.BindData(binddata);
            chart_PositionX1.BindData(binddata);
            chart_PositionY1.BindData(binddata);
            chart_PositionZ1.BindData(binddata);
            richTextBox1.Clear();
            PLCChange[] changes = PLCLog.GetChanges(logdata);
            foreach (var k in changes)
            {
                richTextBox1.AppendText(k.ToString());
            }
        }
        private PLCLogData[] GetBindData()
        {
            DateTime time1 = dateTimePicker1.Value;
            DateTime time2 = dateTimePicker2.Value;
            PLCLogData[] binddata = PLCLog.FilterData(logdata, time1, time2);
            return binddata;
        }
        private void SetDataColumnWidth()
        {
            dataGridView1.Columns[0].Width = 80;
            int index = 1;
            for (int i = 0; i < PLCLog.Registers.Count; i++)
            {
                if (PLCLog.Registers[i].Visibel)
                {
                    if (PLCLog.Registers[i].DataType == PLCDataType.Binary)
                    {
                        dataGridView1.Columns[index++].Width = 120;
                    }
                    else if (PLCLog.Registers[i].DataType == PLCDataType.AlarmText || PLCLog.Registers[i].DataType == PLCDataType.TicketAlarmText)
                    {
                        dataGridView1.Columns[index++].Width = 200;
                    }
                    else
                    {
                        dataGridView1.Columns[index++].Width = 80;
                    }
                }
            }
        }

        private void SetDataGridViewColor()
        {
            SetIntervalErrorColor();
            SetAlarmColor();
            SetRunningColor();
        }
        private void SetIntervalErrorColor()
        {
            Color intervalErrorColor = Color.FromArgb(255, 128, 0);
            if (dataGridView1.Rows.Count > 1)
            {
                DateTime time0 = DateTime.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                DateTime time1 = DateTime.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                if ((time1 - time0).TotalSeconds > 1)
                {
                    dataGridView1.Rows[0].DefaultCellStyle.BackColor = intervalErrorColor;
                }
                for (int i = 1; i < dataGridView1.Rows.Count - 1; i++)
                {
                    DateTime last = DateTime.Parse(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                    DateTime current = DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    DateTime next = DateTime.Parse(dataGridView1.Rows[i + 1].Cells[0].Value.ToString());
                    if ((current - last).TotalSeconds > 1 || (next - current).TotalSeconds > 1)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = intervalErrorColor;
                    }
                }
                time0 = DateTime.Parse(dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Value.ToString());
                time1 = DateTime.Parse(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value.ToString());
                if ((time1 - time0).TotalSeconds > 1)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = intervalErrorColor;
                }
            }
        }
        private void SetAlarmColor()
        {
            Color errorColor = Color.FromArgb(255, 0, 0);
            List<PLCRegister> list = PLCLog.Registers.Where(a => a.Visibel).ToList();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Address == ModbusRegs.Alarm || list[i].Address == ModbusRegs.TicketAlarm)
                    {
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            string value = dataGridView1.Rows[j].Cells[i + 1].Value.ToString();
                            if (value != "0000000000000000" && value != "")
                            {
                                dataGridView1.Rows[j].Cells[i + 1].Style.BackColor = errorColor;
                            }
                        }
                    }
                }
            }
            catch { }
        }
        private void SetRunningColor()
        {
            Color runningColor = Color.FromArgb(0, 255, 0);
            List<PLCRegister> list = PLCLog.Registers.Where(a => a.Visibel).ToList();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Address == ModbusRegs.PLCStart_stop || list[i].Address == ModbusRegs.DeviceRunning)
                    {
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            if (Convert.ToUInt16(dataGridView1.Rows[j].Cells[i + 1].Value) == 1)
                            {
                                dataGridView1.Rows[j].Cells[i + 1].Style.BackColor = runningColor;
                            }
                        }
                    }
                }
            }
            catch { }
        }
        private void FormLog_Load(object sender, EventArgs e)
        {
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            Location = new Point(0, 0);
            StartPosition = FormStartPosition.Manual;            
            LoadAlarmItem();
            cbbAlarm.SelectedIndex = 0;
            cbbTicketAlarm.SelectedIndex = 0;
            cbbStatus.SelectedIndex = 0;            
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            tabControl1.SelectedIndex = 1;
            BindData();
        }
        private void LoadAlarmItem()
        {
            List<KeyValuePair<ushort, string>> AlarmItems = PLCLog.GetAlarmItems();
            List<KeyValuePair<ushort, string>> TicketAlarmItems = PLCLog.GetTicketAlarmItems();
            cbbAlarm.DisplayMember = "Value";
            cbbAlarm.ValueMember = "Key";
            cbbAlarm.DataSource = AlarmItems;
            cbbTicketAlarm.DisplayMember = "Value";
            cbbTicketAlarm.ValueMember = "Key";
            cbbTicketAlarm.DataSource = TicketAlarmItems;
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < mintime)
            {
                dateTimePicker1.Value = mintime;
            }
            if (dateTimePicker2.Value < mintime)
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
            BindData();
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value > maxtime)
            {
                dateTimePicker2.Value = maxtime;
            }
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                dateTimePicker1.Value = mintime;
            }
            BindData();
        }

        private void dateTimePicker1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dateTimePicker1.Value < mintime)
                {
                    dateTimePicker1.Value = mintime;
                }
                if (dateTimePicker2.Value < mintime)
                {
                    dateTimePicker2.Value = dateTimePicker1.Value;
                }
                BindData();
            }
        }

        private void dateTimePicker2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dateTimePicker2.Value > maxtime)
                {
                    dateTimePicker2.Value = maxtime;
                }
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    dateTimePicker1.Value = mintime;
                }
                BindData();
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            new FormLogSetting().ShowDialog();
            BindData();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlSearch.Visible = tabControl1.SelectedIndex == 1;
        }
        private void btnLast1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || cbbAlarm.SelectedValue == null)
            {
                return;
            }
            PLCLogData[] log = GetBindData();
            ushort alarmCode = (ushort)cbbAlarm.SelectedValue;
            int currentIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            int lastIndex = PLCLog.GetLastAlarmIndex(log, alarmCode, currentIndex);
            if (lastIndex == currentIndex)
            {
                MessageBox.Show(NotFoundAlarmMsg);
            }
            else
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
            }
        }
        private void btnNext1_Click(object sender,EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || cbbAlarm.SelectedValue == null)
            {
                return;
            }
            PLCLogData[] log = GetBindData();
            ushort alarmCode = (ushort)cbbAlarm.SelectedValue;
            int currentIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            int lastIndex = PLCLog.GetNextAlarmIndex(log, alarmCode, currentIndex);
            if (lastIndex == currentIndex)
            {
                MessageBox.Show(NotFoundAlarmMsg);
            }
            else
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
            }
        }
        private void btnLast2_Click(object sender,EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || cbbAlarm.SelectedValue == null)
            {
                return;
            }
            PLCLogData[] log = GetBindData();
            ushort alarmCode = (ushort)cbbTicketAlarm.SelectedValue;
            int currentIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            int lastIndex = PLCLog.GetLastTicketAlarmIndex(log, alarmCode, currentIndex);
            if (lastIndex == currentIndex)
            {
                MessageBox.Show(NotFoundAlarmMsg);
            }
            else
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
            }
        }
        private void btnNext2_Click(object sender,EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || cbbAlarm.SelectedValue == null)
            {
                return;
            }
            PLCLogData[] log = GetBindData();
            ushort alarmCode = (ushort)cbbTicketAlarm.SelectedValue;
            int currentIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            int lastIndex = PLCLog.GetNextTicketAlarmIndex(log, alarmCode, currentIndex);
            if (lastIndex == currentIndex)
            {
                MessageBox.Show(NotFoundAlarmMsg);
            }
            else
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
            }
        }
        private void btnLast3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || cbbStatus.SelectedIndex == -1)
            {
                return;
            }
            PLCLogData[] log = GetBindData();
            int currentIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            int lastIndex = currentIndex;
            switch(cbbStatus.SelectedIndex)
            {
                case 0:
                    lastIndex = PLCLog.GetLastOvertimeIndex(log, currentIndex);
                    break;
                case 1:
                    lastIndex = PLCLog.GetLastRealWeightChangedIndex(log, currentIndex);
                    break;
                case 2:
                    lastIndex = PLCLog.GetLastFinalWeightChangedIndex(log, currentIndex);
                    break;
                case 3:
                    lastIndex = PLCLog.GetLastRunningStatusIndex(log, currentIndex);
                    break;
                case 4:
                    lastIndex = PLCLog.GetLastStopStatusIndex(log, currentIndex);
                    break;
                case 5:
                    lastIndex = PLCLog.GetLastResetEncoderIndex(log, currentIndex);
                    break;
                case 6:
                    lastIndex = PLCLog.GetLastSetTicketPositionXIndex(log, currentIndex);
                    break;
                case 7:
                    lastIndex = PLCLog.GetLastSetTicketPositionYIndex(log, currentIndex);
                    break;
                case 8:
                    lastIndex = PLCLog.GetLastTicketIndex(log, currentIndex);
                    break;
                case 9:
                    lastIndex = PLCLog.GetLastSetSpeedIndex(log, currentIndex);
                    break;
                case 10:
                    lastIndex = PLCLog.GetLastSetInspectionTensionIndex(log, currentIndex);
                    break;
                case 11:
                    lastIndex = PLCLog.GetLastSetWindingTensionIndex(log, currentIndex);
                    break;
                case 12:
                    lastIndex = PLCLog.GetLastWriteRepertedly(log, currentIndex);
                    break;
                case 13:
                    lastIndex = PLCLog.GetLastTicketNotZeroing(log, currentIndex);
                    break;
            }
            if (lastIndex == currentIndex)
            {
                MessageBox.Show(NotFoundChangeMsg);
            }
            else
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
            }
        }
        private void btnNext3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || cbbStatus.SelectedIndex == -1)
            {
                return;
            }
            PLCLogData[] log = GetBindData();
            int currentIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            int nextIndex = currentIndex;
            switch (cbbStatus.SelectedIndex)
            {
                case 0:
                    nextIndex = PLCLog.GetNextOvertimeIndex(log, currentIndex);
                    break;
                case 1:
                    nextIndex = PLCLog.GetNextRealWeightChangedIndex(log, currentIndex);
                    break;
                case 2:
                    nextIndex = PLCLog.GetNextFinalWeightChangedIndex(log, currentIndex);
                    break;
                case 3:
                    nextIndex = PLCLog.GetNextRunningStatusIndex(log, currentIndex);
                    break;
                case 4:
                    nextIndex = PLCLog.GetNextStopStatusIndex(log, currentIndex);
                    break;
                case 5:
                    nextIndex = PLCLog.GetNextResetEncoderIndex(log, currentIndex);
                    break;
                case 6:
                    nextIndex = PLCLog.GetNextSetTicketPositionXIndex(log, currentIndex);
                    break;
                case 7:
                    nextIndex = PLCLog.GetNextSetTicketPositionYIndex(log, currentIndex);
                    break;
                case 8:
                    nextIndex = PLCLog.GetNextTicketIndex(log, currentIndex);
                    break;
                case 9:
                    nextIndex = PLCLog.GetNextSetSpeedIndex(log, currentIndex);
                    break;
                case 10:
                    nextIndex = PLCLog.GetNextSetInspectionTensionIndex(log, currentIndex);
                    break;
                case 11:
                    nextIndex = PLCLog.GetNextSetWindingTensionIndex(log, currentIndex);
                    break;
                case 12:
                    nextIndex = PLCLog.GetNextWriteRepertedly(log, currentIndex);
                    break;
                case 13:
                    nextIndex = PLCLog.GetNextTicketNotZeroing(log, currentIndex);
                    break;
            }
            if (nextIndex == currentIndex)
            {
                MessageBox.Show(NotFoundChangeMsg);
            }
            else
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = nextIndex;
            }
        }

        #region 多语言
        private string NotFoundAlarmMsg => LanguageResource.FormLog_NotFoundAlarmMsg;
        private string NotFoundChangeMsg => LanguageResource.FormLog_NotFoundChangeMsg;
        #endregion

    }
}
