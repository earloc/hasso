
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hasso.Cli.Split
{
    internal abstract class YamlSplitterBase : ISplitter
    {
        protected readonly ILogger logger;

        protected YamlSplitterBase(ILogger logger)
        {
            this.logger = logger;
        }

        public abstract string SourceName { get; }

        public Task<IEnumerable<Fragment>?> SplitAsync(DirectoryInfo directory)
        {
            var inputFile = new FileInfo(Path.Combine(directory.FullName, $"{SourceName}.yaml"));

            return SplitAsync(inputFile);
        }

        public async Task<IEnumerable<Fragment>?> SplitAsync(FileInfo inputFile)
        {
            logger.Information("looking for automation-configuration: '{inputFile}'.", inputFile);

            if (!inputFile.Exists)
            {
                logger.Warning("could not find automation-configuration, skipping.");
                return null;
            }

            var fragments = await SplitCoreAsync(inputFile);

            return fragments;
        }

        protected abstract Task<IEnumerable<Fragment>?> SplitCoreAsync(FileInfo inputFile);
    }
}
