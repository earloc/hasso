using FluentAssertions;
using System;
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
            foreach (var file in Directory.GetFiles(".","*.yaml"))
            {
                File.Copy(file, Path.Combine(testRunId, file));
            }
            Directory.SetCurrentDirectory(testRunId);

            var exitCode = await fixture.SystemUnderTest.RunAsync(new[] { "split" });

            exitCode.Should().Be(0, "that indicates a healthy execution, which we expect here");

            var subDirectories = Directory.GetDirectories(".");
            var expectedSubDiretoryCount = 3;

            subDirectories.Should().HaveCount(expectedSubDiretoryCount, "that´s how many configs we placed into the test-env");

            subDirectories.Should().Contain(Path.Combine(".", subDirectory).ToString(), "this should have been created");

            var files = Directory.GetFiles(subDirectory);

            files.Should().HaveCount(expectedFileCount, "that´s the number of entries that should have been splitted");

        }
    }
}
