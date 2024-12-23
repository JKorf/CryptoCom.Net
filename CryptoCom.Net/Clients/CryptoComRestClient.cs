using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using CryptoExchange.Net.Authentication;
using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoCom.Net.Clients.ExchangeApi;
using Microsoft.Extensions.Options;
using CryptoExchange.Net.Objects.Options;

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
        public CryptoComRestClient(Action<CryptoComRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the CryptoComRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public CryptoComRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<CryptoComRestOptions> options) : base(loggerFactory, "CryptoCom")
        {
            Initialize(options.Value);

            ExchangeApi = AddApiClient(new CryptoComRestClientExchangeApi(_logger, httpClient, options.Value));
        }

        #endregion

        /// <inheritdoc />
        public void SetOptions(UpdateOptions options)
        {
            ExchangeApi.SetOptions(options);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<CryptoComRestOptions> optionsDelegate)
        {
            CryptoComRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            
            ExchangeApi.SetApiCredentials(credentials);

        }
    }
}
