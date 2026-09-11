using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace CryptoCom.Net.Objects.Options
{
    /// <summary>
    /// CryptoCom options
    /// </summary>
    public class CryptoComOptions : LibraryOptions<CryptoComRestOptions, CryptoComSocketOptions, CryptoComCredentials, CryptoComEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();
    }
}
