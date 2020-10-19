using Hasso.Cli.Compose;
using Serilog;

namespace Hasso.Tests.Units
{
    public class YamlComposerTestsFixture
    {
        internal IComposer SystemUnderTest { get; } = new YamlComposer(Log.Logger);
    }
}
