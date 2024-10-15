using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using System;
using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Objects.Options;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoCom.Net.Clients.ExchangeApi;

namespace CryptoCom.Net.Clients
{
    /// <inheritdoc cref="ICryptoComSocketClient" />
    public class CryptoComSocketClient : BaseSocketClient, ICryptoComSocketClient
    {
        #region fields
        #endregion

        #region Api clients

        
         /// <inheritdoc />
        public ICryptoComSocketClientExchangeApi ExchangeApi { get; }


        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of CryptoComSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        public CryptoComSocketClient(ILoggerFactory? loggerFactory = null) : this((x) => { }, loggerFactory)
        {
        }

        /// <summary>
        /// Create a new instance of CryptoComSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public CryptoComSocketClient(Action<CryptoComSocketOptions> optionsDelegate) : this(optionsDelegate, null)
        {
        }

        /// <summary>
        /// Create a new instance of CryptoComSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public CryptoComSocketClient(Action<CryptoComSocketOptions>? optionsDelegate, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "CryptoCom")
        {
            var options = CryptoComSocketOptions.Default.Copy();
            optionsDelegate?.Invoke(options);
            Initialize(options);

            
            ExchangeApi = AddApiClient(new CryptoComSocketClientExchangeApi(_logger, options));

        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<CryptoComSocketOptions> optionsDelegate)
        {
            var options = CryptoComSocketOptions.Default.Copy();
            optionsDelegate(options);
            CryptoComSocketOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            
            ExchangeApi.SetApiCredentials(credentials);

        }
    }
}
