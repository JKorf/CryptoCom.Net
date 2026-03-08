using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComWithdrawalWrapper
    {
        /// <summary>
        /// ["<c>withdrawal_list</c>"] Withdrawal list
        /// </summary>
        [JsonPropertyName("withdrawal_list")]
        public CryptoComWithdrawal[] WithdrawalList { get; set; } = Array.Empty<CryptoComWithdrawal>();
    }

    /// <summary>
    /// Withdrawal info
    /// </summary>
    [SerializationModel]
    public record CryptoComWithdrawal
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>client_wid</c>"] Client withdrawal id
        /// </summary>
        [JsonPropertyName("client_wid")]
        public string? ClientWithdrawalId { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public WithdrawalStatus WithdrawalStatus { get; set; }
        /// <summary>
        /// ["<c>txid</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("txid")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>network_id</c>"] Network id
        /// </summary>
        [JsonPropertyName("network_id")]
        public string? NetworkId { get; set; }
    }


}
