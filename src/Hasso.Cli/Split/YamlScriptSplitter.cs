using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hasso.Cli.Split
{
    public class YamlScriptSplitter : IScriptSplitter
    {
        public Task<IEnumerable<object>> SplitAsync(FileInfo inputFile)
        {
            return Task.FromResult(Enumerable.Range(0, 2).Cast<object>());
        }
    }
}
