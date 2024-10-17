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
            await tester.ValidateAsync(client => client.ExchangeApi.Account.SetAccountLeverageAsync("123", 10), "SetAccountLeverage");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.SetAccountSettingsAsync(SelfTradePreventionScope.SubAccount, SelfTradePreventionMode.CancelTaker), "SetAccountSettings");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetAccountSettingsAsync(), "GetAccountSettings", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetTransactionHistoryAsync(), "GetTransactionHistory", nestedJsonProperty: "result.data", ignoreProperties: new List<string> { "event_timestamp_ms" });
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetFeeRatesAsync(), "GetFeeRate", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetSymbolFeeRateAsync("123"), "GetSymbolFeeRate", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.WithdrawAsync("123", 0.1m, "123"), "Withdraw", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetAssetsAsync(), "GetAssets", nestedJsonProperty: "result.currency_map");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetDepositAddressesAsync("123"), "GetDepositAddresses", nestedJsonProperty: "result.deposit_address_list");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetDepositHistoryAsync(), "GetDepositHistory", nestedJsonProperty: "result.deposit_list");
            await tester.ValidateAsync(client => client.ExchangeApi.Account.GetWithdrawalHistoryAsync(), "GetWithdrawalHistory", nestedJsonProperty: "result.withdrawal_list");
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
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetTradeHistoryAsync("123"), "GetTradeHistory", nestedJsonProperty: "result.data", ignoreProperties: new List<string> { "tn" });
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetSymbolsAsync(), "GetSymbols", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetOrderBookAsync("123", 123), "GetOrderBook", nestedJsonProperty: "result.data", useSingleArrayItem: true);
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetRiskParametersAsync(), "GetRiskParameters", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetKlinesAsync("123", Enums.KlineInterval.OneDay), "GetKlines", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetValuationsAsync("123", Enums.ValuationType.EstimatedFundingRate), "GetValuations", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetExpiredSettlementPriceAsync(Enums.SymbolType.DeliveryFuture, 123), "GetExpiredSettlementPrice", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.ExchangeData.GetInsuranceAsync("123"), "GetInsurance", nestedJsonProperty: "result.data");
        }
        [Test]
        public async Task ValidateExchangeStakingCalls()
        {
            var client = new CryptoComRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<CryptoComRestClient>(client, "Endpoints/ExchangeApi/Staking", "https://api.crypto.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.ExchangeApi.Staking.StakeAsync("123", 0.1m), "Stake", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.Staking.UnstakeAsync("123", 0.1m), "Unstake", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.Staking.GetStakingPositionsAsync("123"), "GetStakingPosition", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.Staking.GetStakingSymbolsAsync(), "GetStakingSymbols", nestedJsonProperty: "result.data", ignoreProperties: new List<string>{ "additional_rewards" });
            await tester.ValidateAsync(client => client.ExchangeApi.Staking.GetOpenStakingRequestsAsync(), "GetOpenStakingRequests", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.Staking.GetStakingHistoryAsync(), "GetStakingHistory", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.Staking.GetStakingRewardHistoryAsync(), "GetStakingRewardHistory", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.Staking.ConvertAsync("123", "123", 0.1m, 0.1m, 0.1m), "Convert", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.ExchangeApi.Staking.GetOpenConvertRequestsAsync(), "GetOpenConvertRequests", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(client => client.ExchangeApi.Staking.GetConvertRateAsync("123"), "GetConvertRate", nestedJsonProperty: "result");
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
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetOpenOrdersAsync("123"), "GetOpenOrders", nestedJsonProperty: "result.data", ignoreProperties: new List<string> { "create_time" });
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetOrderAsync(), "GetOrder", nestedJsonProperty: "result", ignoreProperties: new List<string> { "create_time" });
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetClosedOrdersAsync(), "GetClosedOrders", nestedJsonProperty: "result.data", ignoreProperties: new List<string> { "create_time" });
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetUserTradesAsync(), "GetUserTrades", nestedJsonProperty: "result.data", ignoreProperties: new List<string> { "create_time" });
            await tester.ValidateAsync(client => client.ExchangeApi.Trading.GetOcoOrderAsync("ETH_USDT", "123"), "GetOcoOrder", nestedJsonProperty: "result.data", ignoreProperties: new List<string> { "create_time", "trigger_price", "price", "type", "exec_inst", "trigger_price_type" });
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestBody?.Contains("sig") == true;
        }
    }
}
