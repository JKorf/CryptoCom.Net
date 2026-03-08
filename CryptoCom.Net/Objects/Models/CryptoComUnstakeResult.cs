using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Unstake result
    /// </summary>
    [SerializationModel]
    public record CryptoComUnstakeResult
    {
        /// <summary>
        /// ["<c>staking_id</c>"] Staking id
        /// </summary>
        [JsonPropertyName("staking_id")]
        public string StakingId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>instrument_name</c>"] Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public UnstakeStatus UnstakeStatus { get; set; }
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>underlying_inst_name</c>"] Underlying asset
        /// </summary>
        [JsonPropertyName("underlying_inst_name")]
        public string UnderlyingAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>reason</c>"] Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }


}
