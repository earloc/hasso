using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hasso.Cli.Split
{
    internal class YamlAutomationSplitter : YamlSplitterBase<List<object>>
    {

        public YamlAutomationSplitter(ILogger logger) : base(logger)
        {
        }

        public override string SourceName => "automations";

        protected override IEnumerable<Fragment> Split(List<object> content)
        {
            var fragments = content.Select(item =>
            {
                var name = ResolveNameOf(item);
                return new Fragment
                {
                    Name = name,
                    Content = new Dictionary<object, object> { { name, item } }
                };
            });

            return fragments;
        }

        private string ResolveNameOf(object key)
        {
            var content = key as Dictionary<object, object>;

            var name = content?["alias"] as string;

            if (name is null) throw new InvalidOperationException("expected 'alias'-element was not found");

            return name;
        }
    }
}
