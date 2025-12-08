using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Objects.Models;
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
