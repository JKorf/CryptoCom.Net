using CryptoCom.Net.Interfaces.Clients.ExchangeApi;

namespace CryptoCom.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Crypto.com
    /// </summary>
    public interface ICryptoComSharedApiClient
    {
        /// <summary>
        /// REST shared API implementations
        /// </summary>
        ICryptoComRestClientExchangeSharedApi Rest { get; }

        /// <summary>
        /// WebSocket shared API implementations
        /// </summary>
        ICryptoComSocketClientExchangeSharedApi Socket { get; }
    }
}
