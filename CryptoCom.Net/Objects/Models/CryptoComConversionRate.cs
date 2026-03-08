using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Conversion rate
    /// </summary>
    [SerializationModel]
    public record CryptoComConversionRate
    {
        /// <summary>
        /// ["<c>instrument_name</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>conversion_rate</c>"] Conversion rate
        /// </summary>
        [JsonPropertyName("conversion_rate")]
        public decimal ConversionRate { get; set; }
    }
}
