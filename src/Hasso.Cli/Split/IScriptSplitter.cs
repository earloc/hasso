﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hasso.Cli
{
    public interface IScriptSplitter
    {
        public Task<IEnumerable<object>> SplitAsync(string inputFileName)
            => SplitAsync(new FileInfo(inputFileName));

        public Task<IEnumerable<object>> SplitAsync(FileInfo inputFile);

    }
}