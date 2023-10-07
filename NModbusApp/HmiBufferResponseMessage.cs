using NModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NModbusApp
{
    public class HmiBufferResponseMessage : IModbusMessage
    {
        public byte FunctionCode { get; set; }

        public byte SlaveAddress { get; set; }

        public byte[] MessageFrame { get; private set; }

        public byte[] ProtocolDataUnit { get; private set; }

        public ushort TransactionId { get; set; }

        public void Initialize(byte[] frame)
        {
            SlaveAddress = frame[0];
            FunctionCode = frame[1];

            MessageFrame = frame
                .Take(frame.Length - 2)
                .ToArray();

            ProtocolDataUnit = frame
                .Skip(1)
                .ToArray();
        }
    }

}
