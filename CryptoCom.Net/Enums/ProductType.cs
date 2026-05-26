using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Product type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ProductType>))]
    public enum ProductType
    {
        /// <summary>
        /// ["<c>COMMODITIES</c>"] Commodities
        /// </summary>
        [Map("COMMODITIES")]
        Commodities,
        /// <summary>
        /// ["<c>EQUITY_IND</c>"] Equity index
        /// </summary>
        [Map("EQUITY_IND")]
        EquityIndex,
        /// <summary>
        /// ["<c>CURRENCIES</c>"] Currencies
        /// </summary>
        [Map("CURRENCIES")]
        Currencies,
        /// <summary>
        /// ["<c>DIGITAL_CURRENCIES</c>"] Digital currencies
        /// </summary>
        [Map("DIGITAL_CURRENCIES")]
        DigitalCurrencies,
        /// <summary>
        /// ["<c>EVENTS</c>"] Events
        /// </summary>
        [Map("EVENTS")]
        Events,
        /// <summary>
        /// ["<c>EQUITY</c>"] Equity
        /// </summary>
        [Map("EQUITY")]
        Equity,
        /// <summary>
        /// ["<c>PRE_IPO</c>"] Pre IPO
        /// </summary>
        [Map("PRE_IPO")]
        PreIpo
    }
}
