using CryptoCom.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComStakingRequestWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<CryptoComStakingRequest> Data { get; set; } = Array.Empty<CryptoComStakingRequest>();
    }

    /// <summary>
    /// 
    /// </summary>
    public record CryptoComStakingRequest
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
        public string UnderlyingAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Cycle id
        /// </summary>
        [JsonPropertyName("cycle_id")]
        public string CycleId { get; set; } = string.Empty;
        /// <summary>
        /// Staking id
        /// </summary>
        [JsonPropertyName("staking_id")]
        public string StakingId { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public StakeRequestStatus StakeRequestStatus { get; set; }
        /// <summary>
        /// Account
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public StakeSide StakeSide { get; set; }
        /// <summary>
        /// Create timestamp
        /// </summary>
        [JsonPropertyName("create_timestamp_ms")]
        public DateTime CreateTime { get; set; }
    }


}
