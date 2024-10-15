using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Threading.Tasks;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Objects.Models;

namespace CryptoCom.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSpotExchangeDataSubscriptions()
        {
            var client = new CryptoComSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<CryptoComSocketClient>(client, "Subscriptions/Spot", "XXX", stjCompare: true);
            //await tester.ValidateAsync<CryptoComModel>((client, handler) => client.SpotApi.SubscribeToXXXUpdatesAsync(handler), "XXX");
        }
    }
}
