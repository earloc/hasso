using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            var fragments = new List<Fragment>();

            var sequence = yaml.RootNode as YamlSequenceNode;

            foreach (var child in sequence.Children)
            {
                var singleSequence = new YamlSequenceNode();
                singleSequence.Add(child);

                var childDocument = new YamlDocument(singleSequence);
                var childStream = new YamlStream(childDocument);
                var builder = new StringBuilder();
                using var writer = new StringWriter(builder);
                childStream.Save(writer);

                fragments.Add(new Fragment()
                {
                    Name = child["alias"].ToString(),
                    Content = builder.ToString().Replace($"{Environment.NewLine}...", "")
                }) ;
            }

            return fragments;
        }
    }
}
