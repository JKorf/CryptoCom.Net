using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Symbol fee rate
    /// </summary>
    [SerializationModel]
    public record CryptoComSymbolFeeRate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Effective maker rate bps
        /// </summary>
        [JsonPropertyName("effective_maker_rate_bps")]
        public decimal EffectiveMakerRateBps { get; set; }
        /// <summary>
        /// Effective taker rate bps
        /// </summary>
        [JsonPropertyName("effective_taker_rate_bps")]
        public decimal EffectiveTakerRateBps { get; set; }
    }
}
