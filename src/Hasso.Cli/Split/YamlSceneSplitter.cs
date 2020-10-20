using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hasso.Cli.Split
{
    internal class YamlSceneSplitter : YamlSplitterBase<List<object>>
    {
        public YamlSceneSplitter(ILogger logger) : base(logger)
        {
        }

        public override string SourceName => "scenes";

        protected override IEnumerable<Fragment> Split(List<object> content)
        {
            var fragments = content.Select(item =>
            {
                var name = ResolveNameOf(item);
                return new Fragment
                {
                    Name = name,
                    Content = new List<object> { item }
                };

            });

            return fragments;
        }

        private string ResolveNameOf(object key)
        {
            var content = key as Dictionary<object, object>;

            var name = content?["name"] as string;

            if (name is null) throw new InvalidOperationException("expected 'name'-element was not found");

            return name;
        }
    }
}
