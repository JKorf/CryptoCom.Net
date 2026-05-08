// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// Same code works against Crypto.com, Binance, OKX, Bybit, Kraken, and other
// exchanges from the CryptoExchange.Net family.
//
// Setup:
//   dotnet add package CryptoCom.Net
//   dotnet add package Binance.Net   // optional, for a second exchange
//   dotnet add package JK.OKX.Net    // optional, for the OKX example

using CryptoCom.Net.Clients;
using CryptoExchange.Net.SharedApis;

// ---- THE PATTERN ----
// Each exchange client exposes a `.SharedClient` property on its API surfaces.
// CryptoCom.Net exposes it under ExchangeApi.SharedClient.

ISpotTickerRestClient cryptoComShared = new CryptoComRestClient().ExchangeApi.SharedClient;

// To add other exchanges, install their package and use the correct root:
//   ISpotTickerRestClient binanceShared = new BinanceRestClient().SpotApi.SharedClient;
//   ISpotTickerRestClient okxShared     = new OKXRestClient().UnifiedApi.SharedClient;

// Common symbol type handles formatting differences between exchanges automatically.
// Crypto.com uses "BTC_USDT", Binance uses "BTCUSDT", OKX uses "BTC-USDT".
var btcusdt = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

await PrintTicker(cryptoComShared, btcusdt);

// ---- AGNOSTIC METHOD - works against any exchange ----
async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// ---- AVAILABLE SHARED INTERFACES ----
// REST:
//   ISpotTickerRestClient, ISpotSymbolRestClient, ISpotOrderRestClient
//   ISpotOrderClientIdRestClient, ISpotTriggerOrderRestClient
//   IFuturesOrderRestClient, IFuturesSymbolRestClient, IFuturesTpSlRestClient
//   IBalanceRestClient, IPositionRestClient, IFeeRestClient
//   IOrderBookRestClient, IRecentTradeRestClient, IKlineRestClient
//   IDepositRestClient, IWithdrawalRestClient, IWithdrawRestClient
//   IBookTickerRestClient
// WebSocket:
//   ITickerSocketClient, IBookTickerSocketClient
//   IOrderBookSocketClient, ITradeSocketClient, IKlineSocketClient
//   IUserTradeSocketClient, IBalanceSocketClient, ISpotOrderSocketClient
//   IFuturesOrderSocketClient, IPositionSocketClient

// ---- WEBSOCKET EXAMPLE - SHARED SUBSCRIPTION ----
var cryptoComSocket = new CryptoComSocketClient();
ITickerSocketClient tickerSocket = cryptoComSocket.ExchangeApi.SharedClient;

var sub = await tickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(btcusdt),
    update => Console.WriteLine($"[{tickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

Console.WriteLine("Press Enter to exit");
Console.ReadLine();

// Shared socket interfaces do not expose UnsubscribeAsync. Keep the concrete
// socket client and unsubscribe through it.
await cryptoComSocket.UnsubscribeAsync(sub.Data);

// Common variations:
//   Multi-exchange arbitrage: loop over List<ISpotTickerRestClient>, compare bid/ask
//   Portfolio dashboard:     IBalanceRestClient across all configured exchanges
//   Best execution:          ISpotOrderRestClient on multiple venues
//   Futures routing:         IFuturesOrderRestClient with TradingMode.PerpetualLinear
