using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComOrderBookWrapper
    {
        [JsonPropertyName("depth")]
        public int Depth { get; set; }
        [JsonPropertyName("data")]
        public CryptoComOrderBook[] Data { get; set; } = [];
    }

    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
    public record CryptoComOrderBook
    {
        /// <summary>
        /// ["<c>asks</c>"] Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public CryptoComOrderBookEntry[] Asks { get; set; } = [];
        /// <summary>
        /// ["<c>bids</c>"] Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public CryptoComOrderBookEntry[] Bids { get; set; } = [];
    }

    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
    public record CryptoComOrderBookUpdate: CryptoComOrderBook
    {
        /// <summary>
        /// ["<c>t</c>"] Time of the event
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime MessageTime { get; set; }
        /// <summary>
        /// ["<c>tt</c>"] Book update time
        /// </summary>
        [JsonPropertyName("tt")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>u</c>"] Sequence number
        /// </summary>
        [JsonPropertyName("u")]
        public long SequenceNumber { get; set; }
        /// <summary>
        /// ["<c>pu</c>"] Sequence number
        /// </summary>
        [JsonPropertyName("pu")]
        public long? PreviousSequenceNumber { get; set; }
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<CryptoComOrderBookEntry>))]
    [SerializationModel]
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
