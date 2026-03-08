using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComUserTradeWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
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
        /// ["<c>journal_type</c>"] Journal type
        /// </summary>
        [JsonPropertyName("journal_type")]
        public TransactionType TransactionType { get; set; }
        /// <summary>
        /// ["<c>traded_quantity</c>"] Trade quantity
        /// </summary>
        [JsonPropertyName("traded_quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>traded_price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("traded_price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>fees</c>"] Trade fees, the negative sign means a deduction on balance
        /// </summary>
        [JsonPropertyName("fees")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>trade_match_id</c>"] Trade match id
        /// </summary>
        [JsonPropertyName("trade_match_id")]
        public string TradeMatchId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>client_oid</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_oid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>taker_side</c>"] Trade role
        /// </summary>
        [JsonPropertyName("taker_side")]
        public TradeRole Role { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Trade side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>instrument_name</c>"] Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee_instrument_name</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_instrument_name")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>create_time_ns</c>"] Time that the original order was created
        /// </summary>
        [JsonPropertyName("create_time_ns")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>transact_time_ns</c>"] Time that this trade was executed at
        /// </summary>
        [JsonPropertyName("transact_time_ns")]
        public DateTime TransactTime { get; set; }
    }
}
