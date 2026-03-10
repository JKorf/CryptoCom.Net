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
        /// ["<c>POST_ONLY</c>"] Post only order
        /// </summary>
        [Map("POST_ONLY")]
        PostOnly,
        /// <summary>
        /// ["<c>LIQUIDATION</c>"] Liquidation order
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation,
        /// <summary>
        /// ["<c>SMART_POST_ONLY</c>"] Smart post only
        /// </summary>
        [Map("SMART_POST_ONLY")]
        SmartPostOnly,
        /// <summary>
        /// ["<c>ISOLATED_MARGIN</c>"] Isolated margin
        /// </summary>
        [Map("ISOLATED_MARGIN")]
        IsolatedMargin
    }
}
