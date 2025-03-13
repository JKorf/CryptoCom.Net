using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComUserTradeWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComUserTrade[] Data { get; set; } = Array.Empty<CryptoComUserTrade>();
    }

    /// <summary>
    /// User trade
    /// </summary>
    [SerializationModel]
    public record CryptoComUserTrade
    {
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Event date
        /// </summary>
        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; set; }
        /// <summary>
        /// Journal type
        /// </summary>
        [JsonPropertyName("journal_type")]
        public TransactionType TransactionType { get; set; }
        /// <summary>
        /// Trade quantity
        /// </summary>
        [JsonPropertyName("traded_quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("traded_price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fees")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// Trade match id
        /// </summary>
        [JsonPropertyName("trade_match_id")]
        public string TradeMatchId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_oid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Trade role
        /// </summary>
        [JsonPropertyName("taker_side")]
        public TradeRole Role { get; set; }
        /// <summary>
        /// Trade side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_instrument_name")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time_ns")]
        public DateTime CreateTime { get; set; }
    }


}
