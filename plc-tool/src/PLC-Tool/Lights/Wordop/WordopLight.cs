using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PLCTool.Lights.Wordop
{
    /// <summary>
    /// 沃德普光源
    /// </summary>
    public class WordopLight : LightBase
    {
        public WordopLight(string portName, int baudRate, byte deviceStyle = 0x03, byte deviceIDOrAddress = 0, int delayTimeForReceive = 20) : base(portName, baudRate, deviceIDOrAddress, delayTimeForReceive)
        {
            this.DeviceStyle = deviceStyle;

            OpenPort();
        }

        /// <summary>
        /// 设备型号
        /// </summary>
        public byte DeviceStyle = 0x03;

        /// <summary>
        /// 测试连接设备(检查串口和通讯协议是否正确)
        /// </summary>
        /// <param name="finallyClosePort">最后是否关闭串口</param>
        /// <returns></returns>
        public override bool TestConnect(bool finallyClosePort = true)
        {
            try
            {
                OpenPort(); 
                if (!IsPortOpend)
                    return false;

                CommandBase commandReadAllChannel = CommandBase.GetReadAllChannelCommand();
                ReceivePackerBase packerReceive = SendCommandAndWaitReback(commandReadAllChannel);
                if(packerReceive == null || packerReceive.DataLength == 0)
                    ClosePort();

                return packerReceive != null && packerReceive.IsNoErrorPacker;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (finallyClosePort)
                    ClosePort();
            }
        }

        public override string[] ReadChannelList()
        {
            if (!IsPortOpend)
                return new string [0];

            CommandBase commandReadAllChannel = CommandBase.GetReadAllChannelCommand();
            ReceivePackerBase packerReceive = SendCommandAndWaitReback(commandReadAllChannel);

            if (packerReceive == null || !packerReceive.IsNoErrorPacker)
                return new string[0];
            CommandBase command = packerReceive.Commands.FirstOrDefault(item => item.CommandCode == CommandType.AllChannelInfo_DeviceReback);
            if (command == null || command.CommandParas == null)
                return new string[0];
            string[] channels = new string[command.CommandParas.Length];
            for (byte i = 0; i < command.CommandParas.Length; i++)
            {
                channels[i] = i.ToString();
            }

            return channels;
        }

        public override byte ReadOneChannelBrightness(string channel)
        {
            return 0;
        }

        public override byte[] ReadOneChannel(string channel)
        {
            byte btChannel;
            if (!IsPortOpend || !byte.TryParse(channel.Trim(), out btChannel))
                return null;  
                     
            //发送           
            CommandBase commandReadOneChannel = CommandBase.GetReadOneChannelCommand(btChannel);
            ReceivePackerBase packerReceive = SendCommandAndWaitReback(commandReadOneChannel);

            return packerReceive != null ? packerReceive.PackerBytes : null;
        }

        public override byte[] ReadAllChannel()
        {
            if (!IsPortOpend)           
                return null;            

            CommandBase commandReadAllChannel = CommandBase.GetReadAllChannelCommand();
            ReceivePackerBase packerReceive = SendCommandAndWaitReback(commandReadAllChannel);

            return packerReceive != null ? packerReceive.PackerBytes : null;
        }

        public override bool SetDeviceIDOrAddress(byte newIDOrAddress)
        {
            if (!IsPortOpend)
                return false;

            CommandBase commandSetDeviceID = CommandBase.GetSetDeviceIDCommand(newIDOrAddress);
            ReceivePackerBase packerReceive = SendCommandAndWaitReback(commandSetDeviceID);

            return packerReceive != null && packerReceive.IsNoErrorPacker;
        }

        public override bool SetOneChannelBrightness(string channel, byte brightness)
        {
            byte btChannel;
            if (!IsPortOpend || !byte.TryParse(channel.Trim(),out btChannel))
                return false;
           
            //发送            
            CommandBase commandSetBrightness = CommandBase.GetSetLightBrightnessCommand(btChannel, brightness);
            ReceivePackerBase packerReceive = SendCommandAndWaitReback(commandSetBrightness);

            return packerReceive != null && packerReceive.IsNoErrorPacker;
        }

        public override bool SetAllChannelBrightness(byte brightness)
        {
            //获取通道列表
            string[] channels = ReadChannelList();
                   
            //获取设置所有通道的命令
            CommandBase[] commands = new CommandBase[channels.Length];
            for(int i = 0; i < channels.Length; i ++)
            {
                commands[i] = CommandBase.GetSetLightBrightnessCommand(Convert.ToByte(channels[i]), brightness);                
            }
            //发送命令
            ReceivePackerBase packerReceive = SendCommandAndWaitReback(commands);

            return packerReceive != null && packerReceive.IsNoErrorPacker;
        }

        /// <summary>
        /// 发送命令并等待设备回复
        /// </summary>
        /// <param name="command"></param>
        /// <param name="waitTimeBeforeReceive"></param>
        /// <returns></returns>
        private ReceivePackerBase SendCommandAndWaitReback(CommandBase command)
        {
            return SendCommandAndWaitReback(new CommandBase[] { command });
        }

        /// <summary>
        /// 发送命令并等待设备回复
        /// </summary>
        /// <param name="command"></param>
        /// <param name="waitTimeBeforeReceive"></param>
        /// <returns></returns>
        private ReceivePackerBase SendCommandAndWaitReback(CommandBase[] commands)
        {
            if (commands == null)
                return null;

            //发送           
            SendPackerBase spb = new SendPackerBase(DeviceStyle, DeviceIDOrAddress);
            spb.Commands.AddRange(commands);
            byte[] packerBytes = spb.AsBytes();
            byte[] receiveBytes = SendAndReceiveBytes(packerBytes);

            return receiveBytes != null ? new ReceivePackerBase(receiveBytes) : null;            
        }
    }
}
