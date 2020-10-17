﻿using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Hasso.Cli.Split
{
    internal class YamlScriptSplitter : YamlSplitterBase
    {
        public YamlScriptSplitter(ILogger logger) : base(logger)
        {
        }

        public override string SourceName => "scripts";

        protected override async Task<IEnumerable<Fragment>?> SplitCoreAsync(FileInfo inputFile)
        {
            var deserializer = new DeserializerBuilder().Build();
            using var reader = inputFile.OpenText();
            var content = deserializer.Deserialize<Dictionary<object, object>>(reader);

            await Task.CompletedTask;

            var fragments = content.Select(
                    _ =>
                    {
                        var name = _.Key as string;
                        if (name is null) throw new InvalidOperationException("Could not find a scipt´s name");
                        return new Fragment
                        {
                            Name = _.Key as string,
                            Content = new Dictionary<object, object> { { _.Key, _.Value } }
                        };
                    }
                );

            return fragments.ToArray().AsEnumerable();
        }
    }
}
