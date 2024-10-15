using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;

namespace CryptoCom.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the CryptoCom Rest API. 
    /// </summary>
    public interface ICryptoComRestClient : IRestClient
    {
        
        /// <summary>
        /// Exchange API endpoints
        /// </summary>
        public ICryptoComRestClientExchangeApi ExchangeApi { get; }


        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
