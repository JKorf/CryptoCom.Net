using CryptoCom.Net.Clients;
using CryptoCom.Net.Objects.Models;
using CryptoCom.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CryptoCom.Net.UnitTests
{
    [NonParallelizable]
    internal class CryptoComSocketIntegrationTests : SocketIntegrationTest<CryptoComSocketClient>
    {
        public override bool Run { get; set; } = true;

        public CryptoComSocketIntegrationTests()
        {
        }

        public override CryptoComSocketClient GetClient(ILoggerFactory loggerFactory, bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new CryptoComSocketClient(Options.Create(new CryptoComSocketOptions
            {
                OutputOriginalData = true,
                UseUpdatedDeserialization = useUpdatedDeserialization,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSubscriptions(bool useUpdatedDeserialization)
        {
            await RunAndCheckUpdate<CryptoComTicker>(useUpdatedDeserialization , (client, updateHandler) => client.ExchangeApi.SubscribeToBalanceUpdatesAsync(default , default), false, true);
            await RunAndCheckUpdate<CryptoComTicker>(useUpdatedDeserialization , (client, updateHandler) => client.ExchangeApi.SubscribeToTickerUpdatesAsync("ETH_USD", updateHandler, default), true, false);
        } 
    }
}
