using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Hasso.Cli.Split
{
    internal class YamlAutomationSplitter : YamlSplitterBase
    {

        public YamlAutomationSplitter(ILogger logger) : base(logger)
        {
        }

        public override string SourceName => "automations";

        protected override IEnumerable<Fragment> Split(YamlDocument yaml)
        {
            //var fragments = content.Select(item =>
            //{
            //    return new Fragment
            //    {
            //        Name = item.Alias,
            //        Content = new List<object> { item }
            //    };
            //});

            //return fragments;

            throw new NotImplementedException();
        }
    }
}
