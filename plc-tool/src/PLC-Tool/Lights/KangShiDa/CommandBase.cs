using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.KangShiDa
{
    public class CommandBase
    {
        public CommandBase(char PackerStartMark, ChannelIDs? channel, string commandParas)
        {
            this.PackerStartMark = PackerStartMark;
            this.Channel = channel;
            this.CommandParas = commandParas;
        }

        /// <summary>
        /// 包开始标识符
        /// </summary>
        public char PackerStartMark;//'S'或'T';

        /// <summary>
        /// 通道
        /// </summary>
        public ChannelIDs? Channel;

        /// <summary>
        /// 亮度('T'：亮，'F'：灭
        /// </summary>
        public string CommandParas;

        /// <summary>
        /// 包结束标识符
        /// </summary>
        public const char PackerEndMark = '#';


        /// <summary>
        /// 获取设置单个通道亮度数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetSetLightBrightnessCommand(ChannelIDs channel, uint brightness)
        {
            return new CommandBase('S',channel, brightness.ToString().PadLeft(4,'0'));
        }

        /// <summary>
        /// 获取读取单个通道亮度数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetReadOneChannelCommand(ChannelIDs channel)
        {
            return new CommandBase('S', channel, null);
        }

        /// <summary>
        /// 获取设置单个通道常亮/灭数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetSetOneChannelOpenOrCloseCommand(bool Open)
        {
            return new CommandBase('T', null, Open ? "H" : "L");
        }

        /// <summary>
        /// 获取读取单个通道常亮/灭数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetReadOneChannelOpenOrCloseCommand()
        {
            return new CommandBase('T', null, null);
        }

        public byte[] AsBytes()
        {
            List<byte> commandBytes = new List<byte>();
            commandBytes.Add((byte)PackerStartMark);
            //通道号
            if (Channel != null)
            {
                commandBytes.Add((byte)Channel);
            }
            //命令参数
            if (!string.IsNullOrEmpty(CommandParas))
            {
                commandBytes.AddRange(Encoding.ASCII.GetBytes(CommandParas));
            }
            commandBytes.Add((byte)PackerEndMark);

            return commandBytes.ToArray();
        }

        public string AsString()
        {
            string returnStr = "";           
            returnStr += PackerStartMark.ToString();
            //通道号
            if (Channel != null)
            {                
                returnStr += Channel.ToString();
            }
            //命令参数
            if (!string.IsNullOrEmpty(CommandParas))
            {
                returnStr += CommandParas;                
            }
            returnStr += PackerEndMark.ToString();

            return returnStr;
        }
    }
}
