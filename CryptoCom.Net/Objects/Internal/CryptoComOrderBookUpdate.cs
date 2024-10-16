using CryptoCom.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Internal
{
    internal record CryptoComOrderBookUpdate : CryptoComOrderBook
    {
        [JsonPropertyName("update")]
        public CryptoComOrderBook? Update { get; set; }
    }
}
