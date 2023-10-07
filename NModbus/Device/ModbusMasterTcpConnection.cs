using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using NModbus.IO;
using NModbus.Message;
using NModbus.Logging;

namespace NModbus.Device
{
    using Extensions;

    /// <summary>
    /// Represents an incoming connection from a Modbus master. Contains the slave's logic to process the connection.
    /// </summary>
    internal class ModbusMasterTcpConnection : ModbusDevice, IDisposable
    {

        private readonly TcpClient _client;
        private readonly string _endPoint;
        private readonly Stream _stream;
        private readonly IModbusSlaveNetwork _slaveNetwork;
        private readonly IModbusFactory _modbusFactory;
        private readonly Task _requestHandlerTask;

        private readonly byte[] _mbapHeader = new byte[6];
        private byte[] _messageFrame;

        public ModbusMasterTcpConnection(TcpClient client, IModbusSlaveNetwork slaveNetwork, IModbusFactory modbusFactory, IModbusLogger logger)
            : base(new ModbusIpTransport(new TcpClientAdapter(client), modbusFactory, logger))
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _endPoint = client.Client.RemoteEndPoint.ToString();
            _stream = client.GetStream();
            _slaveNetwork = slaveNetwork ?? throw new ArgumentNullException(nameof(slaveNetwork));
            _modbusFactory = modbusFactory ?? throw new ArgumentNullException(nameof(modbusFactory));
            _requestHandlerTask = Task.Run((Func<Task>)HandleRequestAsync);
        }

        /// <summary>
        ///     Occurs when a Modbus master TCP connection is closed.
        /// </summary>
        public event EventHandler<TcpConnectionEventArgs> ModbusMasterTcpConnectionClosed;

        public IModbusLogger Logger { get; }

        public string EndPoint => _endPoint;

        public Stream Stream => _stream;

        public TcpClient TcpClient => _client;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _stream.Dispose();
            }

            base.Dispose(disposing);
        }

        private async Task HandleRequestAsync()
        {
            try
            {
                while (true)
                {
                    Logger.Debug($"开始从上位机【{EndPoint}】读取Header");
                    int readBytes = await Stream.ReadAsync(_mbapHeader, 0, 6).ConfigureAwait(false);
                    if (readBytes == 0)
                    {
                        Logger.Debug($"0 bytes read, 上位机【{EndPoint}】 has closed Socket connection.");
                        ModbusMasterTcpConnectionClosed?.Invoke(this, new TcpConnectionEventArgs(EndPoint));
                        return;
                    }

                    ushort frameLength = (ushort)IPAddress.HostToNetworkOrder(BitConverter.ToInt16(_mbapHeader, 4));
                    Logger.Debug($"上位机【{EndPoint}】 sent header: \"{string.Join(", ", _mbapHeader)}\" with {frameLength} bytes in PDU");

                    _messageFrame = new byte[frameLength];
                    readBytes = await Stream.ReadAsync(_messageFrame, 0, frameLength).ConfigureAwait(false);
                    if (readBytes == 0)
                    {
                        Logger.Debug($"0 bytes read, 上位机【{EndPoint}】关闭Socket连接.");
                        ModbusMasterTcpConnectionClosed?.Invoke(this, new TcpConnectionEventArgs(EndPoint));
                        return;
                    }
                    Logger.Debug($"从上位机【{EndPoint}】读取{readBytes} 字节");
                    byte[] frame = _mbapHeader.Concat(_messageFrame).ToArray();
                    Logger.Trace($"收到来自上位机【{EndPoint}】{frame.Length}字节: {string.Join(", ", frame)}");

                    IModbusMessage request = _modbusFactory.CreateModbusRequest(_messageFrame);
                    request.TransactionId = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(frame, 0));

                    IModbusSlave slave = _slaveNetwork.GetSlave(request.SlaveAddress);

                    if (slave != null)
                    {
                        //TODO: Determine if this is appropriate

                        // perform action and build response
                        IModbusMessage response = slave.ApplyRequest(request);
                        response.TransactionId = request.TransactionId;

                        // write response
                        byte[] responseFrame = Transport.BuildMessageFrame(response);
                        Logger.Information($"发送给上位机【{EndPoint}】{responseFrame.Length}字节: {string.Join(", ", responseFrame)}");
                        await Stream.WriteAsync(responseFrame, 0, responseFrame.Length).ConfigureAwait(false);
                    }
                }
            }
            // If an exception occurs (such as IOException in case of disconnect, or other failures), handle it as if the connection was gracefully closed
            catch (Exception e)
            {
                Logger.Warning($"{e.GetType().Name} occured with 上位机【{EndPoint}】关闭了连接.");
                ModbusMasterTcpConnectionClosed?.Invoke(this, new TcpConnectionEventArgs(EndPoint));
                return;
            }
        }
    }
}
