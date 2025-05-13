using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComDepositWrapper
    {
        /// <summary>
        /// Deposit list
        /// </summary>
        [JsonPropertyName("deposit_list")]
        public CryptoComDeposit[] DepositList { get; set; } = Array.Empty<CryptoComDeposit>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CryptoComDeposit
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus DepositStatus { get; set; }
    }


}
