using Newtonsoft.Json;
using RabbitMQSimpleSetup.Library;
using System.Collections.Generic;

namespace RabbitMQSimpleSetup.Domain {
    public class RabbitMqVirtualHostPolicy {
        [JsonProperty(PropertyName = "pattern")]
        public string Pattern { get; }

        [JsonProperty(PropertyName = "definition")]
        public Dictionary<string, object> Definition { get; }

        [JsonProperty(PropertyName = "priority")]
        public byte Priority { get; }

        [JsonProperty(PropertyName = "apply-to"), JsonConverter(typeof(RabbitMqPolicyScopeConverter))]
        public RabbitMqPolicyScope ApplyTo { get; }

        public RabbitMqVirtualHostPolicy(string pattern, Dictionary<string, object> definition, byte priority = 0B0, RabbitMqPolicyScope applyTo = null) {
            Pattern = pattern;
            Definition = definition;
            Priority = priority;
            ApplyTo = applyTo ?? RabbitMqPolicyScope.All;
        }
    }
}
