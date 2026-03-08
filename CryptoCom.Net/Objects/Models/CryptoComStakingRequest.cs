using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComStakingRequestWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComStakingRequest[] Data { get; set; } = Array.Empty<CryptoComStakingRequest>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CryptoComStakingRequest
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
        public string UnderlyingAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>cycle_id</c>"] Cycle id
        /// </summary>
        [JsonPropertyName("cycle_id")]
        public string CycleId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>staking_id</c>"] Staking id
        /// </summary>
        [JsonPropertyName("staking_id")]
        public string StakingId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public StakeRequestStatus StakeRequestStatus { get; set; }
        /// <summary>
        /// ["<c>account</c>"] Account
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public StakeSide StakeSide { get; set; }
        /// <summary>
        /// ["<c>create_timestamp_ms</c>"] Create timestamp
        /// </summary>
        [JsonPropertyName("create_timestamp_ms")]
        public DateTime CreateTime { get; set; }
    }


}
