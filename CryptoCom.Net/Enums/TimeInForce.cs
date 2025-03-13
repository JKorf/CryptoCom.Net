using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Time in force
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TimeInForce>))]
    public enum TimeInForce
    {
        /// <summary>
        /// Good till cancel
        /// </summary>
        [Map("GOOD_TILL_CANCEL")]
        GoodTillCancel,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("IMMEDIATE_OR_CANCEL")]
        ImmediateOrCancel,
        /// <summary>
        /// Fill or kill
        /// </summary>
        [Map("FILL_OR_KILL")]
        FillOrKill,
    }

}
