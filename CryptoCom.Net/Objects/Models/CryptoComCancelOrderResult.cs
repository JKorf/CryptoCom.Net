using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Cancel order result
    /// </summary>
    public record CryptoComCancelOrderResult
    {
        /// <summary>
        /// Order index in request
        /// </summary>
        [JsonPropertyName("index")]
        public int Index { get; set; }
        /// <summary>
        /// Result code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
