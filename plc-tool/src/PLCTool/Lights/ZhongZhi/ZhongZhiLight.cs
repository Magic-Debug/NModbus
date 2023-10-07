using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.ZhongZhi
{
    /// <summary>
    /// 众智光源
    /// </summary>
    public class ZhongZhiLight : LightBase
    {
        //通道数量
        int channelCount = 1;

        public ZhongZhiLight(string portName, int baudRate, byte deviceIDOrAddress = 0, int delayTimeForReceive = 20) : base(portName, baudRate, deviceIDOrAddress, delayTimeForReceive)
        {
        }

        /// <summary>
        /// 测试连接设备(检查串口和通讯协议是否正确)
        /// </summary>
        /// <param name="finallyClosePort">最后是否关闭串口</param>
        /// <returns></returns>
        public override bool TestConnect(bool finallyClosePort = true)
        {
            return false;
        }

        public override string[] ReadChannelList()
        {
            return new string[0];
        }

        public override byte ReadOneChannelBrightness(string channel)
        {
            return 0;
        }

        public override byte[] ReadOneChannel(string channel)
        {
            return null;
        }

        public override byte[] ReadAllChannel()
        {
            return null;
        }

        public override bool SetDeviceIDOrAddress(byte newIDOrAddress)
        {
            return false;
        }

        public override bool SetOneChannelBrightness(string channel, byte brightness)
        {
            return false;
        }

        public override bool SetAllChannelBrightness(byte brightness)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd">0灭 1亮</param>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string BuildCmd(byte cmd, byte channel, int data)
        {
            string state = cmd == 1 ? "T" : "F"; // T：亮  F：灭  ex: S100T128FC# 1通道亮,亮度100,通道2灭，亮度128实际为0

            StringBuilder sb = new StringBuilder("S");
            for(int i = 0; i < channelCount; i++)
            {
                sb.Append($"{data.ToString("000")}{state}");
            }
            sb.Append("C#");

            return sb.ToString();
        }
    }
}
