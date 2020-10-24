using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Hasso.Models
{
    internal class Trigger
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
