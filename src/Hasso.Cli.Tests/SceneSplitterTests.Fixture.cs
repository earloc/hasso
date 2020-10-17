using Hasso.Cli.Split;

namespace Hasso.Cli.Tests.Scripts
{
    public class SceneSplitterTestsFixture
    {
        internal ISplitter SystemUnderTest { get; private set; } = new YamlSceneSplitter();
    }
}
