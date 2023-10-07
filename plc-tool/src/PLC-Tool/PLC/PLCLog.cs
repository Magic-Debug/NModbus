using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MainFrom;

namespace PLCTool
{
    public static class PLCLog
    {
        //当前日志的日期时间
        private static DateTime CurrentLogDate;
        private static string CurentFileName;
        private static Regex RegxAddress = new Regex(@"^\d{1,2}(-\d{1,2})?(,\d{1,2}(-\d{1,2})?)*$");
        private static ComponentResourceManager rm = new ComponentResourceManager(typeof(PLCLog));
        private const ushort AllAlarmCode = 65535;
        private const ushort AllTicketAlarmCode = 63;

        //需要用文本展现修改记录的寄存器地址
        private static byte[] ChangeAddress = new byte[]
        {
            ModbusRegs.MotorState, ModbusRegs.WorkState, ModbusRegs.DeviceReady, ModbusRegs.Alarm, ModbusRegs.TicketFinish, ModbusRegs.DeviceRunning, ModbusRegs.TicketAlarm
        };

        public static List<PLCRegister> Registers = new List<PLCRegister>();

        public static void Init()
        {
            LoadRegisters();
            DateTime t = DateTime.Now;
            CurrentLogDate = new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
            CurentFileName = GetFileName(CurrentLogDate);
        }

        public static List<KeyValuePair<ushort, string>> GetAlarmItems()
        {
            List<KeyValuePair<ushort, string>> list = new List<KeyValuePair<ushort, string>>();
            ModbusStatus status = new ModbusStatus();
            status.Alarm = AllAlarmCode;
            List<string> alarms = status.GetAlarmList();
            list.Add(new KeyValuePair<ushort, string>(AllAlarmCode, rm.GetString("AllAlarmItem")));
            for (int i = 0; i < alarms.Count; i++)
            {
                list.Add(new KeyValuePair<ushort, string>((ushort)(1 << i), alarms[i]));
            }
            return list;
        }
        public static List<KeyValuePair<ushort, string>> GetTicketAlarmItems()
        {
            List<KeyValuePair<ushort, string>> list = new List<KeyValuePair<ushort, string>>();
            ModbusStatus status = new ModbusStatus();
            status.TicketAlarm = AllTicketAlarmCode;
            List<string> ticketAlarms = status.GetTicketAlarmList();
            list.Add(new KeyValuePair<ushort, string>(AllTicketAlarmCode, rm.GetString("AllAlarmItem")));
            for (int i = 0; i < ticketAlarms.Count; i++)
            {
                list.Add(new KeyValuePair<ushort, string>((ushort)(1 << i), ticketAlarms[i]));
            }
            return list;
        }

        /// <summary>
        /// 把PLC运行的状态写到日志里
        /// </summary>
        /// <param name="time">PLC运行的时间</param>
        /// <param name="data">PLC运行的状态</param>
        public static void WriteLog(DateTime time, ushort[] data)
        {
            try
            {
                time = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
                if ((time - CurrentLogDate).TotalSeconds >= 70)
                {
                    CurrentLogDate = time;
                    CurentFileName = GetFileName(time);
                }
                string logDirectory = Path.GetDirectoryName(CurentFileName);
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
                BinaryWriter writer = new BinaryWriter(new FileStream(CurentFileName, FileMode.Append));
                writer.Write(time.ToBinary());
                writer.Write(data.Length);
                foreach (ushort k in data)
                {
                    writer.Write(k);
                }
                writer.Close();
            }
            catch { }
        }

        /// <summary>
        /// 通过文件名读取日志
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>日志数据</returns>
        public static PLCLogData[] ReadLog(string filename)
        {
            //解决读写冲突问题
            //如果读的文件正好是当前写的文件，换成当前时间的文件名继续写
            if (filename == CurentFileName)
            {
                DateTime t = DateTime.Now;
                CurrentLogDate = new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
                CurentFileName = GetFileName(CurrentLogDate);
                if (CurentFileName == filename)
                {
                    throw new Exception(rm.GetString("ReadFileErrorMsg"));
                }
                Task.Delay(1000);
            }
            List<PLCLogData> list = new List<PLCLogData>();
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader reader = new BinaryReader(fs);
            int defaultcount = 0;
            while (fs.Position < fs.Length)
            {
                try
                {
                    long t = reader.ReadInt64();
                    DateTime time = new DateTime(t);
                    int count = reader.ReadInt32();
                    if (count == 0)
                    {
                        break;
                    }
                    else if (defaultcount == 0)
                    {
                        defaultcount = count;
                    }
                    else if (count != defaultcount)
                    {
                        break;
                    }
                    ushort[] data = new ushort[count];
                    for (int i = 0; i < count; i++)
                    {
                        data[i] = reader.ReadUInt16();
                    }
                    list.Add(new PLCLogData { time = time, data = data });
                }
                catch
                {
                    break;
                }
            }
            fs.Close();
            return list.ToArray();
        }

        /// <summary>
        /// 获取新文件名
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>文件名</returns>
        private static string GetFileName(DateTime date)
        {
            return Directory.GetCurrentDirectory() + $"\\Log\\{date.Year}\\{date.Month}\\{date.Day}\\PLC{date:yyyyMMddHHmm}.dat";
        }

        /// <summary>
        /// 用二分查找法查找时间索引
        /// </summary>
        /// <param name="logdata">日志数据，默认是按时间顺序记录的，如果顺序错乱，得不到正确的结果</param>
        /// <param name="time">查找的时间</param>
        /// <param name="mode">匹配模式
        /// 0：精确匹配，找不到时间节点时返回-1
        /// -1：往左匹配，找不到时间节点时返回时间节点前最接近的索引，如果第一条记录都超出查找的时间则返回-1
        /// 1：往右匹配，找不到时间节点时返回时间节点后最接近的索引，如果最后一条记录都未达到查找的时间则返回-1
        /// </param>
        /// <returns></returns>
        private static int FindIndex(PLCLogData[] logdata, DateTime time, int mode = 0)
        {
            int l = -1;
            int h = logdata.Length;
            int m = (l + h) / 2;
            while (h - l > 1)
            {
                DateTime t = logdata[m].time;
                if (t == time)
                    return m;
                else if (t < time)
                {
                    l = m;
                    m = (l + h) / 2;
                }
                else
                {
                    h = m;
                    m = (l + h) / 2;
                }
            }
            if (mode == 0)
                return -1;
            else if (mode < 0)
                return l;
            else if (h < logdata.Length)
                return h;
            else
                return -1;
        }

        public static PLCChange[] GetChanges(PLCLogData[] logdata)
        {
            if (logdata == null || logdata.Length == 0)
                return new PLCChange[0];
            List<PLCChange> list = new List<PLCChange>();
            for (int j = 0; j < ChangeAddress.Length; j++)
                list.Add(new PLCChange { time = logdata[0].time, Address = ChangeAddress[j], Value = logdata[0].data[ChangeAddress[j]] });
            for (int i = 1; i < logdata.Length; i++)
                for (int j = 0; j < ChangeAddress.Length; j++)
                    if (logdata[i].data[ChangeAddress[j]] != logdata[i - 1].data[ChangeAddress[j]])
                        list.Add(new PLCChange { time = logdata[i].time, Address = ChangeAddress[j], Value = logdata[i].data[ChangeAddress[j]] });
            return list.ToArray();
        }

        /// <summary>
        /// 按时间间隔提取日志数据，用于绑定图表
        /// </summary>
        /// <param name="data">所有数据</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="Interval">时间间隔</param>
        /// <returns>筛选之后的数据</returns>
        public static PLCLogData[] FilterData(PLCLogData[] data, DateTime StartTime, DateTime EndTime)
        {
            if (data == null || data.Length == 0)
                return new PLCLogData[0];
            return data.Where(t => t.time >= StartTime && t.time <= EndTime).ToArray();
        }

        /// <summary>
        /// 按时间间隔提取日志数据，用于绑定图表
        /// </summary>
        /// <param name="data">所有数据</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="Interval">时间间隔</param>
        /// <returns>筛选之后的数据</returns>
        public static PLCLogData[] FilterData(PLCLogData[] data, DateTime StartTime, DateTime EndTime, int Interval)
        {
            if (data == null || data.Length == 0)
                return new PLCLogData[0];
            List<PLCLogData> result = new List<PLCLogData>();
            int second = (int)(StartTime - StartTime.Date).TotalSeconds;
            second = second / Interval * Interval;
            StartTime = StartTime.Date.AddSeconds(second);
            for (DateTime t = StartTime; t <= EndTime; t = t.AddSeconds(Interval))
            {
                int index = FindIndex(data, t);
                if (index >= 0)
                    result.Add(data[index]);
            }
            return result.ToArray();
        }
        public static DataTable GetTable(PLCLogData[] data)
        {
            DataTable dt = new DataTable();
            string timestr = rm.GetString("Time");
            dt.Columns.Add(timestr);
            for (int i = 0; i < Registers.Count; i++)
            {
                if (Registers[i].Visibel)
                {
                    dt.Columns.Add(Registers[i].Name);
                }
            }
            for (int i = 0; i < data.Length; i++)
            {
                PLCLogData d = data[i];
                DataRow dr = dt.NewRow();
                dr[timestr] = d.time.ToString("HH:mm:ss");
                foreach (PLCRegister r in Registers)
                {
                    if (r.Visibel)
                    {
                        dr[r.Name] = d[r];
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static DataTable GetAllRegsTable(PLCLogData[] data)
        {
            DataTable dt = new DataTable();
            string timestr = rm.GetString("Time");
            dt.Columns.Add(timestr);
            for (int i = 0; i < ModbusRegs.Count; i++)
                dt.Columns.Add($"{i:X2} ({i})");
            for (int i = 0; i < data.Length; i++)
            {
                PLCLogData d = data[i];
                DataRow dr = dt.NewRow();
                dr[0] = $"{d.time:HH:mm:ss}";
                for (int j = 0; j < ModbusRegs.Count; j++)
                    dr[j + 1] = $"{d.data[j]:X4} ({d.data[j]})";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static DataTable GetFilterRegsTable(PLCLogData[] data, string filter)
        {
            if (!RegxAddress.IsMatch(filter))
                return GetAllRegsTable(data);
            DataTable dt = new DataTable();
            string timestr = rm.GetString("Time");
            dt.Columns.Add(timestr);
            List<int> adresslist = new List<int>();
            string[] areas = filter.Split(',');
            for (int i = 0; i < areas.Length; i++)
            {
                string[] value = areas[i].Split('-');
                if (value.Length == 1)
                {
                    dt.Columns.Add($"{value[0]:X2} ({value[0]})");
                    adresslist.Add(Convert.ToInt32(value[0], 10));
                }
                else
                {
                    int min = Convert.ToInt32(value[0], 10);
                    int max = Convert.ToInt32(value[1], 10);
                    for (int j = min; j <= max; j++)
                    {
                        dt.Columns.Add($"{j:X2} ({j})");
                        adresslist.Add(j);
                    }
                }
            }
            for (int i = 0; i < data.Length; i++)
            {
                PLCLogData d = data[i];
                DataRow dr = dt.NewRow();
                dr[0] = $"{d.time:HH:mm:ss}";
                for (int j = 0; j < adresslist.Count; j++)
                {
                    dr[j + 1] = $"{d.data[adresslist[j]]:X4} ({d.data[adresslist[j]]})";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static DataTable GetTable(PLCLogData[] data, DateTime startTime, DateTime endTime, int interval)
        {
            PLCLogData[] binddata = FilterData(data, startTime, endTime, interval);
            return GetTable(binddata);
        }
        public static int GetLastAlarmIndex(PLCLogData[] log, ushort alarmCode, int currentIndex)
        {
            return FindLastStatus(log, BitSelector(ModbusRegs.Alarm, alarmCode), currentIndex);
        }
        public static int GetNextAlarmIndex(PLCLogData[] log, ushort alarmCode, int currentIndex)
        {
            return FindNextStatus(log, BitSelector(ModbusRegs.Alarm, alarmCode), currentIndex);
        }
        public static int GetLastTicketAlarmIndex(PLCLogData[] log, ushort ticketAlarmCode, int currentIndex)
        {
            return FindLastStatus(log, BitSelector(ModbusRegs.TicketAlarm, ticketAlarmCode), currentIndex);
        }
        public static int GetNextTicketAlarmIndex(PLCLogData[] log, ushort ticketAlarmCode, int currentIndex)
        {
            return FindNextStatus(log, BitSelector(ModbusRegs.TicketAlarm, ticketAlarmCode), currentIndex);
        }
        public static int GetLastRealWeightChangedIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastStatus(log, FloatRangeSelector(ModbusRegs.RealtimeWeight, 0f, float.PositiveInfinity, false, false), currentIndex);
        }
        public static int GetNextRealWeightChangedIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextStatus(log, FloatRangeSelector(ModbusRegs.RealtimeWeight, 0f, float.PositiveInfinity, false, false), currentIndex);
        }
        public static int GetLastFinalWeightChangedIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastStatus(log, FloatRangeSelector(ModbusRegs.FinalWeight, 0f, float.PositiveInfinity, false, false), currentIndex);
        }
        public static int GetNextFinalWeightChangedIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextStatus(log, FloatRangeSelector(ModbusRegs.FinalWeight, 0f, float.PositiveInfinity, false, false), currentIndex);
        }
        public static int GetLastRunningStatusIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastStatus(log, UshortValueSelector(ModbusRegs.DeviceRunning, 1), currentIndex);
        }
        public static int GetNextRunningStatusIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextStatus(log, UshortValueSelector(ModbusRegs.DeviceRunning, 1), currentIndex);
        }
        public static int GetLastStopStatusIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastStatus(log, UshortValueSelector(ModbusRegs.DeviceRunning, 0), currentIndex);
        }
        public static int GetNextStopStatusIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextStatus(log, UshortValueSelector(ModbusRegs.DeviceRunning, 0), currentIndex);
        }
        public static int GetLastResetEncoderIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastStatus(log, FloatRangeSelector(ModbusRegs.CurrentPositionY, 0f, 0f), currentIndex);
        }
        public static int GetNextResetEncoderIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextStatus(log, FloatRangeSelector(ModbusRegs.CurrentPositionY, 0f, 0f), currentIndex);
        }
        public static int GetLastOvertimeIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastChanged(log, OverTimeSelector, currentIndex);
        }
        public static int GetNextOvertimeIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextChanged(log, OverTimeSelector, currentIndex);
        }
        public static int GetLastSetTicketPositionXIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastChanged(log, FloatValueChangedSelector(ModbusRegs.TicketPositionX), currentIndex);
        }
        public static int GetNextSetTicketPositionXIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextChanged(log, FloatValueChangedSelector(ModbusRegs.TicketPositionX), currentIndex);
        }
        public static int GetLastSetTicketPositionYIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastChanged(log, FloatValueChangedSelector(ModbusRegs.TicketPositionY), currentIndex);
        }
        public static int GetNextSetTicketPositionYIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextChanged(log, FloatValueChangedSelector(ModbusRegs.TicketPositionY), currentIndex);
        }
        public static int GetLastTicketIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastStatus(log, UshortValueSelector(ModbusRegs.TicketFinish, 0), currentIndex);
        }
        public static int GetNextTicketIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextStatus(log, UshortValueSelector(ModbusRegs.TicketFinish, 0), currentIndex);
        }
        public static int GetLastSetSpeedIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastChanged(log, FloatValueChangedSelector(ModbusRegs.MasterMotorSpeed), currentIndex);
        }
        public static int GetNextSetSpeedIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextChanged(log, FloatValueChangedSelector(ModbusRegs.MasterMotorSpeed), currentIndex);
        }
        public static int GetLastSetInspectionTensionIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastChanged(log, FloatValueChangedSelector(ModbusRegs.InspectionTensionSetValue), currentIndex);
        }
        public static int GetNextSetInspectionTensionIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextChanged(log, FloatValueChangedSelector(ModbusRegs.InspectionTensionSetValue), currentIndex);
        }
        public static int GetLastSetWindingTensionIndex(PLCLogData[] log, int currentIndex)
        {
            return FindLastChanged(log, FloatValueChangedSelector(ModbusRegs.WindingTensionSetValue), currentIndex);
        }
        public static int GetNextSetWindingTensionIndex(PLCLogData[] log, int currentIndex)
        {
            return FindNextChanged(log, FloatValueChangedSelector(ModbusRegs.WindingTensionSetValue), currentIndex);
        }
        public static int GetLastWriteRepertedly(PLCLogData[]log,int currentIndex)
        {
            return FindLastChanged(log, WriteRepeatedly, currentIndex);
        }
        public static int GetNextWriteRepertedly(PLCLogData[]log,int currentIndex)
        {
            return FindNextChanged(log, WriteRepeatedly, currentIndex);
        }
        public static int GetLastTicketNotZeroing(PLCLogData[] log, int currentIndex)
        {
            return FindLastChanged(log, TicketNotZeroingSelector, currentIndex);
        }
        public static int GetNextTicketNotZeroing(PLCLogData[] log, int currentIndex)
        {
            return FindNextChanged(log, TicketNotZeroingSelector, currentIndex);
        }
        public static void SaveRegisters()
        {
            string language = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            string filename = "Registers." + language + ".ini";
            SaveRegisters(filename);
        }
        public static void SaveRegisters(string filename)
        {
            string directoryname = Path.GetDirectoryName(filename);
            if (!Directory.Exists(directoryname))
            {
                Directory.CreateDirectory(directoryname);
            }
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            StreamWriter writer = new StreamWriter(filename);
            foreach (PLCRegister r in Registers)
            {
                writer.WriteLine($"{r.Name},{r.Address},{(int)r.DataType},{(r.Visibel ? "1" : "0")}");
            }
            writer.Close();
        }
        public static void LoadRegisters(string filename)
        {
            Registers = new List<PLCRegister>();
            if (File.Exists(filename))
            {
                StreamReader reader = new StreamReader(filename);
                while (!reader.EndOfStream)
                {
                    string[] para = reader.ReadLine().Split(',');
                    if (para.Length == 4)
                    {
                        try
                        {
                            Registers.Add(new PLCRegister(para[0], Convert.ToInt32(para[1]), (PLCDataType)Convert.ToInt32(para[2]), para[3] == "1"));
                        }
                        catch
                        {
                        }
                    }
                }
                reader.Close();
            }
        }
        public static void LoadRegisters()
        {
            string language = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            string filename = "Registers." + language + ".ini";
            if (!File.Exists(filename))
            {
                filename = "Registers.ini";
            }
            LoadRegisters(filename);
        }
        private static int FindLastStatus(PLCLogData[] log, Func<PLCLogData, bool> statusSelector, int currentIndex)
        {
            int result = currentIndex;
            for (int i = result - 1; i >= 0; i--)
            {
                if (statusSelector.Invoke(log[i]))
                {
                    result = i;
                    break;
                }
            }
            if (result == currentIndex)
            {
                return result;
            }
            result++;
            int tag = result;
            for (int i = result - 2; i >= 0; i--)
            {
                if (!statusSelector.Invoke(log[i]))
                {
                    result = i + 1;
                    break;
                }
            }
            if (result == tag)
            {
                return 0;
            }
            return result;
        }
        private static int FindNextStatus(PLCLogData[] log, Func<PLCLogData, bool> statusSelector, int currentIndex)
        {
            int result = currentIndex - 1;
            for (int i = currentIndex; i < log.Length; i++)
            {
                if (!statusSelector.Invoke(log[i]))
                {
                    result = i;
                    break;
                }
            }
            if (result == currentIndex - 1)
            {
                return currentIndex;
            }
            int tag = result;
            for (int i = result + 1; i < log.Length; i++)
            {
                if (statusSelector.Invoke(log[i]))
                {
                    result = i;
                    break;
                }
            }
            if (result == tag)
            {
                return currentIndex;
            }
            return result;
        }
        private static int FindLastChanged(PLCLogData[] log, Func<PLCLogData, PLCLogData, bool> changedSelector, int currentIndex)
        {
            for (int i = currentIndex - 1; i >= 0; i--)
            {
                if (changedSelector.Invoke(log[i], log[i + 1]))
                {
                    return i;
                }
            }
            return currentIndex;
        }
        private static int FindNextChanged(PLCLogData[] log, Func<PLCLogData, PLCLogData, bool> changeSelector, int currentIndex)
        {
            for (int i = currentIndex + 1; i < log.Length - 1; i++)
            {
                if (changeSelector.Invoke(log[i], log[i + 1]))
                {
                    return i;
                }
            }
            return currentIndex;
        }
        private static Func<PLCLogData, bool> BitSelector(int address, ushort bits)
        {
            return (log) => { return (log.data[address] & bits) != 0; };
        }
        private static Func<PLCLogData, bool> UshortValueSelector(int address, ushort value)
        {
            return (log) => { return log.data[address] == value; };
        }
        private static Func<PLCLogData, bool> IntValueSelector(int address, int value)
        {
            return (log) => { return log.data.ToInt32(address) == value; };
        }
        private static Func<PLCLogData, bool> IntRangeSelector(int address, int minValue, int maxValue)
        {
            return (log) =>
            {
                int value = log.data.ToInt32(address);
                return value >= minValue && value <= maxValue;
            };
        }
        private static Func<PLCLogData, bool> FloatRangeSelector(int address, float minValue, float maxValue, bool containMin = true, bool containMax = true)
        {
            return (log) =>
            {
                float value = log.data.ToFloat(address);
                bool flag = true;
                if (containMin)
                {
                    flag = value >= minValue;
                }
                else
                {
                    flag = value > minValue;
                }
                if (!flag)
                {
                    return flag;
                }
                if (containMax)
                {
                    flag = value <= maxValue;
                }
                else
                {
                    flag = value < maxValue;
                }
                return flag;
            };
        }
        private static Func<PLCLogData, PLCLogData, bool> OverTimeSelector = (log1, log2) =>
        {
            return (int)(log2.time - log1.time).TotalSeconds > 1;
        };
        private static Func<PLCLogData, PLCLogData, bool> WriteRepeatedly = (log1, log2) =>
        {
            return log2.time == log1.time;
        };
        private static Func<PLCLogData, PLCLogData, bool> FloatValueChangedSelector(int address)
        {
            return (log1, log2) => { return log1.data.ToFloat(address) != log2.data.ToFloat(address); };
        }
        private static Func<PLCLogData, PLCLogData, bool> ValueChangedSelector(int address)
        {
            return (log1, log2) => { return log1.data[address] != log2.data[address]; };
        }
        private static Func<PLCLogData, PLCLogData, bool> TicketNotZeroingSelector = (log1, log2) =>
        {
            return (log1.data[47] & 0x10) == 0x10 && log2.CurrentPositionX != 0 && Math.Abs(log2.CurrentPositionX) >= log1.CurrentPositionX;
        };
    }

    /// <summary>
    /// 日志记录的数据结构
    /// </summary>
    public struct PLCLogData
    {
        /// <summary>
        ///  PLC运行的时间
        /// </summary>
        public DateTime time;
        /// <summary>
        /// PLC运行的状态
        /// 对应所有寄存器的值，目前是读取50个寄存器
        /// </summary>
        public ushort[] data;

        public object this[PLCRegister r]
        {
            get
            {
                ModbusStatus status;
                string s;
                switch (r.DataType)
                {
                    case PLCDataType.Int16:
                        return (short)data[r.Address];
                    case PLCDataType.Uint16:
                        return data[r.Address];
                    case PLCDataType.Int32:
                        return data.ToInt32(r.Address);
                    case PLCDataType.Uint32:
                        return data.ToUInt32(r.Address);
                    case PLCDataType.Float:
                        return data.ToFloat(r.Address);
                    case PLCDataType.Binary:
                        s = "";
                        for (int i = 15; i >= 0; i--)
                        {
                            s += (data[r.Address] >> i & 1) == 1 ? "1" : "0";
                        }
                        return s;
                    case PLCDataType.AlarmText:
                        status = new ModbusStatus();
                        status.Alarm = data[ModbusRegs.Alarm];
                        List<string> alarmlist = status.GetAlarmList();
                        if (alarmlist.Count == 0)
                        {
                            return "";
                        }
                        else
                        {
                            s = alarmlist[0];
                            for (int i = 1; i < alarmlist.Count; i++)
                            {
                                s += ", " + alarmlist[i];
                            }
                            return s;
                        }
                    case PLCDataType.TicketAlarmText:
                        status = new ModbusStatus();
                        status.TicketAlarm = data[ModbusRegs.TicketAlarm];
                        List<string> ticketalarmlist = status.GetTicketAlarmList();
                        if (ticketalarmlist.Count == 0)
                        {
                            return "";
                        }
                        else
                        {
                            s = ticketalarmlist[0];
                            for (int i = 1; i < ticketalarmlist.Count; i++)
                            {
                                s += ", " + ticketalarmlist[i];
                            }
                            return s;
                        }
                    default:
                        return 0;
                }
            }
        }

        #region 需要用折线图展现的变量
        /// <summary>
        /// 编码器值
        /// </summary>
        public int Encoder => data[0] << 16 | data[1];

        /// <summary>
        /// 称重实时值
        /// </summary>
        public float RealtimeWeight => data.ToFloat(ModbusRegs.RealtimeWeight);

        /// <summary>
        /// 称重变送值
        /// </summary>
        public float FinalWeight => data.ToFloat(ModbusRegs.FinalWeight);

        /// <summary>
        /// 牵引电机速度设定值
        /// </summary>
        public float MasterMotorSpeed => data.ToFloat(ModbusRegs.MasterMotorSpeed);

        /// <summary>
        /// 张力电机速度系数
        /// </summary>
        public float SlaveMotorRatio => data.ToFloat(ModbusRegs.SlaveMotorRatio);

        /// <summary>
        /// 摆布电机速度系数
        /// </summary>
        public float SwingMotorRatio => data.ToFloat(ModbusRegs.SwingMotorRatio);

        /// <summary>
        /// X轴位置
        /// </summary>
        public float CurrentPositionX => data.ToFloat(ModbusRegs.CurrentPositionX);

        /// <summary>
        /// Y轴位置
        /// </summary>
        public float CurrentPositionY => data.ToFloat(ModbusRegs.CurrentPositionY);

        /// <summary>
        /// Z轴位置
        /// </summary>
        public float CurrentPositionZ => data.ToFloat(ModbusRegs.CurrentPositionZ);

        public float TicketPositionX => data.ToFloat(ModbusRegs.TicketPositionX);
        public float TicketPositionY => data.ToFloat(ModbusRegs.TicketPositionY);

        /// <summary>
        /// 恒张力收卷PID给定值
        /// </summary>
        public float WindingTensionSetValue => data.ToFloat(ModbusRegs.WindingTensionSetValue);

        /// <summary>
        /// 恒张力收卷PID反馈值
        /// </summary>
        public float WindingTensionGetValue => data.ToFloat(ModbusRegs.WindingTensionGetValue);

        /// <summary>
        /// 恒张力验布PID给定值
        /// </summary>
        public float InspectionTensionSetValue => data.ToFloat(ModbusRegs.InspectionTensionSetValue);

        /// <summary>
        /// 恒张力验布PID反馈值
        /// </summary>
        public float InspectionTensionGetValue => data.ToFloat(ModbusRegs.InspectionTensionGetValue);
        #endregion
    }

    public struct PLCChange
    {
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime time;

        /// <summary>
        /// 寄存器地址
        /// </summary>
        public byte Address;

        /// <summary>
        /// 修改值
        /// </summary>
        public ushort Value;

        private static ComponentResourceManager rm = new ComponentResourceManager(typeof(PLCLog));

        public override string ToString()
        {
            string option = "";
            switch (Address)
            {
                case ModbusRegs.MotorState:
                    option = Value == 1 ? rm.GetString("MotorState1") : rm.GetString("MotorState0");
                    break;
                case ModbusRegs.WorkState:
                    option = rm.GetString("WorkStateSetting") + (Value == 1 ? rm.GetString("Manual") : rm.GetString("Auto"));
                    break;
                case ModbusRegs.DeviceReady:
                    option = Value == 1 ? rm.GetString("DeviceReady1") : rm.GetString("DeviceReady0");
                    break;
                case ModbusRegs.TicketFinish:
                    option = Value == 1 ? rm.GetString("TicketFinish1") : rm.GetString("TicketFinish0");
                    break;
                case ModbusRegs.DeviceRunning:
                    option = Value == 1 ? rm.GetString("DeviceRunning1") : rm.GetString("DeviceRunning0");
                    break;
                case ModbusRegs.Alarm:
                    option = GetAlarmString(Value);
                    if (option == "")
                        return "";
                    break;
                case ModbusRegs.TicketAlarm:
                    option = GetTicketAlarmString(Value);
                    if (option == "")
                        return "";
                    break;
            }
            return $"{time:MM-dd HH:mm:ss} {option}\n";
        }
        private string GetAlarmString(ushort value)
        {
            string result = "";
            if ((value & 1) == 1)
                result += rm.GetString("Alarm0");
            if ((value & 2) == 2)
                result += rm.GetString("Alarm1");
            if ((value & 4) == 4)
                result += rm.GetString("Alarm2");
            if ((value & 8) == 8)
                result += rm.GetString("Alarm3");
            if ((value & 16) == 16)
                result += rm.GetString("Alarm4");
            if ((value & 32) == 32)
                result += rm.GetString("Alarm5");
            if ((value & 64) == 64)
                result += rm.GetString("Alarm6");
            if ((value & 128) == 128)
                result += rm.GetString("Alarm7");
            if ((value & 256) == 256)
                result += rm.GetString("Alarm8");
            if ((value & 512) == 512)
                result += rm.GetString("Alarm9");
            if ((value & 1024) == 1024)
                result += rm.GetString("Alarm10");
            if ((value & 2048) == 2048)
                result += rm.GetString("Alarm11");
            if ((value & 4096) == 4096)
                result += rm.GetString("Alarm12");
            if ((value & 8192) == 8192)
                result += rm.GetString("Alarm13");
            if ((value & 16384) == 16384)
                result += rm.GetString("Alarm14");
            if ((value & 32768) == 32768)
                result += rm.GetString("Alarm15");
            if (result.Length > 0)
                result = result.Substring(1);
            else
                result = "";
            return result;
        }
        private string GetTicketAlarmString(ushort value)
        {
            string result = "";
            if ((value & 1) == 1)
                result += rm.GetString("TicketAlarm0");
            else if ((value & 2) == 2)
                result += rm.GetString("TicketAlarm1");
            else if ((value & 4) == 4)
                result += rm.GetString("TicketAlarm2");
            else if ((value & 8) == 8)
                result += rm.GetString("TicketAlarm3");
            else if ((value & 16) == 16)
                result += rm.GetString("TicketAlarm4");
            if (result.Length > 0)
                result = result.Substring(1);
            else
                result = "";
            return result;
        }
    }

    public struct PLCRegister
    {
        public string Name { get; set; }
        public PLCDataType DataType { get; set; }
        public int Address { get; set; }
        public bool Visibel { get; set; }
        public string S_Address => (DataType == PLCDataType.Int16 || DataType == PLCDataType.Uint16 || DataType == PLCDataType.Binary ? "VW" : "VD") + Address * 2;
        public PLCRegister(string name, int address, PLCDataType datatype, bool visibel)
        {
            Name = name;
            Address = address;
            DataType = datatype;
            Visibel = visibel;
        }
    }

    public enum PLCDataType
    {
        Int16 = 1,
        Uint16 = 2,
        Int32 = 3,
        Uint32 = 4,
        Float = 5,
        Binary = 6,
        AlarmText = 7,
        TicketAlarmText = 8
    }
}
