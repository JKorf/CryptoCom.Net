using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Timeframe
    /// </summary>
    [JsonConverter(typeof(EnumConverter<Timeframe>))]
    public enum Timeframe
    {
        /// <summary>
        /// One hour
        /// </summary>
        [Map("H1")]
        OneHour,
        /// <summary>
        /// One day
        /// </summary>
        [Map("D1")]
        OneDay
    }
}
