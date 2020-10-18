using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasso.Cli.Compose
{
    internal class YamlComposer : IComposer
    {
        private readonly ILogger logger;

        public YamlComposer(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<IEnumerable<FileInfo>> ComposeAsync(DirectoryInfo sourceDirectory, DirectoryInfo? targetDirectory = null)
        {
            targetDirectory = targetDirectory ?? sourceDirectory;

            if (!targetDirectory.Exists)
                targetDirectory.Create();

            var supportedDirectoriyNames = new[] { "scenes", "scripts", "automations" };
            var supportedDirectories = supportedDirectoriyNames
                .Select(name => new DirectoryInfo(Path.Combine(sourceDirectory.FullName, name)))
                .ToArray();

            var resultFiles = new List<FileInfo>();

            foreach (var directory in supportedDirectories)
            {
                var files = directory.GetFiles("*.partial.yaml");
                var builder = new StringBuilder();

                foreach (var file in files)
                {
                    var content = File.ReadAllText(file.FullName);
                    builder.Append(content);
                }

                var targetFile = Path.Combine(targetDirectory.FullName, $"{directory}.yaml");

                File.WriteAllText(targetFile, builder.ToString());
                resultFiles.Add(new FileInfo(targetFile));
            }

            await Task.CompletedTask;

            return resultFiles;
        }
    }
}