using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hasso.Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Directory.SetCurrentDirectory(args.First());

            var services = new ServiceCollection();
            services.AddScriptsSplitter();
            var provider = services.BuildServiceProvider();


            var scriptSplitter = provider.GetRequiredService<IScriptSplitter>();
            var scriptFragments = await scriptSplitter.SplitAsync("scripts.yaml");

            var writer = provider.GetRequiredService<IFragmentWriter>();

            var files = await writer.WriteAsync(new DirectoryInfo("Scripts"), scriptFragments);

            foreach(var file in files)
                Console.WriteLine(file.FullName);


        }
    }
}
