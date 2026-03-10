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
        /// ["<c>delist</c>"] Delist
        /// </summary>
        [Map("delist")]
        Delisting,
        /// <summary>
        /// ["<c>list</c>"] Listing
        /// </summary>
        [Map("list")]
        Listing,
        /// <summary>
        /// ["<c>event</c>"] Event
        /// </summary>
        [Map("event")]
        Event,
        /// <summary>
        /// ["<c>product</c>"] Product
        /// </summary>
        [Map("product")]
        Product,
        /// <summary>
        /// ["<c>system</c>"] System
        /// </summary>
        [Map("system")]
        System
    }
}
