using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Trade role
    /// </summary>
    public enum TradeRole
    {
        /// <summary>
        /// Maker
        /// </summary>
        [Map("MAKER")]
        Maker,
        /// <summary>
        /// Taker
        /// </summary>
        [Map("TAKER")]
        Taker
    }
}
