using CryptoCom.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace CryptoCom.Net
{
    /// <inheritdoc/>
    public class CryptoComUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComUserSpotDataTracker(
            ILogger<CryptoComUserSpotDataTracker> logger,
            ICryptoComRestClient restClient,
            ICryptoComSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config) : base(
                logger,
                restClient.ExchangeApi.SharedClient,
                null,
                restClient.ExchangeApi.SharedClient,
                socketClient.ExchangeApi.SharedClient,
                restClient.ExchangeApi.SharedClient,
                socketClient.ExchangeApi.SharedClient,
                socketClient.ExchangeApi.SharedClient,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
        {
        }
    }

    /// <inheritdoc/>
    public class CryptoComUserFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc/>
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComUserFuturesDataTracker(
            ILogger<CryptoComUserFuturesDataTracker> logger,
            ICryptoComRestClient restClient,
            ICryptoComSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config) : base(logger,
                restClient.ExchangeApi.SharedClient,
                null,
                restClient.ExchangeApi.SharedClient,
                socketClient.ExchangeApi.SharedClient,
                restClient.ExchangeApi.SharedClient,
                socketClient.ExchangeApi.SharedClient,
                socketClient.ExchangeApi.SharedClient,
                socketClient.ExchangeApi.SharedClient,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {
        }
    }
}
