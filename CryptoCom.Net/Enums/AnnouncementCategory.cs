using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Announcement category
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AnnouncementCategory>))]
    public enum AnnouncementCategory
    {
        /// <summary>
        /// Delist
        /// </summary>
        [Map("delist")]
        Delisting,
        /// <summary>
        /// Listing
        /// </summary>
        [Map("list")]
        Listing,
        /// <summary>
        /// Event
        /// </summary>
        [Map("event")]
        Event,
        /// <summary>
        /// Product
        /// </summary>
        [Map("product")]
        Product,
        /// <summary>
        /// System
        /// </summary>
        [Map("system")]
        System
    }
}
