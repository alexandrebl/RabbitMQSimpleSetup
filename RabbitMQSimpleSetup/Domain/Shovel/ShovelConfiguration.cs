using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RabbitMQSimpleSetup.Domain.Shovel {
    public sealed class ShovelConfiguration {
        [JsonProperty(PropertyName = "value")]
        public ShovelConfigurationContent Value { get; }

        public ShovelConfiguration(ShovelConfigurationContent content) {
            Value = content;
        }
    }
}
