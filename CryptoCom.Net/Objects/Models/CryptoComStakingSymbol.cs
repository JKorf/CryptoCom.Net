using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComStakingSymbolWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
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
        /// ["<c>reward_inst_name</c>"] Reward name
        /// </summary>
        [JsonPropertyName("reward_inst_name")]
        public string RewardName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>out_of_stock</c>"] Out of stock
        /// </summary>
        [JsonPropertyName("out_of_stock")]
        public bool OutOfStock { get; set; }
        /// <summary>
        /// ["<c>block_unstake</c>"] Block unstake
        /// </summary>
        [JsonPropertyName("block_unstake")]
        public bool BlockUnstake { get; set; }
        /// <summary>
        /// ["<c>est_rewards</c>"] Estimated rewards
        /// </summary>
        [JsonPropertyName("est_rewards")]
        public decimal EstimatedRewards { get; set; }
        /// <summary>
        /// ["<c>apr_y</c>"] Whether the rewards are APR or APY
        /// </summary>
        [JsonPropertyName("apr_y")]
        public string AprOrApy { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>min_stake_amt</c>"] Min stake quantity
        /// </summary>
        [JsonPropertyName("min_stake_amt")]
        public decimal MinStakeQuantity { get; set; }
        /// <summary>
        /// ["<c>reward_frequency</c>"] Reward frequency in days
        /// </summary>
        [JsonPropertyName("reward_frequency")]
        public decimal RewardFrequency { get; set; }
        /// <summary>
        /// ["<c>lock_up_period</c>"] Lock up period in days
        /// </summary>
        [JsonPropertyName("lock_up_period")]
        public decimal LockUpPeriod { get; set; }
        /// <summary>
        /// ["<c>is_compound_reward</c>"] Is compound reward
        /// </summary>
        [JsonPropertyName("is_compound_reward")]
        public bool IsCompoundReward { get; set; }
        /// <summary>
        /// ["<c>pre_stake_charge_enable</c>"] Pre stake charge enable
        /// </summary>
        [JsonPropertyName("pre_stake_charge_enable")]
        public bool PreStakeChargeEnable { get; set; }
        /// <summary>
        /// ["<c>pre_stake_charge_rate_in_bps</c>"] Pre stake charge rate in bps
        /// </summary>
        [JsonPropertyName("pre_stake_charge_rate_in_bps")]
        public decimal PreStakeChargeRateInBps { get; set; }
        /// <summary>
        /// ["<c>is_restaked</c>"] Is restaked
        /// </summary>
        [JsonPropertyName("is_restaked")]
        public bool IsRestaked { get; set; }
    }


}
