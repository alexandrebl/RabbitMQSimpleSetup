using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RabbitMQSimpleSetup.Domain.Shovel {

    public sealed class DeleteAfterJsonConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteRawValue($"\"{(DeleteAfter)value}\"");
            writer.Flush();
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            return reader.Value != null ? DeleteAfter.Parse(reader.Value.ToString()) : null;
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(DeleteAfter);
        }
    }
}
