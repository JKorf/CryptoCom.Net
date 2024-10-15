using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Balance history info
    /// </summary>
    public record CryptoComBalanceHistory
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<CryptoComBalanceHistoryValue> Data { get; set; } = Array.Empty<CryptoComBalanceHistoryValue>();
    }

    /// <summary>
    /// History value
    /// </summary>
    public record CryptoComBalanceHistoryValue
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        [JsonPropertyName("c")]
        public decimal Balance { get; set; }
    }


}
