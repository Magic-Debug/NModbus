using System.IO.Ports;
using NModbus;
using NModbus.Serial;

namespace NModbusApp
{
    public partial class FrmSerialAscii : BaseForm
    {

        private const string PrimarySerialPortName = "COM4";
        private const string SecondarySerialPortName = "COM2";


        public FrmSerialAscii()
        {
            InitializeComponent();
        }

        private static async Task<int> Demos(string[] args)
        {
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) => cts.Cancel();

            try
            {
                ModbusSerialAsciiMasterReadRegisters();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            return 0;
        }


        /// <summary>
        ///     Simple Modbus serial ASCII master read holding registers example.
        /// </summary>
        public static void ModbusSerialAsciiMasterReadRegisters()
        {
            using (SerialPort port = new SerialPort(PrimarySerialPortName))
            {
                // configure serial port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();

                var factory = new ModbusFactory();
                IModbusSerialMaster master = factory.CreateAsciiMaster(port);

                byte slaveId = 1;
                ushort startAddress = 1;
                ushort numRegisters = 5;

                // read five registers		
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

                for (int i = 0; i < numRegisters; i++)
                {
                    Console.WriteLine($"Register {startAddress + i}={registers[i]}");
                }
            }

            // output: 
            // Register 1=0
            // Register 2=0
            // Register 3=0
            // Register 4=0
            // Register 5=0
        }



        /// <summary>
        ///     Simple Modbus serial ASCII slave example.
        /// </summary>
        public static void StartModbusSerialAsciiSlave()
        {
            using (SerialPort slavePort = new SerialPort(PrimarySerialPortName))
            {
                // configure serial port
                slavePort.BaudRate = 9600;
                slavePort.DataBits = 8;
                slavePort.Parity = Parity.None;
                slavePort.StopBits = StopBits.One;
                slavePort.Open();

                var factory = new ModbusFactory();
                IModbusSlaveNetwork slaveNetwork = factory.CreateAsciiSlaveNetwork(slavePort);

                IModbusSlave slave1 = factory.CreateSlave(1);
                IModbusSlave slave2 = factory.CreateSlave(2);

                slaveNetwork.AddSlave(slave1);
                slaveNetwork.AddSlave(slave2);

                slaveNetwork.ListenAsync().GetAwaiter().GetResult();
            }
        }



        /// <summary>
        ///     Modbus serial ASCII master and slave example.
        /// </summary>
        public static void ModbusSerialAsciiMasterReadRegistersFromModbusSlave()
        {
            using (SerialPort masterPort = new SerialPort(PrimarySerialPortName))
            using (SerialPort slavePort = new SerialPort(SecondarySerialPortName))
            {
                // configure serial ports
                masterPort.BaudRate = slavePort.BaudRate = 9600;
                masterPort.DataBits = slavePort.DataBits = 8;
                masterPort.Parity = slavePort.Parity = Parity.None;
                masterPort.StopBits = slavePort.StopBits = StopBits.One;
                masterPort.Open();
                slavePort.Open();

                // create modbus slave on seperate thread
                byte slaveId = 1;
                var factory = new ModbusFactory();
                var transport = factory.CreateAsciiTransport(slavePort);
                var network = factory.CreateSlaveNetwork(transport);
                var slave = factory.CreateSlave(slaveId);
                network.AddSlave(slave);

                var listenTask = network.ListenAsync();

                var masterTransport = factory.CreateAsciiTransport(masterPort);
                IModbusSerialMaster master = factory.CreateMaster(masterTransport);

                master.Transport.Retries = 5;
                ushort startAddress = 100;
                ushort numRegisters = 5;
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

                for (int i = 0; i < numRegisters; i++)
                    Console.WriteLine($"Register {(startAddress + i)}={registers[i]}");
            }

            // output
            // Register 100=0
            // Register 101=0
            // Register 102=0
            // Register 103=0
            // Register 104=0
        }

    }
}
