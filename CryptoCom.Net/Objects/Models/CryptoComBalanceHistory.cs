using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Balance history info
    /// </summary>
    [SerializationModel]
    public record CryptoComBalanceHistory
    {
        /// <summary>
        /// ["<c>instrument_name</c>"] Asset name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComBalanceHistoryValue[] Data { get; set; } = Array.Empty<CryptoComBalanceHistoryValue>();
    }

    /// <summary>
    /// History value
    /// </summary>
    [SerializationModel]
    public record CryptoComBalanceHistoryValue
    {
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Balance
        /// </summary>
        [JsonPropertyName("c")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>i</c>"] Isolated position balance
        /// </summary>
        [JsonPropertyName("i")]
        public decimal IsolatedBalance { get; set; }
    }


}
