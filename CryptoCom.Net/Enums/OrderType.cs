using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// Limit
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// Market
        /// </summary>
        [Map("MARKET")]
        Market,
        /// <summary>
        /// Stop loss
        /// </summary>
        [Map("STOP_LOSS")]
        StopLoss,
        /// <summary>
        /// Stop limit
        /// </summary>
        [Map("STOP_LIMIT")]
        StopLimit,
        /// <summary>
        /// Take profit
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfit,
        /// <summary>
        /// Take profit limit
        /// </summary>
        [Map("TAKE_PROFIT_LIMIT")]
        TakeProfitLimit,
    }

}
