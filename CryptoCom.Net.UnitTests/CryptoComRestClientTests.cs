using CryptoExchange.Net.Clients;
using NUnit.Framework;
using System.Collections.Generic;
using CryptoCom.Net.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CryptoExchange.Net.Objects;
using CryptoCom.Net.Interfaces.Clients;

namespace CryptoCom.Net.UnitTests
{
    [TestFixture()]
    public class CryptoComRestClientTests
    {
        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<CryptoComRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<CryptoComSocketClient>();
        }

        [Test]
        [TestCase(TradeEnvironmentNames.Live, "https://api.crypto.com/exchange/v1")]
        [TestCase("", "https://api.crypto.com/exchange/v1")]
        public void TestConstructorEnvironments(string environmentName, string expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "CryptoCom:Environment:Name", environmentName },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddCryptoCom(configuration.GetSection("CryptoCom"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<ICryptoComRestClient>();

            var address = client.ExchangeApi.BaseAddress;

            Assert.That(address, Is.EqualTo(expected));
        }

        [Test]
        public void TestConstructorNullEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "CryptoCom", null },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddCryptoCom(configuration.GetSection("CryptoCom"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<ICryptoComRestClient>();

            var address = client.ExchangeApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.crypto.com/exchange/v1"));
        }

        [Test]
        public void TestConstructorApiOverwriteEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "CryptoCom:Environment:Name", "test" },
                    { "CryptoCom:Rest:Environment:Name", "live" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddCryptoCom(configuration.GetSection("CryptoCom"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<ICryptoComRestClient>();

            var address = client.ExchangeApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.crypto.com/exchange/v1"));
        }

        [Test]
        public void TestConstructorConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ApiCredentials:Key", "123" },
                    { "ApiCredentials:Secret", "456" },
                    { "ApiCredentials:Memo", "000" },
                    { "Socket:ApiCredentials:Key", "456" },
                    { "Socket:ApiCredentials:Secret", "789" },
                    { "Socket:ApiCredentials:Memo", "xxx" },
                    { "Rest:OutputOriginalData", "true" },
                    { "Socket:OutputOriginalData", "false" },
                    { "Rest:Proxy:Host", "host" },
                    { "Rest:Proxy:Port", "80" },
                    { "Socket:Proxy:Host", "host2" },
                    { "Socket:Proxy:Port", "81" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddCryptoCom(configuration);
            var provider = collection.BuildServiceProvider();

            var restClient = provider.GetRequiredService<ICryptoComRestClient>();
            var socketClient = provider.GetRequiredService<ICryptoComSocketClient>();

            Assert.That(((BaseApiClient)restClient.ExchangeApi).OutputOriginalData, Is.True);
            Assert.That(((BaseApiClient)socketClient.ExchangeApi).OutputOriginalData, Is.False);
            Assert.That(((BaseApiClient)restClient.ExchangeApi).AuthenticationProvider.ApiKey, Is.EqualTo("123"));
            Assert.That(((BaseApiClient)socketClient.ExchangeApi).AuthenticationProvider.ApiKey, Is.EqualTo("456"));
            Assert.That(((BaseApiClient)restClient.ExchangeApi).ClientOptions.Proxy.Host, Is.EqualTo("host"));
            Assert.That(((BaseApiClient)restClient.ExchangeApi).ClientOptions.Proxy.Port, Is.EqualTo(80));
            Assert.That(((BaseApiClient)socketClient.ExchangeApi).ClientOptions.Proxy.Host, Is.EqualTo("host2"));
            Assert.That(((BaseApiClient)socketClient.ExchangeApi).ClientOptions.Proxy.Port, Is.EqualTo(81));
        }
    }
}
