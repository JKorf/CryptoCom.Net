using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Timeframe
    /// </summary>
    public enum Timeframe
    {
        /// <summary>
        /// One hour
        /// </summary>
        [Map("H1")]
        OneHour,
        /// <summary>
        /// One day
        /// </summary>
        [Map("D1")]
        OneDay
    }
}
