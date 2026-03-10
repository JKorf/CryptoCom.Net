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
        /// ["<c>YES</c>"] Impacted
        /// </summary>
        [Map("YES")]
        Impacted,
        /// <summary>
        /// ["<c>BAU</c>"] Not impacted
        /// </summary>
        [Map("BAU")]
        NotImpacted,
        /// <summary>
        /// ["<c>Partial</c>"] Partially impacted
        /// </summary>
        [Map("Partial")]
        PartiallyImpacted
    }
}
