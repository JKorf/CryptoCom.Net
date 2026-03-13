using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace CryptoCom.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the CryptoCom Rest API. 
    /// </summary>
    public interface ICryptoComRestClient : IRestClient<CryptoComCredentials>
    {

        /// <summary>
        /// Exchange API endpoints
        /// </summary>
        /// <see cref="ICryptoComRestClientExchangeApi"/>
        public ICryptoComRestClientExchangeApi ExchangeApi { get; }

    }
}
