using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hasso.Cli.Split
{
    class SplitCommandHandler
    {
        private readonly ISplitter[] splitters;
        private readonly IFragmentWriter fragmentWriter;
        private readonly ILogger logger;

        public SplitCommandHandler(ISplitter[] splitters, IFragmentWriter fragmentWriter, ILogger logger)
        {
            this.splitters = splitters;
            this.fragmentWriter = fragmentWriter;
            this.logger = logger;
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
                {
                    logger.Information($"creating directory '{directory.FullName}'");
                    directory.Create();
                }

                return directory;
            }

            foreach (var splitter in splitters)
            {
                var inputFilePath = Path.Combine(workingDirectory.FullName, $"{splitter.SourceName}.yaml");
                var inputFile = new FileInfo(inputFilePath);
                if (!inputFile.Exists)
                {
                    logger.Warning($"skipping '{inputFilePath}', as it was not found");
                }

                var fragments = await splitter.SplitAsync(inputFile);
                if (fragments is null)
                {
                    continue;
                }
                var directory = EnsureDirectory(workingDirectory, splitter.SourceName);
                await fragmentWriter.WriteAsync(directory, fragments);
            }

            //var scriptFragments = await scriptSplitter.SplitAsync(Path.Combine(workingDirectory.FullName, "scripts.yaml"));
            //var scriptsDirectory = EnsureDirectory(workingDirectory, "scripts");
            //var scripts = await fragmentWriter.WriteAsync(scriptsDirectory, scriptFragments);

            //var sceneFragments = await sceneSplitter.SplitAsync(Path.Combine(workingDirectory.FullName, "scenes.yaml"));
            //var sceneDirectory = EnsureDirectory(workingDirectory, "scenes");
            //var scenes = await fragmentWriter.WriteAsync(sceneDirectory, sceneFragments);

            //var automationFragments = await automationSplitter.SplitAsync(Path.Combine(workingDirectory.FullName, "automations.yaml"));
            //var automationsDirectory = EnsureDirectory(workingDirectory, "automations");
            //var automations = await fragmentWriter.WriteAsync(automationsDirectory, automationFragments);

            //var files = scripts.Concat(scenes).Concat(automations);
        }
    }
}
