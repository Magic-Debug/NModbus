using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCommon.Utils
{
    [Serializable]
    public class LicenseData
    {
        //----------以下日期精确到天------------
        ///// <summary>
        ///// 本机机器码生成的Key
        ///// </summary>
        //public string Key { get; set; }
        ///// <summary>
        ///// 安装时间
        ///// </summary>
        //public string InstallationDate { get;set;}
        /// <summary>
        /// 授权起始时间,安装时写入，后期授权重写
        /// </summary>
        public string StartingDate { get; set; }
        /// <summary>
        /// 程序最后一次关闭的时间
        /// </summary>
        public string LastRuningDate { get; set; }
        /// <summary>
        /// 授权有效天数
        /// </summary>
        public int EffectiveDays { get; set; }
    }
}
