using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace CryptoCom.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the CryptoCom websocket API
    /// </summary>
    public interface ICryptoComSocketClient : ISocketClient
    {

        /// <summary>
        /// Exchange API endpoints
        /// </summary>
        /// <see cref="ICryptoComSocketClientExchangeApi"/>
        public ICryptoComSocketClientExchangeApi ExchangeApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
