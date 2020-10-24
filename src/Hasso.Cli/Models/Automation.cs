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
        public List<TriggerType> Trigger { get; set; } = new List<TriggerType>();

        [YamlMember(Alias = "condition")]
        public List<object> Condition { get; set; } = new List<object>();

        [YamlMember(Alias = "action")]
        public List<object> Action { get; set; } = new List<object>();

        [YamlMember(Alias = "mode")]
        public string Mode { get; set; } = "Single";


        internal class TriggerType
        {
            [YamlMember(Alias = "from", ScalarStyle = ScalarStyle.SingleQuoted)]
            public string? From { get; set; }
            
            [YamlMember(Alias = "to", ScalarStyle = ScalarStyle.SingleQuoted)]
            public string? To { get; set; }
            
            [YamlMember(Alias = "platform")]
            public string? Platform { get; set; }
            
            [YamlMember(Alias = "type")]
            public string? Type { get; set; }
            
            [YamlMember(Alias = "device_id")]
            public string? DeviceId { get; set; }
            
            [YamlMember(Alias = "entity_id")]
            public string? EntityId { get; set; }

            [YamlMember(Alias = "domain")]
            public string? Domain { get; set; }

            [YamlMember(Alias = "at")]
            public string? At { get; set; }

        }
    }
}
