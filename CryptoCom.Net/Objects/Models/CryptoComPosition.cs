using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComPositionWrapper
    {
        /// <summary>
        /// Data
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
        /// Account id
        /// </summary>
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Position size
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Cost
        /// </summary>
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// Open position profit and loss
        /// </summary>
        [JsonPropertyName("open_position_pnl")]
        public decimal OpenPositionPnl { get; set; }
        /// <summary>
        /// Open pos cost
        /// </summary>
        [JsonPropertyName("open_pos_cost")]
        public decimal OpenPosCost { get; set; }
        /// <summary>
        /// Session profit and loss
        /// </summary>
        [JsonPropertyName("session_pnl")]
        public decimal SessionPnl { get; set; }
        /// <summary>
        /// Update timestamp
        /// </summary>
        [JsonPropertyName("update_timestamp_ms")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public SymbolType SymbolType { get; set; }
    }


}
