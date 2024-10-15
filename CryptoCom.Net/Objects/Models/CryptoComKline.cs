using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComKlineWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<CryptoComKline> Data { get; set; } = Array.Empty<CryptoComKline>();
    }

    /// <summary>
    /// Kline/candlestick data
    /// </summary>
    public record CryptoComKline
    {
        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Open timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime OpenTime { get; set; }
    }


}
