using Hasso.Models;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace Hasso.Cli.Split
{
    internal class YamlAutomationSplitter : YamlSplitterBase<List<Automation>>
    {

        public YamlAutomationSplitter(ILogger logger) : base(logger)
        {
        }

        public override string SourceName => "automations";

        protected override IEnumerable<Fragment> Split(List<Automation> content)
        {
            var fragments = content.Select(item =>
            {
                return new Fragment
                {
                    Name = item.Alias,
                    Content = new List<object> { item }
                };
            });

            return fragments;
        }
    }
}
