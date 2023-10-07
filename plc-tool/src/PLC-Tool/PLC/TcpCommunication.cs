using FrameworkCommon;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace MainFrom
{
    public class TcpCommunication : SignalCommunication
    {
        private TcpClient tcpClient;
        private int port;
        IPAddress ipaddress;
        private NetworkStream StreamToServer { get; set; }
        public event CommunicationEventHandler Sended;
        public event CommunicationEventHandler Received;
        public int ScanRate { get; set; } = 50;

        #region 单例
        private static TcpCommunication Instance;
        private static object locker = new object();
        private TcpCommunication() { }
        public static TcpCommunication GetInstance()
        {
            lock (locker)
            {
                if (Instance == null)
                    Instance = new TcpCommunication();
            }
            return Instance;
        }
        #endregion

        private Thread SendThread;
        private Thread ReceiveThread;
        private Thread AnalysisThread;
        private Thread ReConnectThread;// 断线重连
        private ConcurrentQueue<byte[]> SendQueue;
        private ConcurrentQueue<byte[]> ReceiveQueue;
        private bool IsOvetTime = false;


        private ushort ReadAllRegister_ReadNumEach = 120;
        private ushort ReadAllRegister_ReadStartAddress = 0;
        private Regex RegWriteInt16 = new Regex(@"^vw\d*[02468]=\d+$", RegexOptions.IgnoreCase);
        private Regex RegWriteInt32 = new Regex(@"^vd\d*[02468]=\d+$", RegexOptions.IgnoreCase);
        private Regex RegWriteFloat = new Regex(@"^vd\d*[02468]=\d+\.\d+$", RegexOptions.IgnoreCase);
        private Regex RegWriteBit = new Regex(@"^v\d+\.[0-7]=[01]$", RegexOptions.IgnoreCase);
        private Regex RegNum = new Regex(@"\d+(\.\d+)?");

        public override bool Connect()
        {
            try
            {
                if (!IsConnected)
                {
                    tcpClient.Connect(ipaddress, port);
                    StreamToServer = tcpClient.GetStream();
                    IsConnected = true;
                    if (SendThread.ThreadState == ThreadState.Unstarted)
                    {
                        SendThread.Start();
                    }
                    if (ReceiveThread.ThreadState == ThreadState.Unstarted)
                    {
                        ReceiveThread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                IsConnected = false;
                LogHelper.Default.Error(ex);
            }
            return IsConnected;
        }
        public override void Disconnect()
        {
            try
            {
                IsClosing = true;
                if (IsConnected)
                {
                    StreamToServer.Close();
                    StreamToServer.Dispose();
                    tcpClient.Close();
                }
                if (SendThread.ThreadState != ThreadState.Unstarted)
                {
                    SendThread.Abort();
                }
                if (ReceiveThread.ThreadState != ThreadState.Unstarted)
                {
                    ReceiveThread.Abort();
                }
                if (ReConnectThread.ThreadState != ThreadState.Unstarted)
                {
                    ReConnectThread.Abort();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error(ex);
            }
        }
        public override void Init()
        {
            IsClosing = false;
            string host = PLCTool.Common.GetInstance().IP;
            ipaddress = IPAddress.Parse(host);
            port = PLCTool.Common.GetInstance().Port;
            ScanRate = PLCTool.Common.GetInstance().ScanRate;
            tcpClient = new TcpClient();
            SendQueue = new ConcurrentQueue<byte[]>();
            ReceiveQueue = new ConcurrentQueue<byte[]>();
            IsCanRunning = true;
            SendThread = new Thread(SendMethod);
            ReceiveThread = new Thread(ReceiveMethod);
            AnalysisThread = new Thread(AnalysisMethod);
            ReConnectThread = new Thread(ReConnect);
            Task.Run(() =>
            {
                Connect();
                AnalysisThread.Start();
                ReConnectThread.Start();
            });
        }

        private void SendMethod()
        {
            while (true)
            {
                try
                {
                    if (IsOvetTime)
                    {
                        Thread.Sleep(ScanRate * 2);
                        IsOvetTime = false;
                        continue;
                    }
                    if (SendQueue.Count > 0)
                    {
                        byte[] data;
                        if (SendQueue.TryDequeue(out data))
                        {
                            StreamToServer.Write(data, 0, data.Length);
                            if (Sended != null)
                            {
                                CommunicationEventArgs e = new CommunicationEventArgs { Time = DateTime.Now, Data = data };
                                Sended.BeginInvoke(e, null, null);
                            }
                            Thread.Sleep(ScanRate);
                        }
                    }
                    if (IsCanRunning)
                    {
                        byte[] ReadAllBytes = ReadAllRegisteCommand();
                        //不加判断，可能会显示未连接，加判断重连时间需要久一点
                        if(tcpClient.Connected)
                        {
                            StreamToServer.Write(ReadAllBytes, 0, ReadAllBytes.Length);
                        }
                        if (Sended != null)
                        {
                            CommunicationEventArgs e = new CommunicationEventArgs { Time = DateTime.Now, Data = ReadAllBytes };
                            Sended.BeginInvoke(e, null, null);
                        }
                        Thread.Sleep(ScanRate);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ThreadAbortException))
                    {
                        IsConnected = false;
                        LogHelper.Default.Error(e);
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
                if (!IsConnected)
                {
                    return;
                }
                if ((DateTime.Now - LastReceivePackageTime).TotalMilliseconds > 4 * ScanRate)
                {
                    IsOvetTime = true;
                    LastReceivePackageTime = DateTime.Now;
                    //LogHelper.Default.Debug("接收超时");
                }
                try
                {
                    int available = tcpClient.Available;
                    if (available > 0)
                    {
                        byte[] buffer = new byte[available];
                        StreamToServer.Read(buffer, 0, available);
                        if (Received != null)
                        {
                            CommunicationEventArgs e = new CommunicationEventArgs { Time = DateTime.Now, Data = buffer };
                            Received.BeginInvoke(e, null, null);
                        }
                        List<byte[]> list = ConvertHelper.SplitData(buffer);
                        for (int i = 0; i < list.Count; i++)
                        {
                            ReceiveQueue.Enqueue(list[i]);
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Default.Error(e);
                }
            }
        }
        private void AnalysisMethod()
        {
            while (true)
            {
                try
                {
                    if (ReceiveQueue.Count > 0)
                    {
                        byte[] data;
                        if (ReceiveQueue.TryDequeue(out data))
                        {
                            AnalysisData(data);
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Default.Error(e);
                }
            }
        }
        private void AnalysisData(byte[] data)
        {
            if (data[7] == 3)
            {
                int index = (data[1] - 1) * 120;
                var readbuffer = CongertBytes(data);
                Array.Copy(readbuffer, 0, PLCTool.Common.GetInstance().modbusValues, index, readbuffer.Length);
            }
        }

        public void WriteRegister<T>(ushort registeraddress, T value) where T : struct
        {
            byte[] bytes = GetWriteRegisterCommand(registeraddress, value);
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
        /// <param name="registeraddress">开始的寄存器地址</param>
        /// <param name="value">要写入的数据</param>
        /// <returns></returns>
        public byte[] GetWriteRegisterCommand<T>(ushort registeraddress, T value) where T : struct
        {
            byte[] returnBytes = null;

            byte[] reisterAddressBytes = BitConverter.GetBytes(registeraddress).Reverse();
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

        public void ReadAllRegister()
        {
            List<byte[]> list = ReadAllRegisteCommandList();
            for (int i = 0; i < list.Count; i++)
            {
                SendQueue.Enqueue(list[i]);
            }
        }
        public List<byte[]> ReadAllRegisteCommandList()
        {
            List<byte[]> list = new List<byte[]>();
            for (int i = 0; i <= (ModbusRegs.Count - 1) / ReadAllRegister_ReadNumEach; i++)
            {
                list.Add(ReadAllRegisteCommand());
            }
            return list;
        }

        private byte[] ReadAllRegisteCommand()
        {
            byte[] ReadAllBytes = { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00 };

            //当前组号
            byte GroupNo = (byte)(ReadAllRegister_ReadStartAddress / ReadAllRegister_ReadNumEach + 1);
            ReadAllBytes[1] = GroupNo;

            //读取的起始地址
            byte[] ReadStartAddress = BitConverter.GetBytes(ReadAllRegister_ReadStartAddress).Reverse();
            ReadStartAddress.CopyTo(ReadAllBytes, 8);

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
            ReadNumBytes.CopyTo(ReadAllBytes, 10);

            return ReadAllBytes;
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

        public bool CheckCommand(string s)
        {
            return RegWriteInt16.IsMatch(s) || RegWriteInt32.IsMatch(s) || RegWriteFloat.IsMatch(s) || RegWriteBit.IsMatch(s);
        }
        public byte[] GetCommand(string s)
        {
            if (RegWriteInt16.IsMatch(s))
            {
                MatchCollection matches = RegNum.Matches(s);
                ushort registeraddress = (ushort)(Convert.ToUInt16(matches[0].Value) >> 1);
                ushort value = Convert.ToUInt16(matches[1].Value);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else if (RegWriteInt32.IsMatch(s))
            {
                MatchCollection matches = RegNum.Matches(s);
                ushort registeraddress = (ushort)(Convert.ToUInt16(matches[0].Value) >> 1);
                uint value = Convert.ToUInt32(matches[1].Value);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else if (RegWriteFloat.IsMatch(s))
            {
                MatchCollection matches = RegNum.Matches(s);
                ushort registeraddress = (ushort)(Convert.ToUInt16(matches[0].Value) >> 1);
                float value = Convert.ToSingle(matches[1].Value);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else if (RegWriteBit.IsMatch(s))
            {
                MatchCollection matches = RegNum.Matches(s);
                string[] a = matches[0].Value.Split('.');
                ushort registeraddress = Convert.ToUInt16(s[0]);
                int bit = Convert.ToInt32(s[1]);
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

        private void ReConnect()
        {
            while (true)
            {
                if (!IsConnected && !IsClosing)
                {
                    tcpClient = new TcpClient();
                    if (!Connect())
                    {
                        Thread.Sleep(3000);
                    }
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }
        private ushort[] CongertBytes(byte[] bytes)
        {
            try
            {
                if (bytes.Length > 0)
                {
                    int len = bytes[8] / 2;
                    ushort[] result = new ushort[len];
                    for (int i = 9, j = 0; i < bytes.Length; i = i + 2, j++)
                    {
                        result[j] = (ushort)(bytes[i] * (1 << 8) + bytes[i + 1]);
                    }
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error($"bytes.Length:{bytes.Length}", ex);
                return null;
            }
        }
    }

    #region 事件定义
    public delegate void CommunicationEventHandler(CommunicationEventArgs e);
    public class CommunicationEventArgs : EventArgs
    {
        public DateTime Time { get; set; }
        public byte[] Data { get; set; }
    }
    #endregion
}
