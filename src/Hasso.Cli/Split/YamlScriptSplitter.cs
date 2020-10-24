using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Hasso.Cli.Split
{
    internal class YamlScriptSplitter : YamlSplitterBase
    {
        public YamlScriptSplitter(ILogger logger) : base(logger, "foo")
        {
        }

        public override string SourceName => "scripts";
    }
}
