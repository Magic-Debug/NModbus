using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.Wordop
{
    public class SendPackerBase
    {
        public SendPackerBase(byte deviceStyle = 0x03, byte deviceID = 0x0)
        {
            this.DeviceStyle = deviceStyle;
            this.DeviceID = deviceID;
        }

        /// <summary>
        /// 通讯标识符
        /// </summary>
        public const byte PackerMark = 0x40;

        /// <summary>
        /// 数据长度
        /// </summary>
        public byte DataLength { get; private set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public byte DeviceStyle = 0x03;

        /// <summary>
        /// 设备编号
        /// </summary>
        public byte DeviceID;

        /// <summary>
        /// 命令
        /// </summary>
        public List<CommandBase> Commands { get; private set; } = new List<CommandBase>();        

        /// <summary>
        /// 校验和
        /// </summary>
        public byte CheckSum { get; private set; }

        public virtual byte[] AsBytes()
        {
            try
            {
                List<byte> packerBytes = new List<byte>();
                packerBytes.Add(PackerMark);
                packerBytes.Add(DataLength);
                packerBytes.Add(DeviceStyle);
                packerBytes.Add(DeviceID);
                //添加命令
                foreach (CommandBase commandBase in Commands)
                {
                    packerBytes.AddRange(commandBase.AsBytes());
                }
                packerBytes.Add(CheckSum);

                //修改数据长度
                packerBytes[1] = (byte)(packerBytes.Count - 3);
                //计算校验和         
                for (int i = 0; i < packerBytes.Count - 1; i++)
                {
                    packerBytes[packerBytes.Count - 1] += packerBytes[i];
                }

                return packerBytes.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
