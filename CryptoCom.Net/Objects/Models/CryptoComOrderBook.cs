using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComOrderBookWrapper
    {
        [JsonPropertyName("depth")]
        public int Depth { get; set; }
        [JsonPropertyName("data")]
        public IEnumerable<CryptoComOrderBook> Data { get; set; } = [];
    }

    /// <summary>
    /// Order book info
    /// </summary>
    public record CryptoComOrderBook
    {
        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public IEnumerable<CryptoComOrderBookEntry> Asks { get; set; } = [];
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public IEnumerable<CryptoComOrderBookEntry> Bids { get; set; } = [];
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public record CryptoComOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// Price level
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Number of orders
        /// </summary>
        [ArrayProperty(2)]
        public int OrderCount { get; set; }
    }
}
