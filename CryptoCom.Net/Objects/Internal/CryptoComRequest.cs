using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Internal
{
    internal class CryptoComRequest
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;
        [JsonPropertyName("params")]
        public ParameterCollection Parameters { get; set; } = new ParameterCollection();
        [JsonPropertyName("api_key")]
        public string? ApiKey { get; set; }
        [JsonPropertyName("nonce")]
        public long? Nonce { get; set; }
        [JsonPropertyName("sig")]
        public string? Signature { get; set; }
    }
}
