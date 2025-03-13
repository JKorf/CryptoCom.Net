using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Access type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccessType>))]
    public enum AccessType
    {
        /// <summary>
        /// Default
        /// </summary>
        [Map("DEFAULT")]
        Default,
        /// <summary>
        /// Disabled
        /// </summary>
        [Map("DISABLED")]
        Disabled
    }
}
