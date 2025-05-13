using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Threading.Tasks;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Objects.Models;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using CryptoExchange.Net.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using CryptoCom.Net.Objects.Options;

namespace CryptoCom.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSpotExchangeDataSubscriptions()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());
            var client = new CryptoComSocketClient(Options.Create(new CryptoComSocketOptions
            {
                DelayAfterConnect = TimeSpan.Zero,
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456")
            }), loggerFactory);
            var tester = new SocketSubscriptionValidator<CryptoComSocketClient>(client, "Subscriptions/ExchangeApi", "wss://stream.crypto.com");
            await tester.ValidateAsync<CryptoComOrderBookUpdate>((client, handler) => client.ExchangeApi.SubscribeToOrderBookSnapshotUpdatesAsync("ETH_USDT", 10, handler), "BookSnapshot", nestedJsonProperty: "result.data", useFirstUpdateItem: true);
            await tester.ValidateAsync<CryptoComTicker>((client, handler) => client.ExchangeApi.SubscribeToTickerUpdatesAsync("ETH_USDT", handler), "Ticker", nestedJsonProperty: "result.data", useFirstUpdateItem: true);
            await tester.ValidateAsync<CryptoComTrade[]>((client, handler) => client.ExchangeApi.SubscribeToTradeUpdatesAsync("ETH_USDT", handler), "Trades", nestedJsonProperty: "result.data");
            await tester.ValidateAsync<CryptoComKline[]>((client, handler) => client.ExchangeApi.SubscribeToKlineUpdatesAsync("ETH_USDT", Enums.KlineInterval.OneDay, handler), "Klines", nestedJsonProperty: "result.data");
        }
    }
}
