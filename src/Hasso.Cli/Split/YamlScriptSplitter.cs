using Serilog;

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
