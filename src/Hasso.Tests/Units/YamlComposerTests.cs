using Xunit;

namespace Hasso.Tests.Units
{
    public class YamlComposerTests : IClassFixture<YamlComposerTestsFixture>
    {
        private readonly YamlComposerTestsFixture fixture;

        public YamlComposerTests(YamlComposerTestsFixture fixture)
        {
            this.fixture = fixture;
        }
        [Theory]
        [InlineData("assets")]
        public void Composing_Partial_Yamls_Produces_Monolithic_Config(string sourceDirectory)
        {
            
        }

    }
}
