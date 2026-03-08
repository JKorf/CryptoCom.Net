using CryptoCom.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComOrderWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComOrder[] Data { get; set; } = Array.Empty<CryptoComOrder>();
    }

    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record CryptoComOrder
    {
        /// <summary>
        /// ["<c>account_id</c>"] Account id
        /// </summary>
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isolation_id</c>"] Isolation id
        /// </summary>
        [JsonPropertyName("isolation_id")]
        public string? IsolationId { get; set; }
        /// <summary>
        /// ["<c>client_oid</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_oid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderType OrderType { get; set; }
        // OCO order response uses 'type'
        [JsonInclude, JsonPropertyName("type")]
        internal OrderType OrderTypeOco
        {
            set => OrderType = value;
        }
        /// <summary>
        /// ["<c>time_in_force</c>"] Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>side</c>"] OrderSide
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>limit_price</c>"] Limit price
        /// </summary>
        [JsonPropertyName("limit_price")]
        public decimal Price { get; set; }
        // OCO order response uses 'price'
        [JsonInclude, JsonPropertyName("price")]
        internal decimal PriceOco
        {
            set => Price = value;
        }
        /// <summary>
        /// ["<c>order_value</c>"] Order value
        /// </summary>
        [JsonPropertyName("order_value")]
        public decimal OrderValue { get; set; }
        /// <summary>
        /// ["<c>maker_fee_rate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("maker_fee_rate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>taker_fee_rate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("taker_fee_rate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>avg_price</c>"] Average fill price
        /// </summary>
        [JsonPropertyName("avg_price")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>cumulative_quantity</c>"] Cumulative quantity filled
        /// </summary>
        [JsonPropertyName("cumulative_quantity")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>cumulative_value</c>"] Cumulative value filled
        /// </summary>
        [JsonPropertyName("cumulative_value")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>cumulative_fee</c>"] Cumulative fee
        /// </summary>
        [JsonPropertyName("cumulative_fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>update_user_id</c>"] Update user id
        /// </summary>
        [JsonPropertyName("update_user_id")]
        public string UpdateUserId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>reason</c>"] Reason as enum
        /// </summary>
        [JsonPropertyName("reason")]
        public OrderRejectedReason Reason { get; set; }
        /// <summary>
        /// ["<c>order_date</c>"] Order date
        /// </summary>
        [JsonPropertyName("order_date")]
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// ["<c>instrument_name</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee_instrument_name</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_instrument_name")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>create_time_ns</c>"] Creation time
        /// </summary>
        [JsonPropertyName("create_time_ns")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>list_id</c>"] Order list id
        /// </summary>
        [JsonPropertyName("list_id")]
        public string? ListId { get; set; }
        /// <summary>
        /// ["<c>contingency_type</c>"] Contingency type
        /// </summary>
        [JsonPropertyName("contingency_type")]
        public string? ContingencyType { get; set; }
        /// <summary>
        /// ["<c>exec_inst</c>"] Execution instructions
        /// </summary>
        [JsonPropertyName("exec_inst")]
        public ExecutionInstruction[] ExecutionInstructions { get; set; } = [];
        /// <summary>
        /// ["<c>ref_price</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("ref_price")]
        public decimal? TriggerPrice { get; set; }
        // OCO order response uses 'trigger_price'
        [JsonInclude, JsonPropertyName("trigger_price")]
        internal decimal? TriggerPriceOco
        {
            set => TriggerPrice = value;
        }
        /// <summary>
        /// ["<c>ref_price_type</c>"] Trigger price type
        /// </summary>
        [JsonPropertyName("ref_price_type")]
        public PriceType? TriggerPriceType { get; set; }
    }
}
