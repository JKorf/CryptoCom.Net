using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Time in force
    /// </summary>
    public enum TimeInForce
    {
        /// <summary>
        /// Good till cancel
        /// </summary>
        [Map("GOOD_TILL_CANCEL")]
        GoodTillCancel,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("IMMEDIATE_OR_CANCEL")]
        ImmediateOrCancel,
        /// <summary>
        /// Fill or kill
        /// </summary>
        [Map("FILL_OR_KILL")]
        FillOrKill,
    }

}
