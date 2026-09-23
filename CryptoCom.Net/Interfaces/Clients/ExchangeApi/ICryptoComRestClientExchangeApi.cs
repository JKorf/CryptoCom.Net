using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace CryptoCom.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// CryptoCom Exchange API endpoints
    /// </summary>
    public interface ICryptoComRestClientExchangeApi : IRestApiClient<CryptoComCredentials>, IDisposable
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
        /// [V1] Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
        /// </summary>
        public ICryptoComRestClientExchangeApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public ICryptoComRestClientExchangeSharedApi SharedApi { get; }
    }
}
