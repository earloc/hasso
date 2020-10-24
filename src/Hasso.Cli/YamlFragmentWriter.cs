using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hasso.Cli
{
    internal class YamlFragmentWriter : IFragmentWriter
    {
        private readonly ILogger logger;

        public YamlFragmentWriter(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<IEnumerable<FileInfo>> WriteAsync(DirectoryInfo baseDirectory, IEnumerable<Fragment> fragments)
        {
            if (!baseDirectory.Exists) baseDirectory.Create();

            var tasks = fragments.Select(async _ =>
            {
                var targetFileName = Path.Combine(baseDirectory.FullName, $"{_.Name}.partial.yaml");
                var targetFile = new FileInfo(targetFileName);

                await File.WriteAllTextAsync(targetFile.FullName, _.Content.Trim());

                logger.Information("created '{targetFileName}'", targetFileName);

                return targetFile;
            }).ToArray().AsEnumerable();

            return await Task.WhenAll(tasks);
        }
    }
}