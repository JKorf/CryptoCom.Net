using CryptoCom.Net.Clients;
using CryptoCom.Net.SymbolOrderBooks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CryptoCom.Net.UnitTests
{
    [NonParallelizable]
    internal class CryptoComRestIntegrationTests : RestIntegrationTest<CryptoComRestClient>
    {
        public override bool Run { get; set; } = true;

        public CryptoComRestIntegrationTests()
        {
        }

        public override CryptoComRestClient GetClient(ILoggerFactory loggerFactory, bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new CryptoComRestClient(null, loggerFactory, Options.Create(new Objects.Options.CryptoComRestOptions
            {
                UseUpdatedDeserialization = useUpdatedDeserialization,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new ApiCredentials(key, sec) : null
            }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestErrorResponseParsing(bool useUpdatedDeserialization)
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient(useUpdatedDeserialization).ExchangeApi.ExchangeData.GetTickersAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("40004"));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestExchangeApiAccount(bool useUpdatedDeserialization)
        {
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Account.GetBalancesAsync(default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Account.GetBalanceHistoryAsync(Enums.Timeframe.OneDay, default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Account.GetAccountInfoAsync(default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Account.GetAccountSettingsAsync(default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Account.GetFeeRatesAsync(default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Account.GetSymbolFeeRateAsync("ETH_USD", default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Account.GetAssetsAsync(default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Account.GetDepositHistoryAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Account.GetWithdrawalHistoryAsync(default, default, default, default, default, default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestExchangeApiExchangeData(bool useUpdatedDeserialization)
        {
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.ExchangeData.GetRiskParametersAsync(default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.ExchangeData.GetSymbolsAsync(default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.ExchangeData.GetOrderBookAsync("ETH_USD", 10, default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.ExchangeData.GetKlinesAsync("ETH_USD", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.ExchangeData.GetTickersAsync(default, default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.ExchangeData.GetTradeHistoryAsync("ETH_USD", default, default, default, default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.ExchangeData.GetValuationsAsync("ETH_USD", Enums.ValuationType.EstimatedFundingRate, default, default, default, default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.ExchangeData.GetInsuranceAsync("USD", default, default, default, default), false);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestExchangeApiTrading(bool useUpdatedDeserialization)
        {
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Trading.GetPositionsAsync(default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Trading.GetOpenOrdersAsync(default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Trading.GetClosedOrdersAsync(default, default, default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.ExchangeApi.Trading.GetUserTradesAsync(default, default, default, default, default), true);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new CryptoComSymbolOrderBook("ETH_USD"));
        }
    }
}