using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComValuationWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComValuation[] Data { get; set; } = Array.Empty<CryptoComValuation>();
    }

    /// <summary>
    /// Valuation
    /// </summary>
    [SerializationModel]
    public record CryptoComValuation
    {
        /// <summary>
        /// ["<c>v</c>"] Value
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Value { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }


}
