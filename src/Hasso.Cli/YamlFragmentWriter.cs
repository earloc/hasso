using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hasso.Cli.Tests
{
    public class YamlFragmentWriter : IFragmentWriter
    {
        public Task<IEnumerable<FileInfo>> WriteAsync(IEnumerable<Fragment> fragments)
        {
            return Task.FromResult(new FileInfo[2].AsEnumerable());
        }
    }
}