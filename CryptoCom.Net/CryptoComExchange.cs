using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net
{
    /// <summary>
    /// CryptoCom exchange information and configuration
    /// </summary>
    public static class CryptoComExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "CryptoCom";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.crypto.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#introduction"
            };

        /// <summary>
        /// Rate limiter configuration for the CryptoCom API
        /// </summary>
        public static CryptoComRateLimiters RateLimiter { get; } = new CryptoComRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the CryptoCom API
    /// </summary>
    public class CryptoComRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal CryptoComRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            RestPrivate = new RateLimitGate("Rest Private")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, Array.Empty<IGuardFilter>(), 3, TimeSpan.FromMilliseconds(100), RateLimitWindowType.Sliding));
            RestPublic = new RateLimitGate("Rest Public")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, Array.Empty<IGuardFilter>(), 100, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            RestStaking = new RateLimitGate("Rest Staking")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, Array.Empty<IGuardFilter>(), 50, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            SocketPublic = new RateLimitGate("Socket Public")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, new LimitItemTypeFilter(RateLimitItemType.Request), 100, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            SocketPrivate = new RateLimitGate("Socket Private")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, new LimitItemTypeFilter(RateLimitItemType.Request), 150, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            RestPrivate.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestPublic.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestStaking.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            SocketPublic.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            SocketPrivate.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }


        internal IRateLimitGate RestPrivate { get; private set; }
        internal IRateLimitGate RestPublic { get; private set; }
        internal IRateLimitGate RestStaking { get; private set; }
        internal IRateLimitGate SocketPublic { get; private set; }
        internal IRateLimitGate SocketPrivate { get; private set; }

    }
}
