using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Cancel order result
    /// </summary>
    [SerializationModel]
    public record CryptoComCancelOrderResult
    {
        /// <summary>
        /// ["<c>index</c>"] Order index in request
        /// </summary>
        [JsonPropertyName("index")]
        public int Index { get; set; }
        /// <summary>
        /// ["<c>code</c>"] Result code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// ["<c>message</c>"] Error message
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
