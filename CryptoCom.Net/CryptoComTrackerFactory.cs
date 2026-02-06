using CryptoCom.Net.Clients;
using CryptoCom.Net.Interfaces;
using CryptoCom.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<ICryptoComRestClient>() ?? new CryptoComRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<ICryptoComSocketClient>() ?? new CryptoComSocketClient();
            return new CryptoComUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<CryptoComUserSpotDataTracker>>() ?? new NullLogger<CryptoComUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, ApiCredentials credentials, SpotUserDataTrackerConfig? config = null, CryptoComEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<ICryptoComUserClientProvider>() ?? new CryptoComUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new CryptoComUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<CryptoComUserSpotDataTracker>>() ?? new NullLogger<CryptoComUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<ICryptoComRestClient>() ?? new CryptoComRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<ICryptoComSocketClient>() ?? new CryptoComSocketClient();
            return new CryptoComUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<CryptoComUserFuturesDataTracker>>() ?? new NullLogger<CryptoComUserFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, ApiCredentials credentials, FuturesUserDataTrackerConfig? config = null, CryptoComEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<ICryptoComUserClientProvider>() ?? new CryptoComUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new CryptoComUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<CryptoComUserFuturesDataTracker>>() ?? new NullLogger<CryptoComUserFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }
    }
}
