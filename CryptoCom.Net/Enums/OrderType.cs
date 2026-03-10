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
        /// ["<c>LIMIT</c>"] Limit
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// ["<c>MARKET</c>"] Market
        /// </summary>
        [Map("MARKET")]
        Market,
        /// <summary>
        /// ["<c>STOP_LOSS</c>"] Stop loss
        /// </summary>
        [Map("STOP_LOSS")]
        StopLoss,
        /// <summary>
        /// ["<c>STOP_LIMIT</c>"] Stop limit
        /// </summary>
        [Map("STOP_LIMIT")]
        StopLimit,
        /// <summary>
        /// ["<c>TAKE_PROFIT</c>"] Take profit
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfit,
        /// <summary>
        /// ["<c>TAKE_PROFIT_LIMIT</c>"] Take profit limit
        /// </summary>
        [Map("TAKE_PROFIT_LIMIT")]
        TakeProfitLimit,
    }

}
