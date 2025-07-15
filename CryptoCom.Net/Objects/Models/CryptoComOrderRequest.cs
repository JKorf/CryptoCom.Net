using CryptoCom.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Order request
    /// </summary>
    [SerializationModel]
    public record CryptoComOrderRequest
    {
        // NOTE; THE ORDER OF THE PROPERTIES (JsonPropertyName) SHOULD BE IN ALPHABETICAL ORDER SO SERIALIZATION MATCHES SIGNATURE

        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_oid"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Post only
        /// </summary>
        [JsonIgnore]
        public bool PostOnly { get; set; }
        /// <summary>
        /// Smart post only
        /// </summary>
        [JsonIgnore]
        public bool SmartPostOnly { get; set; }
        [JsonInclude, JsonPropertyName("exec_inst"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        internal string[]? ExecutionMode 
        { 
            get
            {
                var result = new List<string>();
                if (PostOnly == true) result.Add("POST_ONLY");
                if (SmartPostOnly == true) result.Add("SMART_POST_ONLY");
                return result.ToArray();
            }
        }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonPropertyName("notional"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Trigger price
        /// </summary>
        [JsonPropertyName("ref_price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side"), JsonConverter(typeof(EnumConverter<OrderSide>))]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Self trade prevention id
        /// </summary>
        [JsonPropertyName("stp_id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? StpId { get; set; }
        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stp_inst"), JsonConverter(typeof(EnumConverter<SelfTradePreventionMode>)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SelfTradePreventionMode? StpMode { get; set; }
        /// <summary>
        /// Self trade prevention scope
        /// </summary>
        [JsonPropertyName("stp_scope"), JsonConverter(typeof(EnumConverter<SelfTradePreventionScope>)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SelfTradePreventionScope? StpScope { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("time_in_force"), JsonConverter(typeof(EnumConverter<TimeInForce>)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type"), JsonConverter(typeof(EnumConverter<OrderType>))]
        public OrderType OrderType { get; set; }
    }
}
