using CryptoCom.Net.Clients;
using CryptoCom.Net.Enums;
using CryptoCom.Net.Objects.Models;
using CryptoCom.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCom.Net.UnitTests
{
    [TestFixture]
    public class SocketRequestTests
    {
        private CryptoComSocketClient CreateClient(bool useUpdatedDeserialization)
        {
            var fact = new LoggerFactory();
            fact.AddProvider(new TraceLoggerProvider());
            var client = new CryptoComSocketClient(Options.Create(new CryptoComSocketOptions
            {
                UseUpdatedDeserialization = useUpdatedDeserialization,
                RequestTimeout = TimeSpan.FromSeconds(5),
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456")
            }), fact);
            return client;
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task ValidateExchangeApiCalls(bool useUpdatedDeserialization)
        {
            var tester = new SocketRequestValidator<CryptoComSocketClient>("Socket/ExchangeApi");

            await tester.ValidateAsync(CreateClient(useUpdatedDeserialization), client => client.ExchangeApi.GetBalancesAsync(), "GetBalances", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(CreateClient(useUpdatedDeserialization), client => client.ExchangeApi.GetPositionsAsync(), "GetPositions", nestedJsonProperty: "result.data");
            await tester.ValidateAsync(CreateClient(useUpdatedDeserialization), client => client.ExchangeApi.PlaceOrderAsync("ETH_USDT", Enums.OrderSide.Buy, OrderType.Limit, 1), "PlaceOrder", nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(useUpdatedDeserialization), client => client.ExchangeApi.CancelOrderAsync("123"), "CancelOrder", nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(useUpdatedDeserialization), client => client.ExchangeApi.ClosePositionAsync("ETH_USDT", OrderType.StopLoss), "ClosePosition", nestedJsonProperty: "result");
            await tester.ValidateAsync(CreateClient(useUpdatedDeserialization), client => client.ExchangeApi.GetOpenOrdersAsync("ETH_USDT"), "GetOpenOrders", nestedJsonProperty: "result.data", ignoreProperties: ["create_time"]);
            await tester.ValidateAsync(CreateClient(useUpdatedDeserialization), client => client.ExchangeApi.PlaceMultipleOrdersAsync(new[] { new CryptoComOrderRequest { } }), "PlaceMultipleOrders", nestedJsonProperty: "result.result_list", skipResponseValidation: true);
            await tester.ValidateAsync(CreateClient(useUpdatedDeserialization), client => client.ExchangeApi.CancelOrdersAsync(new[] { new CryptoComCancelOrderRequest { Symbol = "ETH_USDT", OrderId = "123" } }), "CancelOrders", nestedJsonProperty: "result.result_list", skipResponseValidation: true);
            await tester.ValidateAsync(CreateClient(useUpdatedDeserialization), client => client.ExchangeApi.PlaceOcoOrderAsync(new CryptoComOrderRequest(), new CryptoComOrderRequest()), "PlaceOcoOrder", nestedJsonProperty: "result.result_list");
            await tester.ValidateAsync(CreateClient(useUpdatedDeserialization), client => client.ExchangeApi.WithdrawAsync("ETH", 1, "123"), "Withdraw", nestedJsonProperty: "result");
        }
    }
}
