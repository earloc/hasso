using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Hasso.Cli.Tests.Integrations
{
    public class SplitTests : IClassFixture<SplitTestsFixture>
    {
        private readonly SplitTestsFixture fixture;

        public SplitTests(SplitTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("scripts", 2)]
        [InlineData("automations", 2)]
        [InlineData("scenes", 2)]

        public async Task SplitCommand_Produces_Expected_Count_Of_Partial_Yamls(string subDirectory, int expectedFileCount)
        {
            var testRunId = nameof(SplitCommand_Produces_Expected_Count_Of_Partial_Yamls);

            if (Directory.Exists(testRunId))
            {
                Directory.Delete(testRunId, true);
            }

            Directory.CreateDirectory(testRunId);

            foreach (var filePath in Directory.GetFiles("./assets", "*.yaml"))
            {
                var file = new FileInfo(filePath);
                File.Copy(filePath, Path.Combine(testRunId, file.Name));
            }

            var currentDirectory = Directory.GetCurrentDirectory();

            Directory.SetCurrentDirectory(testRunId);

            try
            {
                var exitCode = await fixture.SystemUnderTest.RunAsync(new[] { "split" });

                exitCode.Should().Be(0, "that indicates a healthy execution, which we expect here");

                var subDirectories = Directory.GetDirectories(".");

                subDirectories.Should().Contain(Path.Combine(".", subDirectory).ToString(), "this should have been created");

                var files = Directory.GetFiles(subDirectory);

                files.Should().HaveCount(expectedFileCount, "that´s the number of entries that should have been splitted");
            }
            finally
            {
                Directory.SetCurrentDirectory(currentDirectory);
            }

        }
    }
}
