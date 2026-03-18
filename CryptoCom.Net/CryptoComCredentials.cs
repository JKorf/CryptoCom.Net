using CryptoExchange.Net.Authentication;

namespace CryptoCom.Net
{
    /// <summary>
    /// CryptoCom API credentials
    /// </summary>
    public class CryptoComCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public CryptoComCredentials(string key, string secret) : base(key, secret)
        {
        }
    }
}
