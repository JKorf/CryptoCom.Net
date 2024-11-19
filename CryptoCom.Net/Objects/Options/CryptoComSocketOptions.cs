using CryptoExchange.Net.Objects.Options;
using System;

namespace CryptoCom.Net.Objects.Options
{
    /// <summary>
    /// Options for the CryptoComSocketClient
    /// </summary>
    public class CryptoComSocketOptions : SocketExchangeOptions<CryptoComEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static CryptoComSocketOptions Default { get; set; } = new CryptoComSocketOptions()
        {
            Environment = CryptoComEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10,
            DelayAfterConnect = TimeSpan.FromSeconds(1)
        };

        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Exchange API options
        /// </summary>
        public SocketApiOptions ExchangeOptions { get; private set; } = new SocketApiOptions();

        internal CryptoComSocketOptions Set(CryptoComSocketOptions targetOptions)
        {
            targetOptions = base.Set<CryptoComSocketOptions>(targetOptions);
            targetOptions.ExchangeOptions = ExchangeOptions.Set(targetOptions.ExchangeOptions);
            return targetOptions;
        }
    }
}
