using NModbus;

namespace NModbusApp
{
    public class HmiBufferRequestmessage : IModbusMessage
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
