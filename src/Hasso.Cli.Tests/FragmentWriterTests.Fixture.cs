using System.IO;

namespace Hasso.Cli.Tests
{
    public class FragmentWriterFixture
    {
        public IFragmentWriter FragmentWriter { get; internal set; } = new YamlFragmentWriter();
    }
}