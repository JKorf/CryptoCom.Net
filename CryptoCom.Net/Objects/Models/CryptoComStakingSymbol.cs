using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComStakingSymbolWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComStakingSymbol[] Data { get; set; } = Array.Empty<CryptoComStakingSymbol>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CryptoComStakingSymbol
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
        /// Reward name
        /// </summary>
        [JsonPropertyName("reward_inst_name")]
        public string RewardName { get; set; } = string.Empty;
        /// <summary>
        /// Out of stock
        /// </summary>
        [JsonPropertyName("out_of_stock")]
        public bool OutOfStock { get; set; }
        /// <summary>
        /// Block unstake
        /// </summary>
        [JsonPropertyName("block_unstake")]
        public bool BlockUnstake { get; set; }
        /// <summary>
        /// Estimated rewards
        /// </summary>
        [JsonPropertyName("est_rewards")]
        public decimal EstimatedRewards { get; set; }
        /// <summary>
        /// Whether the rewards are APR or APY
        /// </summary>
        [JsonPropertyName("apr_y")]
        public string AprOrApy { get; set; } = string.Empty;
        /// <summary>
        /// Min stake quantity
        /// </summary>
        [JsonPropertyName("min_stake_amt")]
        public decimal MinStakeQuantity { get; set; }
        /// <summary>
        /// Reward frequency in days
        /// </summary>
        [JsonPropertyName("reward_frequency")]
        public decimal RewardFrequency { get; set; }
        /// <summary>
        /// Lock up period in days
        /// </summary>
        [JsonPropertyName("lock_up_period")]
        public decimal LockUpPeriod { get; set; }
        /// <summary>
        /// Is compound reward
        /// </summary>
        [JsonPropertyName("is_compound_reward")]
        public bool IsCompoundReward { get; set; }
        /// <summary>
        /// Pre stake charge enable
        /// </summary>
        [JsonPropertyName("pre_stake_charge_enable")]
        public bool PreStakeChargeEnable { get; set; }
        /// <summary>
        /// Pre stake charge rate in bps
        /// </summary>
        [JsonPropertyName("pre_stake_charge_rate_in_bps")]
        public decimal PreStakeChargeRateInBps { get; set; }
        /// <summary>
        /// Is restaked
        /// </summary>
        [JsonPropertyName("is_restaked")]
        public bool IsRestaked { get; set; }
    }


}
