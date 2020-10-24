using Serilog;

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
