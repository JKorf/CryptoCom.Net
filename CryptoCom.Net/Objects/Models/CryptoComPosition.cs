using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComPositionWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComPosition[] Data { get; set; } = Array.Empty<CryptoComPosition>();
    }

    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record CryptoComPosition
    {
        /// <summary>
        /// ["<c>account_id</c>"] Account id
        /// </summary>
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isolation_id</c>"] Id of isolated position
        /// </summary>
        [JsonPropertyName("isolation_id")]
        public string? IsolationId { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Isolated position max leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int? Leverage { get; set; }
        /// <summary>
        /// ["<c>liquidation_price</c>"] Isolated position liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>isolated_margin_balance</c>"] Isolated position margin balance
        /// </summary>
        [JsonPropertyName("isolated_margin_balance")]
        public decimal? IsolatedMarginBalance { get; set; }
        /// <summary>
        /// ["<c>quantity</c>"] Position size
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>cost</c>"] Cost
        /// </summary>
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// ["<c>open_position_pnl</c>"] Open position profit and loss
        /// </summary>
        [JsonPropertyName("open_position_pnl")]
        public decimal OpenPositionPnl { get; set; }
        /// <summary>
        /// ["<c>open_pos_cost</c>"] Open pos cost
        /// </summary>
        [JsonPropertyName("open_pos_cost")]
        public decimal OpenPosCost { get; set; }
        /// <summary>
        /// ["<c>session_pnl</c>"] Session profit and loss
        /// </summary>
        [JsonPropertyName("session_pnl")]
        public decimal SessionPnl { get; set; }
        /// <summary>
        /// ["<c>update_timestamp_ms</c>"] Update timestamp
        /// </summary>
        [JsonPropertyName("update_timestamp_ms")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>instrument_name</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public SymbolType SymbolType { get; set; }
    }


}
