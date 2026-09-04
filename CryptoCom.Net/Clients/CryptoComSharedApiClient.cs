using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;

namespace CryptoCom.Net.Clients
{
    /// <inheritdoc />
    public class CryptoComSharedApiClient : ICryptoComSharedApiClient
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
            ICryptoComSocketClient socketClient)
        {
            Rest = restClient.ExchangeApi.SharedApi;
            Socket = socketClient.ExchangeApi.SharedApi;
        }
    }
}
