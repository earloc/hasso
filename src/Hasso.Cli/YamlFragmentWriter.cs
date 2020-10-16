using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hasso.Cli.Tests
{
    internal class YamlFragmentWriter : IFragmentWriter
    {
        public Task<IEnumerable<FileInfo>> WriteAsync(IEnumerable<Fragment> fragments)
        {
            throw new System.NotImplementedException();
        }
    }
}