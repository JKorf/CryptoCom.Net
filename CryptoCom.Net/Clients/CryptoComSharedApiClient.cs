using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoCom.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;

namespace CryptoCom.Net.Clients
{
    /// <inheritdoc />
    public class CryptoComSharedApiClient : SharedApiClientBase, ICryptoComSharedApiClient
    {
        /// <inheritdoc />
        public ICryptoComRestClientExchangeSharedApi Rest { get; }
        /// <inheritdoc />
        public ICryptoComSocketClientExchangeSharedApi Socket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComSharedApiClient(
            ICryptoComRestClient restClient,
            ICryptoComSocketClient socketClient,
            IOptions<CryptoComOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                    restClient.ExchangeApi.SharedApi,
                    socketClient.ExchangeApi.SharedApi
                  )
        {
            Rest = restClient.ExchangeApi.SharedApi;
            Socket = socketClient.ExchangeApi.SharedApi;
        }
    }
}
