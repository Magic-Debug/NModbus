using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using NModbus.Unme.Common;

namespace NModbus.IO
{
    /// <summary>
    ///     Concrete Implementor - http://en.wikipedia.org/wiki/Bridge_Pattern
    /// </summary>
    public class TcpClientAdapter : IStreamResource
    {
        private TcpClient _tcpClient;

        public TcpClientAdapter(TcpClient tcpClient)
        {
            Debug.Assert(tcpClient != null, "Argument tcpClient cannot be null.");

            _tcpClient = tcpClient;
        }

        public int InfiniteTimeout => Timeout.Infinite;

        public int ReadTimeout
        {
            get => _tcpClient.GetStream().ReadTimeout;
            set => _tcpClient.GetStream().ReadTimeout = value;
        }

        public int WriteTimeout
        {
            get => _tcpClient.GetStream().WriteTimeout;
            set => _tcpClient.GetStream().WriteTimeout = value;
        }

        public void Write(byte[] buffer, int offset, int size)
        {
            string data = string.Join(" ", buffer.Select(b => b.ToString()));
            File.AppendAllText("modbus.txt", $"{data}[{buffer.Length}]W{Environment.NewLine}");
            _tcpClient.GetStream().Write(buffer, offset, size);
        }

        public int Read(byte[] buffer, int offset, int size)
        {
            NetworkStream stream = _tcpClient.GetStream();
            int len = stream.Read(buffer, offset, size);
            string data = string.Join(" ", buffer.Select(b => b.ToString()));
            File.AppendAllText("modbus.txt", $"{buffer.Length}{data}[{buffer.Length}]R{Environment.NewLine}");
            return len;
        }

        public void DiscardInBuffer()
        {
            _tcpClient.GetStream().Flush();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposableUtility.Dispose(ref _tcpClient);
            }
        }
    }
}
