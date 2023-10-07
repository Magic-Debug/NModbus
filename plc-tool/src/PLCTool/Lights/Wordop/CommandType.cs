using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Lights.Wordop
{
    public enum CommandType : byte
    {
        /// <summary>
        /// 设置设备ID(发送命令)，参数范围0~99
        /// </summary>
        SetDeviceID_Send = 0x09,
        /// <summary>
        /// 设置亮度(发送命令)，参数范围；0~255
        /// </summary>
        SetBrightNess_Send = 0x1A,
        /// <summary>
        /// 读取通道参数(发送命令)
        /// </summary>
        ReadChannelInfo_Send = 0x31,
        /// <summary>
        /// 单个通道的信息(设备回复命令)
        /// </summary>
        OneChannelInfo_DeviceReback = 0x5A,
        /// <summary>
        /// 所有通道的信息(设备回复命令)
        /// </summary>
        AllChannelInfo_DeviceReback = 0x60,
        /// <summary>
        /// 正确(设备回复命令)
        /// </summary>
        Right_DeviceReback = 0x00,
        /// <summary>
        /// 错误(设备回复命令)
        /// </summary>
        Error_DeviceReback = 0x01
    }
}
