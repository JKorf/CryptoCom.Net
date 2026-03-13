using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace CryptoCom.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the CryptoCom websocket API
    /// </summary>
    public interface ICryptoComSocketClient : ISocketClient<CryptoComCredentials>
    {

        /// <summary>
        /// Exchange API endpoints
        /// </summary>
        /// <see cref="ICryptoComSocketClientExchangeApi"/>
        public ICryptoComSocketClientExchangeApi ExchangeApi { get; }
    }
}
