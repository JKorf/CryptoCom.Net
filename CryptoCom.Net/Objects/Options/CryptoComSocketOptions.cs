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
        public static CryptoComSocketOptions Default { get; set; } = new CryptoComSocketOptions()
        {
            Environment = CryptoComEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10,
            DelayAfterConnect = TimeSpan.FromSeconds(1)
        };

        
         /// <summary>
        /// Exchange API options
        /// </summary>
        public SocketApiOptions ExchangeOptions { get; private set; } = new SocketApiOptions();


        internal CryptoComSocketOptions Copy()
        {
            var options = Copy<CryptoComSocketOptions>();
            
            options.ExchangeOptions = ExchangeOptions.Copy<SocketApiOptions>();

            return options;
        }
    }
}
