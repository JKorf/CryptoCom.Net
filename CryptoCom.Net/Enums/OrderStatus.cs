using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Active
        /// </summary>
        [Map("ACTIVE")]
        Active,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,

    }

}
