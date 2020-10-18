namespace Hasso.Cli.Tests.Units
{
    public class FragmentWriterFixture
    {
        internal IFragmentWriter FragmentWriter { get; private set; } = new YamlFragmentWriter();
    }
}