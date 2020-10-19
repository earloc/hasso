using FluentAssertions;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hasso.Cli.Tests.Integrations
{
    public class ComposeTests : IClassFixture<AppFixture>
    {
        private readonly AppFixture fixture;

        public ComposeTests(AppFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("scripts")]
        [InlineData("automations")]
        [InlineData("scenes")]
        [Trait("quality", "crap")]
        public async Task ComposeCommand_Produces_Expected_Content_Of_Monolithic_Configs(string subDirectory)
        {
            var workingDirectory = fixture.ProvideAssetsForTest();

            var exitCode = await fixture.SystemUnderTest.RunAsync(new[] { "compose", "--source-directory", workingDirectory.FullName });

            exitCode.Should().Be(0, "that indicates a healthy execution, which we expect here");

            var fileName = $"{subDirectory}.yaml";

            var expected = File.ReadAllText(Path.Combine("assets", fileName));
            var actual = File.ReadAllText(Path.Combine(workingDirectory.FullName, fileName));

            actual.Should().Be(expected, "this is the expected product of the compose-command");

        }
    }
}
