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

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ICryptoComRestClient and ICryptoComSocketClient to the sevice collection so they can be injected
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultRestOptionsDelegate">Set default options for the rest client</param>
        /// <param name="defaultSocketOptionsDelegate">Set default options for the socket client</param>
        /// <param name="socketClientLifeTime">The lifetime of the ICryptoComSocketClient for the service collection. Defaults to Singleton.</param>
        /// <returns></returns>
        public static IServiceCollection AddCryptoCom(
            this IServiceCollection services,
            Action<CryptoComRestOptions>? defaultRestOptionsDelegate = null,
            Action<CryptoComSocketOptions>? defaultSocketOptionsDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            var restOptions = CryptoComRestOptions.Default.Copy();

            if (defaultRestOptionsDelegate != null)
            {
                defaultRestOptionsDelegate(restOptions);
                CryptoComRestClient.SetDefaultOptions(defaultRestOptionsDelegate);
            }

            if (defaultSocketOptionsDelegate != null)
                CryptoComSocketClient.SetDefaultOptions(defaultSocketOptionsDelegate);

            services.AddHttpClient<ICryptoComRestClient, CryptoComRestClient>(options =>
            {
                options.Timeout = restOptions.RequestTimeout;
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                if (restOptions.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{restOptions.Proxy.Host}:{restOptions.Proxy.Port}"),
                        Credentials = restOptions.Proxy.Password == null ? null : new NetworkCredential(restOptions.Proxy.Login, restOptions.Proxy.Password)
                    };
                }
                return handler;
            });

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddSingleton<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<ICryptoComOrderBookFactory, CryptoComOrderBookFactory>();

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<ICryptoComRestClient>().ExchangeApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<ICryptoComSocketClient>().ExchangeApi.SharedClient);

            if (socketClientLifeTime == null)
                services.AddSingleton<ICryptoComSocketClient, CryptoComSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(ICryptoComSocketClient), typeof(CryptoComSocketClient), socketClientLifeTime.Value));
            return services;
        }
    }
}
