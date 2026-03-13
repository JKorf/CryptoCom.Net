using CryptoExchange.Net.Authentication;

namespace CryptoCom.Net
{
    /// <summary>
    /// CryptoCom credentials
    /// </summary>
    public class CryptoComCredentials : ApiCredentials
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public CryptoComCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }
       
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public CryptoComCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new CryptoComCredentials(Hmac!);
    }
}
