using Serilog;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Hasso.Cli.Tests.Integrations
{
    public class AppFixture
    {

        public AppFixture()
        {
            SystemUnderTest.ConfigureCommands();
        }

        internal App SystemUnderTest { get; } = new App(Log.Logger);

        internal DirectoryInfo ProvideAssetsForSplit([CallerMemberName]string testName = "")
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

        internal DirectoryInfo ProvideAssetsForCompose([CallerMemberName] string testName = "")
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
    }
}