using CryptoCom.Net.Converters;
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
        /// Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public CryptoComOrderBookEntry[] Asks { get; set; } = [];
        /// <summary>
        /// Bids
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
        /// Time of the event
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime MessageTime { get; set; }
        /// <summary>
        /// Book update time
        /// </summary>
        [JsonPropertyName("tt")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Sequence number
        /// </summary>
        [JsonPropertyName("u")]
        public long SequenceNumber { get; set; }
        /// <summary>
        /// Sequence number
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
