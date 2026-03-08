using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Order result
    /// </summary>
    [SerializationModel]
    public record CryptoComOrderResult : CryptoComOrderId
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
