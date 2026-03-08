using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Order cancellation request
    /// </summary>
    [SerializationModel]
    public record CryptoComCancelOrderRequest
    {
        // NOTE; THE ORDER OF THE PROPERTIES (JsonPropertyName) SHOULD BE IN ALPHABETICAL ORDER SO SERIALIZATION MATCHES SIGNATURE

        /// <summary>
        /// ["<c>instrument_name</c>"] Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
    }
}
