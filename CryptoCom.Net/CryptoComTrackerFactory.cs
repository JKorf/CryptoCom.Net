using CryptoCom.Net.Clients;
using CryptoCom.Net.Interfaces;
using CryptoCom.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CryptoCom.Net
{
    /// <inheritdoc />
    public class CryptoComTrackerFactory : ICryptoComTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public CryptoComTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = (_serviceProvider?.GetRequiredService<ICryptoComSocketClient>() ?? new CryptoComSocketClient());
            return client.ExchangeApi.SharedClient.SubscribeKlineOptions.IsSupported(interval);
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = (_serviceProvider?.GetRequiredService<ICryptoComRestClient>() ?? new CryptoComRestClient()).ExchangeApi.SharedClient;
            var socketClient = (_serviceProvider?.GetRequiredService<ICryptoComSocketClient>() ?? new CryptoComSocketClient()).ExchangeApi.SharedClient;

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                socketClient,
                symbol,
                interval,
                limit,
                period
                );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = (_serviceProvider?.GetRequiredService<ICryptoComRestClient>() ?? new CryptoComRestClient()).ExchangeApi.SharedClient;
            var socketClient = (_serviceProvider?.GetRequiredService<ICryptoComSocketClient>() ?? new CryptoComSocketClient()).ExchangeApi.SharedClient;

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                null,
                socketClient,
                symbol,
                limit,
                period
                );
        }
    }
}
