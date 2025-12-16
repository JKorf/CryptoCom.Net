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
        /// Balances
        /// </summary>
        [JsonPropertyName("balances")]
        public CryptoComBalanceUpdate[] Balances { get; set; } = [];
        /// <summary>
        /// Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public CryptoComPosition[] Positions { get; set; } = [];
    }

    /// <summary>
    /// Balanace update
    /// </summary>
    [SerializationModel]
    public record CryptoComBalanceUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Balance
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("update_timestamp_ms")]
        public DateTime UpdateTime { get; set; }
    }
}
