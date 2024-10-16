using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComAssetWrapper
    {
        /// <summary>
        /// Asset map
        /// </summary>
        [JsonPropertyName("currency_map")]
        public Dictionary<string, CryptoComAsset> AssetMap { get; set; } = null!;
    }

    /// <summary>
    /// Asset network info
    /// </summary>
    public record CryptoComAsset
    {
        /// <summary>
        /// Full name
        /// </summary>
        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Default network
        /// </summary>
        [JsonPropertyName("default_network")]
        public string? DefaultNetwork { get; set; }
        /// <summary>
        /// Network list
        /// </summary>
        [JsonPropertyName("network_list")]
        public IEnumerable<CryptoComAssetNetwork> Networks { get; set; } = Array.Empty<CryptoComAssetNetwork>();
    }

    /// <summary>
    /// Network info
    /// </summary>
    public record CryptoComAssetNetwork
    {
        /// <summary>
        /// Network id
        /// </summary>
        [JsonPropertyName("network_id")]
        public string NetworkId { get; set; } = string.Empty;
        /// <summary>
        /// Withdrawal fee
        /// </summary>
        [JsonPropertyName("withdrawal_fee")]
        public decimal? WithdrawalFee { get; set; }
        /// <summary>
        /// Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdraw_enabled")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// Min withdrawal quantity
        /// </summary>
        [JsonPropertyName("min_withdrawal_amount")]
        public decimal MinWithdrawalQuantity { get; set; }
        /// <summary>
        /// Deposit enabled
        /// </summary>
        [JsonPropertyName("deposit_enabled")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// Confirmation required
        /// </summary>
        [JsonPropertyName("confirmation_required")]
        public int ConfirmationRequired { get; set; }
    }

}
