using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Order cancellation request
    /// </summary>
    public record CryptoComCancelOrderRequest
    {
        // NOTE; THE ORDER OF THE PROPERTIES (JsonPropertyName) SHOULD BE IN ALPHABETICAL ORDER SO SERIALIZATION MATCHES SIGNATURE

        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
    }
}
