using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComAssetWrapper
    {
        /// <summary>
        /// ["<c>currency_map</c>"] Asset map
        /// </summary>
        [JsonPropertyName("currency_map")]
        public Dictionary<string, CryptoComAsset> AssetMap { get; set; } = null!;
    }

    /// <summary>
    /// Asset network info
    /// </summary>
    [SerializationModel]
    public record CryptoComAsset
    {
        /// <summary>
        /// ["<c>full_name</c>"] Full name
        /// </summary>
        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>default_network</c>"] Default network
        /// </summary>
        [JsonPropertyName("default_network")]
        public string? DefaultNetwork { get; set; }
        /// <summary>
        /// ["<c>network_list</c>"] Network list
        /// </summary>
        [JsonPropertyName("network_list")]
        public CryptoComAssetNetwork[] Networks { get; set; } = Array.Empty<CryptoComAssetNetwork>();
    }

    /// <summary>
    /// Network info
    /// </summary>
    [SerializationModel]
    public record CryptoComAssetNetwork
    {
        /// <summary>
        /// ["<c>network_id</c>"] Network id
        /// </summary>
        [JsonPropertyName("network_id")]
        public string NetworkId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdrawal_fee</c>"] Withdrawal fee
        /// </summary>
        [JsonPropertyName("withdrawal_fee")]
        public decimal? WithdrawalFee { get; set; }
        /// <summary>
        /// ["<c>withdraw_enabled</c>"] Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdraw_enabled")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>min_withdrawal_amount</c>"] Min withdrawal quantity
        /// </summary>
        [JsonPropertyName("min_withdrawal_amount")]
        public decimal MinWithdrawalQuantity { get; set; }
        /// <summary>
        /// ["<c>deposit_enabled</c>"] Deposit enabled
        /// </summary>
        [JsonPropertyName("deposit_enabled")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// ["<c>confirmation_required</c>"] Confirmation required
        /// </summary>
        [JsonPropertyName("confirmation_required")]
        public int ConfirmationRequired { get; set; }
    }

}
