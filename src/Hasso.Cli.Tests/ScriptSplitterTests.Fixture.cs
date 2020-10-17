using Hasso.Cli.Split;

namespace Hasso.Cli.Tests.Scripts
{
    public class ScriptSplitterTestsFixture
    {
        internal ISplitter SystemUnderTest { get; private set; } = new YamlScriptSplitter();
    }
}
