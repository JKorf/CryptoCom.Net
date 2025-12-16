using CryptoExchange.Net.Interfaces.Clients;
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
        /// <see cref="ICryptoComRestClientExchangeApiAccount"/>
        public ICryptoComRestClientExchangeApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="ICryptoComRestClientExchangeApiExchangeData"/>
        public ICryptoComRestClientExchangeApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to staking
        /// </summary>
        /// <see cref="ICryptoComRestClientExchangeApiStaking"/>
        public ICryptoComRestClientExchangeApiStaking Staking { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="ICryptoComRestClientExchangeApiTrading"/>
        public ICryptoComRestClientExchangeApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public ICryptoComRestClientExchangeApiShared SharedClient { get; }
    }
}
