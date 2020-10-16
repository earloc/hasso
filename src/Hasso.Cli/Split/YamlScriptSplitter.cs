using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Hasso.Cli.Split
{
    public class YamlScriptSplitter : IScriptSplitter
    {
        public Task<IEnumerable<Fragment>> SplitAsync(FileInfo inputFile)
        {
            var deserializer = new DeserializerBuilder().Build();
            using var reader = inputFile.OpenText();
            var content = deserializer.Deserialize<Dictionary<object, object>>(reader);

            return Task.FromResult(
                content.Select(
                    _ => new Fragment { 
                        Name = _.Key as string,
                        Content = new Dictionary<object, object> { {_.Key, _.Value } }
                    }
                )
            );
        }
    }
}
