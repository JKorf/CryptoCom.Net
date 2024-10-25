using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;

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
        /// Format a base and quote asset to a Crypto.com recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            if (tradingMode == TradingMode.Spot)
                return $"{baseAsset.ToUpperInvariant()}_{quoteAsset.ToUpperInvariant()}";

            if (tradingMode.IsPerpetual())
                return $"{baseAsset.ToUpperInvariant()}{quoteAsset.ToUpperInvariant()}-PERP";

            if (deliverTime == null)
                throw new ArgumentException("DeliverDate required to format delivery futures symbol");

            return $"{baseAsset.ToUpperInvariant()}{quoteAsset.ToUpperInvariant()}-{deliverTime.Value.ToString("yyMMdd")}";
        }

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
           RestPrivateSpecific = new RateLimitGate("Rest Specific")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, new ExactPathsFilter(["/exchange/v1/private/create-order", "/exchange/v1/private/cancel-order", "/exchange/v1/private/cancel-all-orders"]), 15, TimeSpan.FromMilliseconds(100), RateLimitWindowType.Sliding))
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, new ExactPathsFilter(["/exchange/v1/private/get-order-detail"]), 30, TimeSpan.FromMilliseconds(100), RateLimitWindowType.Sliding))
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, new ExactPathsFilter(["private/get-trades", "private/get-order-history"]), 1, TimeSpan.FromMilliseconds(1000), RateLimitWindowType.Sliding));
            RestPublic = new RateLimitGate("Rest Public")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, Array.Empty<IGuardFilter>(), 100, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            RestStaking = new RateLimitGate("Rest Staking")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, Array.Empty<IGuardFilter>(), 50, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            Socket = new RateLimitGate("Socket Public")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, [new LimitItemTypeFilter(RateLimitItemType.Request), new ExactPathFilter("/exchange/v1/market")], 100, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding))
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, [new LimitItemTypeFilter(RateLimitItemType.Request), new ExactPathFilter("/exchange/v1/user")], 150, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            RestPrivate.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestPrivateSpecific.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestPublic.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestStaking.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            Socket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }


        internal IRateLimitGate RestPrivate { get; private set; }
        internal IRateLimitGate RestPrivateSpecific { get; private set; }
        internal IRateLimitGate RestPublic { get; private set; }
        internal IRateLimitGate RestStaking { get; private set; }
        internal IRateLimitGate Socket { get; private set; }

    }
}
