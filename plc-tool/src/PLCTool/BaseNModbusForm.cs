using NModbus;
using NModbus.Data;
using NModbus.Extensions;
using NModbus.Logging;
using NModbus.UnitTests.Message;
using System.Net.Sockets;

namespace PLCTool
{
    public partial class BaseNModbusForm : Form
    {
        public string Host => "192.168.0.9";
        public int Port => 502;


        protected ModbusFactory Factory { get; set; }

        protected IModbusMaster Master { get; set; }

        protected ModbusMasterEnhanced ModbusEnhanced { get; set; }

        protected TcpClient TcpClient { get; set; }

        public ModbusStatus modbusStatus { get; set; }

        public ushort[] modbusValues = new ushort[256];


        public BaseNModbusForm()
        {
            InitializeComponent();
            modbusStatus = new ModbusStatus();
        }

        public void Init()
        {
            TcpClient = new TcpClient(Host, Port);
            Factory = new ModbusFactory(null, true, NullModbusLogger.Instance);
            Master = Factory.CreateMaster(TcpClient);
            ModbusEnhanced = new ModbusMasterEnhanced(Master);
            RegisterCollection col = MessageUtility.CreateDefaultCollection<RegisterCollection, ushort>(3, 5);
        }
    }
}
