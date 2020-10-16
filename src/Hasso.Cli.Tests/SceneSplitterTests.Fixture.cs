using Hasso.Cli.Split;

namespace Hasso.Cli.Tests.Scripts
{
    public class SceneSplitterTestsFixture
    {
        public IScriptSplitter SystemUnderTest { get; internal set; } = new YamlSceneSplitter();
    }
}
