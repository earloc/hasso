﻿using Hasso.Cli.Split;

namespace Hasso.Cli.Tests.Scripts
{
    public class SplitFixture
    {
        public IScriptSplitter ScriptSplitter { get; internal set; } = new YamlScriptSplitter();
    }
}