using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComStakePositionWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<CryptoComStakePosition> Data { get; set; } = Array.Empty<CryptoComStakePosition>();
    }

    /// <summary>
    /// Staking position info
    /// </summary>
    public record CryptoComStakePosition
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Underlying asset name
        /// </summary>
        [JsonPropertyName("underlying_inst_name")]
        public string UnderlyingAsset { get; set; } = string.Empty;
        /// <summary>
        /// Staked quantity
        /// </summary>
        [JsonPropertyName("staked_quantity")]
        public decimal StakedQuantity { get; set; }
        /// <summary>
        /// Pending staked quantity
        /// </summary>
        [JsonPropertyName("pending_staked_quantity")]
        public decimal PendingStakedQuantity { get; set; }
        /// <summary>
        /// Pending unstaked quantity
        /// </summary>
        [JsonPropertyName("pending_unstaked_quantity")]
        public decimal PendingUnstakedQuantity { get; set; }
        /// <summary>
        /// Reward eligible quantity
        /// </summary>
        [JsonPropertyName("reward_eligible_quantity")]
        public decimal RewardEligibleQuantity { get; set; }
    }


}
