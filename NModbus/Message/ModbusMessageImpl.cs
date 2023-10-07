using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NModbus.Data;

namespace NModbus.Message
{
    /// <summary>
    ///     Class holding all implementation shared between two or more message types.
    ///     Interfaces expose subsets of type specific implementations.
    /// </summary>
    public class ModbusMessageImpl
    {
        // smallest supported message frame size (sans checksum)
        private const int MinimumFrameSize = 2;


        public ModbusMessageImpl()
        {
        }

        public ModbusMessageImpl(byte slaveAddress, byte functionCode)
        {
            SlaveAddress = slaveAddress;
            FunctionCode = functionCode;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte? ByteCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte? ExceptionCode { get; set; }

        /// <summary>
        /// 事务处理标识符：确认发出和受到的信息属于同一个序列，每次通信后就要加1；
        /// </summary>
        public ushort TransactionId { get; set; }

        /// <summary>
        /// 功能码
        /// </summary>

        public byte FunctionCode { get; set; }

        /// <summary>
        ///长度：用于说明此字节之后还有多少个字节的数
        /// </summary>
        public ushort? NumberOfPoints { get; set; }

        /// <summary>
        /// 设备地址，在slave中对应其ID；
        /// </summary>
        public byte SlaveAddress { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public ushort? StartAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public ushort? SubFunctionCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IModbusMessageDataCollection Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] MessageFrame
        {
            get
            {
                byte[] pdu = ProtocolDataUnit;
                MemoryStream frame = new MemoryStream(1 + pdu.Length);

                frame.WriteByte(SlaveAddress);
                frame.Write(pdu, 0, pdu.Length);

                var msg = frame.ToArray();
                return msg;
            }
        }

        /// <summary>
        /// 协议数据单元
        /// </summary>
        public byte[] ProtocolDataUnit
        {
            get
            {
                List<byte> pdu = new List<byte>();

                pdu.Add(FunctionCode);

                if (ExceptionCode.HasValue)
                {
                    pdu.Add(ExceptionCode.Value);
                }

                if (SubFunctionCode.HasValue)
                {
                    pdu.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)SubFunctionCode.Value)));
                }

                if (StartAddress.HasValue)
                {
                    //将整数值由主机字节顺序转换为网络字节顺序
                    short value = IPAddress.HostToNetworkOrder((short)StartAddress.Value);
                    byte[] address = BitConverter.GetBytes(value);
                    pdu.AddRange(address);
                }

                if (NumberOfPoints.HasValue)
                {
                    pdu.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)NumberOfPoints.Value)));
                }

                if (ByteCount.HasValue)
                {
                    pdu.Add(ByteCount.Value);
                }

                if (Data != null)
                {
                    pdu.AddRange(Data.NetworkBytes);
                }

                return pdu.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameBody"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public void Initialize(byte[] frameBody)
        {
            if (frameBody == null)
            {
                throw new ArgumentNullException(nameof(frameBody), "Argument frame cannot be null.");
            }

            if (frameBody.Length < MinimumFrameSize)
            {
                string msg = $"Message frame must contain at least {MinimumFrameSize} bytes of data.";
                throw new FormatException(msg);
            }

            SlaveAddress = frameBody[0];
            FunctionCode = frameBody[1];
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
