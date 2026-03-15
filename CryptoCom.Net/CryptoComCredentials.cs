using CryptoExchange.Net.Authentication;
using System;

namespace CryptoCom.Net
{
    /// <summary>
    /// CryptoCom credentials
    /// </summary>
    public class CryptoComCredentials : ApiCredentials
    {
        /// <summary>
        /// </summary>
        [Obsolete("Parameterless constructor is only for deserialization purposes and should not be used directly. Use parameterized constructor instead.")]
        public CryptoComCredentials() { }

        /// <summary>
        /// Create credentials using an HMAC key and secret.
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public CryptoComCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }

        /// <summary>
        /// Create CryptoCom credentials using HMAC credentials
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public CryptoComCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
#pragma warning disable CS0618 // Type or member is obsolete
        public override ApiCredentials Copy() => new CryptoComCredentials { CredentialPairs = CredentialPairs };
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
