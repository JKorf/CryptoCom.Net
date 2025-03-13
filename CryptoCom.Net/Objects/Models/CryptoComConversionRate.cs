using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// Symbol name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Conversion rate
        /// </summary>
        [JsonPropertyName("conversion_rate")]
        public decimal ConversionRate { get; set; }
    }
}
