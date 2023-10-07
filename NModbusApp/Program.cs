using Newtonsoft.Json;

namespace NModbusApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var x = exp(25000, 5);

            
            float data = BitConverter.ToSingle(new byte[] { 2, 4, 8, 16 }, 0);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmDashboard());// FrmDashboard  FrmTcpSlave
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string? msg = JsonConvert.SerializeObject(e.ExceptionObject);
            File.WriteAllLines("exception.log", new List<string> { msg });
        }

        static float exp(float x, int n)
        {
            return n == 0 ? 1 : n % 2 == 0 ? exp(x * x, n / 2) : exp(x * x, (n - 1) / 2) * x;
        }
    }
}