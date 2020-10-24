using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace Hasso.Cli.Split
{
    internal class YamlAutomationSplitter : YamlSplitterBase
    {

        public YamlAutomationSplitter(ILogger logger) : base(logger, "alias")
        {
        }

        public override string SourceName => "automations";

        
    }
}
