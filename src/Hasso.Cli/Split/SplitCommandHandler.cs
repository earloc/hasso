using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hasso.Cli.Split
{
    class SplitCommandHandler
    {
        private readonly IScriptSplitter scriptSplitter;
        private readonly ISceneSplitter sceneSplitter;
        private readonly IAutomationSplitter automationSplitter;
        private readonly IFragmentWriter fragmentWriter;

        public SplitCommandHandler(IScriptSplitter scriptSplitter, ISceneSplitter sceneSplitter, IAutomationSplitter automationSplitter, IFragmentWriter fragmentWriter)
        {
            this.scriptSplitter = scriptSplitter;
            this.sceneSplitter = sceneSplitter;
            this.automationSplitter = automationSplitter;
            this.fragmentWriter = fragmentWriter;
        }

        public Task ExecuteAsync(string? workingDirectory = null)
        {
            var directory = new DirectoryInfo(workingDirectory ?? ".");

            if (!directory.Exists)
                throw new ArgumentException($"Could not find path'{workingDirectory}'", nameof(workingDirectory));

            return ExecuteAsyncCore(directory);
        }

        private async Task ExecuteAsyncCore(DirectoryInfo workingDirectory)
        {
            DirectoryInfo EnsureDirectory(DirectoryInfo baseDirectory, string path)
            {
                var directory = new DirectoryInfo(Path.Combine(baseDirectory.FullName, path));
                if (!directory.Exists)
                    directory.Create();

                return directory;
            }

            var scriptFragments = await scriptSplitter.SplitAsync(Path.Combine(workingDirectory.FullName, "scripts.yaml"));
            var scriptsDirectory = EnsureDirectory(workingDirectory, "scripts");
            var scripts = await fragmentWriter.WriteAsync(scriptsDirectory, scriptFragments);

            var sceneFragments = await sceneSplitter.SplitAsync(Path.Combine(workingDirectory.FullName, "scenes.yaml"));
            var sceneDirectory = EnsureDirectory(workingDirectory, "scenes");
            var scenes = await fragmentWriter.WriteAsync(sceneDirectory, sceneFragments);

            var automationFragments = await automationSplitter.SplitAsync(Path.Combine(workingDirectory.FullName, "automations.yaml"));
            var automationsDirectory = EnsureDirectory(workingDirectory, "automations");
            var automations = await fragmentWriter.WriteAsync(automationsDirectory, automationFragments);

            var files = scripts.Concat(scenes).Concat(automations);

            foreach (var file in files)
                Console.WriteLine(file.FullName);
        }
    }
}
