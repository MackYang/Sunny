using Newtonsoft.Json;
using System;

namespace Sunny.Common.JsonTypeConverter
{
    public class LongConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long) || objectType == typeof(long?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long result = 0;
            if (reader.Value != null)
            {
                long.TryParse(reader.Value.ToString(), out result);
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
