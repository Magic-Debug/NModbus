using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.ChenKe
{
    public class CommandBase
    {
        public CommandBase(CommandType commandType, byte channel, byte commandParam)
        {
            this.CommandCode = commandType;
            this.Channel = channel;
            this.CommandParam = commandParam;
        }

        public CommandBase(byte[] commandBytes)
        {
            if (commandBytes == null || commandBytes.Length != 3)
                return;

            Enum.TryParse(commandBytes[0].ToString(), out CommandCode);
            switch (CommandCode)
            {
                case CommandType.Right_DeviceReback:
                    Brightness = commandBytes[1];
                    IsOpened = commandBytes[2] == 1;
                    CommandParam = commandBytes[2];
                    break;
                default:
                    Channel = commandBytes[1];
                    CommandParam = commandBytes[2];
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
        public byte Channel;

        /// <summary>
        /// 命令参数(设备返回值解析：1命令代码不对，2命令长度不对，3通道号不存在，4参数不对)
        /// </summary>
        public byte CommandParam;

        /// <summary>
        /// 通道亮度
        /// </summary>
        public byte Brightness { get; private set; }

        /// <summary>
        /// 通道是否打开
        /// </summary>
        public bool IsOpened { get; private set; }

        /// <summary>
        /// 获取读取单个通道数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetReadOneChannelCommand(byte channel)
        {
            return new CommandBase(CommandType.ReadChannelInfo_Send, channel, 0);
        }

        /// <summary>
        /// 获取读取所有通道数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetReadAllChannelCommand()
        {
            return new CommandBase(CommandType.ReadChannelInfo_Send, 0xFF, 0);
        }

        /// <summary>
        /// 获取设置设备ID数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetSetDeviceIDCommand(byte id)
        {
            return new CommandBase(CommandType.SetDeviceID_Send, 0, id);
        }

        /// <summary>
        /// 获取设置单个通道亮度数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetSetOneChannelLightBrightnessCommand(byte channel, byte brightness)
        {
            return new CommandBase(CommandType.SetBrightNess_Send, channel, brightness);
        }

        /// <summary>
        /// 获取设置所有通道亮度数据包
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static CommandBase GetSetAllChannelLightBrightnessCommand(byte brightness)
        {
            return new CommandBase(CommandType.SetBrightNess_Send, 0xFF, brightness);
        }

        public byte[] AsBytes()
        {
            List<byte> commandBytes = new List<byte>();
            commandBytes.Add((byte)CommandCode);            
            commandBytes.Add(Channel);//通道号            
            commandBytes.Add(CommandParam);//命令参数

            return commandBytes.ToArray();
        }
    }
}
