using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.ZhongZhi
{
    public class SendPackerBase
    {
        /// <summary>
        /// 包开始标识符
        /// </summary>
        public const byte PackerStartMark = (byte)'S';

        /// <summary>
        /// 包结束标识符
        /// </summary>
        public const byte PackerEndMark = (byte)'#';

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
            List<byte> packerBytes = new List<byte>();
            packerBytes.Add(PackerStartMark);           
            //添加命令
            foreach (CommandBase commandBase in Commands)
            {
                packerBytes.AddRange(commandBase.AsBytes());
            }
            packerBytes.Add(CheckSum);//校验和            
            packerBytes.Add(PackerEndMark);//包结束标识符

            //计算校验和         
            for (int i = 0; i < packerBytes.Count - 1; i++)
            {
                packerBytes[packerBytes.Count - 2] += packerBytes[i];
            }

            return packerBytes.ToArray();
        }
    }
}
