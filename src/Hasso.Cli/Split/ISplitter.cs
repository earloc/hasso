using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hasso.Cli.Split
{
    internal interface ISplitter
    {
        public string SourceName { get; }
        public Task<IEnumerable<Fragment>?> SplitAsync(DirectoryInfo directory)
        {
            var inputFile = new FileInfo(Path.Combine(directory.FullName, $"{SourceName}.yaml"));
            return SplitAsync(inputFile);
        }
        public Task<IEnumerable<Fragment>?> SplitAsync(FileInfo inputFile);
    }
}
