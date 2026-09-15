using CryptoCom.Net;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Interfaces;
using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Objects.Options;
using CryptoCom.Net.SymbolOrderBooks;
using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services such as the ICryptoComRestClient and ICryptoComSocketClient. Configures the services based on the provided configuration.<br />
        /// See <see href="https://github.com/JKorf/CryptoCom.Net/blob/main/Examples/example-config.json" /> for an example of how to set up the configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddCryptoCom(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = CryptoComOptions.CreateFromConfiguration(configuration);
            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddCryptoComCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the ICryptoComRestClient and ICryptoComSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the CryptoCom services</param>
        /// <returns></returns>
        public static IServiceCollection AddCryptoCom(
            this IServiceCollection services,
            Action<CryptoComOptions>? optionsDelegate = null)
        {
            var options = CryptoComOptions.Create(optionsDelegate);
            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddCryptoComCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddCryptoComCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<ICryptoComRestClient, CryptoComRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<CryptoComRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new CryptoComRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<CryptoComRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var options = serviceProvider.GetRequiredService<IOptions<CryptoComRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(ICryptoComSocketClient), x => { return new CryptoComSocketClient(x.GetRequiredService<IOptions<CryptoComSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<ICryptoComOrderBookFactory, CryptoComOrderBookFactory>();
            services.AddTransient<ICryptoComTrackerFactory, CryptoComTrackerFactory>();
            services.AddTransient<ITrackerFactory, CryptoComTrackerFactory>();
            services.AddSingleton<ICryptoComUserClientProvider, CryptoComUserClientProvider>(x =>
            new CryptoComUserClientProvider(
                x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(ICryptoComRestClient).Name),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<CryptoComRestOptions>>(),
                x.GetRequiredService<IOptions<CryptoComSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<ICryptoComRestClient>().ExchangeApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<ICryptoComSocketClient>().ExchangeApi.SharedClient);

            services.RegisterSharedApiClient<
                ICryptoComSharedApiClient,
                CryptoComSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.Rest)
                    .Add(client => client.Socket)
                    );
            return services;
        }
    }
}
