using NModbus;
using NModbus.Logging;
using NModbus.Serial;
using System.IO.Ports;

namespace NModbusApp
{
    public partial class FrmSerialRtu : BaseForm
    {
        private const string PrimarySerialPortName = "COM4";
        private const string SecondarySerialPortName = "COM2";

        public FrmSerialRtu()
        {
            InitializeComponent();
        }

        private static async Task<int> Demos(string[] args)
        {
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) => cts.Cancel();

            try
            {
                await Task.Run(() => { });
                ModbusSerialRtuMasterWriteRegisters();
                await StartModbusSerialRtuSlaveNetwork(cts.Token);
                await StartModbusSerialRtuSlaveWithCustomMessage(cts.Token);
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
        ///     Simple Modbus serial RTU master write holding registers example.
        /// </summary>
        public static void ModbusSerialRtuMasterWriteRegisters()
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
                IModbusMaster master = factory.CreateRtuMaster(port);

                byte slaveId = 1;
                ushort startAddress = 100;
                ushort[] registers = new ushort[] { 1, 2, 3 };

                // write three registers
                master.WriteMultipleRegisters(slaveId, startAddress, registers);
            }
        }



        /// <summary>
        ///     Simple Modbus serial RTU slave example.
        /// </summary>
        public static void StartModbusSerialRtuSlave()
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
                var slaveNetwork = factory.CreateRtuSlaveNetwork(slavePort);

                IModbusSlave slave1 = factory.CreateSlave(1);
                IModbusSlave slave2 = factory.CreateSlave(2);

                slaveNetwork.AddSlave(slave1);
                slaveNetwork.AddSlave(slave2);

                slaveNetwork.ListenAsync().GetAwaiter().GetResult();
            }
        }




        public static async Task StartModbusSerialRtuSlaveNetwork(CancellationToken cancellationToken)
        {
            using (SerialPort slavePort = new SerialPort(PrimarySerialPortName))
            {
                // configure serial port
                slavePort.BaudRate = 57600;
                slavePort.DataBits = 8;
                slavePort.Parity = Parity.Even;
                slavePort.StopBits = StopBits.One;
                slavePort.Open();

                IModbusFactory factory = new ModbusFactory();
                IModbusSlaveNetwork modbusSlaveNetwork = factory.CreateRtuSlaveNetwork(slavePort);

                slavePort.ReadTimeout = 50;
                slavePort.WriteTimeout = 500;

                var acTechDataStore = new SlaveStorage();

                //acTechDataStore.CoilDiscretes.StorageOperationOccurred += (sender, args) => Console.WriteLine($"Coil discretes: {args.Operation} starting at {args.StartingAddress}");
                //acTechDataStore.CoilInputs.StorageOperationOccurred += (sender, args) => Console.WriteLine($"Coil  inputs: {args.Operation} starting at {args.StartingAddress}");
                acTechDataStore.InputRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"ACTECH Input registers: {args.Operation} starting at {args.StartingAddress}");
                acTechDataStore.HoldingRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"ACTECH Holding registers: {args.Operation} starting at {args.StartingAddress}");

                var casHmiDataStore = new SlaveStorage();

                casHmiDataStore.InputRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"CASHMI Input registers: {args.Operation} starting at {args.StartingAddress}");
                casHmiDataStore.HoldingRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"CASHMI Holding registers: {args.Operation} starting at {args.StartingAddress}");

                var danfossStore = new SlaveStorage();

                danfossStore.InputRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"DANFOSS Input registers: {args.Operation} starting at {args.StartingAddress}");
                danfossStore.HoldingRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"DANFOSS Holding registers: {args.Operation} starting at {args.StartingAddress}");

                IModbusSlave slave1 = factory.CreateSlave(21, acTechDataStore);
                IModbusSlave slave2 = factory.CreateSlave(55, casHmiDataStore);

                IModbusSlave slave3 = factory.CreateSlave(1, danfossStore);

                modbusSlaveNetwork.AddSlave(slave1);
                //modbusSlaveNetwork.AddSlave(slave2);
                modbusSlaveNetwork.AddSlave(slave2);
                modbusSlaveNetwork.AddSlave(slave3);

                await modbusSlaveNetwork.ListenAsync(cancellationToken);

                await Task.Delay(1, cancellationToken);
            }
        }


        /// <summary>
        /// Simple Modbus serial RTU slave example.
        /// </summary>
        public static async Task StartModbusSerialRtuSlaveWithCustomMessage(CancellationToken cancellationToken)
        {
            using (SerialPort slavePort = new SerialPort(PrimarySerialPortName))
            {
                // configure serial port
                slavePort.BaudRate = 57600;
                slavePort.DataBits = 8;
                slavePort.Parity = Parity.Even;
                slavePort.StopBits = StopBits.One;
                slavePort.Open();

                var adapter = new SerialPortAdapter(slavePort);

                var functionServices = new IModbusFunctionService[]
                {
                    new HmiBufferFunctionService()
                };

                var factory = new ModbusFactory(functionServices, true, new ConsoleModbusLogger(LoggingLevel.Debug));

                // create modbus slave
                var slaveNetwork = factory.CreateRtuSlaveNetwork(adapter);

                var acTechDataStore = new SlaveStorage();

                acTechDataStore.InputRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"ACTECH Input registers: {args.Operation} starting at {args.StartingAddress}");
                acTechDataStore.HoldingRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"ACTECH Holding registers: {args.Operation} starting at {args.StartingAddress}");

                var danfossStore = new SlaveStorage();

                danfossStore.InputRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"DANFOSS Input registers: {args.Operation} starting at {args.StartingAddress}");
                danfossStore.HoldingRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"DANFOSS Holding registers: {args.Operation} starting at {args.StartingAddress}");

                IModbusSlave actechSlave = factory.CreateSlave(21, acTechDataStore);
                IModbusSlave danfossSlave = factory.CreateSlave(1, danfossStore);

                slaveNetwork.AddSlave(actechSlave);
                slaveNetwork.AddSlave(danfossSlave);

                await slaveNetwork.ListenAsync(cancellationToken);
            }
        }


        public static async Task StartModbusSerialRtuSlaveWithCustomStore()
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
                var slaveNetwork = factory.CreateRtuSlaveNetwork(slavePort);

                var dataStore = new SlaveStorage();

                dataStore.CoilDiscretes.StorageOperationOccurred += (sender, args) => Console.WriteLine($"Coil discretes: {args.Operation} starting at {args.StartingAddress}");
                dataStore.CoilInputs.StorageOperationOccurred += (sender, args) => Console.WriteLine($"Coil inputs: {args.Operation} starting at {args.StartingAddress}");
                dataStore.InputRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"Input registers: {args.Operation} starting at {args.StartingAddress}");
                dataStore.HoldingRegisters.StorageOperationOccurred += (sender, args) => Console.WriteLine($"Holding registers: {args.Operation} starting at {args.StartingAddress}");

                IModbusSlave slave1 = factory.CreateSlave(1, dataStore);

                slaveNetwork.AddSlave(slave1);

                await slaveNetwork.ListenAsync();
            }
        }

    }
}
