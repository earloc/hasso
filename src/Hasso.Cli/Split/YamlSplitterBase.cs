
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
        private readonly string nameIdentifier;
        private readonly IDeserializer deserializer;

        protected YamlSplitterBase(ILogger logger, string nameIdentifier)
        {
            this.logger = logger;
            this.nameIdentifier = nameIdentifier;
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

        private IEnumerable<Fragment> Split(YamlDocument yaml)
        {
            switch (yaml.RootNode)
            {
                case YamlSequenceNode sequence: return Split(sequence);
                case YamlMappingNode mapping: return Split(mapping);
                default: throw new NotImplementedException();
            }
        }

        private IEnumerable<Fragment> Split(YamlMappingNode mapping)
        {
            var fragments = new List<Fragment>();

            foreach (var child in mapping)
            {
                var singleMapping = new YamlMappingNode();
                singleMapping.Add(child.Key, child.Value);

                var childDocument = new YamlDocument(singleMapping);
                var childStream = new YamlStream(childDocument);
                var builder = new StringBuilder();
                using var writer = new StringWriter(builder);
                childStream.Save(writer);

                fragments.Add(new Fragment()
                {
                    Name = child.Key.ToString(),
                    Content = builder.ToString().Replace($"{Environment.NewLine}...", "")
                });
            }

            return fragments;
        }

        private IEnumerable<Fragment> Split(YamlSequenceNode sequence)
        {
            var fragments = new List<Fragment>();

            foreach (var child in sequence.Children)
            {
                var singleSequence = new YamlSequenceNode();
                singleSequence.Add(child);

                var childDocument = new YamlDocument(singleSequence);
                var childStream = new YamlStream(childDocument);
                var builder = new StringBuilder();
                using var writer = new StringWriter(builder);
                childStream.Save(writer);

                fragments.Add(new Fragment()
                {
                    Name = child[nameIdentifier].ToString(),
                    Content = builder.ToString().Replace($"{Environment.NewLine}...", "")
                });
            }

            return fragments;
        }
    }
}
