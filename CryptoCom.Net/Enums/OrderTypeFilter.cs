using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Order type filter
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderTypeFilter>))]
    public enum OrderTypeFilter
    {
        /// <summary>
        /// Limit orders
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// Trigger orders
        /// </summary>
        [Map("TRIGGER")]
        Trigger,
        /// <summary>
        /// All types
        /// </summary>
        [Map("ALL")]
        All
    }
}
