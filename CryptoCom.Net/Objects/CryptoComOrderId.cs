using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects
{
    /// <summary>
    /// Order ids
    /// </summary>
    [SerializationModel]
    public record CryptoComOrderId
    {
        /// <summary>
        /// Client oid
        /// </summary>
        [JsonPropertyName("client_oid")]
        public string? ClientOid { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
    }


}
