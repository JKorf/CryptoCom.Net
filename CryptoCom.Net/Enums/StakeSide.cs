using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Staking request side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StakeSide>))]
    public enum StakeSide
    {
        /// <summary>
        /// Stake
        /// </summary>
        [Map("STAKE")]
        Stake,
        /// <summary>
        /// Unstake
        /// </summary>
        [Map("UNSTAKE")]
        Unstake
    }

}
