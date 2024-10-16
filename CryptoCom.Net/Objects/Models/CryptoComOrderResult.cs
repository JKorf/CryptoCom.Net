using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComOrderResultWrapper
    {
        [JsonPropertyName("result_list")]
        public IEnumerable<CryptoComOrderResult> Results { get; set; } = [];
    }

    /// <summary>
    /// Order result
    /// </summary>
    public record CryptoComOrderResult : CryptoComOrderId
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
