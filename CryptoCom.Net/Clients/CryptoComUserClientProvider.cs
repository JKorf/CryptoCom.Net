using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace CryptoCom.Net.Clients
{
    /// <inheritdoc />
    public class CryptoComUserClientProvider : ICryptoComUserClientProvider
    {
        private ConcurrentDictionary<string, ICryptoComRestClient> _restClients = new ConcurrentDictionary<string, ICryptoComRestClient>();
        private ConcurrentDictionary<string, ICryptoComSocketClient> _socketClients = new ConcurrentDictionary<string, ICryptoComSocketClient>();

        private readonly IOptions<CryptoComRestOptions> _restOptions;
        private readonly IOptions<CryptoComSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

        /// <inheritdoc />
        public string ExchangeName => CryptoComExchange.ExchangeName;

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
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, CryptoComEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public ICryptoComRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, CryptoComEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public ICryptoComSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, CryptoComEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private ICryptoComRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, CryptoComEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new CryptoComRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private ICryptoComSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, CryptoComEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new CryptoComSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<CryptoComRestOptions> SetRestEnvironment(CryptoComEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new CryptoComRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<CryptoComSocketOptions> SetSocketEnvironment(CryptoComEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new CryptoComSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
