using Hasso.Cli;
using Serilog;

namespace Hasso.Tests.Units
{
    public class FragmentWriterFixture
    {
        internal IFragmentWriter FragmentWriter { get; private set; } = new YamlFragmentWriter(Log.Logger);
    }
}