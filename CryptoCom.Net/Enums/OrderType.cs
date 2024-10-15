using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
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
