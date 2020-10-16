using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hasso.Cli.Split
{
    internal interface ISplitter
    {
        public Task<IEnumerable<Fragment>?> SplitAsync(string inputFileName)
            => SplitAsync(new FileInfo(inputFileName));

        public Task<IEnumerable<Fragment>?> SplitAsync(FileInfo inputFile);
    }
}
