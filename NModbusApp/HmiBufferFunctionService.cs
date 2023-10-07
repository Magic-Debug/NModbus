using NModbus;

namespace NModbusApp
{
    public class HmiBufferFunctionService : IModbusFunctionService
    {
        public byte FunctionCode => 45;

        public IModbusMessage CreateRequest(byte[] frame)
        {
            Console.WriteLine($"HMI Buffer Message Receieved - {frame.Length} bytes");

            var request = new HmiBufferRequestmessage();

            request.Initialize(frame);

            return request;
        }

        public IModbusMessage HandleSlaveRequest(IModbusMessage request, ISlaveDataStore dataStore)
        {
            Console.WriteLine("HMI Buffer Message Receieved");

            throw new NotImplementedException();
        }

        public int GetRtuRequestBytesToRead(byte[] frameStart)
        {
            byte registerCountMSB = frameStart[4];
            byte registerCountLSB = frameStart[5];

            int numberOfRegisters = (registerCountMSB << 8) + registerCountLSB;

            Console.WriteLine($"Got Hmi Buffer Request for {numberOfRegisters} registers.");

            return (numberOfRegisters * 2) + 1;
        }

        public int GetRtuResponseBytesToRead(byte[] frameStart)
        {
            return 4;
        }
    }

}
