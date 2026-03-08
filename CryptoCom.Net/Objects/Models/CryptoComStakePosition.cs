using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComStakePositionWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComStakePosition[] Data { get; set; } = Array.Empty<CryptoComStakePosition>();
    }

    /// <summary>
    /// Staking position info
    /// </summary>
    [SerializationModel]
    public record CryptoComStakePosition
    {
        /// <summary>
        /// ["<c>instrument_name</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>underlying_inst_name</c>"] Underlying asset name
        /// </summary>
        [JsonPropertyName("underlying_inst_name")]
        public string UnderlyingAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>staked_quantity</c>"] Staked quantity
        /// </summary>
        [JsonPropertyName("staked_quantity")]
        public decimal StakedQuantity { get; set; }
        /// <summary>
        /// ["<c>pending_staked_quantity</c>"] Pending staked quantity
        /// </summary>
        [JsonPropertyName("pending_staked_quantity")]
        public decimal PendingStakedQuantity { get; set; }
        /// <summary>
        /// ["<c>pending_unstaked_quantity</c>"] Pending unstaked quantity
        /// </summary>
        [JsonPropertyName("pending_unstaked_quantity")]
        public decimal PendingUnstakedQuantity { get; set; }
        /// <summary>
        /// ["<c>reward_eligible_quantity</c>"] Reward eligible quantity
        /// </summary>
        [JsonPropertyName("reward_eligible_quantity")]
        public decimal RewardEligibleQuantity { get; set; }
    }


}
