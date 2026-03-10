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
        /// ["<c>0</c>"] Not arrived
        /// </summary>
        [Map("0")]
        NotArrived,
        /// <summary>
        /// ["<c>1</c>"] Arrived
        /// </summary>
        [Map("1")]
        Arrived,
        /// <summary>
        /// ["<c>2</c>"] Failed
        /// </summary>
        [Map("2")]
        Failed,
        /// <summary>
        /// ["<c>3</c>"] Pending
        /// </summary>
        [Map("3")]
        Pending
    }
}
