using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Hasso.Models
{
    internal class Scene
    {
        [YamlMember(Alias = "id", ScalarStyle = ScalarStyle.SingleQuoted)]
        public string? Id { get; set; }

        [YamlMember(Alias = "name")]
        public string? Name { get; set; }

        [YamlMember(Alias = "entities")]
        public Dictionary<string, object> Entities { get; set; } = new Dictionary<string, object>();

    }
}
