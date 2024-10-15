using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Order type filter
    /// </summary>
    public enum OrderTypeFilter
    {
        /// <summary>
        /// Limit orders
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// Trigger orders
        /// </summary>
        [Map("TRIGGER")]
        Trigger,
        /// <summary>
        /// All types
        /// </summary>
        [Map("ALL")]
        All
    }
}
