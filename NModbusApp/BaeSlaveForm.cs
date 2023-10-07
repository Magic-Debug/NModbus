using CommunityToolkit.HighPerformance.Buffers;
using NModbus;
using System.Net;
using System.Net.Sockets;

namespace NModbusApp
{
    /// <summary>
    ///Modbus从站不会主动发出指令
    ///主站是唯一的,从站可以有多个，它时刻监视总线的报文 ，根据报文响应主站的命令；从站不能主动发布查询命令，只能响应主站的请求
    /// </summary>
    public partial class BaeSlaveForm : Form
    {
        const int PORT = 502;

        public ModbusFactory Factory { get; }

        public StringPool CharPool => new StringPool(1024);

        public TcpListener SlaveTcpListener = new TcpListener(IPAddress.Any, PORT);

        public IModbusSlaveNetwork SlaveNetwork => Factory.CreateSlaveNetwork(SlaveTcpListener);

        public ModbusStatus modbusStatus { get; set; }

        public ushort[] modbusValues = new ushort[256];

        public IModbusSlave Slave1 => Factory.CreateSlave(1);
        IModbusSlave Slave2 => Factory.CreateSlave(2);
        IModbusSlave Slave3 => Factory.CreateSlave(3);

        public BaeSlaveForm()
        {
            InitializeComponent();
            Factory = new ModbusFactory(null, true, new JsonModbusLogger(LoggingLevel.Trace));
            modbusStatus = new ModbusStatus(Slave1.DataStore.HoldingRegisters.Points);
            modbusValues = Slave1.DataStore.HoldingRegisters.Points;
            StringPool? tt = StringPool.Shared;
            string chars = String.Join(",", Environment.GetCommandLineArgs());
            CharPool.Add(chars);
        }

        private void FrmTcpSlave_Load(object sender, EventArgs e)
        {
            Span<int> numbers = new[] { 1, 2, 3, 4, 5, 6, 7 };
        }

        /// <summary>
        ///     Simple Modbus TCP slave example.
        /// </summary>
        public void StartSlave()
        {
            SlaveTcpListener.Start();
            using (SpanOwner<byte> buffer = SpanOwner<byte>.Allocate(1024))
            {

            }
            SlaveNetwork.AddSlave(Slave1);
            SlaveNetwork.AddSlave(Slave2);
            SlaveNetwork.AddSlave(Slave3);

            SlaveNetwork.ListenAsync().GetAwaiter().GetResult();

            // prevent the main thread from exiting
            Thread.Sleep(Timeout.Infinite);
        }

        private void btnStartSlave_Click(object sender, EventArgs e)
        {
            StartSlave();
        }
    }
}
