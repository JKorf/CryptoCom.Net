using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Internal
{
    internal class CryptoComSubscriptionEvent
    {
        [JsonPropertyName("instrument_name")]
        public string? Symbol { get; set; } = string.Empty;
        [JsonPropertyName("subscription")]
        public string Subscription { get; set; } = string.Empty;
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;

        [JsonPropertyName("depth")]
        public int? Depth { get; set; }

        [JsonPropertyName("interval")]
        public string? Interval { get; set; }
    }

    internal class CryptoComSubscriptionEvent<T> : CryptoComSubscriptionEvent
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
