using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Internal
{
    internal class CryptoComSubscriptionEvent<T>
    {
        [JsonPropertyName("instrument_name")]
        public string? Symbol { get; set; } = string.Empty;
        [JsonPropertyName("subscription")]
        public string Subscription { get; set; } = string.Empty;
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
