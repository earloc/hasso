using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Hasso.Cli.Split
{
    internal class YamlAutomationSplitter : IAutomationSplitter
    {
        public Task<IEnumerable<Fragment>> SplitAsync(FileInfo inputFile)
        {
            var deserializer = new DeserializerBuilder().Build();
            using var reader = inputFile.OpenText();

            var content = deserializer.Deserialize<List<object>>(reader);

            return Task.FromResult(
                content.Select(item =>
                    {
                        var name = ResolveNameOf(item);
                        return new Fragment
                        {
                            Name = name,
                            Content = new Dictionary<object, object> { { name, item } }
                        };

                    }
                ).ToArray().AsEnumerable()
            );
        }

        private string ResolveNameOf(object key)
        {
            var content = key as Dictionary<object, object>;

            var name = content?["alias"] as string;

            if (name is null) throw new InvalidOperationException("Could not locate an automation´s name");

            return name;
        }
    }
}
