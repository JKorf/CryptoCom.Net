using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Valuation type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ValuationType>))]
    public enum ValuationType
    {
        /// <summary>
        /// Per minute data of underlying reference price of the instrument.
        /// </summary>
        [Map("index_price")]
        IndexPrice,
        /// <summary>
        /// Per minute data of mark price of the instrument.
        /// </summary>
        [Map("mark_price")]
        MarkPrice,
        /// <summary>
        /// Hourly data of the funding rate settled in past hourly settlement.
        /// </summary>
        [Map("funding_hist")]
        FundingHistory,
        /// <summary>
        /// Per minute data of current hourly funding rate that will settle at the end of each hour of current 4-hour interval.
        /// </summary>
        [Map("funding_rate")]
        FundingRate,
        /// <summary>
        /// Per minute data of estimated funding rate for the next interval.
        /// </summary>
        [Map("estimated_funding_rate")]
        EstimatedFundingRate
    }
}
