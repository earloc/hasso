using Serilog;

namespace Hasso.Cli.Tests.Integrations
{
    public class AppFixture
    {

        public AppFixture()
        {
            SystemUnderTest.ConfigureCommands();
        }
        internal App SystemUnderTest { get; } = new App(Log.Logger);
    }
}