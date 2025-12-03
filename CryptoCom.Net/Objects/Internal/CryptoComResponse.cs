using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Internal
{
    internal class CryptoComResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        public virtual object? GetResult() => null;
    }

    internal class CryptoComResponse<T> : CryptoComResponse
    {
        [JsonPropertyName("result")]
        public T Result { get; set; } = default!;

        public override object GetResult() => Result;
    }
}
