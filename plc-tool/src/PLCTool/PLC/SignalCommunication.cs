using NModbus;
using NModbus.Extensions;
using NModbus.Logging;
using System.Net;
using System.Net.Sockets;

namespace MainFrom
{
    public abstract class SignalCommunication
    {
        public int ScanRate { get; set; } = 50;
        public int Port { get; set; } = 502;
        public IPAddress ipaddress { get; set; }

        protected ModbusFactory Factory { get; }

        protected IModbusMaster Master { get; set; }

        protected ModbusMasterEnhanced ModbusEnhanced { get; }

        public SignalCommunication()
        {
            TcpClient = new TcpClient("192.168.0.9", Port);
            Factory = new ModbusFactory(null, true, NullModbusLogger.Instance);
            Master = Factory.CreateMaster(TcpClient);
            ModbusEnhanced = new ModbusMasterEnhanced(Master);
        }
        protected TcpClient TcpClient { get; }
        public abstract void Init();
        public abstract bool Connect();
        public abstract void Disconnect();
        #region 属性

        /// <summary>
        /// 是否正在关闭系统
        /// </summary>
        public bool IsClosing { get; set; }

        /// <summary>
        /// PLC是否连接
        /// </summary>
        public virtual bool Connected => TcpClient.Connected;

        /// <summary>
        /// 读写超时时间(毫秒数)
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// 读取所有数据的线程是否可以运行
        /// </summary>
        public bool IsCanRunning { get; set; }


        public string ByteArrayToString(byte[] data)
        {
            string text = string.Empty;
            foreach (byte t in data)
            {
                text += $"{t}\t";
            }
            return text;
        }

        #endregion
    }
}
