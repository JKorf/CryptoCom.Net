using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    /// <summary>
    /// Limit info
    /// </summary>
    public record CryptoComRiskParameters
    {
        /// <summary>
        /// Default max product leverage for spot
        /// </summary>
        [JsonPropertyName("default_max_product_leverage_for_spot")]
        public decimal DefaultMaxProductLeverageForSpot { get; set; }
        /// <summary>
        /// Default max product leverage for perps
        /// </summary>
        [JsonPropertyName("default_max_product_leverage_for_perps")]
        public decimal DefaultMaxProductLeverageForPerps { get; set; }
        /// <summary>
        /// Default max product leverage for futures
        /// </summary>
        [JsonPropertyName("default_max_product_leverage_for_futures")]
        public decimal DefaultMaxProductLeverageForFutures { get; set; }
        /// <summary>
        /// Default unit margin rate
        /// </summary>
        [JsonPropertyName("default_unit_margin_rate")]
        public decimal DefaultUnitMarginRate { get; set; }
        /// <summary>
        /// Default collateral cap
        /// </summary>
        [JsonPropertyName("default_collateral_cap")]
        public decimal? DefaultCollateralCap { get; set; }
        /// <summary>
        /// Update timestamp
        /// </summary>
        [JsonPropertyName("update_timestamp_ms")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Base asset config
        /// </summary>
        [JsonPropertyName("base_currency_config")]
        public IEnumerable<CryptoComRiskParametersAsset> BaseAssetConfig { get; set; } = Array.Empty<CryptoComRiskParametersAsset>();
    }

    /// <summary>
    /// 
    /// </summary>
    public record CryptoComRiskParametersAsset
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// the maximum $notional that is counted towards the margin balance. Any additional token balance would not contribute to the margin balance
        /// </summary>
        [JsonPropertyName("collateral_cap_notional")]
        public decimal? CollateralCapNotional { get; set; }
        /// <summary>
        /// Max product leverage for spot
        /// </summary>
        [JsonPropertyName("max_product_leverage_for_spot")]
        public decimal? MaxProductLeverageForSpot { get; set; }
        /// <summary>
        /// Max product leverage for perps
        /// </summary>
        [JsonPropertyName("max_product_leverage_for_perps")]
        public decimal? MaxProductLeverageForPerps { get; set; }
        /// <summary>
        /// Max product leverage for futures
        /// </summary>
        [JsonPropertyName("max_product_leverage_for_futures")]
        public decimal? MaxProductLeverageForFutures { get; set; }
        /// <summary>
        /// Minimum haircut
        /// </summary>
        [JsonPropertyName("minimum_haircut")]
        public decimal MinimumHaircut { get; set; }
        /// <summary>
        /// Unit margin rate
        /// </summary>
        [JsonPropertyName("unit_margin_rate")]
        public decimal UnitMarginRate { get; set; }
        /// <summary>
        /// Max negative asset balance user can hold on the base token
        /// </summary>
        [JsonPropertyName("max_short_sell_limit")]
        public decimal MaxShortSellLimit { get; set; }
        /// <summary>
        /// Max spot order notional user can place in rolling 24-hour window. If field is omitted, user can trade unlimited on this base token
        /// </summary>
        [JsonPropertyName("daily_notional_limit")]
        public decimal? DailyNotionalLimit { get; set; }
        /// <summary>
        /// Max value per order in USD
        /// </summary>
        [JsonPropertyName("order_limit")]
        public decimal? OrderLimit { get; set; }
    }


}
