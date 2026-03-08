using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Enums;
using System;
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
        /// ["<c>master_account</c>"] Master account
        /// </summary>
        [JsonPropertyName("master_account")]
        public CryptoComAccountDetails MasterAccount { get; set; } = null!;
        /// <summary>
        /// ["<c>sub_account_list</c>"] Sub account list
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
        /// ["<c>uuid</c>"] Unique id
        /// </summary>
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>master_account_uuid</c>"] Master account uuid
        /// </summary>
        [JsonPropertyName("master_account_uuid")]
        public string? MasterAccountUuid { get; set; }
        /// <summary>
        /// ["<c>user_uuid</c>"] User uuid
        /// </summary>
        [JsonPropertyName("user_uuid")]
        public string UserUuid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_account_uuid</c>"] Margin account uuid
        /// </summary>
        [JsonPropertyName("margin_account_uuid")]
        public string? MarginAccountUuid { get; set; }
        /// <summary>
        /// ["<c>enabled</c>"] Enabled
        /// </summary>
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
        /// <summary>
        /// ["<c>tradable</c>"] Tradable
        /// </summary>
        [JsonPropertyName("tradable")]
        public bool Tradable { get; set; }
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        /// <summary>
        /// ["<c>email</c>"] Email
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        /// <summary>
        /// ["<c>mobile_number</c>"] Mobile number
        /// </summary>
        [JsonPropertyName("mobile_number")]
        public string? MobileNumber { get; set; }
        /// <summary>
        /// ["<c>country_code</c>"] Country code
        /// </summary>
        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        /// <summary>
        /// ["<c>margin_access</c>"] Margin access
        /// </summary>
        [JsonPropertyName("margin_access")]
        public AccessType MarginAccess { get; set; }
        /// <summary>
        /// ["<c>derivatives_access</c>"] Derivatives access
        /// </summary>
        [JsonPropertyName("derivatives_access")]
        public AccessType DerivativesAccess { get; set; }
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>two_fa_enabled</c>"] Two fa enabled
        /// </summary>
        [JsonPropertyName("two_fa_enabled")]
        public bool TwoFaEnabled { get; set; }
        /// <summary>
        /// ["<c>kyc_level</c>"] Kyc level
        /// </summary>
        [JsonPropertyName("kyc_level")]
        public string KycLevel { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>suspended</c>"] Suspended
        /// </summary>
        [JsonPropertyName("suspended")]
        public bool Suspended { get; set; }
        /// <summary>
        /// ["<c>terminated</c>"] Terminated
        /// </summary>
        [JsonPropertyName("terminated")]
        public bool Terminated { get; set; }
        /// <summary>
        /// ["<c>spot_enabled</c>"] Spot enabled
        /// </summary>
        [JsonPropertyName("spot_enabled")]
        public bool SpotEnabled { get; set; }
        /// <summary>
        /// ["<c>margin_enabled</c>"] Margin enabled
        /// </summary>
        [JsonPropertyName("margin_enabled")]
        public bool MarginEnabled { get; set; }
        /// <summary>
        /// ["<c>derivatives_enabled</c>"] Derivatives enabled
        /// </summary>
        [JsonPropertyName("derivatives_enabled")]
        public bool DerivativesEnabled { get; set; }
        /// <summary>
        /// ["<c>label</c>"] Label
        /// </summary>
        [JsonPropertyName("label")]
        public string? Label { get; set; }
    }
}
