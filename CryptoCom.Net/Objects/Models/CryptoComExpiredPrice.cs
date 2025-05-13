using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComExpiredPriceWrapper
    {
        /// <summary>
        /// Data
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
        /// Symbol
        /// </summary>
        [JsonPropertyName("i")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Expire timestamp
        /// </summary>
        [JsonPropertyName("x")]
        public DateTime ExpireTime { get; set; }
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
