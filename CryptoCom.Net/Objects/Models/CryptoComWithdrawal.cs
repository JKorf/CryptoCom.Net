using CryptoCom.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComWithdrawalWrapper
    {
        /// <summary>
        /// Withdrawal list
        /// </summary>
        [JsonPropertyName("withdrawal_list")]
        public IEnumerable<CryptoComWithdrawal> WithdrawalList { get; set; } = Array.Empty<CryptoComWithdrawal>();
    }

    /// <summary>
    /// Withdrawal info
    /// </summary>
    public record CryptoComWithdrawal
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Client withdrawal id
        /// </summary>
        [JsonPropertyName("client_wid")]
        public string? ClientWithdrawalId { get; set; }
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
        public DateTime? UpdateTime { get; set; }
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
        public WithdrawalStatus WithdrawalStatus { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("txid")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Network id
        /// </summary>
        [JsonPropertyName("network_id")]
        public string? NetworkId { get; set; }
    }


}
