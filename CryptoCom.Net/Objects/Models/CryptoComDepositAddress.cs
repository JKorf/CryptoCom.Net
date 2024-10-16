using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComDepositAddressWrapper
    {
        /// <summary>
        /// Deposit address list
        /// </summary>
        [JsonPropertyName("deposit_address_list")]
        public IEnumerable<CryptoComDepositAddress> DepositAddressList { get; set; } = Array.Empty<CryptoComDepositAddress>();
    }

    /// <summary>
    /// 
    /// </summary>
    public record CryptoComDepositAddress
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
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
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Active
        /// </summary>
        [JsonPropertyName("status")]
        public bool Active { get; set; }
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
    }


}
