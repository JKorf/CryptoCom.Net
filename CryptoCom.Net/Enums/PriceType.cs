using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PriceType>))]
    public enum PriceType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("NULL_VAL")]
        Unknown,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("MARK_PRICE")]
        MarkPrice,
        /// <summary>
        /// Index price
        /// </summary>
        [Map("INDEX_PRICE")]
        IndexPrice,
        /// <summary>
        /// Last price
        /// </summary>
        [Map("LAST_PRICE")]
        LastPrice,
    }

}
