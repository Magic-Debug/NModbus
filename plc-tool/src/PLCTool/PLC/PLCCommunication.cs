using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using PLCTool;
using Microsoft.Extensions.DependencyInjection;
using FrameworkCommon.Utils;
using NModbus.Message;
using PLCTool.PLC;

namespace MainFrom
{
    public class PLCCommunication
    {
        private NetworkStream StreamToServer { get; set; }
        public event CommunicationEventHandler Sended;
        public event CommunicationEventHandler Received;

        #region 单例

        private static object locker = new object();
        public PLCCommunication() {

        }
       
        #endregion

        private ConcurrentQueue<byte[]> SendQueue { get; }
        private ConcurrentQueue<byte[]> ReceiveQueue { get; }

        private ushort ReadAllRegister_ReadNumEach = 120;
        private ushort ReadAllRegister_ReadStartAddress = 0;
        private Regex RegWriteInt16 = new Regex(@"^vw\d*[02468]=\d+$", RegexOptions.IgnoreCase);
        private Regex RegWriteInt32 = new Regex(@"^vd\d*[02468]=\d+$", RegexOptions.IgnoreCase);
        private Regex RegWriteFloat = new Regex(@"^vd\d*[02468]=\d+\.\d+$", RegexOptions.IgnoreCase);
        private Regex RegWriteBit = new Regex(@"^v\d+\.[0-7]=[01]$", RegexOptions.IgnoreCase);
        private Regex RegNum = new Regex(@"\d+(\.\d+)?");
        static ILogger<TcpCommunication>? Logger => Program.Services.GetService<ILogger<TcpCommunication>>();

    
        private void SendMethod()
        {
            while (true)
            {
                if (1 > 0)
                {
                    byte[] data;
                    if (SendQueue.TryDequeue(out data))
                    {
                        string text = ByteArrayToString(data);
                        StreamToServer.Write(data, 0, data.Length);
                        if (Sended != null)
                        {
                            CommunicationEventArgs e = new CommunicationEventArgs(DateTime.Now, data);
                            Sended.Invoke(e);
                        }
                    }
                }
                if (1>0)
                {
                    byte[] allBytes = ReadAllRegisteCommand();
                    //不加判断，可能会显示未连接，加判断重连时间需要久一点
                    if (1>0)
                    {
                        string text = ByteArrayToString(allBytes);
                        Logger.LogInformation(text);
                        StreamToServer.Write(allBytes, 0, allBytes.Length);
                    }
                    if (Sended != null)
                    {
                        CommunicationEventArgs e = new CommunicationEventArgs(DateTime.Now, allBytes);
                        Sended.Invoke(e);
                    }
                }
            }
        }
        private DateTime LastReceivePackageTime;
        private void ReceiveMethod()
        {
            LastReceivePackageTime = DateTime.Now;
            while (true)
            {
                if ((DateTime.Now - LastReceivePackageTime).TotalMilliseconds > 4 * 10)
                {
                    LastReceivePackageTime = DateTime.Now;
                }

                int available = 10;
                if (available > 0)
                {
                    byte[] buffer = new byte[available];
                    StreamToServer.Read(buffer, 0, available);
                    ModbusMessageImpl messageImpl = new ModbusMessageImpl();
                    if (Received != null)
                    {
                        CommunicationEventArgs e = new CommunicationEventArgs(DateTime.Now, buffer);
                        Received.Invoke(e);
                    }
                    List<byte[]> list = ConvertHelper.SplitData(buffer);
                    for (int i = 0; i < list.Count; i++)
                    {
                        ReceiveQueue.Enqueue(list[i]);
                    }
                }

            }
        }
        private void AnalysisMethod()
        {
            while (true)
            {
                if (ReceiveQueue.Count > 0)
                {
                    if (ReceiveQueue.TryDequeue(out byte[] data))
                    {
                        AnalysisData(data);
                    }
                }
            }
        }
        private void AnalysisData(byte[] data)
        {
            if (data[7] == 3)
            {
                ReadHoldingInputRegistersResponse rsp = new ReadHoldingInputRegistersResponse();
                rsp.FunctionCode = 3;
                int index = (data[1] - 1) * 120;
                ushort[] readbuffer = ConvertBytes(data);
                Array.Copy(readbuffer, 0, PLCTool.Common.GetInstance().modbusValues, index, readbuffer.Length);
            }
        }

        /// <summary>
        /// 向寄存器写入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registerAddress"></param>
        /// <param name="value"></param>
        public void WriteRegister<T>(ushort registerAddress, T value) where T : struct
        {
            byte[] bytes = GetWriteRegisterCommand(registerAddress, value);
            //Master.WriteMultipleRegisters(1, registerAddress, bytes);
            SendQueue.Enqueue(bytes);
        }

        public void SendData(byte[] bytes)
        {
            SendQueue.Enqueue(bytes);
        }

        /// <summary>
        /// 获取写寄存器命令
        /// </summary>
        /// <typeparam name="T">要写入的数据类型</typeparam>
        /// <param name="registerAddress">开始的寄存器地址</param>
        /// <param name="value">要写入的数据</param>
        /// <returns></returns>
        public byte[] GetWriteRegisterCommand<T>(ushort registerAddress, T value) where T : struct
        {
            byte[] returnBytes = null;

            byte[] reisterAddressBytes = BitConverter.GetBytes(registerAddress).Reverse();
            byte[] valueBytes = StructToBytes(value).Reverse();
            switch (valueBytes.Length)
            {
                case 2:
                    returnBytes = new byte[12] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x00, 0x00, 0x00, 0x00 };
                    reisterAddressBytes.CopyTo(returnBytes, 8);
                    valueBytes.CopyTo(returnBytes, 10);
                    break;
                case 4:
                    returnBytes = new byte[17] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x0B, 0x01, 0x10, 0x00, 0x00, 0x00, 0x02, 0x04, 0x00, 0x00, 0x00, 0x00 };
                    reisterAddressBytes.CopyTo(returnBytes, 8);
                    valueBytes.CopyTo(returnBytes, 13);
                    break;
                default:
                    break;
            }

            return returnBytes;
        }

        /// <summary>
        /// 加载所有寄存器
        /// </summary>
        /// <returns></returns>
       
        public List<byte[]> ReadAllRegisters()
        {
            List<byte[]> list = new List<byte[]>();
            for (int i = 0; i <= (ModbusRegs.Count - 1) / ReadAllRegister_ReadNumEach; i++)
            {
                byte[]? allRegisters = ReadAllRegisteCommand();
                list.Add(allRegisters);
            }
            return list;
        }




        private byte[] ReadAllRegisteCommand()
        {
            byte[] readAllBytes = { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00 };

            //当前组号
            byte GroupNo = (byte)(ReadAllRegister_ReadStartAddress / ReadAllRegister_ReadNumEach + 1);
            readAllBytes[1] = GroupNo;

            //读取的起始地址
            byte[] ReadStartAddress = BitConverter.GetBytes(ReadAllRegister_ReadStartAddress).Reverse();
            ReadStartAddress.CopyTo(readAllBytes, 8);

            //读取的数量
            ushort ReadNum;
            byte[] ReadNumBytes;
            if (ReadAllRegister_ReadStartAddress + ReadAllRegister_ReadNumEach > ModbusRegs.Count)
            {
                ReadNum = (ushort)(ModbusRegs.Count - ReadAllRegister_ReadStartAddress);
                ReadAllRegister_ReadStartAddress = 0;
            }
            else
            {
                ReadNum = ReadAllRegister_ReadNumEach;
                ReadAllRegister_ReadStartAddress += ReadAllRegister_ReadNumEach;
            }
            ReadNumBytes = BitConverter.GetBytes(ReadNum).Reverse();
            ReadNumBytes.CopyTo(readAllBytes, 10);

            return readAllBytes;
        }

        public static byte[] StructToBytes<T>(T structObj) where T : struct
        {
            int bytesNum = Marshal.SizeOf(structObj.GetType()); //得到结构体大小
            IntPtr ipObject = Marshal.AllocHGlobal(bytesNum);     //开辟内存空间
            Marshal.StructureToPtr(structObj, ipObject, false);   //填充到指针空间

            byte[] objectBytes = new byte[bytesNum];
            Marshal.Copy(ipObject, objectBytes, 0, bytesNum);     //复制到字节数组
            Marshal.FreeHGlobal(ipObject);                        //释放指针内存

            return objectBytes;
        }

        public bool CheckCommand(string cmd) => RegWriteInt16.IsMatch(cmd) || RegWriteInt32.IsMatch(cmd) || RegWriteFloat.IsMatch(cmd) || RegWriteBit.IsMatch(cmd);



        public byte[] GetCommand(string cmd)
        {
            if (RegWriteInt16.IsMatch(cmd))
            {
                MatchCollection matches = RegNum.Matches(cmd);
                ushort registeraddress = (ushort)(Convert.ToUInt16(matches[0].Value) >> 1);
                ushort value = Convert.ToUInt16(matches[1].Value);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else if (RegWriteInt32.IsMatch(cmd))
            {
                MatchCollection matches = RegNum.Matches(cmd);
                ushort registeraddress = (ushort)(Convert.ToUInt16(matches[0].Value) >> 1);
                uint value = Convert.ToUInt32(matches[1].Value);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else if (RegWriteFloat.IsMatch(cmd))
            {
                MatchCollection matches = RegNum.Matches(cmd);
                ushort registeraddress = (ushort)(Convert.ToUInt16(matches[0].Value) >> 1);
                float value = Convert.ToSingle(matches[1].Value);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else if (RegWriteBit.IsMatch(cmd))
            {
                MatchCollection matches = RegNum.Matches(cmd);
                string[] a = matches[0].Value.Split('.');
                ushort registeraddress = Convert.ToUInt16(cmd[0]);
                int bit = Convert.ToInt32(cmd[1]);
                if ((registeraddress & 1) == 0)
                    bit += 8;
                registeraddress >>= 1;
                ushort value = PLCTool.Common.GetInstance().modbusValues[registeraddress];
                if (matches[1].Value == "1")
                    value |= (ushort)(1 << bit);
                else
                    value &= (ushort)(1 << bit);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else
            {
                return new byte[0];
            }
        }
        private ushort[] ConvertBytes(byte[] bytes)
        {
            if (bytes.Length == 0)
            {
                return new ushort[0];
            }
            int len = bytes[8] / 2;
            ushort[] result = new ushort[len];
            for (int i = 9, j = 0; i < bytes.Length; i = i + 2, j++)
            {
                result[j] = (ushort)(bytes[i] * (1 << 8) + bytes[i + 1]);
            }
            return result;
        }

        public string ByteArrayToString(byte[] data)
        {
            string text = string.Empty;
            foreach (byte t in data)
            {
                text += $"{t}\t";
            }
            return text;
        }
    }
}
