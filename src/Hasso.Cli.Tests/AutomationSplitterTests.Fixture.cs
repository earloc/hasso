using Hasso.Cli.Split;

namespace Hasso.Cli.Tests.Scripts
{
    public class AutomationSplitterTestsFixture
    {
        public IAutomationSplitter SystemUnderTest { get; internal set; } = new YamlAutomationSplitter();
    }
}
