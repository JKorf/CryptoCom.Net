using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        SmartPostOnly
    }
}
