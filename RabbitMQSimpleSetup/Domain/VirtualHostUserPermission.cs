using Newtonsoft.Json;

namespace RabbitMQSimpleSetup.Domain {
    public sealed class VirtualHostUserPermission {
        [JsonProperty(PropertyName = "configure")]
        public string Configure { get; set; }

        [JsonProperty(PropertyName = "write")]
        public string Write { get; set; }

        [JsonProperty(PropertyName = "read")]
        public string Read { get; set; }
    }
}
