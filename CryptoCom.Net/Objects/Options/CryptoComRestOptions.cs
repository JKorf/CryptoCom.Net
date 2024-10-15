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
        public static CryptoComRestOptions Default { get; set; } = new CryptoComRestOptions()
        {
            Environment = CryptoComEnvironment.Live,
            AutoTimestamp = true
        };

        
         /// <summary>
        /// Exchange API options
        /// </summary>
        public RestApiOptions ExchangeOptions { get; private set; } = new RestApiOptions();


        internal CryptoComRestOptions Copy()
        {
            var options = Copy<CryptoComRestOptions>();
            
            options.ExchangeOptions = ExchangeOptions.Copy<RestApiOptions>();

            return options;
        }
    }
}
