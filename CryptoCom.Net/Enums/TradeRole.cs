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
        /// ["<c>MAKER</c>"] Maker
        /// </summary>
        [Map("MAKER")]
        Maker,
        /// <summary>
        /// ["<c>TAKER</c>"] Taker
        /// </summary>
        [Map("TAKER")]
        Taker
    }
}
