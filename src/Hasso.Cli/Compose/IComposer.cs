using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hasso.Cli.Compose
{
    internal interface IComposer
    {
        Task<IEnumerable<FileInfo>> ComposeAsync(DirectoryInfo sourceDirectory, DirectoryInfo? targetDirectory = null);
    }
}
