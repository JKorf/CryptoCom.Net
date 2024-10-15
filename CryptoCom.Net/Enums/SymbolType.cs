using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Symbol type
    /// </summary>
    public enum SymbolType
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("CCY_PAIR")]
        Spot,
        /// <summary>
        /// Perpetual swap
        /// </summary>
        [Map("PERPETUAL_SWAP")]
        PerpetualSwap,
        /// <summary>
        /// Future
        /// </summary>
        [Map("FUTURE")]
        DeliveryFuture
    }
}
