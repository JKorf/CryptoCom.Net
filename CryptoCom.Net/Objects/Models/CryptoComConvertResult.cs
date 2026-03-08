using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Convert request response
    /// </summary>
    [SerializationModel]
    public record CryptoComConvertResult
    {
        /// <summary>
        /// ["<c>from_instrument_name</c>"] From symbol
        /// </summary>
        [JsonPropertyName("from_instrument_name")]
        public string FromSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>to_instrument_name</c>"] To symbol
        /// </summary>
        [JsonPropertyName("to_instrument_name")]
        public string ToSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>expected_rate</c>"] Expected rate
        /// </summary>
        [JsonPropertyName("expected_rate")]
        public decimal ExpectedRate { get; set; }
        /// <summary>
        /// ["<c>from_quantity</c>"] From quantity
        /// </summary>
        [JsonPropertyName("from_quantity")]
        public decimal FromQuantity { get; set; }
        /// <summary>
        /// ["<c>slippage_tolerance_bps</c>"] Slippage tolerance bps
        /// </summary>
        [JsonPropertyName("slippage_tolerance_bps")]
        public decimal SlippageToleranceBps { get; set; }
        /// <summary>
        /// ["<c>convert_id</c>"] Convert id
        /// </summary>
        [JsonPropertyName("convert_id")]
        public long ConvertId { get; set; }
        /// <summary>
        /// ["<c>reason</c>"] Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }


}
