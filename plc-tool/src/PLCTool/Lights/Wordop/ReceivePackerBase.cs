using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.Wordop
{
    public class ReceivePackerBase
    {
        public ReceivePackerBase(byte[] packerBytes)
        {
            if (packerBytes == null || packerBytes.Length < 6)
                return;
            PackerBytes = new byte[packerBytes.Length];
            packerBytes.CopyTo(PackerBytes, 0);

            PackerMark = PackerBytes[0];
            DataLength = PackerBytes[1];
            DeviceStyle = PackerBytes[2];
            DeviceID = PackerBytes[3];

            byte[] commandBytes = new byte[PackerBytes.Length - 5];
            Array.Copy(PackerBytes, 4, commandBytes, 0, commandBytes.Length);
            Commands.Add(new CommandBase(commandBytes));
        }

        /// <summary>
        /// 通讯标识符
        /// </summary>
        public byte PackerMark { get; private set; } = 0x40;

        /// <summary>
        /// 数据长度
        /// </summary>
        public byte DataLength { get; private set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public byte DeviceStyle { get; private set; } = 0x03;

        /// <summary>
        /// 设备编号
        /// </summary>
        public byte DeviceID { get; private set; }        

        /// <summary>
        /// 是否为无错误数据包
        /// </summary>
        public bool IsNoErrorPacker
        {
            get
            {
                return Commands.Count > 0 && Commands.FirstOrDefault(item => item.CommandCode == CommandType.Error_DeviceReback) == null;
            }
        }

        /// <summary>
        /// 命令
        /// </summary>
        public List<CommandBase> Commands { get; private set; } = new List<CommandBase>();        

        /// <summary>
        /// 校验和
        /// </summary>
        public byte CheckSum { get; private set; }

        /// <summary>
        /// 包字节数据
        /// </summary>
        public byte[] PackerBytes { get; private set; }
    }
}
