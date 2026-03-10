using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Staking request status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StakeRequestStatus>))]
    public enum StakeRequestStatus
    {
        /// <summary>
        /// ["<c>NEW</c>"] New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// ["<c>PENDING</c>"] Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// ["<c>PENDING_WITHDRAWAL</c>"] Pending withdrawal
        /// </summary>
        [Map("PENDING_WITHDRAWAL")]
        PendingWithdrawal,
        /// <summary>
        /// ["<c>PENDING_UNSTAKING</c>"] Pending unstaking
        /// </summary>
        [Map("PENDING_UNSTAKING")]
        PendingUnstaking,
        /// <summary>
        /// ["<c>STAKED</c>"] Staked
        /// </summary>
        [Map("STAKED")]
        Staked,
        /// <summary>
        /// ["<c>COMPLETED</c>"] Completed
        /// </summary>
        [Map("COMPLETED")]
        Completed,
        /// <summary>
        /// ["<c>REJECTED</c>"] Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected
    }

}
