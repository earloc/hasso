using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hasso.Cli
{
    public interface IFragmentWriter
    {
        public Task<IEnumerable<FileInfo>> WriteAsync(DirectoryInfo baseDirectory, IEnumerable<Fragment> fragments);
    }
}