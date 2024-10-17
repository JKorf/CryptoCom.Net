﻿using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Staking request status
    /// </summary>
    public enum StakeRequestStatus
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
        /// Pending withdrawal
        /// </summary>
        [Map("PENDING_WITHDRAWAL")]
        PendingWithdrawal,
        /// <summary>
        /// Pending unstaking
        /// </summary>
        [Map("PENDING_UNSTAKING")]
        PendingUnstaking,
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
        Rejected
    }

}