using FluentAssertions;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        [InlineData("assets", "scripts.yaml")]
        [InlineData("assets", "scenes.yaml")]
        [InlineData("assets", "automations.yaml")]

        public async Task Composing_Partial_Yamls_Produces_Monolithic_Config(string directoryName, string fileName)
        {
            var sourceDirectory = new DirectoryInfo(directoryName);
            var targetDirectory = new DirectoryInfo("test1");

            if (targetDirectory.Exists)
                targetDirectory.Delete(true);

            targetDirectory.Create();

            var expected = File.ReadAllText(Path.Combine(sourceDirectory.FullName, fileName));

            var compositions = await fixture.SystemUnderTest.ComposeAsync(sourceDirectory, targetDirectory);

            var lookup = compositions.ToDictionary(x => x.Name);
            lookup.Should().ContainKey(fileName, "this monolithic config should have been generated");

            var actual = File.ReadAllText(lookup[fileName].FullName);

            actual.Should().Be(expected, "the composer should produce the exact match");
        }

    }
}
