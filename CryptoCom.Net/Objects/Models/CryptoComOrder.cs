using CryptoCom.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComOrderWrapper
    {
        /// <summary>
        /// Data
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
        /// Account id
        /// </summary>
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_oid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order type
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
        /// Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// OrderSide
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Limit price
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
        /// Order value
        /// </summary>
        [JsonPropertyName("order_value")]
        public decimal OrderValue { get; set; }
        /// <summary>
        /// Maker fee rate
        /// </summary>
        [JsonPropertyName("maker_fee_rate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("taker_fee_rate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonPropertyName("avg_price")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Cumulative quantity filled
        /// </summary>
        [JsonPropertyName("cumulative_quantity")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Cumulative value filled
        /// </summary>
        [JsonPropertyName("cumulative_value")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Cumulative fee
        /// </summary>
        [JsonPropertyName("cumulative_fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Update user id
        /// </summary>
        [JsonPropertyName("update_user_id")]
        public string UpdateUserId { get; set; } = string.Empty;
        /// <summary>
        /// Reason
        /// </summary>
        [JsonPropertyName("reason"), JsonConverter(typeof(NumberStringConverter))]
        public string? Reason { get; set; }
        /// <summary>
        /// Order date
        /// </summary>
        [JsonPropertyName("order_date")]
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_instrument_name")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("create_time_ns")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Order list id
        /// </summary>
        [JsonPropertyName("list_id")]
        public string? ListId { get; set; }
        /// <summary>
        /// Contingency type
        /// </summary>
        [JsonPropertyName("contingency_type")]
        public string? ContingencyType { get; set; }
        /// <summary>
        /// Trigger price
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
        /// Trigger price type
        /// </summary>
        [JsonPropertyName("ref_price_type")]
        public PriceType? TriggerPriceType { get; set; }
    }
}
