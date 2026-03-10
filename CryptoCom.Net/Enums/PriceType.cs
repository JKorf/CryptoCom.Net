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
        /// ["<c>NULL_VAL</c>"] Unknown
        /// </summary>
        [Map("NULL_VAL")]
        Unknown,
        /// <summary>
        /// ["<c>MARK_PRICE</c>"] Mark price
        /// </summary>
        [Map("MARK_PRICE")]
        MarkPrice,
        /// <summary>
        /// ["<c>INDEX_PRICE</c>"] Index price
        /// </summary>
        [Map("INDEX_PRICE")]
        IndexPrice,
        /// <summary>
        /// ["<c>LAST_PRICE</c>"] Last price
        /// </summary>
        [Map("LAST_PRICE")]
        LastPrice,
    }

}
