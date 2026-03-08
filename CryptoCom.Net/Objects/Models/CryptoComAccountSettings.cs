using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Account settings
    /// </summary>
    [SerializationModel]
    public record CryptoComAccountSettings
    {
        /// <summary>
        /// ["<c>leverage</c>"] Account leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>stp_id</c>"] Self trade prevention id
        /// </summary>
        [JsonPropertyName("stp_id")]
        public long? StpId { get; set; }
        /// <summary>
        /// ["<c>stp_scope</c>"] Self trade prevention scope
        /// </summary>
        [JsonPropertyName("stp_scope")]
        public SelfTradePreventionScope StpScope { get; set; }
        /// <summary>
        /// ["<c>stp_inst</c>"] Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stp_inst")]
        public SelfTradePreventionMode? StpMode { get; set; }
    }


}
