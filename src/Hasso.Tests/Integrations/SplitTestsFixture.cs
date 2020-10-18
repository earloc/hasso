using Serilog;

namespace Hasso.Cli.Tests.Integrations
{
    public class SplitTestsFixture
    {

        public SplitTestsFixture()
        {
            SystemUnderTest.ConfigureCommands();
        }
        internal App SystemUnderTest { get; } = new App(Log.Logger);
    }
}