
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Hasso.Cli.Split
{

    internal class YamlSceneSplitter : YamlSplitterBase
    {
        public YamlSceneSplitter(ILogger logger) : base(logger, "name")
        {
        }

        public override string SourceName => "scenes";
    }
}
