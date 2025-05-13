using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// OCO order result
    /// </summary>
    [SerializationModel]
    public record CryptoComOcoResult
    {
        /// <summary>
        /// Order list id
        /// </summary>
        [JsonPropertyName("list_id")]
        public long ListId { get; set; }
    }
}
