using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Staking status
    /// </summary>
    public enum StakeStatus
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
        /// Staked
        /// </summary>
        [Map("STAKED")]
        Staked,
        /// <summary>
        /// Completed
        /// </summary>
        [Map("COMPLETED")]
        Completed,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
    }

}
