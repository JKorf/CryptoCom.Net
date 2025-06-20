using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Announcement impact
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AnnouncementImpact>))]
    public enum AnnouncementImpact
    {
        /// <summary>
        /// Impacted
        /// </summary>
        [Map("YES")]
        Impacted,
        /// <summary>
        /// Not impacted
        /// </summary>
        [Map("BAU")]
        NotImpacted,
        /// <summary>
        /// Partially impacted
        /// </summary>
        [Map("Partial")]
        PartiallyImpacted
    }
}
