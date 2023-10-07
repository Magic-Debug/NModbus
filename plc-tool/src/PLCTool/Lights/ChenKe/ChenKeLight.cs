using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace PLCTool.Lights.ChenKe
{
    /// <summary>
    /// 辰科光源
    /// </summary>
    public class ChenKeLight : LightBase
    {
        public static int CurrentBrightness;

        public ChenKeLight(string portName,int baudRate, byte deviceIDOrAddress = 0, int delayTimeForReceive = 20) : base(portName, baudRate, deviceIDOrAddress, delayTimeForReceive)
        {            
            OpenPort();
        }

        /// <summary>
        /// 测试连接设备(检查串口和通讯协议是否正确)
        /// </summary>
        /// <param name="finallyClosePort">最后是否关闭串口</param>
        /// <returns></returns>
        public override bool TestConnect(bool finallyClosePort = true)
        {
            try
            {
                if (!IsPortOpend)
                    return false;

                CommandBase comandReadChannel = CommandBase.GetReadAllChannelCommand();
                ReceivePackerBase packerReceive = SendCommandAndWaitReback(comandReadChannel);
                if (packerReceive == null || packerReceive.DataLength == 0)
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
                return new string[0];

            CommandBase commandReadAllChannel = CommandBase.GetReadAllChannelCommand();
            ReceivePackerBase packerReceive = SendCommandAndWaitReback(commandReadAllChannel);

            if (packerReceive == null || !packerReceive.IsNoErrorPacker)
                return new string[0];
            CommandBase command = packerReceive.Commands.FirstOrDefault(item => item.CommandCode == CommandType.Right_DeviceReback);
            if (command == null)
                return new string[0];
            string[] channels = new string[command.CommandParam];
            for (byte i = 0; i < command.CommandParam; i++)
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
            if (!IsPortOpend || string.IsNullOrEmpty(channel.Trim()) || channel.Trim().Length != 1)            
                return null;
            
            byte btChannel;
            if (!byte.TryParse(channel.Trim(), out btChannel))
                return null;
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
            if (string.IsNullOrEmpty(channel.Trim()) || channel.Trim().Length != 1)
                return false;

            //发送
            byte btChannel;
            if (!byte.TryParse(channel.Trim(), out btChannel))
                return false;
            CommandBase commandSetBrightness = CommandBase.GetSetOneChannelLightBrightnessCommand(btChannel, brightness);
            ReceivePackerBase packerReceive = SendCommandAndWaitReback(commandSetBrightness);

            return packerReceive != null && packerReceive.IsNoErrorPacker;
        }

        public override bool SetAllChannelBrightness(byte brightness)
        {
            //发送
            CommandBase commandSetBrightness = CommandBase.GetSetAllChannelLightBrightnessCommand(brightness);
            ReceivePackerBase packerReceive = SendCommandAndWaitReback(commandSetBrightness);

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
            if (command == null)
                return null;

            //发送           
            SendPackerBase spb = new SendPackerBase();
            spb.Commands.Add(command);
            byte[] packerBytes = spb.AsBytes();           
            byte[] receiveBytes = SendAndReceiveBytes(packerBytes);

            return receiveBytes != null ? new ReceivePackerBase(receiveBytes) : null;
        }
    }
}
