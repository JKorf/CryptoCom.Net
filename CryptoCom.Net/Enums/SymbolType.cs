using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Symbol type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolType>))]
    public enum SymbolType
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("CCY_PAIR")]
        Spot,
        /// <summary>
        /// Perpetual swap
        /// </summary>
        [Map("PERPETUAL_SWAP")]
        PerpetualSwap,
        /// <summary>
        /// Future
        /// </summary>
        [Map("FUTURE")]
        DeliveryFuture,
        /// <summary>
        /// CRO stake
        /// </summary>
        [Map("CRO_STAKE")]
        CroStake
    }
}
