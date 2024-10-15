using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// STP scope
    /// </summary>
    public enum SelfTradePreventionScope
    {
        /// <summary>
        /// Matches master or sub a/c
        /// </summary>
        [Map("M")]
        MasterAndSubAccount,
        /// <summary>
        /// Matches sub a/c only
        /// </summary>
        [Map("S")]
        SubAccount,
    }

}
