using FluentAssertions;
using Serilog;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace Hasso.Cli.Tests.Integrations
{
    public class ComposeTests
    {
        internal DirectoryInfo ProvideAssets([CallerMemberName] string testName = "")
        {
            var testDirectory = new DirectoryInfo(testName);

            if (testDirectory.Exists)
            {
                testDirectory.Delete(true);
            }

            testDirectory.Create();

            var sourceDirectory = new DirectoryInfo("assets");
            foreach (var directory in sourceDirectory.GetDirectories())
            {
                var targetDirectory = testDirectory.CreateSubdirectory(directory.Name);
                foreach (var file in directory.GetFiles())
                {
                    File.Copy(file.FullName, Path.Combine(targetDirectory.FullName, file.Name));
                }
            }

            return testDirectory;
        }

        public ComposeTests()
        {
            SystemUnderTest.ConfigureCommands();
        }

        internal App SystemUnderTest { get; } = new App(Log.Logger);


        [Theory]
        [InlineData("scripts")]
        [InlineData("automations")]
        [InlineData("scenes")]
        [Trait("quality", "crap")]
        public async Task ComposeCommand_Produces_Expected_Content_Of_Monolithic_Configs(string subDirectory)
        {
            var workingDirectory = ProvideAssets();

            var exitCode = await SystemUnderTest.RunAsync(new[] { "compose", "--source-directory", workingDirectory.FullName });

            exitCode.Should().Be(0, "that indicates a healthy execution, which we expect here");

            var fileName = $"{subDirectory}.yaml";

            var expected = File.ReadAllText(Path.Combine("assets", fileName));
            var actual = File.ReadAllText(Path.Combine(workingDirectory.FullName, fileName));

            actual.Should().Be(expected, "this is the expected product of the compose-command");

        }
    }
}
