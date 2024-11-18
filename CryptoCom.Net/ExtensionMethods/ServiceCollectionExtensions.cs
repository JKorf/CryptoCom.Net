using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Interfaces;
using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Objects.Options;
using CryptoCom.Net.SymbolOrderBooks;
using CryptoCom.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services such as the ICryptoComRestClient and ICryptoComSocketClient. Configures the services based on the provided configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddCryptoCom(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new CryptoComOptions();
            // Reset environment so we know if theyre overriden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            configuration.Bind(options);

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restEnvName = options.Rest.Environment?.Name ?? options.Environment?.Name ?? CryptoComEnvironment.Live.Name;
            var socketEnvName = options.Socket.Environment?.Name ?? options.Environment?.Name ?? CryptoComEnvironment.Live.Name;
            options.Rest.Environment = CryptoComEnvironment.GetEnvironmentByName(restEnvName) ?? options.Rest.Environment!;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = CryptoComEnvironment.GetEnvironmentByName(socketEnvName) ?? options.Socket.Environment!;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;


            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

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
            var options = new CryptoComOptions();
            // Reset environment so we know if theyre overriden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            optionsDelegate?.Invoke(options);
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment = options.Rest.Environment ?? options.Environment ?? CryptoComEnvironment.Live;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = options.Socket.Environment ?? options.Environment ?? CryptoComEnvironment.Live;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;

            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddCryptoComCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// DEPRECATED; use <see cref="AddCryptoCom(IServiceCollection, Action{CryptoComOptions}?)" /> instead
        /// </summary>
        public static IServiceCollection AddCryptoCom(
            this IServiceCollection services,
            Action<CryptoComRestOptions> restDelegate,
            Action<CryptoComSocketOptions>? socketDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.Configure<CryptoComRestOptions>((x) => { restDelegate?.Invoke(x); });
            services.Configure<CryptoComSocketOptions>((x) => { socketDelegate?.Invoke(x); });

            return AddCryptoComCore(services, socketClientLifeTime);
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
                var handler = new HttpClientHandler();
                try
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                catch (PlatformNotSupportedException)
                { }

                var options = serviceProvider.GetRequiredService<IOptions<CryptoComRestOptions>>().Value;
                if (options.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{options.Proxy.Host}:{options.Proxy.Port}"),
                        Credentials = options.Proxy.Password == null ? null : new NetworkCredential(options.Proxy.Login, options.Proxy.Password)
                    };
                }
                return handler;
            });
            services.Add(new ServiceDescriptor(typeof(ICryptoComSocketClient), x => { return new CryptoComSocketClient(x.GetRequiredService<IOptions<CryptoComSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddSingleton<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<ICryptoComOrderBookFactory, CryptoComOrderBookFactory>();
            services.AddTransient<ICryptoComTrackerFactory, CryptoComTrackerFactory>();

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<ICryptoComRestClient>().ExchangeApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<ICryptoComSocketClient>().ExchangeApi.SharedClient);

            return services;
        }
    }
}
