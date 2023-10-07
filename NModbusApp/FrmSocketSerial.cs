using NModbus;
using System.Net;
using System.Net.Sockets;

namespace NModbusApp
{
    public partial class FrmSocketSerial : BaseForm
    {
        public FrmSocketSerial()
        {
            InitializeComponent();
        }

        private static async Task<int> Demos(string[] args)
        {
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) => cts.Cancel();

            try
            {
                ModbusSocketSerialMasterReadRegisters();
                ModbusSocketSerialMasterWriteRegisters();
                ModbusSocketSerialMasterReadRegisters();
                await Task.Run(() => { });
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
        /// Simple Read registers using socket and expecting RTU fromatted response messages.
        /// </summary>
        public static void ModbusSocketSerialMasterReadRegisters()
        {
            using (var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {

                // configure socket
                var serverIP = IPAddress.Parse("192.168.2.100");
                var serverFullAddr = new IPEndPoint(serverIP, 9000);
                sock.Connect(serverFullAddr);

                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(sock);

                byte slaveId = 1;
                ushort startAddress = 100;
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, 3);
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"Input {(startAddress + i)}={registers[i]}");
                }
            }
        }
        public static void ModbusSocketSerialMasterWriteRegisters()
        {
            using (var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {

                // configure socket
                var serverIP = IPAddress.Parse("192.168.2.100");
                var serverFullAddr = new IPEndPoint(serverIP, 9000);
                sock.Connect(serverFullAddr);

                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(sock);

                byte slaveId = 1;
                ushort startAddress = 100;
                ushort[] registers = new ushort[] { 10, 20, 30 };

                // write three registers
                master.WriteMultipleRegisters(slaveId, startAddress, registers);
            }
        }

    }
}
