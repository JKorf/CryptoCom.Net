using CryptoCom.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Staking request result
    /// </summary>
    public record CryptoComStakeResult
    {
        /// <summary>
        /// Staking id
        /// </summary>
        [JsonPropertyName("staking_id")]
        public string StakingId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public StakeStatus StakeStatus { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Underlying asset
        /// </summary>
        [JsonPropertyName("underlying_inst_name")]
        public string UnderlyingAsset { get; set; } = string.Empty;
        /// <summary>
        /// Pre stake charge rate in bps
        /// </summary>
        [JsonPropertyName("pre_stake_charge_rate_in_bps")]
        public decimal PreStakeChargeRateInBps { get; set; }
        /// <summary>
        /// Pre stake charge
        /// </summary>
        [JsonPropertyName("pre_stake_charge")]
        public decimal PreStakeCharge { get; set; }
        /// <summary>
        /// Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }


}
