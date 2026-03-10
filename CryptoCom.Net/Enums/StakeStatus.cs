using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Staking status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StakeStatus>))]
    public enum StakeStatus
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
        Rejected,
    }

}
