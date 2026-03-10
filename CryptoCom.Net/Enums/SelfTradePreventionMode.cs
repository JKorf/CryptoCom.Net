using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SelfTradePreventionMode>))]
    public enum SelfTradePreventionMode
    {
        /// <summary>
        /// ["<c>M</c>"] Cancel maker
        /// </summary>
        [Map("M")]
        CancelMaker,
        /// <summary>
        /// ["<c>T</c>"] Cancel taker
        /// </summary>
        [Map("T")]
        CancelTaker,
        /// <summary>
        /// ["<c>B</c>"] Cancel both maker and taker
        /// </summary>
        [Map("B")]
        CancelBoth,
    }

}
