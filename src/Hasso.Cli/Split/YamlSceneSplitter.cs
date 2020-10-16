using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Hasso.Cli.Split
{
    public class YamlSceneSplitter : ISceneSplitter
    {
        public Task<IEnumerable<Fragment>> SplitAsync(FileInfo inputFile)
        {
            var deserializer = new DeserializerBuilder().Build();
            using var reader = inputFile.OpenText();

            var content = deserializer.Deserialize(reader) as List<object>;

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
                )
            );
        }

        private string ResolveNameOf(object key)
        {
            var content = key as Dictionary<object, object>;

            var name = content["name"] as string;

            return name;
        }
    }
}
