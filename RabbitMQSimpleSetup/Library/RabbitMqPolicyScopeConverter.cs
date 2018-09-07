using Newtonsoft.Json;
using System;

namespace RabbitMQSimpleSetup.Library {
    public sealed class RabbitMqPolicyScopeConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteRawValue($"\"{(RabbitMqPolicyScope)value}\"");
            writer.Flush();
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            return reader.Value != null ? RabbitMqPolicyScope.Parse(reader.Value.ToString()) : null;
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(RabbitMqPolicyScope);
        }
    }
}
