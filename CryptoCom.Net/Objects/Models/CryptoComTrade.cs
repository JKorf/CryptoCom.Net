using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComTradeWrapper
    {
        /// <summary>
        /// Data
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
        /// Trade id
        /// </summary>
        [JsonPropertyName("d")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// Trade timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Trade quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Trade side
        /// </summary>
        [JsonPropertyName("s")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("i")]
        public string Symbol { get; set; } = string.Empty;
    }


}
