using Hasso.Cli.Split;

namespace Hasso.Cli.Tests.Scripts
{
    public class AutomationSplitterTestsFixture
    {
        internal IAutomationSplitter SystemUnderTest { get; private set; } = new YamlAutomationSplitter();
    }
}
