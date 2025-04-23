using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CryptoCom.Net.Enums;

namespace CryptoCom.Net.Converters
{
    /// <summary>
    /// Custom converter to handle integer-to-enum conversion for OrderRejectedReason.
    /// </summary>
    public class OrderRejectedReasonConverter : JsonConverter<OrderRejectedReason?>
    {
        public override OrderRejectedReason? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out int intValue))
            {
                if (Enum.IsDefined(typeof(OrderRejectedReason), intValue))
                    return (OrderRejectedReason)intValue;
            }
            return null; // Return null if the value is not valid
        }

        public override void Write(Utf8JsonWriter writer, OrderRejectedReason? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteNumberValue((int)value.Value);
            else
                writer.WriteNullValue();
        }
    }
}
