using CryptoCom.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComSymbolWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<CryptoComSymbol> Data { get; set; } = Array.Empty<CryptoComSymbol>();
    }

    /// <summary>
    /// 
    /// </summary>
    public record CryptoComSymbol
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Symbol type
        /// </summary>
        [JsonPropertyName("inst_type")]
        public SymbolType SymbolType { get; set; }
        /// <summary>
        /// Display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("base_ccy")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quote_ccy")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Price decimals
        /// </summary>
        [JsonPropertyName("quote_decimals")]
        public decimal QuoteDecimals { get; set; }
        /// <summary>
        /// Quantity decimals
        /// </summary>
        [JsonPropertyName("quantity_decimals")]
        public decimal QuantityDecimals { get; set; }
        /// <summary>
        /// Price step
        /// </summary>
        [JsonPropertyName("price_tick_size")]
        public decimal PriceTickQuantity { get; set; }
        /// <summary>
        /// Quantity step
        /// </summary>
        [JsonPropertyName("qty_tick_size")]
        public decimal QuantityTickQuantity { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Tradable
        /// </summary>
        [JsonPropertyName("tradable")]
        public bool Tradable { get; set; }
        /// <summary>
        /// Expiry timestamp
        /// </summary>
        [JsonPropertyName("expiry_timestamp_ms")]
        public DateTime ExpiryTime { get; set; }
        /// <summary>
        /// Underlying symbol
        /// </summary>
        [JsonPropertyName("underlying_symbol")]
        public string? UnderlyingSymbol { get; set; }
    }


}
