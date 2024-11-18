using CryptoExchange.Net.Objects.Options;

namespace CryptoCom.Net.Objects.Options
{
    /// <summary>
    /// Options for the CryptoComRestClient
    /// </summary>
    public class CryptoComRestOptions : RestExchangeOptions<CryptoComEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static CryptoComRestOptions Default { get; set; } = new CryptoComRestOptions()
        {
            Environment = CryptoComEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Exchange API options
        /// </summary>
        public RestApiOptions ExchangeOptions { get; private set; } = new RestApiOptions();

        internal CryptoComRestOptions Set(CryptoComRestOptions targetOptions)
        {
            targetOptions = base.Set<CryptoComRestOptions>(targetOptions);
            targetOptions.ExchangeOptions = ExchangeOptions.Set(targetOptions.ExchangeOptions);
            return targetOptions;
        }
    }
}
