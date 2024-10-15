using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComValuationWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<CryptoComValuation> Data { get; set; } = Array.Empty<CryptoComValuation>();
    }

    /// <summary>
    /// Valuation
    /// </summary>
    public record CryptoComValuation
    {
        /// <summary>
        /// Value
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Value { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }


}
