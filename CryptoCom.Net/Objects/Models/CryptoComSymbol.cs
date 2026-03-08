using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComSymbolWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComSymbol[] Data { get; set; } = Array.Empty<CryptoComSymbol>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CryptoComSymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>inst_type</c>"] Symbol type
        /// </summary>
        [JsonPropertyName("inst_type")]
        public SymbolType SymbolType { get; set; }
        /// <summary>
        /// ["<c>display_name</c>"] Display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>base_ccy</c>"] Base asset
        /// </summary>
        [JsonPropertyName("base_ccy")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote_ccy</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quote_ccy")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote_decimals</c>"] Price decimals
        /// </summary>
        [JsonPropertyName("quote_decimals")]
        public int PriceDecimals { get; set; }
        /// <summary>
        /// ["<c>quantity_decimals</c>"] Quantity decimals
        /// </summary>
        [JsonPropertyName("quantity_decimals")]
        public int QuantityDecimals { get; set; }
        /// <summary>
        /// ["<c>price_tick_size</c>"] Price step
        /// </summary>
        [JsonPropertyName("price_tick_size")]
        public decimal PriceTickSize { get; set; }
        /// <summary>
        /// ["<c>qty_tick_size</c>"] Quantity step
        /// </summary>
        [JsonPropertyName("qty_tick_size")]
        public decimal QuantityTickSize { get; set; }
        /// <summary>
        /// ["<c>max_leverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>tradable</c>"] Tradable
        /// </summary>
        [JsonPropertyName("tradable")]
        public bool Tradable { get; set; }
        /// <summary>
        /// ["<c>expiry_timestamp_ms</c>"] Expiry timestamp
        /// </summary>
        [JsonPropertyName("expiry_timestamp_ms")]
        public DateTime ExpiryTime { get; set; }
        /// <summary>
        /// ["<c>underlying_symbol</c>"] Underlying symbol
        /// </summary>
        [JsonPropertyName("underlying_symbol")]
        public string? UnderlyingSymbol { get; set; }
    }


}
