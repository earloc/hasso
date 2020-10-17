using Hasso.Cli.Split;
using Serilog;

namespace Hasso.Cli.Tests.Scripts
{
    public class SceneSplitterTestsFixture
    {
        internal ISplitter SystemUnderTest { get; private set; } = new YamlSceneSplitter(Log.Logger);
    }
}
