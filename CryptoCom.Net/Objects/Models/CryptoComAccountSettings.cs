using CryptoCom.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Account settings
    /// </summary>
    public record CryptoComAccountSettings
    {
        /// <summary>
        /// Account leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Self trade prevention id
        /// </summary>
        [JsonPropertyName("stp_id")]
        public long? StpId { get; set; }
        /// <summary>
        /// Self trade prevention scope
        /// </summary>
        [JsonPropertyName("stp_scope")]
        public SelfTradePreventionScope StpScope { get; set; }
        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stp_inst")]
        public SelfTradePreventionMode? StpMode { get; set; }
    }


}
