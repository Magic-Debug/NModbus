using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.KangShiDa
{
    public class SendPackerBase
    {
        /// <summary>
        /// 命令
        /// </summary>
        public List<CommandBase> Commands { get; private set; } = new List<CommandBase>();        

        public virtual byte[] AsBytes()
        {
            List<byte> packerBytes = new List<byte>();
                    
            //添加命令
            foreach (CommandBase commandBase in Commands)
            {
                packerBytes.AddRange(commandBase.AsBytes());
            }

            return packerBytes.ToArray();
        }

        public virtual string AsString()
        {
            string returnStr = "";

            //添加命令
            foreach (CommandBase commandBase in Commands)
            {
                returnStr += commandBase.AsString();
            }

            return returnStr;
        }
    }
}
