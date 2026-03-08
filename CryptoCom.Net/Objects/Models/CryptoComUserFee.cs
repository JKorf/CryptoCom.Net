using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// User fee rates
    /// </summary>
    [SerializationModel]
    public record CryptoComUserFee
    {
        /// <summary>
        /// ["<c>spot_tier</c>"] Spot tier
        /// </summary>
        [JsonPropertyName("spot_tier")]
        public string SpotTier { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>deriv_tier</c>"] Deriv tier
        /// </summary>
        [JsonPropertyName("deriv_tier")]
        public string DerivTier { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>effective_spot_maker_rate_bps</c>"] Effective spot maker rate bps
        /// </summary>
        [JsonPropertyName("effective_spot_maker_rate_bps")]
        public decimal EffectiveSpotMakerRateBps { get; set; }
        /// <summary>
        /// ["<c>effective_spot_taker_rate_bps</c>"] Effective spot taker rate bps
        /// </summary>
        [JsonPropertyName("effective_spot_taker_rate_bps")]
        public decimal EffectiveSpotTakerRateBps { get; set; }
        /// <summary>
        /// ["<c>effective_deriv_maker_rate_bps</c>"] Effective deriv maker rate bps
        /// </summary>
        [JsonPropertyName("effective_deriv_maker_rate_bps")]
        public decimal EffectiveDerivMakerRateBps { get; set; }
        /// <summary>
        /// ["<c>effective_deriv_taker_rate_bps</c>"] Effective deriv taker rate bps
        /// </summary>
        [JsonPropertyName("effective_deriv_taker_rate_bps")]
        public decimal EffectiveDerivTakerRateBps { get; set; }
    }


}
