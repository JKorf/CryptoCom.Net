using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComTransactionWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComTransaction[] Data { get; set; } = Array.Empty<CryptoComTransaction>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CryptoComTransaction
    {
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Isolation id
        /// </summary>
        [JsonPropertyName("isolation_id")]
        public string? IsolationId { get; set; }
        /// <summary>
        /// Event date
        /// </summary>
        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; set; }
        /// <summary>
        /// Transaction type
        /// </summary>
        [JsonPropertyName("journal_type")]
        public TransactionType TransactionType { get; set; }
        /// <summary>
        /// Journal id
        /// </summary>
        [JsonPropertyName("journal_id")]
        public string JournalId { get; set; } = string.Empty;
        /// <summary>
        /// Transaction quantity
        /// </summary>
        [JsonPropertyName("transaction_qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Transaction cost
        /// </summary>
        [JsonPropertyName("transaction_cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("realized_pnl")]
        public decimal? RealizedPnl { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public string? TradeId { get; set; }
        /// <summary>
        /// Trade match id
        /// </summary>
        [JsonPropertyName("trade_match_id")]
        public string? TradeMatchId { get; set; }
        /// <summary>
        /// Event timestamp
        /// </summary>
        [JsonPropertyName("event_timestamp_ns")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_oid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Trade role
        /// </summary>
        [JsonPropertyName("taker_side")]
        public TradeRole? TradeRole { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide? OrderSide { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
    }


}
