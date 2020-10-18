using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hasso.Cli.Compose
{
    internal class ComposeCommandHandler
    {
        private readonly IComposer composer;

        public ComposeCommandHandler(IComposer composer)
        {
            this.composer = composer;
        }

        public Task ExecuteAsync(string? sourceDirectory = null, string? targetDirectory = null)
        {
            var source = new DirectoryInfo(sourceDirectory ?? ".");
            var target = new DirectoryInfo(targetDirectory ?? source.FullName);

            return composer.ComposeAsync(source, target);
        }
    }
}
