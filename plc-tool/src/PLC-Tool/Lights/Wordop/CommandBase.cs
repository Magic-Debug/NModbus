using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.Wordop
{
    public class CommandBase
    {
        public CommandBase(CommandType commandType, byte? channel, byte[] commandParas)
        {
            this.CommandCode = commandType;
            this.Channel = channel;
            this.CommandParas = commandParas;
        }

        public CommandBase(byte[] commandBytes)
        {
            if (commandBytes == null || commandBytes.Length < 1)
                return;

            Enum.TryParse(commandBytes[0].ToString(), out CommandCode);
            switch (CommandCode)
            {
                case CommandType.Right_DeviceReback:                    
                case CommandType.Error_DeviceReback:                   
                    break;
                case CommandType.OneChannelInfo_DeviceReback:
                case CommandType.AllChannelInfo_DeviceReback:                                      
                    CommandParas = new byte[commandBytes.Length - 1];
                    Array.Copy(commandBytes, 1, CommandParas, 0, CommandParas.Length);
                    break;
            }            
        }

        /// <summary>
        /// 命令码
        /// </summary>
        public CommandType CommandCode;

        /// <summary>
        /// 通道
        /// </summary>
        public byte? Channel;

        /// <summary>
        /// 命令参数
        /// </summary>
        public byte[] CommandParas;        

        /// <summary>
        /// 获取读取单个通道数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetReadOneChannelCommand(byte channel)
        {
            return new CommandBase(CommandType.ReadChannelInfo_Send, channel, null);
        }

        /// <summary>
        /// 获取读取所有通道数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetReadAllChannelCommand()
        {
            return new CommandBase(CommandType.ReadChannelInfo_Send, 0xFF, null);
        }

        /// <summary>
        /// 获取设置设备ID数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetSetDeviceIDCommand(byte id)
        {
            return new CommandBase(CommandType.SetDeviceID_Send, null, new byte[] { id });
        }

        /// <summary>
        /// 获取设置亮度数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetSetLightBrightnessCommand(byte channel, byte brightness)
        {
            return new CommandBase(CommandType.SetBrightNess_Send, channel, new byte[] { brightness });
        }

        public byte[] AsBytes()
        {
            List<byte> commandBytes = new List<byte>();
            commandBytes.Add((byte)CommandCode);
            //通道号
            if (Channel != null)
            {
                commandBytes.Add((byte)Channel);
            }
            //命令参数
            if (CommandParas != null)
            {
                commandBytes.AddRange(CommandParas);
            }

            return commandBytes.ToArray();
        }
    }
}
