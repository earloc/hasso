using Hasso.Cli.Split;

namespace Hasso.Cli.Tests.Scripts
{
    public class SceneSplitterTestsFixture
    {
        public ISceneSplitter SystemUnderTest { get; internal set; } = new YamlSceneSplitter();
    }
}
