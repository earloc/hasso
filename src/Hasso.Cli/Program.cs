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
            services.AddSplitters();
            var provider = services.BuildServiceProvider();


            var scriptFragments = await provider
                .GetRequiredService<IScriptSplitter>()
                .SplitAsync("scripts.yaml");

            var sceneFragments = await provider
                .GetRequiredService<IScriptSplitter>()
                .SplitAsync("scenes.yaml");

            var writer = provider.GetRequiredService<IFragmentWriter>();

            var scripts = await writer.WriteAsync(new DirectoryInfo("Scripts"), scriptFragments);
            var scenes = await writer.WriteAsync(new DirectoryInfo("Scenes"), sceneFragments);

            var files = scripts.Concat(scenes);

            foreach (var file in files)
                Console.WriteLine(file.FullName);


        }
    }
}
