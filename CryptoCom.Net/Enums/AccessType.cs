using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Access type
    /// </summary>
    public enum AccessType
    {
        /// <summary>
        /// Default
        /// </summary>
        [Map("DEFAULT")]
        Default,
        /// <summary>
        /// Disabled
        /// </summary>
        [Map("DISABLED")]
        Disabled
    }
}
