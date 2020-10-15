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

            var content = deserializer.Deserialize(reader) as Dictionary<object, object>;

            return Task.FromResult(new[] { 
                new Fragment {
                    Name = "some_script_name_1"
                },
                new Fragment {
                    Name = "some_script_name_2"
                }
            }.AsEnumerable());
        }
    }
}
