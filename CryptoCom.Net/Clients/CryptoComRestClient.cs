using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using CryptoExchange.Net.Authentication;
using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoCom.Net.Clients.ExchangeApi;

namespace CryptoCom.Net.Clients
{
    /// <inheritdoc cref="ICryptoComRestClient" />
    public class CryptoComRestClient : BaseRestClient, ICryptoComRestClient
    {
        #region Api clients

        
         /// <inheritdoc />
        public ICryptoComRestClientExchangeApi ExchangeApi { get; }


        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the CryptoComRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public CryptoComRestClient(Action<CryptoComRestOptions>? optionsDelegate = null) : this(null, null, optionsDelegate)
        {
        }

        /// <summary>
        /// Create a new instance of the CryptoComRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public CryptoComRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, Action<CryptoComRestOptions>? optionsDelegate = null) : base(loggerFactory, "CryptoCom")
        {
            var options = CryptoComRestOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            
            ExchangeApi = AddApiClient(new CryptoComRestClientExchangeApi(_logger, httpClient, options));

        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<CryptoComRestOptions> optionsDelegate)
        {
            var options = CryptoComRestOptions.Default.Copy();
            optionsDelegate(options);
            CryptoComRestOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            
            ExchangeApi.SetApiCredentials(credentials);

        }
    }
}
