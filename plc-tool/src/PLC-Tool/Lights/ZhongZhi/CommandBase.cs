using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.ZhongZhi
{
    public class CommandBase
    {
        public CommandBase(char channel, string commandParas)
        {            
            this.Channel = channel;
            this.CommandParas = commandParas;
        }

        /// <summary>
        /// 通道
        /// </summary>
        public char Channel;

        /// <summary>
        /// 亮度('T'：亮，'F'：灭
        /// </summary>
        public string CommandParas;
               

        /// <summary>
        /// 获取设置单个通道亮度数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetSetOneChannelLightBrightnessCommand(char channel, byte brightness)
        {
            return new CommandBase(channel, brightness.ToString().PadLeft(4,'0'));
        }

        /// <summary>
        /// 获取设置单个通道开关数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetSetOneChannelOpenOrCloseCommand(char channel, bool Open)
        {
            return new CommandBase(channel, Open ? "T" : "F");
        }
        

        public byte[] AsBytes()
        {
            List<byte> commandBytes = new List<byte>();
            
            commandBytes.Add((byte)Channel);//通道号
            //命令参数
            if (!string.IsNullOrEmpty(CommandParas))
            {
                commandBytes.AddRange(Encoding.ASCII.GetBytes(CommandParas));
            }

            return commandBytes.ToArray();
        }
    }
}
