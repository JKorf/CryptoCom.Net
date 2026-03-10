using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// ["<c>NULL_VAL</c>"] Unknown
        /// </summary>
        [Map("NULL_VAL")]
        Unknown,
        /// <summary>
        /// ["<c>BUY</c>"] Buy
        /// </summary>
        [Map("BUY")]
        Buy,
        /// <summary>
        /// ["<c>SELL</c>"] Sell
        /// </summary>
        [Map("SELL")]
        Sell
    }
}
