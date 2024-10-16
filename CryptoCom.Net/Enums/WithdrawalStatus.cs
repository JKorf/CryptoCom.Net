using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Withdrawal status
    /// </summary>
    public enum WithdrawalStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        [Map("0")]
        Pending,
        /// <summary>
        /// Processing
        /// </summary>
        [Map("1")]
        Processing,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("2")]
        Rejected,
        /// <summary>
        /// Payment in-progress
        /// </summary>
        [Map("3")]
        PaymentInProgress,
        /// <summary>
        /// Payment failed
        /// </summary>
        [Map("4")]
        PaymentFailed,
        /// <summary>
        /// Completed
        /// </summary>
        [Map("5")]
        Completed,
        /// <summary>
        /// Cancelled
        /// </summary>
        [Map("6")]
        Canceled
    }

}
