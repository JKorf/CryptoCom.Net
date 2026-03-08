using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComStakingRewardWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComStakingReward[] Data { get; set; } = Array.Empty<CryptoComStakingReward>();
    }

    /// <summary>
    /// Reward info
    /// </summary>
    [SerializationModel]
    public record CryptoComStakingReward
    {
        /// <summary>
        /// ["<c>staking_inst_name</c>"] Staking symbol name
        /// </summary>
        [JsonPropertyName("staking_inst_name")]
        public string StakingSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>underlying_inst_name</c>"] Underlying asset name
        /// </summary>
        [JsonPropertyName("underlying_inst_name")]
        public string UnderlyingAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>reward_inst_name</c>"] Reward name
        /// </summary>
        [JsonPropertyName("reward_inst_name")]
        public string RewardName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>reward_quantity</c>"] Reward quantity
        /// </summary>
        [JsonPropertyName("reward_quantity")]
        public decimal RewardQuantity { get; set; }
        /// <summary>
        /// ["<c>staked_balance</c>"] Staked balance
        /// </summary>
        [JsonPropertyName("staked_balance")]
        public decimal StakedBalance { get; set; }
        /// <summary>
        /// ["<c>event_timestamp_ms</c>"] Event timestamp
        /// </summary>
        [JsonPropertyName("event_timestamp_ms")]
        public DateTime EventTime { get; set; }
    }


}
