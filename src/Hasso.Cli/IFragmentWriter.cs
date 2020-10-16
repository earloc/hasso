using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hasso.Cli
{
    public interface IFragmentWriter
    {
        public Task<FileInfo> WriteAsync(IEnumerable<Fragment> fragments);
    }
}