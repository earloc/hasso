using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hasso.Cli.Split
{
    internal class SplitCommandHandler
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

        public Task ExecuteAsync(string? sourceDirectory = null)
        {
            var directory = new DirectoryInfo(sourceDirectory ?? ".");

            if (!directory.Exists)
                throw new ArgumentException($"Could not find path'{sourceDirectory}'", nameof(sourceDirectory));

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
                    continue;
                }

                logger.Information($"reading {inputFilePath}");
                var fragments = await splitter.SplitAsync(inputFile);

                var directory = EnsureDirectory(workingDirectory, splitter.SourceName);
                await fragmentWriter.WriteAsync(directory, fragments);
            }
        }
    }
}
