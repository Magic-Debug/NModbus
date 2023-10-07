using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainFrom
{
    public abstract class SignalCommunication
    {
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
        public virtual bool IsConnected { get; set; }

        /// <summary>
        /// 读写超时时间(毫秒数)
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// 读取所有数据的线程是否可以运行
        /// </summary>
        public bool IsCanRunning { get; set; }

        #endregion
    }
}
 