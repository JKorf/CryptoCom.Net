using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Deposit status
    /// </summary>
    public enum DepositStatus
    {
        /// <summary>
        /// Not arrived
        /// </summary>
        [Map("0")]
        NotArrived,
        /// <summary>
        /// Arrived
        /// </summary>
        [Map("1")]
        Arrived,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("2")]
        Failed,
        /// <summary>
        /// Pending
        /// </summary>
        [Map("3")]
        Pending
    }
}
