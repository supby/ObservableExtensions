using System;
using System.IO;
using System.Text;
using System.Threading;

namespace ObservableExtensions
{
    public class PortEmulator : IDisposable
    {
        private readonly FileStream _dataFileStream;
        private readonly Timer _timer;
        private readonly Random _random;

        public event EventHandler<PortEventArgs> OnDataArrived;

        public PortEmulator(string dataFile)
        {
            _dataFileStream = File.OpenRead(dataFile);
            _timer = new Timer(OnTimer, null, 0, 1000);
            _random = new Random(91);
        }

        private void OnTimer(object state)
        {
            int bytesCount = _random.Next(1,10);
            byte[] buf = new byte[bytesCount];
            _dataFileStream.Read(buf, 0, bytesCount);
            OnDataArrived?.Invoke(this, new PortEventArgs(Encoding.UTF8.GetString(buf)));
        }

        public void Dispose()
        {
            _dataFileStream.Dispose();
            _timer.Dispose();
        }
    }
}