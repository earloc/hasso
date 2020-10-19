using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Hasso.Tests")]
namespace Hasso.Cli
{
    static class Program
    {

        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("hasso.log.txt")
                .WriteTo.ColoredConsole(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            var app = new App(Log.Logger);

            app.ConfigureCommands();

            await app.RunAsync(args);
        }
    }
}
