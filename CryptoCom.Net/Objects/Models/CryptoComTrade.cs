using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComTradeWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComTrade[] Data { get; set; } = Array.Empty<CryptoComTrade>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CryptoComTrade
    {
        /// <summary>
        /// ["<c>d</c>"] Trade id
        /// </summary>
        [JsonPropertyName("d")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>t</c>"] Trade timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Trade quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Trade price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Trade side
        /// </summary>
        [JsonPropertyName("s")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>i</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("i")]
        public string Symbol { get; set; } = string.Empty;
    }


}
