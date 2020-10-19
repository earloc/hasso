using Hasso.Cli.Split;
using Serilog;

namespace Hasso.Tests.Units
{
    public class ScriptSplitterTestsFixture
    {
        internal ISplitter SystemUnderTest { get; private set; } = new YamlScriptSplitter(Log.Logger);
    }
}
