using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace CryptoCom.Net.Clients
{
    /// <inheritdoc />
    public class CryptoComUserClientProvider : UserClientProvider<
        ICryptoComRestClient,
        ICryptoComSocketClient,
        CryptoComRestOptions,
        CryptoComSocketOptions,
        CryptoComCredentials,
        CryptoComEnvironment
        >, ICryptoComUserClientProvider
    {
       
        /// <inheritdoc />
        public override string ExchangeName => CryptoComExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public CryptoComUserClientProvider(Action<CryptoComOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<CryptoComRestOptions> restOptions,
            IOptions<CryptoComSocketOptions> socketOptions)
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override ICryptoComRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<CryptoComRestOptions> options)
            => new CryptoComRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override ICryptoComSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<CryptoComSocketOptions> options) 
            => new CryptoComSocketClient(options, loggerFactory);
    }
}
