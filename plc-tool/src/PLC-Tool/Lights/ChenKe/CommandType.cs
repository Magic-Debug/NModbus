using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PLCTool.Lights.ChenKe
{
    public enum CommandType : byte
    {
        /// <summary>
        /// 设置设备ID(发送命令)，参数范围0~99
        /// </summary>
        SetDeviceID_Send = 0x01,
        /// <summary>
        /// 设置亮度(发送命令)，参数范围；0~255
        /// </summary>
        SetBrightNess_Send = 0x03,
        /// <summary>
        /// 打开/关闭通道(发送命令)
        /// </summary>
        OpenOrCloseChannel_Send = 0x05,
        /// <summary>
        /// 读取通道参数(发送命令)
        /// </summary>
        ReadChannelInfo_Send = 0x08,
        /// <summary>
        /// 恢复出厂设置(发送命令)
        /// </summary>
        ResetToRaw_Send = 0x0B,
        /// <summary>
        /// 命令代码不对(设备回复命令
        /// </summary>
        CodeError_DeviceReback = 0x01,
        /// <summary>
        /// 命令长度不对
        /// </summary>
        LengthError_DeviceReback = 0x2,
        /// <summary>
        /// 通道号不存在
        /// </summary>
        ChannelError_DeviceReback = 0x03,
        /// <summary>
        /// 参数不对
        /// </summary>
        ParameterError_DeviceReback = 0x04,
        /// <summary>
        /// 正确(设备回复命令)
        /// </summary>
        Right_DeviceReback = 0x06,
        /// <summary>
        /// 命令错误(设备回复命令)
        /// </summary>        
        Error_DeviceReback = 0x07   
    }
}
