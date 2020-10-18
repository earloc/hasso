using Hasso.Cli.Split;
using Serilog;

namespace Hasso.Cli.Tests.Units
{
    public class SceneSplitterTestsFixture
    {
        internal ISplitter SystemUnderTest { get; private set; } = new YamlSceneSplitter(Log.Logger);
    }
}
