using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Internal
{
    [SerializationModel]
    internal record CryptoComOrderBookUpdateInt : CryptoComOrderBookUpdate
    {
        [JsonPropertyName("update")]
        public CryptoComOrderBookUpdate? Update { get; set; }
    }
}
