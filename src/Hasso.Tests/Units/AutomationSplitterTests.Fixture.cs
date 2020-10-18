using Hasso.Cli.Split;
using Serilog;

namespace Hasso.Tests.Units
{
    public class AutomationSplitterTestsFixture
    {
        internal ISplitter SystemUnderTest { get; private set; } = new YamlAutomationSplitter(Log.Logger);
    }
}
