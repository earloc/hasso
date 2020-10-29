using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Hasso.Tests")]
namespace Hasso.Cli
{
    static class Program
    {

        static async Task Main(string[] args)
        {

            var userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var hassoDirectory = Path.Combine(userDirectory, ".hasso");

            var logFile = new FileInfo(Path.Combine(hassoDirectory, "hasso.log.txt"));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFile.FullName)
                .WriteTo.ColoredConsole(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            var app = new App(Log.Logger);

            app.ConfigureCommands();

            await app.RunAsync(args);
        }
    }
}
