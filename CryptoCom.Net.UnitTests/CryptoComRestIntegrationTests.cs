using CryptoCom.Net.Clients;
using CryptoCom.Net.SymbolOrderBooks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCom.Net.UnitTests
{
    [NonParallelizable]
    internal class CryptoComRestIntegrationTests : RestIntegrationTest<CryptoComRestClient>
    {
        public override bool Run { get; set; } = false;

        public CryptoComRestIntegrationTests()
        {
        }

        public override CryptoComRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new CryptoComRestClient(null, loggerFactory, Options.Create(new Objects.Options.CryptoComRestOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoComCredentials(key, sec) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().ExchangeApi.ExchangeData.GetTickersAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("40004"));
        }

        [Test]
        public async Task TestExchangeApiAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Account.GetBalancesAsync(default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Account.GetBalanceHistoryAsync(Enums.Timeframe.OneDay, default, default, default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Account.GetAccountInfoAsync(default, default, default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Account.GetAccountSettingsAsync(default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Account.GetFeeRatesAsync(default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Account.GetSymbolFeeRateAsync("ETH_USD", default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Account.GetAssetsAsync(default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Account.GetDepositHistoryAsync(default, default, default, default, default, default, default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Account.GetWithdrawalHistoryAsync(default, default, default, default, default, default, default), true, "result");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestExchangeApiExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.ExchangeApi.ExchangeData.GetRiskParametersAsync(default), false, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.ExchangeData.GetSymbolsAsync(default), false, "result.data");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.ExchangeData.GetOrderBookAsync("ETH_USD", 10, default), false, "result.data");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.ExchangeData.GetKlinesAsync("ETH_USD", Enums.KlineInterval.OneDay, default, default, default, default), false, "result.data");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.ExchangeData.GetTickersAsync(default, default), false, "result.data");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.ExchangeData.GetTradeHistoryAsync("ETH_USD", default, default, default, default), false, "result.data", ignoreProperties: ["t"]);
            await RunAndCheckResult(warnings, client => client.ExchangeApi.ExchangeData.GetValuationsAsync("ETH_USD", Enums.ValuationType.EstimatedFundingRate, default, default, default, default), false, "result.data");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.ExchangeData.GetInsuranceAsync("USD", default, default, default, default), false, "result.data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestExchangeApiTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Trading.GetPositionsAsync(default, default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Trading.GetOpenOrdersAsync(default, default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Trading.GetClosedOrdersAsync(default, default, default, default, default, default), true, "result");
            await RunAndCheckResult(warnings, client => client.ExchangeApi.Trading.GetUserTradesAsync(default, default, default, default, default, default), true, "result");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new CryptoComSymbolOrderBook("ETH_USD"));
        }
    }
}