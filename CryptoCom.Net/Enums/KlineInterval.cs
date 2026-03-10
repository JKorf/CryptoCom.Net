using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Interval for klines/candlesticks
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KlineInterval>))]
    public enum KlineInterval
    {
        /// <summary>
        /// ["<c>1m</c>"] One minute
        /// </summary>
        [Map("1m")]
        OneMinute = 60,
        /// <summary>
        /// ["<c>5m</c>"] Five minutes
        /// </summary>
        [Map("5m")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// ["<c>15m</c>"] Fifteen minutes
        /// </summary>
        [Map("15m")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// ["<c>30m</c>"] Thirty minutes
        /// </summary>
        [Map("30m")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// ["<c>1h</c>"] One hour
        /// </summary>
        [Map("1h")]
        OneHour = 60 * 60,
        /// <summary>
        /// ["<c>2h</c>"] Two hours
        /// </summary>
        [Map("2h")]
        TwoHours = 60 * 60 * 2,
        /// <summary>
        /// ["<c>4h</c>"] Four hours
        /// </summary>
        [Map("4h")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// ["<c>12h</c>"] Twelve hours
        /// </summary>
        [Map("12h")]
        TwelveHours = 60 * 60 * 12,
        /// <summary>
        /// ["<c>1D</c>"] One day
        /// </summary>
        [Map("1D")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// ["<c>7D</c>"] One week
        /// </summary>
        [Map("7D")]
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// ["<c>14D</c>"] Two weeks
        /// </summary>
        [Map("14D")]
        TwoWeeks = 60 * 60 * 24 * 14,
        /// <summary>
        /// ["<c>1M</c>"] One month
        /// </summary>
        [Map("1M")]
        OneMonth = 60 * 60 * 24 * 30
    }
}
