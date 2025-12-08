using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComConvertRequestWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComConvertRequest[] Data { get; set; } = Array.Empty<CryptoComConvertRequest>();
    }

    /// <summary>
    /// Convert request info
    /// </summary>
    [SerializationModel]
    public record CryptoComConvertRequest
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
        /// Actual rate
        /// </summary>
        [JsonPropertyName("actual_rate")]
        public decimal ActualRate { get; set; }
        /// <summary>
        /// To quantity
        /// </summary>
        [JsonPropertyName("to_quantity")]
        public decimal ToQuantity { get; set; }
        /// <summary>
        /// Convert id
        /// </summary>
        [JsonPropertyName("convert_id")]
        public long ConvertId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Create timestamp
        /// </summary>
        [JsonPropertyName("create_timestamp_ms")]
        public DateTime CreateTime { get; set; }
    }


}
