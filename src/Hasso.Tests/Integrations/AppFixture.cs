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

        internal DirectoryInfo ProvideAssetsForTest([CallerMemberName]string testName = "")
        {
            var workingDirectory = new DirectoryInfo(testName);

            if (workingDirectory.Exists)
            {
                workingDirectory.Delete(true);
            }

            workingDirectory.Create();

            foreach (var filePath in Directory.GetFiles("./assets", "*.yaml"))
            {
                var file = new FileInfo(filePath);
                File.Copy(filePath, Path.Combine(workingDirectory.FullName, file.Name));
            }

            return workingDirectory;
        }
    }
}