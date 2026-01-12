using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Execution mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ExecutionInstruction>))]
    public enum ExecutionInstruction
    {
        /// <summary>
        /// Post only order
        /// </summary>
        [Map("POST_ONLY")]
        PostOnly,
        /// <summary>
        /// Liquidation order
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation,
        /// <summary>
        /// Smart post only
        /// </summary>
        [Map("SMART_POST_ONLY")]
        SmartPostOnly,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("ISOLATED_MARGIN")]
        IsolatedMargin
    }
}
