using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Hasso.Cli.Tests")]
namespace Hasso.Cli
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var app = new App();

            app.ConfigureCommands();

            await app.RunAsync(args);
        }
    }
}
