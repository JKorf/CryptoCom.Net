using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// STP scope
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SelfTradePreventionScope>))]
    public enum SelfTradePreventionScope
    {
        /// <summary>
        /// ["<c>M</c>"] Matches master or sub a/c
        /// </summary>
        [Map("M")]
        MasterAndSubAccount,
        /// <summary>
        /// ["<c>S</c>"] Matches sub a/c only
        /// </summary>
        [Map("S")]
        SubAccount,
        /// <summary>
        /// ["<c>D</c>"] Unknown
        /// </summary>
        [Map("D")]
        Unknown
    }

}
