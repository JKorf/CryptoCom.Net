using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Deposit status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DepositStatus>))]
    public enum DepositStatus
    {
        /// <summary>
        /// Not arrived
        /// </summary>
        [Map("0")]
        NotArrived,
        /// <summary>
        /// Arrived
        /// </summary>
        [Map("1")]
        Arrived,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("2")]
        Failed,
        /// <summary>
        /// Pending
        /// </summary>
        [Map("3")]
        Pending
    }
}
