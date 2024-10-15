using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoCom.Net.Clients;
using NUnit.Framework.Constraints;
using System.Drawing;
using CryptoCom.Net.Enums;

namespace CryptoCom.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateExchangeAccountDataCalls()
        {
            var client = new CryptoComRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<CryptoComRestClient>(client, "Endpoints/ExchangeApi/Account", "https://api.crypto.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetBalancesAsync(), "GetBalances", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetBalanceHistoryAsync(Enums.Timeframe.OneDay, DateTime.UtcNow), "GetBalanceHistory", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetAccountInfoAsync(), "GetAccountInfo", nestedJsonProperty: "result");
        }

        [Test]
        public async Task ValidateExchangeExchangeDataCalls()
        {
            var client = new CryptoComRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<CryptoComRestClient>(client, "Endpoints/ExchangeApi/ExchangeData", "https://api.crypto.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetTickersAsync(), "GetTickers", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetTradeHistoryAsync("123"), "GetTradeHistory", nestedJsonProperty: "result.data", ignoreProperties: new List<string> { "t" });
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetSymbolsAsync(), "GetSymbols", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetOrderBookAsync("123", 123), "GetOrderBook", nestedJsonProperty: "result.data", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetRiskParametersAsync(), "GetRiskParameters", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetKlinesAsync("123", Enums.KlineInterval.OneDay), "GetKlines", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetValuationsAsync("123", Enums.ValuationType.EstimatedFundingRate), "GetValuations", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetExpiredSettlementPriceAsync(Enums.SymbolType.DeliveryFuture, 123), "GetExpiredSettlementPrice", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetInsuranceAsync("123"), "GetInsurance", nestedJsonProperty: "result.data");
        }

        [Test]
        public async Task ValidateExchangeTradingDataCalls()
        {
            var client = new CryptoComRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<CryptoComRestClient>(client, "Endpoints/ExchangeApi/Trading", "https://api.crypto.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetPositionsAsync(), "GetPositions", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.PlaceOrderAsync("123", Enums.OrderSide.Buy, OrderType.Market), "PlaceOrder", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.CancelOrderAsync("123"), "CancelOrder", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.CancelAllOrdersAsync(), "CancelAllOrders");
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.ClosePositionAsync("123", OrderType.Market), "ClosePosition", nestedJsonProperty: "result");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestBody?.Contains("sig") == true;
        }
    }
}
