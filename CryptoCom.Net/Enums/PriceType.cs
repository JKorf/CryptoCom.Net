using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Price type
    /// </summary>
    public enum PriceType
    {
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
