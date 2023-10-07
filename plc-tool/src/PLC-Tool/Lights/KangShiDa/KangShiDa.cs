using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLCTool.Lights.KangShiDa
{
    /// <summary>
    /// 众智光源
    /// </summary>
    public class KangShiDaLight : LightBase
    {
        public KangShiDaLight(string portName, int baudRate, byte deviceIDOrAddress = 0, int delayTimeForReceive = 20) : base(portName, baudRate, deviceIDOrAddress, delayTimeForReceive)
        {
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
                OpenPort();
                if (!IsPortOpend)
                    return false;

                string returnStr = ReadChannelLightAlwaysOnStatus()?.ToUpper();
                return returnStr == "H" || returnStr == "L";
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
            return new string[0];
        }

        public override byte[] ReadOneChannel(string channel)
        {
            ChannelIDs tempChannel;
            if (!IsPortOpend || string.IsNullOrEmpty(channel) || !Enum.TryParse(channel[0].ToString(), out tempChannel))
                return null;

            //发送           
            CommandBase commandReadOneChannel = CommandBase.GetReadOneChannelCommand(tempChannel);
            string receiveStr = SendCommandAndWaitReback(commandReadOneChannel);

            return receiveStr != null ? Encoding.ASCII.GetBytes(receiveStr): null;
        }

        public override byte[] ReadAllChannel()
        {
            return null;
        }

        public override bool SetDeviceIDOrAddress(byte newIDOrAddress)
        {
            return false;
        }

        public override byte ReadOneChannelBrightness(string channel)
        {
            ChannelIDs tempChannel;
            if (!IsPortOpend || string.IsNullOrEmpty(channel) || !Enum.TryParse(channel[0].ToString(), out tempChannel))
                return 0;

            //发送            
            CommandBase commandSetBrightness = CommandBase.GetReadOneChannelCommand(tempChannel);
            string receiveStr = SendCommandAndWaitReback(commandSetBrightness);

            byte brightness = 0;
            receiveStr = receiveStr?.ToUpper().Trim(tempChannel.ToString()[0]);
            byte.TryParse(receiveStr, out brightness);
                        
            return brightness;
        }

        public override bool SetOneChannelBrightness(string channel, byte brightness)
        {
            ChannelIDs tempChannel;
            if (!IsPortOpend || string.IsNullOrEmpty(channel ) || !Enum.TryParse(channel[0].ToString(), out tempChannel))
                return false;

            //发送            
            CommandBase commandSetBrightness = CommandBase.GetSetLightBrightnessCommand(tempChannel, brightness);
            string receiveStr = SendCommandAndWaitReback(commandSetBrightness);
            
            return receiveStr?.ToUpper() == channel.ToUpper();
        }

        public override bool SetAllChannelBrightness(byte brightness)
        {          
            if (!IsPortOpend)
                return false;

            string[] channels = Enum.GetNames(typeof(ChannelIDs));
            foreach (string channel in channels)
            {
                if (!SetOneChannelBrightness(channel, brightness))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 读取通道常亮/灭状态
        /// </summary>
        /// <returns></returns>
        public override string ReadChannelLightAlwaysOnStatus()
        {
            //发送            
            CommandBase command = CommandBase.GetReadOneChannelOpenOrCloseCommand();
            string receiveStr = SendCommandAndWaitReback(command);

            return receiveStr;
        }

        /// <summary>
        /// 设置通道常亮/灭状态
        /// </summary>
        /// <returns></returns>
        public override bool SetChannelLightAlwaysOnStatus(bool open)
        {
            //发送            
            CommandBase commandSetBrightness = CommandBase.GetSetOneChannelOpenOrCloseCommand(open);
            string receiveStr = SendCommandAndWaitReback(commandSetBrightness);

            string operateStr = open ? "H" : "L";
            return receiveStr?.ToUpper() == operateStr;
        }

        /// <summary>
        /// 发送命令并等待设备回复
        /// </summary>
        /// <param name="command"></param>
        /// <param name="waitTimeBeforeReceive"></param>
        /// <returns></returns>
        private string SendCommandAndWaitReback(CommandBase command)
        {
            return SendCommandAndWaitReback(new CommandBase[] { command });
        }

        /// <summary>
        /// 发送命令并等待设备回复
        /// </summary>
        /// <param name="command"></param>
        /// <param name="waitTimeBeforeReceive"></param>
        /// <returns></returns>
        private string SendCommandAndWaitReback(CommandBase[] commands)
        {
            if (commands == null)
                return null;

            //发送           
            SendPackerBase spb = new SendPackerBase();
            spb.Commands.AddRange(commands);
            string strSend = spb.AsString();                      
            return SendAndReceiveString(strSend, Encoding.ASCII);
        }
    }
}
