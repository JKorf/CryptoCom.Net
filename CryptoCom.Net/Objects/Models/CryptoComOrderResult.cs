using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComOrderResultWrapper
    {
        [JsonPropertyName("result_list")]
        public CryptoComOrderResult[] Results { get; set; } = [];
    }

    /// <summary>
    /// Order result
    /// </summary>
    [SerializationModel]
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
