using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Hasso.Cli
{
    internal class YamlFragmentWriter : IFragmentWriter
    {
        private readonly ILogger logger;
        private readonly ISerializer serializer;

        public YamlFragmentWriter(ILogger logger)
        {
            this.logger = logger;
            serializer = new SerializerBuilder()
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
                .Build();
        }
        public async Task<IEnumerable<FileInfo>> WriteAsync(DirectoryInfo baseDirectory, IEnumerable<Fragment> fragments)
        {
            if (!baseDirectory.Exists) baseDirectory.Create();

            var tasks = fragments.Select(async _ =>
            {
                var targetFileName = Path.Combine(baseDirectory.FullName, $"{_.Name}.partial.yaml");
                var targetFile = new FileInfo(targetFileName);

                var content = await WriteAsync(_);

                File.WriteAllText(targetFile.FullName, content.Trim());

                logger.Information("created '{targetFileName}'", targetFileName);

                return targetFile;
            }).ToArray().AsEnumerable();

            return await Task.WhenAll(tasks);
        }

        public Task<string> WriteAsync(Fragment fragment)
        {
            return Task.FromResult(serializer.Serialize(fragment.Content));
        }
    }
}