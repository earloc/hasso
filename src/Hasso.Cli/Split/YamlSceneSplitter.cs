
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Hasso.Cli.Split
{

    internal class YamlSceneSplitter : YamlSplitterBase
    {
        public YamlSceneSplitter(ILogger logger) : base(logger)
        {
        }

        public override string SourceName => "scenes";

        protected override IEnumerable<Fragment> Split(YamlDocument yaml)
        {
            //var fragments = content.Select(item =>
            //{
            //    return new Fragment
            //    {
            //        Name = item.Name,
            //        Content = new List<object> { item }
            //    };

            //});

            //return fragments;

            throw new NotImplementedException();
        }
    }
}
