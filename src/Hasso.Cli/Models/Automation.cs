using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Hasso.Models
{
    internal class Automation
    {
        [YamlMember(Alias = "id", ScalarStyle = ScalarStyle.SingleQuoted)]
        public string? Id { get; set; }

        [YamlMember(Alias = "alias")]
        public string? Alias { get; set; }

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }

        [YamlMember(Alias = "trigger")]
        public List<object> Trigger { get; set; } = new List<object>();

        [YamlMember(Alias = "condition")]
        public List<object> Condition { get; set; } = new List<object>();

        [YamlMember(Alias = "action")]
        public List<object> Action { get; set; } = new List<object>();

        [YamlMember(Alias = "mode")]
        public string Mode { get; set; } = "Single";

    }
}
