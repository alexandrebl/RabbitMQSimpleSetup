using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RabbitMQSimpleSetup.Domain.Shovel {
    public sealed class AckModeJsonConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteRawValue($"\"{(AckMode)value}\"");
            writer.Flush();
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            return reader.Value != null ? AckMode.Parse(reader.Value.ToString()) : null;
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(AckMode);
        }
    }
}
