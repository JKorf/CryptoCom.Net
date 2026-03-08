using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComConvertRequestWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
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
        /// ["<c>actual_rate</c>"] Actual rate
        /// </summary>
        [JsonPropertyName("actual_rate")]
        public decimal ActualRate { get; set; }
        /// <summary>
        /// ["<c>to_quantity</c>"] To quantity
        /// </summary>
        [JsonPropertyName("to_quantity")]
        public decimal ToQuantity { get; set; }
        /// <summary>
        /// ["<c>convert_id</c>"] Convert id
        /// </summary>
        [JsonPropertyName("convert_id")]
        public long ConvertId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>create_timestamp_ms</c>"] Create timestamp
        /// </summary>
        [JsonPropertyName("create_timestamp_ms")]
        public DateTime CreateTime { get; set; }
    }


}
