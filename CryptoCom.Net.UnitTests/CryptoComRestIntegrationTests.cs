using CryptoCom.Net.Clients;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CryptoCom.Net.UnitTests
{
    [NonParallelizable]
    internal class CryptoComRestIntegrationTests : RestIntergrationTest<CryptoComRestClient>
    {
        public override bool Run { get; set; }

        public CryptoComRestIntegrationTests()
        {
        }

        public override CryptoComRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new CryptoComRestClient(null, loggerFactory, opts =>
            {
                opts.OutputOriginalData = true;
                opts.ApiCredentials = Authenticated ? new ApiCredentials(key, sec) : null;
            });
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().ExchangeApi.ExchangeData.GetTickersAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.Code, Is.EqualTo(40004));
        }

        [Test]
        public async Task TestExchangeApiAccount()
        {
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetBalancesAsync(default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetBalanceHistoryAsync(Enums.Timeframe.OneDay, default, default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetAccountInfoAsync(default, default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetAccountSettingsAsync(default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetFeeRatesAsync(default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetSymbolFeeRateAsync("ETH_USD", default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetAssetsAsync(default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetDepositHistoryAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Account.GetWithdrawalHistoryAsync(default, default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestExchangeApiExchangeData()
        {
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetRiskParametersAsync(default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetSymbolsAsync(default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetOrderBookAsync("ETH_USD", 10, default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetKlinesAsync("ETH_USD", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetTickersAsync(default, default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetTradeHistoryAsync("ETH_USD", default, default, default, default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetValuationsAsync("ETH_USD", Enums.ValuationType.EstimatedFundingRate, default, default, default, default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetExpiredSettlementPriceAsync(Enums.SymbolType.PerpetualSwap, default, default), false);
            await RunAndCheckResult(client => client.ExchangeApi.ExchangeData.GetInsuranceAsync("USD", default, default, default, default), false);
        }

        [Test]
        public async Task TestExchangeApiTrading()
        {
            await RunAndCheckResult(client => client.ExchangeApi.Trading.GetPositionsAsync(default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Trading.GetOpenOrdersAsync(default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Trading.GetClosedOrdersAsync(default, default, default, default, default), true);
            await RunAndCheckResult(client => client.ExchangeApi.Trading.GetUserTradesAsync(default, default, default, default, default), true);
        }
    }
}