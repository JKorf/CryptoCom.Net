using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// From symbol
        /// </summary>
        [JsonPropertyName("from_instrument_name")]
        public string FromSymbol { get; set; } = string.Empty;
        /// <summary>
        /// To symbol
        /// </summary>
        [JsonPropertyName("to_instrument_name")]
        public string ToSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Expected rate
        /// </summary>
        [JsonPropertyName("expected_rate")]
        public decimal ExpectedRate { get; set; }
        /// <summary>
        /// From quantity
        /// </summary>
        [JsonPropertyName("from_quantity")]
        public decimal FromQuantity { get; set; }
        /// <summary>
        /// Slippage tolerance bps
        /// </summary>
        [JsonPropertyName("slippage_tolerance_bps")]
        public decimal SlippageToleranceBps { get; set; }
        /// <summary>
        /// Convert id
        /// </summary>
        [JsonPropertyName("convert_id")]
        public long ConvertId { get; set; }
        /// <summary>
        /// Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }


}
