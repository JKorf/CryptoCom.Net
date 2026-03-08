using CryptoCom.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComAnnouncementWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>category</c>"] Category
        /// </summary>
        [JsonPropertyName("category")]
        public AnnouncementCategory Category { get; set; }
        /// <summary>
        /// ["<c>product_type</c>"] Product type
        /// </summary>
        [JsonPropertyName("product_type")]
        public string? ProductType { get; set; }
        /// <summary>
        /// ["<c>announced_at</c>"] Announced at time
        /// </summary>
        [JsonPropertyName("announced_at")]
        public DateTime AnnounceTime { get; set; }
        /// <summary>
        /// ["<c>title</c>"] Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>content</c>"] Content
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>instrument_name</c>"] Instrument name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string? InstrumentName { get; set; }
        /// <summary>
        /// ["<c>impacted_params</c>"] Impacted params
        /// </summary>
        [JsonPropertyName("impacted_params")]
        public CryptoComAnnouncementImpact Impact { get; set; } = null!;
        /// <summary>
        /// ["<c>start_time</c>"] Start time
        /// </summary>
        [JsonPropertyName("start_time")]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// ["<c>end_time</c>"] End time
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
        /// ["<c>spot_trading_impacted</c>"] Spot trading impacted
        /// </summary>
        [JsonPropertyName("spot_trading_impacted")]
        public AnnouncementImpact SpotTradingImpacted { get; set; }
        /// <summary>
        /// ["<c>derivative_trading_impacted</c>"] Derivative trading impacted
        /// </summary>
        [JsonPropertyName("derivative_trading_impacted")]
        public AnnouncementImpact DerivativeTradingImpacted { get; set; }
        /// <summary>
        /// ["<c>margin_trading_impacted</c>"] Margin trading impacted
        /// </summary>
        [JsonPropertyName("margin_trading_impacted")]
        public AnnouncementImpact MarginTradingImpacted { get; set; }
        /// <summary>
        /// ["<c>otc_trading_impacted</c>"] Otc trading impacted
        /// </summary>
        [JsonPropertyName("otc_trading_impacted")]
        public AnnouncementImpact OtcTradingImpacted { get; set; }
        /// <summary>
        /// ["<c>convert_impacted</c>"] Convert impacted
        /// </summary>
        [JsonPropertyName("convert_impacted")]
        public AnnouncementImpact ConvertImpacted { get; set; }
        /// <summary>
        /// ["<c>staking_impacted</c>"] Staking impacted
        /// </summary>
        [JsonPropertyName("staking_impacted")]
        public AnnouncementImpact StakingImpacted { get; set; } 
        /// <summary>
        /// ["<c>trading_bot_impacted</c>"] Trading bot impacted
        /// </summary>
        [JsonPropertyName("trading_bot_impacted")]
        public AnnouncementImpact TradingBotImpacted { get; set; }
        /// <summary>
        /// ["<c>crypto_wallet_impacted</c>"] Crypto wallet impacted
        /// </summary>
        [JsonPropertyName("crypto_wallet_impacted")]
        public AnnouncementImpact CryptoWalletImpacted { get; set; }
        /// <summary>
        /// ["<c>fiat_wallet_impacted</c>"] Fiat wallet impacted
        /// </summary>
        [JsonPropertyName("fiat_wallet_impacted")]
        public AnnouncementImpact FiatWalletImpacted { get; set; }
        /// <summary>
        /// ["<c>login_impacted</c>"] Login impacted
        /// </summary>
        [JsonPropertyName("login_impacted")]
        public AnnouncementImpact LoginImpacted { get; set; }
    }

}
