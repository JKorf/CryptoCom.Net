using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record CryptoComAccountInfo
    {
        /// <summary>
        /// Master account
        /// </summary>
        [JsonPropertyName("master_account")]
        public CryptoComAccountDetails MasterAccount { get; set; } = null!;
        /// <summary>
        /// Sub account list
        /// </summary>
        [JsonPropertyName("sub_account_list")]
        public CryptoComAccountDetails[] SubAccountList { get; set; } = Array.Empty<CryptoComAccountDetails>();
    }

    /// <summary>
    /// Account details
    /// </summary>
    [SerializationModel]
    public record CryptoComAccountDetails
    {
        /// <summary>
        /// Unique id
        /// </summary>
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; } = string.Empty;
        /// <summary>
        /// Master account uuid
        /// </summary>
        [JsonPropertyName("master_account_uuid")]
        public string? MasterAccountUuid { get; set; }
        /// <summary>
        /// User uuid
        /// </summary>
        [JsonPropertyName("user_uuid")]
        public string UserUuid { get; set; } = string.Empty;
        /// <summary>
        /// Margin account uuid
        /// </summary>
        [JsonPropertyName("margin_account_uuid")]
        public string? MarginAccountUuid { get; set; }
        /// <summary>
        /// Enabled
        /// </summary>
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
        /// <summary>
        /// Tradable
        /// </summary>
        [JsonPropertyName("tradable")]
        public bool Tradable { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        /// <summary>
        /// Mobile number
        /// </summary>
        [JsonPropertyName("mobile_number")]
        public string? MobileNumber { get; set; }
        /// <summary>
        /// Country code
        /// </summary>
        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        /// <summary>
        /// Margin access
        /// </summary>
        [JsonPropertyName("margin_access")]
        public AccessType MarginAccess { get; set; }
        /// <summary>
        /// Derivatives access
        /// </summary>
        [JsonPropertyName("derivatives_access")]
        public AccessType DerivativesAccess { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Two fa enabled
        /// </summary>
        [JsonPropertyName("two_fa_enabled")]
        public bool TwoFaEnabled { get; set; }
        /// <summary>
        /// Kyc level
        /// </summary>
        [JsonPropertyName("kyc_level")]
        public string KycLevel { get; set; } = string.Empty;
        /// <summary>
        /// Suspended
        /// </summary>
        [JsonPropertyName("suspended")]
        public bool Suspended { get; set; }
        /// <summary>
        /// Terminated
        /// </summary>
        [JsonPropertyName("terminated")]
        public bool Terminated { get; set; }
        /// <summary>
        /// Spot enabled
        /// </summary>
        [JsonPropertyName("spot_enabled")]
        public bool SpotEnabled { get; set; }
        /// <summary>
        /// Margin enabled
        /// </summary>
        [JsonPropertyName("margin_enabled")]
        public bool MarginEnabled { get; set; }
        /// <summary>
        /// Derivatives enabled
        /// </summary>
        [JsonPropertyName("derivatives_enabled")]
        public bool DerivativesEnabled { get; set; }
        /// <summary>
        /// Label
        /// </summary>
        [JsonPropertyName("label")]
        public string? Label { get; set; }
    }
}
