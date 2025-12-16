using CryptoCom.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComAnnouncementWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComAnnouncement[] Data { get; set; } = [];
    }

    /// <summary>
    /// Announcement
    /// </summary>
    public record CryptoComAnnouncement
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Category
        /// </summary>
        [JsonPropertyName("category")]
        public AnnouncementCategory Category { get; set; }
        /// <summary>
        /// Product type
        /// </summary>
        [JsonPropertyName("product_type")]
        public string? ProductType { get; set; }
        /// <summary>
        /// Announced at time
        /// </summary>
        [JsonPropertyName("announced_at")]
        public DateTime AnnounceTime { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Content
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// Instrument name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string? InstrumentName { get; set; }
        /// <summary>
        /// Impacted params
        /// </summary>
        [JsonPropertyName("impacted_params")]
        public CryptoComAnnouncementImpact Impact { get; set; } = null!;
        /// <summary>
        /// Start time
        /// </summary>
        [JsonPropertyName("start_time")]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// System impact
    /// </summary>
    public record CryptoComAnnouncementImpact
    {
        /// <summary>
        /// Spot trading impacted
        /// </summary>
        [JsonPropertyName("spot_trading_impacted")]
        public AnnouncementImpact SpotTradingImpacted { get; set; }
        /// <summary>
        /// Derivative trading impacted
        /// </summary>
        [JsonPropertyName("derivative_trading_impacted")]
        public AnnouncementImpact DerivativeTradingImpacted { get; set; }
        /// <summary>
        /// Margin trading impacted
        /// </summary>
        [JsonPropertyName("margin_trading_impacted")]
        public AnnouncementImpact MarginTradingImpacted { get; set; }
        /// <summary>
        /// Otc trading impacted
        /// </summary>
        [JsonPropertyName("otc_trading_impacted")]
        public AnnouncementImpact OtcTradingImpacted { get; set; }
        /// <summary>
        /// Convert impacted
        /// </summary>
        [JsonPropertyName("convert_impacted")]
        public AnnouncementImpact ConvertImpacted { get; set; }
        /// <summary>
        /// Staking impacted
        /// </summary>
        [JsonPropertyName("staking_impacted")]
        public AnnouncementImpact StakingImpacted { get; set; } 
        /// <summary>
        /// Trading bot impacted
        /// </summary>
        [JsonPropertyName("trading_bot_impacted")]
        public AnnouncementImpact TradingBotImpacted { get; set; }
        /// <summary>
        /// Crypto wallet impacted
        /// </summary>
        [JsonPropertyName("crypto_wallet_impacted")]
        public AnnouncementImpact CryptoWalletImpacted { get; set; }
        /// <summary>
        /// Fiat wallet impacted
        /// </summary>
        [JsonPropertyName("fiat_wallet_impacted")]
        public AnnouncementImpact FiatWalletImpacted { get; set; }
        /// <summary>
        /// Login impacted
        /// </summary>
        [JsonPropertyName("login_impacted")]
        public AnnouncementImpact LoginImpacted { get; set; }
    }

}
