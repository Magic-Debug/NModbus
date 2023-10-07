using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using MainFrom;

namespace PLCTool
{
    class Common
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public int ScanRate { get; set; }
        public ModbusStatus modbusStatus { get; set; }
        public ushort[] modbusValues;
        public bool IsAdmin;
        public bool IsSuperAdmin;
        public string MachineSerialNumber { get; private set; }
        public string MachineModel { get; private set; }
        public string ConfigDirectory => Path.Combine(Application.StartupPath, "Config");

        private static DateTime lastreceivetime = DateTime.Now;
        //称重特别处理的变量
        public float LogFinalWeight = 0f;

        #region 单例
        private static readonly object locker = new object();
        private static Common Instance = null;
        public static Common GetInstance()
        {
            lock (locker)
            {
                if (Instance == null)
                {
                    Instance = new Common();
                }
            }
            return Instance;
        }
        #endregion

        private Common()
        {
            CreateConfigFolder();
            string configfile = Path.Combine(Application.StartupPath, "PLCTool.exe.config");
            SystemConfig.LoadSettings(configfile);
            MachineSerialNumber ="MachineSerialNumber";// SysValueService.GetSysSetValue("");
            GetMachineModel(MachineSerialNumber);
            IP = SystemConfig.GetConfigValues("IP");
            Port = Convert.ToInt32(SystemConfig.GetConfigValues("Port"));
            ScanRate = Convert.ToInt32(SystemConfig.GetConfigValues("ScanRate"));
            ModbusStatus.OnePlusLength = Convert.ToSingle(SystemConfig.GetConfigValues("OnePlusLength"));
            modbusStatus = new ModbusStatus();
            modbusValues = new ushort[ModbusRegs.Count];
            PLCLog.Init();
            if (SystemConfig.GetConfigValues("AutoLog") == "1")
            {
                new Thread(WriteLog).Start();
            }
        }
        public void WriteLog()
        {
            while (true)
            {
                try
                {
                    if (TcpCommunication.GetInstance().IsConnected)
                    {
                        DateTime time = DateTime.Now;
                        time = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
                        if (time != lastreceivetime)
                        {
                            ushort[] value = new ushort[modbusValues.Length];
                            Array.Copy(modbusValues, value, modbusValues.Length);
                            if (value.ToFloat(ModbusRegs.FinalWeight) == 0 && LogFinalWeight != 0)
                            {
                                Array.Copy(LogFinalWeight.ToByteArray(), 0, value, ModbusRegs.FinalWeight, 2);
                            }
                            LogFinalWeight = 0;
                            PLCLog.WriteLog(time, value);
                            lastreceivetime = time;
                        }
                    }
                }
                catch { }
            }
        }

        private void GetMachineModel(string machineserialnumber)
        {
            string[] s = machineserialnumber.Split('-');
            if (s.Length >= 2)
            {
                MachineModel = s[1];
            }
            else
            {
                MachineModel = "";
            }
        }
        private void CreateConfigFolder()
        {
            if (!Directory.Exists(ConfigDirectory))
            {
                Directory.CreateDirectory(ConfigDirectory);
            }
        }
    }
}
