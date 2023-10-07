using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.ChenKe
{
    public class ReceivePackerBase
    {
        public ReceivePackerBase(byte[] packerBytes)
        {
            if (packerBytes == null || packerBytes.Length < 7)
                return;

            PackerBytes = new byte[packerBytes.Length];
            packerBytes.CopyTo(PackerBytes, 0);

            int offSet = 0;            
            //包标志
            Array.Copy(PackerBytes, offSet, PackerMark, 0, 2);
            offSet += 2;
            //机器地址
            DeviceAddress = PackerBytes[offSet];
            offSet += 1;
            //命令长度
            DataLength = PackerBytes[offSet];
            offSet += 1;
            //命令
            byte[] commandBytes = new byte[3];
            for (; offSet < PackerBytes.Length; offSet += 3)
            {
                Array.Copy(PackerBytes, offSet, commandBytes, 0, commandBytes.Length);
                Commands.Add(new CommandBase(commandBytes));
            }
        }

        /// <summary>
        /// 通讯标识符
        /// </summary>
        public byte[] PackerMark { get; private set; } = new byte[] { 0x53, 0x4C };

        /// <summary>
        /// 机器地址
        /// </summary>
        public byte DeviceAddress { get; private set; } = 0xAA;

        /// <summary>
        /// 命令长度
        /// </summary>
        public byte DataLength { get; private set; } = 0x03;

        /// <summary>
        /// 包字节数据
        /// </summary>
        public byte[] PackerBytes { get; private set; }

        /// <summary>
        /// 命令
        /// </summary>
        public List<CommandBase> Commands { get; private set; } = new List<CommandBase>();

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
