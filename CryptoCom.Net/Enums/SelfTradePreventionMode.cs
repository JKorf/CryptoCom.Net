using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    public enum SelfTradePreventionMode
    {
        /// <summary>
        /// Cancel maker
        /// </summary>
        [Map("M")]
        CancelMaker,
        /// <summary>
        /// Cancel taker
        /// </summary>
        [Map("T")]
        CancelTaker,
        /// <summary>
        /// Cancel both maker and taker
        /// </summary>
        [Map("B")]
        CancelBoth,
    }

}
