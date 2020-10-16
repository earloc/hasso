namespace Hasso.Cli.Tests
{
    public class FragmentWriterFixture
    {
        internal IFragmentWriter FragmentWriter { get; private set; } = new YamlFragmentWriter();
    }
}