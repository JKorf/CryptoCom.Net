using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.JsonNet;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using CryptoCom.Net.Clients;

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
    }
}
