using NModbus;
using NModbus.Logging;

namespace NModbusApp
{
    internal class JsonModbusLogger : ModbusLogger
    {
        public JsonModbusLogger(LoggingLevel minimumLoggingLevel) : base(minimumLoggingLevel)
        {
        }
        protected override void LogCore(LoggingLevel level, string message)
        {
            File.AppendAllText("modbus.log", $"{message}{Environment.NewLine}");
        }
    }
}
