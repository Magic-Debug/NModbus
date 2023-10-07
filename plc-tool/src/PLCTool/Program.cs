using System.Globalization;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using PLCTool.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System.Net.Sockets;
using Microsoft.VisualBasic.Devices;
using NModbus;

namespace PLCTool
{
    public static class Program
    {
        private static Logger? Logger { get; set; }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main()
        {

            BitHelper.HasByteEqualTo(23,25);

            var result = Math.Round(85.2 * 0.9144, 1);
            BitHelper.HasFlag(253,2);
            BitHelper.HasLookupFlag(2654, 2, 12);

            AppDomain.CurrentDomain.UnhandledException+=CurrentDomain_UnhandledException;
            Services = ConfigureServices();

            string language = SystemConfig.GetConfigValues("Language");
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Logger.Information("Run PLCTool");
            var frm = Services.GetService<FrmDashboard>();//FormMain
            Application.Run(frm);
            Common.GetInstance();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error(e.ExceptionObject.ToString());
            if (e.ExceptionObject is SocketException)
            { 
            
            }
        }

        public static IServiceProvider Services { get; private set; }

        private static IServiceProvider ConfigureServices()
        {
            //http://www.qb5200.com/article/478972.html
            ServiceCollection services = new ServiceCollection();
            // Forms
            services.AddTransient<FormMain>();
            services.AddTransient<FrmDashboard>();

            //注册配置
            IConfigurationBuilder cfgBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("plc-settings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: false)
                ;
            IConfiguration configuration = cfgBuilder.Build();
            services.AddSingleton<IConfiguration>(configuration);

            //http://www.qb5200.com/article/478972.html
            services.AddOptions();
            //实例化一个对应 PlcDevices json 数组对象, 使用了 IConfiguration.Get<T>()
            List<PlcOptions> plcDeviceSettings = configuration.GetSection("plc").Get<List<PlcOptions>>();
            //或直接通过 service.Configure<T>() 将appsettings 指定 section 放入DI 容器, 这里的T 为 List<PlcDevice>
            services.Configure<List<PlcOptions>>(configuration.GetSection("plc"));

            //创建 logger
            Logger serilogLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            //register logger
            services.AddLogging(builder =>
            {
                ILoggingBuilder p = builder.AddSerilog(logger: serilogLogger, dispose: true);
            });
            Logger=serilogLogger;
            return services.BuildServiceProvider();
        }

    }
}
