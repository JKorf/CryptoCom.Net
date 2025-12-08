using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComTickersWrapper
    {
        [JsonPropertyName("data")]
        public CryptoComTicker[] Tickers { get; set; } = [];
    }

    /// <summary>
    /// Ticker info
    /// </summary>
    [SerializationModel]
    public record CryptoComTicker
    {
        /// <summary>
        /// High price in last 24 hours
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// Low price in last 24 hours
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("a")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("i")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Volume in last 24 hours
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Volume in last 24 hours in USD
        /// </summary>
        [JsonPropertyName("vv")]
        public decimal? VolumeUsd { get; set; }
        /// <summary>
        /// Current open interest
        /// </summary>
        [JsonPropertyName("oi")]
        public decimal? OpenInterest { get; set; }
        /// <summary>
        /// Price change factor since 24 hours ago, 0.01 = 1%
        /// </summary>
        [JsonPropertyName("c")]
        public decimal? PriceChange { get; set; }
        /// <summary>
        /// Current best bid price
        /// </summary>
        [JsonPropertyName("b")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// Current best bid quantity
        /// </summary>
        [JsonPropertyName("bs")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// Current best ask price
        /// </summary>
        [JsonPropertyName("k")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// Current best ask quantity
        /// </summary>
        [JsonPropertyName("ks")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }


}
