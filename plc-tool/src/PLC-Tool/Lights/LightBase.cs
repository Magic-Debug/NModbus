using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameworkCommon;

namespace PLCTool.Lights
{
    public abstract class LightBase
    {
        public LightBase(string portName, int baudRate, byte deviceIDOrAddress = 0, int delayTimeForReceive = 20)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            this.DeviceIDOrAddress = deviceIDOrAddress;
            this.DelayTimeForReceive = delayTimeForReceive;

            serialPort = new SerialPort(PortName, BaudRate);            
        }
        ~LightBase()
        {
            serialPort?.Close();
            serialPort?.Dispose();
        }

        #region 属性

        /// <summary>
        /// 设备ID或设备地址
        /// </summary>
        public byte DeviceIDOrAddress;

        /// <summary>
        /// 串口是否已打开
        /// </summary>
        public bool IsPortOpend { get; set; } = false;

        /// <summary>
        /// 串口名称
        /// </summary>
        public string PortName { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// 接收数据前等待的时间
        /// </summary>
        public int DelayTimeForReceive { get; set; }

        public SerialPort serialPort { get; set; }

        #endregion

        #region 方法
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public virtual bool OpenPort()
        {
            try
            {                
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
                IsPortOpend = serialPort.IsOpen;
            }
            catch (Exception e)
            {
                LogHelper.Default.Error($"串口[{PortName}]打开失败", e);
                ErrorMsgShow(e.Message);
            }
            string resultMsg = IsPortOpend ? "成功" : "失败";
            LogHelper.Default.Info($"串口[{PortName}]打开{resultMsg}!");
            return IsPortOpend;
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public virtual bool ClosePort()
        {
            try
            {
                serialPort?.DiscardInBuffer();
                serialPort?.DiscardOutBuffer();
                serialPort?.Close();
                LogHelper.Default.Info($"串口[{PortName}]关闭成功");
                return true;
            }
            catch (Exception e)
            {
                LogHelper.Default.Error($"串口[{PortName}]关闭失败", e);
                ErrorMsgShow(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 测试连接设备(检查串口和通讯协议是否正确)
        /// </summary>
        /// <param name="finallyClosePort">最后是否关闭串口</param>
        /// <returns></returns>
        public abstract bool TestConnect(bool finallyClosePort = true);

        /// <summary>
        /// 读取通道列表
        /// </summary>
        public abstract string[] ReadChannelList();

        /// <summary>
        /// 读取通道参数
        /// </summary>
        public abstract byte[] ReadOneChannel(string channel);

        /// <summary>
        /// 读取所有通道参数
        /// </summary>
        public abstract byte[] ReadAllChannel();

        /// <summary>
        /// 设置设备ID或地址
        /// </summary>
        public abstract bool SetDeviceIDOrAddress(byte newIDOrAddress);

        /// <summary>
        /// 读取单个通道亮度
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public abstract byte ReadOneChannelBrightness(string channel);

        /// <summary>
        /// 设置单个通道亮度
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public abstract bool SetOneChannelBrightness(string channel, byte brightness);

        /// <summary>
        /// 设置所有通道亮度
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public abstract bool SetAllChannelBrightness(byte brightness);

        /// <summary>
        /// 读取通道常亮/灭状态
        /// </summary>
        /// <returns></returns>
        public virtual string ReadChannelLightAlwaysOnStatus()
        {
            return "";
        }

        /// <summary>
        /// 设置通道常亮/灭状态
        /// </summary>
        /// <returns></returns>
        public virtual bool SetChannelLightAlwaysOnStatus(bool open)
        {
            return true;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">待发送的数据</param>
        /// <param name="clearInBuffer">是否清空接收缓冲区数据</param>
        /// <param name="clearOutBuffer">是否清空发送缓冲区数据</param>
        /// <returns></returns>
        public virtual string SendBytes(byte[] data, Encoding encoding = null)
        {
            try
            {
                if (!IsPortOpend)                
                    return "串口未打开";                

                //先删除之前的缓冲区
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();                
                serialPort.Write(data, 0, data.Length);
                AfterSentDataEvent?.Invoke(data, encoding);//调用发送数据后事件

                return "";               
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error("串口数据发送失败", ex);
                return ex.Message;                
            }
        }        

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns></returns>
        public byte[] ReadBytes(Encoding encoding = null)
        {
            if (!IsPortOpend)
                return null;

            byte[] readBuffer = new byte[serialPort.BytesToRead];
            serialPort.Read(readBuffer, 0, readBuffer.Length);
            if(readBuffer.Length > 0) AfterReceiveDataEvent?.Invoke(readBuffer, encoding);//调用接收到数据后事件
            return readBuffer;
        }

        /// <summary>
        /// 发送数据并接受数据
        /// </summary>
        /// <param name="data">待发送的数据</param>
        /// <param name="clearInBuffer">是否清空接收缓冲区数据</param>
        /// <param name="clearOutBuffer">是否清空发送缓冲区数据</param>
        /// <returns></returns>
        public virtual byte[] SendAndReceiveBytes(byte[] data, Encoding encoding = null)
        {
            try
            {
                if (!IsPortOpend)
                    return null;

                //先删除之前的缓冲区
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
                serialPort.Write(data, 0, data.Length);
                AfterSentDataEvent?.Invoke(data, encoding);//调用发送数据后事件

                //接收
                Thread.Sleep(DelayTimeForReceive);
                byte[] receiveBytes = ReadBytes();

                return receiveBytes;
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error("串口数据发送失败", ex);
                return null;
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">待发送的数据</param>
        /// <param name="clearInBuffer">是否清空接收缓冲区数据</param>
        /// <param name="clearOutBuffer">是否清空发送缓冲区数据</param>
        /// <returns></returns>
        public virtual string SendString(string data, Encoding encoding)
        {
            try
            {
                if (!IsPortOpend)
                    return "串口未打开";

                //先删除之前的缓冲区
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
                serialPort.WriteLine(data);
                AfterSentDataEvent?.Invoke(encoding.GetBytes(data), encoding);//调用发送数据后事件

                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error("串口数据发送失败", ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns></returns>
        public string ReadString(Encoding encoding)
        {
            if (!IsPortOpend)
                return null;

            string receiveString = serialPort.ReadExisting();
            if (!string.IsNullOrEmpty(receiveString)) AfterReceiveDataEvent?.Invoke(encoding.GetBytes(receiveString), encoding);//调用接收到数据后事件

            return receiveString;
        }

        /// <summary>
        /// 发送数据并接受数据
        /// </summary>
        /// <param name="data">待发送的数据</param>
        /// <param name="clearInBuffer">是否清空接收缓冲区数据</param>
        /// <param name="clearOutBuffer">是否清空发送缓冲区数据</param>
        /// <returns></returns>
        public virtual string SendAndReceiveString(string data, Encoding encoding)
        {
            try
            {
                if (!IsPortOpend)
                    return null;

                //先删除之前的缓冲区
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
                serialPort.WriteLine(data);
                AfterSentDataEvent?.Invoke(encoding.GetBytes(data), encoding);//调用发送数据后事件

                //接收
                Thread.Sleep(DelayTimeForReceive); 
                string receiveString = serialPort.ReadExisting();
                if(!string.IsNullOrEmpty(receiveString)) AfterReceiveDataEvent?.Invoke(encoding.GetBytes(receiveString), encoding);//调用接收到数据后事件

                return receiveString;
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error("串口数据发送失败", ex);
                return null;
            }
        }

        public void ErrorMsgShow(string msg)
        {
            ErrorMsgEvent?.Invoke(msg);
        }

        /// <summary>
        /// 清除事件绑定的方法
        /// </summary>
        public void ClearEvent()
        {
            ErrorMsgEvent = null;
            LoadCurrentBrightnessEvent = null;
        }

        #endregion

        #region 事件

        public event Action<string> ErrorMsgEvent;
        protected virtual void OnErrorMsgEvent(string value)
        {
            ErrorMsgEvent?.Invoke(value);
        }

        public event Action<int> LoadCurrentBrightnessEvent;
        protected virtual void OnLoadCurrentBrightnessEvent(int value)
        {
            LoadCurrentBrightnessEvent?.Invoke(value);
        }

        /// <summary>
        /// 在成功发送数据后事件
        /// </summary>
        public event Action<byte[], Encoding> AfterSentDataEvent;        

        /// <summary>
        /// 在接收数据后事件
        /// </summary>
        public event Action<byte[], Encoding> AfterReceiveDataEvent;

        #endregion
    }

    /// <summary>
    /// 光源供应商
    /// </summary>
    public enum LightSupplier
    {
        WORDOP,
        辰科,
        众智,
        康视达
    }
}
