
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace Hasso.Cli.Split
{
    internal abstract class YamlSplitterBase : ISplitter
    {
        protected readonly ILogger logger;
        private readonly IDeserializer deserializer;

        protected YamlSplitterBase(ILogger logger)
        {
            this.logger = logger;
            deserializer = new DeserializerBuilder().Build();
        }

        public abstract string SourceName { get; }

        public async Task<IEnumerable<Fragment>> SplitAsync(FileInfo inputFile)
        {
            var yaml = await File.ReadAllTextAsync(inputFile.FullName);

            var content = await SplitAsync(yaml);

            return content;

            
        }

        public async Task<IEnumerable<Fragment>> SplitAsync(string yaml)
        {
            using var reader = new StringReader(yaml);
            var stream = new YamlStream();

            await Task.Run( () => stream.Load(reader));

            var yamlDocument = stream.Documents.FirstOrDefault();

            var fragments = Split(yamlDocument)
                .ToArray()
                .AsEnumerable();

            logger.Information("found {count} items", fragments.Count());

            return fragments;
        }

        protected abstract IEnumerable<Fragment> Split(YamlDocument yaml);
    }
}
