
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Hasso.Cli.Split
{
    internal abstract class YamlSplitterBase<T> : ISplitter
    {
        protected readonly ILogger logger;

        protected YamlSplitterBase(ILogger logger)
        {
            this.logger = logger;
        }

        public abstract string SourceName { get; }

        public async Task<IEnumerable<Fragment>> SplitAsync(FileInfo inputFile)
        {
            var deserializer = new DeserializerBuilder().Build();

            using var reader = inputFile.OpenText();

            var content = await Task.Run(() => deserializer.Deserialize<T>(reader));

            var fragments = Split(content)
                .ToArray()
                .AsEnumerable();

            logger.Information("found {count} items", fragments.Count());

            return fragments;
        }
        protected abstract IEnumerable<Fragment> Split(T content);
    }
}
