using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
using System;

namespace CryptoCom.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// CryptoCom Exchange API endpoints
    /// </summary>
    public interface ICryptoComRestClientExchangeApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public ICryptoComRestClientExchangeApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        public ICryptoComRestClientExchangeApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to staking
        /// </summary>
        public ICryptoComRestClientExchangeApiStaking Staking { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public ICryptoComRestClientExchangeApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exhanges to allow for a common implementation for different exchanges.
        /// </summary>
        public ICryptoComRestClientExchangeApiShared SharedClient { get; }
    }
}
