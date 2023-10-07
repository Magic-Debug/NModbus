using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using NModbus;
using NModbus.Serial;
using NModbus.Utility;

namespace NModbusApp
{
    using System.Linq;
    using NModbus.Logging;

    /// <summary>
    ///     Demonstration of NModbus
    /// </summary>
    public class Demo
    {
        private static async Task<int> Demos(string[] args)
        {
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) => cts.Cancel();

            try
            {
                await Task.Run(() => { });
                ModbusTcpMasterReadInputsFromModbusSlave();
                StartModbusUdpSlave();
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
        /// Simple write to socket connector sending RTU messages
        /// </summary>


        /// <summary>
        ///     Simple Modbus UDP master write coils example.
        /// </summary>
        public static void ModbusUdpMasterWriteCoils()
        {
            using (UdpClient client = new UdpClient())
            {
                IPEndPoint endPoint = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 502);
                client.Connect(endPoint);

                var factory = new ModbusFactory();

                var master = factory.CreateMaster(client);

                ushort startAddress = 1;

                // write three coils
                master.WriteMultipleCoils(0, startAddress, new bool[] { true, false, true });
            }
        }

   
        
    
        /// <summary>
        ///     Simple Modbus serial USB ASCII slave example.
        /// </summary>
        public static void StartModbusSerialUsbAsciiSlave()
        {
            // TODO
        }

        /// <summary>
        ///     Simple Modbus serial USB RTU slave example.
        /// </summary>
        public static void StartModbusSerialUsbRtuSlave()
        {
            // TODO
        }

      
        /// <summary>
        ///     Simple Modbus UDP slave example.
        /// </summary>
        public static void StartModbusUdpSlave()
        {
            using (UdpClient client = new UdpClient(502))
            {
                var factory = new ModbusFactory();
                IModbusSlaveNetwork network = factory.CreateSlaveNetwork(client);

                IModbusSlave slave1 = factory.CreateSlave(1);
                IModbusSlave slave2 = factory.CreateSlave(2);

                network.AddSlave(slave1);
                network.AddSlave(slave2);

                network.ListenAsync().GetAwaiter().GetResult();

                // prevent the main thread from exiting
                Thread.Sleep(Timeout.Infinite);
            }
        }

        /// <summary>
        ///     Modbus TCP master and slave example.
        /// </summary>
        public static void ModbusTcpMasterReadInputsFromModbusSlave()
        {
            byte slaveId = 1;
            int port = 502;
            IPAddress address = new IPAddress(new byte[] { 127, 0, 0, 1 });

            // create and start the TCP slave
            TcpListener slaveTcpListener = new TcpListener(address, port);
            slaveTcpListener.Start();

            var factory = new ModbusFactory();
            var network = factory.CreateSlaveNetwork(slaveTcpListener);

            IModbusSlave slave = factory.CreateSlave(slaveId);

            network.AddSlave(slave);

            var listenTask = network.ListenAsync();

            // create the master
            TcpClient masterTcpClient = new TcpClient(address.ToString(), port);
            IModbusMaster master = factory.CreateMaster(masterTcpClient);

            ushort numInputs = 5;
            ushort startAddress = 100;

            // read five register values
            ushort[] inputs = master.ReadInputRegisters(0, startAddress, numInputs);

            for (int i = 0; i < numInputs; i++)
            {
                Console.WriteLine($"Register {(startAddress + i)}={(inputs[i])}");
            }

            // clean up
            masterTcpClient.Close();
            slaveTcpListener.Stop();

            // output
            // Register 100=0
            // Register 101=0
            // Register 102=0
            // Register 103=0
            // Register 104=0
        }


        /// <summary>
        ///     Write a 32 bit value.
        /// </summary>
        public static void ReadWrite32BitValue()
        {
            using (SerialPort port = new SerialPort("COM1"))
            {
                // configure serial port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();

                var factory = new ModbusFactory();
                IModbusRtuTransport transport = factory.CreateRtuTransport(port);
                IModbusSerialMaster master = factory.CreateMaster(transport);

                byte slaveId = 1;
                ushort startAddress = 1008;
                uint largeValue = UInt16.MaxValue + 5;

                ushort lowOrderValue = BitConverter.ToUInt16(BitConverter.GetBytes(largeValue), 0);
                ushort highOrderValue = BitConverter.ToUInt16(BitConverter.GetBytes(largeValue), 2);

                // write large value in two 16 bit chunks
                master.WriteMultipleRegisters(slaveId, startAddress, new ushort[] { lowOrderValue, highOrderValue });

                // read large value in two 16 bit chunks and perform conversion
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, 2);
                uint value = ModbusUtility.GetUInt32(registers[1], registers[0]);
            }
        }
    }
}
