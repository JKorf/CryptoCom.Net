using CryptoExchange.Net.Interfaces;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the CryptoCom REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static ICryptoComRestClient CryptoCom(this ICryptoRestClient baseClient) => baseClient.TryGet<ICryptoComRestClient>(() => new CryptoComRestClient());

        /// <summary>
        /// Get the CryptoCom Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static ICryptoComSocketClient CryptoCom(this ICryptoSocketClient baseClient) => baseClient.TryGet<ICryptoComSocketClient>(() => new CryptoComSocketClient());
    }
}
