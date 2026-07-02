using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Limit info
    /// </summary>
    [SerializationModel]
    public record CryptoComRiskParameters
    {
        /// <summary>
        /// ["<c>default_max_product_leverage_for_spot</c>"] Default max product leverage for spot
        /// </summary>
        [JsonPropertyName("default_max_product_leverage_for_spot")]
        public decimal DefaultMaxProductLeverageForSpot { get; set; }
        /// <summary>
        /// ["<c>default_max_product_leverage_for_perps</c>"] Default max product leverage for perps
        /// </summary>
        [JsonPropertyName("default_max_product_leverage_for_perps")]
        public decimal DefaultMaxProductLeverageForPerps { get; set; }
        /// <summary>
        /// ["<c>default_max_product_leverage_for_futures</c>"] Default max product leverage for futures
        /// </summary>
        [JsonPropertyName("default_max_product_leverage_for_futures")]
        public decimal DefaultMaxProductLeverageForFutures { get; set; }
        /// <summary>
        /// ["<c>default_umr_multiplier_for_spot</c>"] Default UMR multiplier for spot
        /// </summary>
        [JsonPropertyName("default_umr_multiplier_for_spot")]
        public decimal DefaultUmrMultiplierForSpot { get; set; }
        /// <summary>
        /// ["<c>default_umr_multiplier_for_perps</c>"] Default UMR multiplier for perps
        /// </summary>
        [JsonPropertyName("default_umr_multiplier_for_perps")]
        public decimal DefaultUmrMultiplierForPerps { get; set; }
        /// <summary>
        /// ["<c>default_umr_multiplier_for_futures</c>"] Default UMR multiplier for futures
        /// </summary>
        [JsonPropertyName("default_umr_multiplier_for_futures")]
        public decimal DefaultUmrMultiplierForFutures { get; set; }
        /// <summary>
        /// ["<c>default_long_pos_limit_perps</c>"] Default long position limit for perps
        /// </summary>
        [JsonPropertyName("default_long_pos_limit_perps")]
        public decimal DefaultLongPositionLimitPerps { get; set; }
        /// <summary>
        /// ["<c>default_short_pos_limit_perps</c>"] Default short position limit for perps
        /// </summary>
        [JsonPropertyName("default_short_pos_limit_perps")]
        public decimal DefaultShortPositionLimitPerps { get; set; }
        /// <summary>
        /// ["<c>default_long_pos_limit_futures</c>"] Default long position limit for futures
        /// </summary>
        [JsonPropertyName("default_long_pos_limit_futures")]
        public decimal DefaultLongPositionLimitFutures { get; set; }
        /// <summary>
        /// ["<c>default_short_pos_limit_futures</c>"] Default short position limit for futures
        /// </summary>
        [JsonPropertyName("default_short_pos_limit_futures")]
        public decimal DefaultShortPositionLimitFutures { get; set; }
        /// <summary>
        /// ["<c>default_unit_margin_rate</c>"] Default unit margin rate
        /// </summary>
        [JsonPropertyName("default_unit_margin_rate")]
        public decimal DefaultUnitMarginRate { get; set; }
        /// <summary>
        /// ["<c>default_collateral_cap</c>"] Default collateral cap
        /// </summary>
        [JsonPropertyName("default_collateral_cap")]
        public decimal? DefaultCollateralCap { get; set; }
        /// <summary>
        /// ["<c>update_timestamp_ms</c>"] Update timestamp
        /// </summary>
        [JsonPropertyName("update_timestamp_ms")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>base_currency_config</c>"] Base asset config
        /// </summary>
        [JsonPropertyName("base_currency_config")]
        public CryptoComRiskParametersAsset[] BaseAssetConfig { get; set; } = Array.Empty<CryptoComRiskParametersAsset>();
        /// <summary>
        /// ["<c>perpetual_swap_config</c>"] Perpetual swap config
        /// </summary>
        [JsonPropertyName("perpetual_swap_config")]
        public CryptoComRiskParametersSwap[] SwapConfig { get; set; } = Array.Empty<CryptoComRiskParametersSwap>();
        /// <summary>
        /// ["<c>futures_config</c>"] Futures config
        /// </summary>
        [JsonPropertyName("futures_config")]
        public CryptoComRiskParametersSwap[] FuturesConfig { get; set; } = Array.Empty<CryptoComRiskParametersSwap>();
    }

    /// <summary>
    /// Swap config
    /// </summary>
    [SerializationModel]
    public record CryptoComRiskParametersSwap
    {
        /// <summary>
        /// ["<c>instrument_name</c>"] Asset name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>max_order_notional_usd</c>"] Max order notional value in USD
        /// </summary>
        [JsonPropertyName("max_order_notional_usd")]
        public decimal? MaxOrderNotionalUsd { get; set; }
    }

    /// <summary>
    /// Asset config
    /// </summary>
    [SerializationModel]
    public record CryptoComRiskParametersAsset
    {
        /// <summary>
        /// ["<c>instrument_name</c>"] Asset name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateral_cap_notional</c>"] the maximum $notional that is counted towards the margin balance. Any additional token balance would not contribute to the margin balance
        /// </summary>
        [JsonPropertyName("collateral_cap_notional")]
        public decimal? CollateralCapNotional { get; set; }
        /// <summary>
        /// ["<c>max_product_leverage_for_spot</c>"] Max product leverage for spot
        /// </summary>
        [JsonPropertyName("max_product_leverage_for_spot")]
        public decimal? MaxProductLeverageForSpot { get; set; }
        /// <summary>
        /// ["<c>max_product_leverage_for_perps</c>"] Max product leverage for perps
        /// </summary>
        [JsonPropertyName("max_product_leverage_for_perps")]
        public decimal? MaxProductLeverageForPerps { get; set; }
        /// <summary>
        /// ["<c>max_product_leverage_for_futures</c>"] Max product leverage for futures
        /// </summary>
        [JsonPropertyName("max_product_leverage_for_futures")]
        public decimal? MaxProductLeverageForFutures { get; set; }
        /// <summary>
        /// ["<c>minimum_haircut</c>"] Minimum haircut
        /// </summary>
        [JsonPropertyName("minimum_haircut")]
        public decimal MinimumHaircut { get; set; }
        /// <summary>
        /// ["<c>unit_margin_rate</c>"] Unit margin rate
        /// </summary>
        [JsonPropertyName("unit_margin_rate")]
        public decimal UnitMarginRate { get; set; }
        /// <summary>
        /// ["<c>max_short_sell_limit</c>"] Max negative asset balance user can hold on the base token
        /// </summary>
        [JsonPropertyName("max_short_sell_limit")]
        public decimal MaxShortSellLimit { get; set; }
        /// <summary>
        /// ["<c>daily_notional_limit</c>"] Max spot order notional user can place in rolling 24-hour window. If field is omitted, user can trade unlimited on this base token
        /// </summary>
        [JsonPropertyName("daily_notional_limit")]
        public decimal? DailyNotionalLimit { get; set; }
        /// <summary>
        /// ["<c>order_limit</c>"] Max value per order in USD
        /// </summary>
        [JsonPropertyName("order_limit")]
        public decimal? OrderLimit { get; set; }
        /// <summary>
        /// ["<c>max_order_notional_usd</c>"] Max notional order value in USD
        /// </summary>
        [JsonPropertyName("max_order_notional_usd")]
        public decimal MaxOrderNotionalUsd { get; set; }
        /// <summary>
        /// ["<c>min_order_notional_usd</c>"] Min notional order value in USD
        /// </summary>
        [JsonPropertyName("min_order_notional_usd")]
        public decimal MinOrderNotionalUsd { get; set; }
        /// <summary>
        /// ["<c>long_pos_limit_perps</c>"] Long position limit for perps
        /// </summary>
        [JsonPropertyName("long_pos_limit_perps")]
        public decimal LongPosLimitPerps { get; set; }
        /// <summary>
        /// ["<c>short_pos_limit_perps</c>"] Short position limit for perps
        /// </summary>
        [JsonPropertyName("short_pos_limit_perps")]
        public decimal ShortPosLimitPerps { get; set; }
        /// <summary>
        /// ["<c>long_pos_limit_futures</c>"] Long position limit for futures
        /// </summary>
        [JsonPropertyName("long_pos_limit_futures")]
        public decimal LongPosLimitFutures { get; set; }
        /// <summary>
        /// ["<c>short_pos_limit_futures</c>"] Short position limit for futures
        /// </summary>
        [JsonPropertyName("short_pos_limit_futures")]
        public decimal ShortPosLimitFutures { get; set; }
    }


}
