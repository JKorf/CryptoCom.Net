using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Active
        /// </summary>
        [Map("ACTIVE")]
        Active,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,

    }

}
