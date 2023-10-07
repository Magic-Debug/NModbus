using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.ChenKe
{
    public class SendPackerBase
    {
        /// <summary>
        /// 通讯标识符
        /// </summary>
        public byte[] PackerMark { get; private set; } = new byte[] { 0x53, 0x4C };

        /// <summary>
        /// 机器地址
        /// </summary>
        public const byte DeviceAddress = 0xAA;    

        /// <summary>
        /// 命令长度
        /// </summary>
        public byte DataLength { get; private set; } = 0x03;

        /// <summary>
        /// 命令
        /// </summary>
        public List<CommandBase> Commands { get; private set; } = new List<CommandBase>();
        

        public virtual byte[] AsBytes()
        {
            List<byte> packerBytes = new List<byte>();
            packerBytes.AddRange(PackerMark);
            packerBytes.Add(DeviceAddress);
            packerBytes.Add(DataLength);          
            //添加命令
            foreach (CommandBase commandBase in Commands)
            {
                packerBytes.AddRange(commandBase.AsBytes());
            }
            //修改命令长度
            packerBytes[3] = (byte)(packerBytes.Count - 4);

            return packerBytes.ToArray();
        }
    }
}
