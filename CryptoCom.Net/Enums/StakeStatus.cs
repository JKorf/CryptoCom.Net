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
