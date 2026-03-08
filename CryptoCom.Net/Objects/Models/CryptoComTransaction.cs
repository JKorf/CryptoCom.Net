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
        /// ["<c>data</c>"] Data
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
        /// ["<c>account_id</c>"] Account id
        /// </summary>
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isolation_id</c>"] Isolation id
        /// </summary>
        [JsonPropertyName("isolation_id")]
        public string? IsolationId { get; set; }
        /// <summary>
        /// ["<c>event_date</c>"] Event date
        /// </summary>
        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; set; }
        /// <summary>
        /// ["<c>journal_type</c>"] Transaction type
        /// </summary>
        [JsonPropertyName("journal_type")]
        public TransactionType TransactionType { get; set; }
        /// <summary>
        /// ["<c>journal_id</c>"] Journal id
        /// </summary>
        [JsonPropertyName("journal_id")]
        public string JournalId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transaction_qty</c>"] Transaction quantity
        /// </summary>
        [JsonPropertyName("transaction_qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>transaction_cost</c>"] Transaction cost
        /// </summary>
        [JsonPropertyName("transaction_cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// ["<c>realized_pnl</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("realized_pnl")]
        public decimal? RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public string? TradeId { get; set; }
        /// <summary>
        /// ["<c>trade_match_id</c>"] Trade match id
        /// </summary>
        [JsonPropertyName("trade_match_id")]
        public string? TradeMatchId { get; set; }
        /// <summary>
        /// ["<c>event_timestamp_ns</c>"] Event timestamp
        /// </summary>
        [JsonPropertyName("event_timestamp_ns")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// ["<c>client_oid</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_oid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>taker_side</c>"] Trade role
        /// </summary>
        [JsonPropertyName("taker_side")]
        public TradeRole? TradeRole { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide? OrderSide { get; set; }
        /// <summary>
        /// ["<c>instrument_name</c>"] Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
    }


}
