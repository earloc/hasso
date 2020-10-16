using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Hasso.Cli.Tests
{
    public class YamlFragmentWriter : IFragmentWriter
    {
        private readonly DirectoryInfo baseDirectory;

        public YamlFragmentWriter(DirectoryInfo baseDirectory) 
            => this.baseDirectory = baseDirectory;

        public Task<IEnumerable<FileInfo>> WriteAsync(IEnumerable<Fragment> fragments)
        {
            if (!baseDirectory.Exists) 
                baseDirectory.Create();

            var serializer = new SerializerBuilder().Build();

            var tasks = fragments.Select(_ => {
                var targetFileName = Path.Combine(baseDirectory.FullName, $"{_.Name}.partial.yaml");
                var targetFile = new FileInfo(targetFileName);
                using var writer = File.CreateText(targetFile.FullName);

                serializer.Serialize(writer, _.Content);

                return targetFile;
            });

            return Task.FromResult(tasks);



        }
    }
}