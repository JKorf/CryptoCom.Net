using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComExpiredPriceWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComExpiredPrice[] Data { get; set; } = Array.Empty<CryptoComExpiredPrice>();
    }

    /// <summary>
    /// Price info
    /// </summary>
    [SerializationModel]
    public record CryptoComExpiredPrice
    {
        /// <summary>
        /// ["<c>i</c>"] Symbol
        /// </summary>
        [JsonPropertyName("i")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>x</c>"] Expire timestamp
        /// </summary>
        [JsonPropertyName("x")]
        public DateTime ExpireTime { get; set; }
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
