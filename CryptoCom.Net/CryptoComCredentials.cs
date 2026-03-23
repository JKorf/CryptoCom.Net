using CryptoExchange.Net.Authentication;
using System;

namespace CryptoCom.Net
{
    /// <summary>
    /// CryptoCom API credentials
    /// </summary>
    public class CryptoComCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials
        /// </summary>
        public CryptoComCredentials() { }

        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public CryptoComCredentials(string key, string secret) : base(key, secret)
        {
        }

        /// <summary>
        /// Create new credentials providing HMAC credentials
        /// </summary>
        /// <param name="credential">HMAC credentials</param>
        public CryptoComCredentials(HMACCredential credential) : base(credential.Key, credential.Secret)
        {
        }

        /// <summary>
        /// Specify the HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public CryptoComCredentials WithHMAC(string key, string secret)
        {
            if (!string.IsNullOrEmpty(Key)) throw new InvalidOperationException("Credentials already set");

            Key = key;
            Secret = secret;
            return this;
        }
    }
}
