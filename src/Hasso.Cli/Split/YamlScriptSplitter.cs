using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Hasso.Cli.Split
{
    internal class YamlScriptSplitter : YamlSplitterBase
    {
        public YamlScriptSplitter(ILogger logger) : base(logger)
        {
        }

        public override string SourceName => "scripts";

        protected override IEnumerable<Fragment> Split(YamlDocument yaml)
        {
            //var fragments = content.Select(_ =>
            //    {
            //        var name = _.Key as string;
            //        if (name is null)
            //        {
            //            throw new InvalidOperationException("top-level name was not found");
            //        }
            //        return new Fragment
            //        {
            //            Name = _.Key as string,
            //            Content = new Dictionary<object, object> { { _.Key, _.Value } }
            //        };
            //    }
            //);

            //return fragments;

            throw new NotImplementedException();
        }
    }
}
