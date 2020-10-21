using Hasso.Models;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace Hasso.Cli.Split
{

    internal class YamlSceneSplitter : YamlSplitterBase<List<Scene>>
    {
        public YamlSceneSplitter(ILogger logger) : base(logger)
        {
        }

        public override string SourceName => "scenes";

        protected override IEnumerable<Fragment> Split(List<Scene> content)
        {
            var fragments = content.Select(item =>
            {
                return new Fragment
                {
                    Name = item.Name,
                    Content = new List<object> { item }
                };

            });

            return fragments;
        }
    }
}
