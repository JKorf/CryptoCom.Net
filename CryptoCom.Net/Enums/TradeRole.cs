using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Trade role
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradeRole>))]
    public enum TradeRole
    {
        /// <summary>
        /// Maker
        /// </summary>
        [Map("MAKER")]
        Maker,
        /// <summary>
        /// Taker
        /// </summary>
        [Map("TAKER")]
        Taker
    }
}
