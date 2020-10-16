using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hasso.Cli
{
    public interface IAutomationSplitter
    {
        public Task<IEnumerable<Fragment>> SplitAsync(string inputFileName)
            => SplitAsync(new FileInfo(inputFileName));

        public Task<IEnumerable<Fragment>> SplitAsync(FileInfo inputFile);

    }
}