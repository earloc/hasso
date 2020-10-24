
using Serilog;

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
