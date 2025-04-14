using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Order result
    /// </summary>
    [SerializationModel]
    public record CryptoComListOrderResult
    {
        /// <summary>
        /// List id
        /// </summary>
        [JsonPropertyName("list_id")]
        public long ListId { get; set; }
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
        /// Error code
        /// </summary>
        [JsonIgnore]
        internal int? ErrorCode { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        [JsonIgnore]
        internal string? ErrorMessage { get; set; }
    }
}
