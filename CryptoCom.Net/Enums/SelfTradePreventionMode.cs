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
        /// Cancel maker
        /// </summary>
        [Map("M")]
        CancelMaker,
        /// <summary>
        /// Cancel taker
        /// </summary>
        [Map("T")]
        CancelTaker,
        /// <summary>
        /// Cancel both maker and taker
        /// </summary>
        [Map("B")]
        CancelBoth,
    }

}
