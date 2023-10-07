using NModbus;
using NModbus.Extensions;
using NModbus.Extensions.Enron;
using NModbus.Logging;
using NModbus.Message;
using System.Net.Sockets;

namespace NModbusApp
{
    public partial class BaseForm : Form
    {
        protected CancellationTokenSource CancellationToken { get; }

        protected TcpClient Client { get; }
        protected ModbusFactory Factory { get; }

        protected IModbusMaster Master { get; set; }

        protected ModbusMasterEnhanced ModbusEnhanced { get; }

        protected byte SlaveId { get; set; } = 1;

        protected ushort Register => ushort.Parse(txtRegister.Text);

        protected UInt32 Value => ushort.Parse(txtValue.Text);

        protected ushort MumberOfPoints => ushort.Parse(txtNumberOfPoints.Text);

        public BaseForm()
        {
            InitializeComponent();
            this.KeyDown += BaseForm_KeyDown;
            CancellationToken = new CancellationTokenSource();
            Client = new TcpClient("192.168.0.9", 502);
            Factory = new ModbusFactory(null, true, new JsonModbusLogger(LoggingLevel.Trace));
            Master = Factory.CreateMaster(Client);
            ModbusEnhanced = new ModbusMasterEnhanced(Master);
        }

        private void BaseForm_KeyDown(object? sender, KeyEventArgs e)
        {
            CancellationToken.Cancel();
        }

        private void Run()
        {
            try
            {
                ModbusEnhanced.WriteFloatHoldingRegisters(SlaveId, Register, new float[5] { 2.23f, 3.45f, 21f, 45f, 1.247f });
                //ModbusTcpMasterReadInputs();
                //await WriteSingleCoilAsync();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        /// <summary>
        ///     Simple Modbus TCP master read inputs example.
        /// </summary>
        public void ModbusTcpMasterReadHoldingRegisters32()
        {
            Master.WriteSingleRegister32(SlaveId, Register, Value);
            uint[] registers = Master.ReadHoldingRegisters32(SlaveId, Register, MumberOfPoints);
            string text = "";
            for (int i = 0; i < MumberOfPoints; i++)
            {
                text += $"输入 {(Register + i)}={registers[i]}{Environment.NewLine}";
            }
            rTextBoxHoldingRegisters32.Text = text;
        }


        /// <summary>
        ///     Simple Modbus TCP master read inputs example.
        /// </summary>
        public void ModbusTcpMasterReadInputs()
        {
            bool[] inputs = Master.ReadInputs(SlaveId, Register, MumberOfPoints);

            for (int i = 0; i < MumberOfPoints; i++)
            {
                Console.WriteLine($"Input {(Register + i)}={(inputs[i] ? 1 : 0)}");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Run();
        }

        public void ReadCoils()
        {
            uint[] registers = Master.ReadHoldingRegisters32(SlaveId, Register, MumberOfPoints);
        }

        public async Task ReadCoilsAsync()
        {
            bool[]? result = await Master.ReadCoilsAsync(SlaveId, Register, MumberOfPoints);
        }

        public void ReadInputs()
        {
            bool[]? result = Master.ReadInputs(SlaveId, Register, MumberOfPoints);
        }

        public async Task ReadInputsAsync()
        {
            var result = await Master.ReadInputsAsync(SlaveId, Register, MumberOfPoints);
        }

        public void ReadHoldingRegisters()
        {
            ushort[]? result = Master.ReadHoldingRegisters(SlaveId, Register, MumberOfPoints);
        }

        public Task<ushort[]> ReadHoldingRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public void ReadInputRegisters()
        {
            ushort[]? result = Master.ReadInputRegisters(SlaveId, Register, MumberOfPoints);
        }

        public Task<ushort[]> ReadInputRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public void WriteSingleCoil() => Master.WriteSingleCoil(SlaveId, Register, 1 > 0);

        public Task WriteSingleCoilAsync() => Master.WriteSingleCoilAsync(SlaveId, Register, 1 > 0);

        public void WriteSingleRegister() => Master.WriteSingleRegister(SlaveId, Register, (ushort)Value);

        public Task WriteSingleRegisterAsync(byte slaveAddress, ushort registerAddress, ushort value)
        {
            throw new NotImplementedException();
        }

        public void WriteMultipleRegisters() => Master.WriteMultipleRegisters(SlaveId, Register, new ushort[4] { 1, 2, 3, 4 });

        public Task WriteMultipleRegistersAsync(byte slaveAddress, ushort startAddress, ushort[] data)
        {
            throw new NotImplementedException();
        }

        public void WriteMultipleCoils() => Master.WriteMultipleCoils(SlaveId, Register, new bool[4] { true, false, true, false });

        public Task WriteMultipleCoilsAsync(byte slaveAddress, ushort startAddress, bool[] data)
        {
            throw new NotImplementedException();
        }

        public void ReadWriteMultipleRegisters()
        {
            var result = Master.ReadWriteMultipleRegisters(SlaveId, Register, MumberOfPoints, 2, new ushort[4] { 1, 2, 3, 4 });
        }

        public Task<ushort[]> ReadWriteMultipleRegistersAsync(byte slaveAddress, ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
        {
            throw new NotImplementedException();
        }

        public void WriteFileRecord() => Master.WriteFileRecord(SlaveId, 3, Register, new byte[4] { 1, 2, 3, 4 });

        private void btnReadCoils_Click(object sender, EventArgs e) => ReadCoils();

        /// <summary>
        /// 写入单线圈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteSingleCoil_Click(object sender, EventArgs e) => WriteSingleCoil();

        /// <summary>
        /// 写入单寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteSingleRegister_Click(object sender, EventArgs e) => WriteSingleRegister();

        /// <summary>
        /// 写入多寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteMultipleRegisters_Click(object sender, EventArgs e) => WriteMultipleRegisters();

        /// <summary>
        /// 写入多线圈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteMultipleCoils_Click(object sender, EventArgs e) => WriteMultipleCoils();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        public void ExecuteCustomMessage()
        {
            WriteMultipleRegistersResponse response = ModbusMessageFactory.CreateModbusMessage<WriteMultipleRegistersResponse>(new byte[] { 17, ModbusFunctionCodes.WriteMultipleRegisters, 0, 1, 0, 2 });
            WriteMultipleRegistersResponse? rsp = Master.ExecuteCustomMessage<WriteMultipleRegistersResponse>(response);
        }

        private void btnWriteFileRecord_Click(object sender, EventArgs e)
        {
            WriteFileRecord();
        }

        /// <summary>
        /// 读取离散线圈数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadInputs_Click(object sender, EventArgs e) => ReadInputs();


        /// <summary>
        /// 读取保存寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadHoldingRegisters_Click(object sender, EventArgs e) => ReadHoldingRegisters();

        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadInputRegisters_Click(object sender, EventArgs e) => ReadInputRegisters();

        private void btnReadWriteMultipleRegisters_Click(object sender, EventArgs e)
        {
            ReadWriteMultipleRegisters();
        }
    }
}
