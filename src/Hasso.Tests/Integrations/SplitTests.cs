using FluentAssertions;
using Serilog;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace Hasso.Cli.Tests.Integrations
{
    public class SplitTests
    {
        public SplitTests()
        {
            SystemUnderTest.ConfigureCommands();
        }

        internal App SystemUnderTest { get; } = new App(Log.Logger);


        internal DirectoryInfo ProvideAssets([CallerMemberName] string testName = "")
        {
            var testDirectory = new DirectoryInfo(testName);

            if (testDirectory.Exists)
            {
                testDirectory.Delete(true);
            }

            testDirectory.Create();

            foreach (var filePath in Directory.GetFiles("./assets", "*.yaml"))
            {
                var file = new FileInfo(filePath);
                File.Copy(filePath, Path.Combine(testDirectory.FullName, file.Name));
            }

            return testDirectory;
        }


        [Theory]
        [InlineData("scripts", 2)]
        [InlineData("automations", 2)]
        [InlineData("scenes", 2)]
        [Trait("quality", "crap")]
        public async Task SplitCommand_Produces_Expected_Count_Of_Partial_Yamls(string subDirectory, int expectedFileCount)
        {
            var workingDirectory = ProvideAssets();

            var exitCode = await SystemUnderTest.RunAsync(new[] { "split", "--source-directory", workingDirectory.FullName });

            exitCode.Should().Be(0, "that indicates a healthy execution, which we expect here");

            var subDirectories = workingDirectory.GetDirectories().Select(x => x.Name); ;

            subDirectories.Should().Contain(subDirectory, "this should have been created");

            var files = Directory.GetFiles(Path.Combine(workingDirectory.FullName, subDirectory));

            files.Should().HaveCount(expectedFileCount, "that´s the number of entries that should have been splitted");
        }
    }
}
