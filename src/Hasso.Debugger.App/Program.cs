using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Reflection;

namespace Hasso.Debugger.App
{
    public class Program
    {

        protected Program()
        {

        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[]? args = null, Action<IWebHostBuilder>? configure = null, ILogger? logger = null)
        {
            var userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            var logFile = new FileInfo(Path.Combine(userDirectory, "hasso.debugger.log.txt"));

            Log.Logger = logger = logger ?? new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFile.FullName)
                .WriteTo.ColoredConsole(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            var callingAssembly = new FileInfo(Assembly.GetEntryAssembly()?.Location);

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseUrls()
                        .UseContentRoot(callingAssembly.Directory.FullName)
                        .UseStaticWebAssets()
                        .UseSerilog(logger);
                    configure?.Invoke(webBuilder);
                });
        }
            
    }
}
