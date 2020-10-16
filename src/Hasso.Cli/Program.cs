using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly:InternalsVisibleTo("Hasso.Cli.Tests")]
namespace Hasso.Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Directory.SetCurrentDirectory(args.First());

            var services = new ServiceCollection();
            services.AddSplitters();
            services.AddFragmentWriter();

            var provider = services.BuildServiceProvider();

            var writer = provider.GetRequiredService<IFragmentWriter>();

            var scriptFragments = await provider
                .GetRequiredService<IScriptSplitter>()
                .SplitAsync("scripts.yaml");
            var scripts = await writer.WriteAsync(new DirectoryInfo("Scripts"), scriptFragments);

            var sceneFragments = await provider
                .GetRequiredService<ISceneSplitter>()
                .SplitAsync("scenes.yaml");
            var scenes = await writer.WriteAsync(new DirectoryInfo("Scenes"), sceneFragments);

            var automationFragments = await provider
                .GetRequiredService<IAutomationSplitter>()
                .SplitAsync("automations.yaml");
            var automations = await writer.WriteAsync(new DirectoryInfo("Automations"), automationFragments);

            var files = scripts.Concat(scenes).Concat(automations);

            foreach (var file in files)
                Console.WriteLine(file.FullName);

        }
    }
}
