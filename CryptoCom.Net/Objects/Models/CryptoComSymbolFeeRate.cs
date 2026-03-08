using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// ["<c>instrument_name</c>"] Symbol
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>effective_maker_rate_bps</c>"] Effective maker rate bps
        /// </summary>
        [JsonPropertyName("effective_maker_rate_bps")]
        public decimal EffectiveMakerRateBps { get; set; }
        /// <summary>
        /// ["<c>effective_taker_rate_bps</c>"] Effective taker rate bps
        /// </summary>
        [JsonPropertyName("effective_taker_rate_bps")]
        public decimal EffectiveTakerRateBps { get; set; }
    }
}
