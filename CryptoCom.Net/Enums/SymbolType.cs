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
        /// ["<c>CCY_PAIR</c>"] Spot
        /// </summary>
        [Map("CCY_PAIR")]
        Spot,
        /// <summary>
        /// ["<c>PERPETUAL_SWAP</c>"] Perpetual swap
        /// </summary>
        [Map("PERPETUAL_SWAP")]
        PerpetualSwap,
        /// <summary>
        /// ["<c>FUTURE</c>"] Future
        /// </summary>
        [Map("FUTURE")]
        DeliveryFuture,
        /// <summary>
        /// ["<c>CRO_STAKE</c>"] CRO stake
        /// </summary>
        [Map("CRO_STAKE")]
        CroStake
    }
}
