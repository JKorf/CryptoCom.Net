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
        /// ["<c>h</c>"] High price in last 24 hours
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Low price in last 24 hours
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Last price
        /// </summary>
        [JsonPropertyName("a")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>i</c>"] Symbol
        /// </summary>
        [JsonPropertyName("i")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>v</c>"] Volume in last 24 hours
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>vv</c>"] Volume in last 24 hours in USD
        /// </summary>
        [JsonPropertyName("vv")]
        public decimal? VolumeUsd { get; set; }
        /// <summary>
        /// ["<c>oi</c>"] Current open interest
        /// </summary>
        [JsonPropertyName("oi")]
        public decimal? OpenInterest { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Price change factor since 24 hours ago, 0.01 = 1%
        /// </summary>
        [JsonPropertyName("c")]
        public decimal? PriceChange { get; set; }
        /// <summary>
        /// ["<c>b</c>"] Current best bid price
        /// </summary>
        [JsonPropertyName("b")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bs</c>"] Current best bid quantity
        /// </summary>
        [JsonPropertyName("bs")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>k</c>"] Current best ask price
        /// </summary>
        [JsonPropertyName("k")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>ks</c>"] Current best ask quantity
        /// </summary>
        [JsonPropertyName("ks")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }


}
