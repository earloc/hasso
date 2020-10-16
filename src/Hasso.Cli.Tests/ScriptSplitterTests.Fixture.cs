using Hasso.Cli.Split;

namespace Hasso.Cli.Tests.Scripts
{
    public class ScriptSplitterTestsFixture
    {
        public IScriptSplitter SystemUnderTest { get; internal set; } = new YamlScriptSplitter();
    }
}
