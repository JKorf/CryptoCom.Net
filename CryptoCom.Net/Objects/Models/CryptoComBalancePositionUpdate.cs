using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Balance and position update
    /// </summary>
    [SerializationModel]
    public record CryptoComBalancePositionUpdate
    {
        /// <summary>
        /// ["<c>balances</c>"] Balances
        /// </summary>
        [JsonPropertyName("balances")]
        public CryptoComBalanceUpdate[] Balances { get; set; } = [];
        /// <summary>
        /// ["<c>positions</c>"] Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public CryptoComPosition[] Positions { get; set; } = [];
    }

    /// <summary>
    /// Balance update
    /// </summary>
    [SerializationModel]
    public record CryptoComBalanceUpdate
    {
        /// <summary>
        /// ["<c>instrument_name</c>"] Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quantity</c>"] Balance
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>update_timestamp_ms</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_timestamp_ms")]
        public DateTime UpdateTime { get; set; }
    }
}
