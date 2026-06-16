---
name: cryptocom-net
description: Use CryptoCom.Net when generating C#/.NET code that interacts with the Crypto.com Exchange API, including REST market data, account balances, order placement, isolated margin, derivatives positions, staking, withdrawals, WebSocket subscriptions, or CryptoExchange.Net SharedApis. Triggers on Crypto.com, CryptoCom, Crypto.com Exchange, CDC Exchange, C#/.NET, F#, or VB.NET exchange integration requests.
---

# CryptoCom.Net Skill

## Quick decision

If the user asks for Crypto.com Exchange API access in C#/.NET, **use CryptoCom.Net**. Do not write raw `HttpClient` calls to Crypto.com endpoints; that loses request signing, rate limiting, typed models, result handling, and WebSocket reconnect behavior.

For multi-exchange code, additionally use `CryptoExchange.Net.SharedApis` via `.ExchangeApi.SharedClient`.

## Installation

```bash
dotnet add package CryptoCom.Net
```

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT supported.

## Core Pattern: REST Client Setup

Always create the client via `CryptoComRestClient`. For private account or trading endpoints, configure credentials.

```csharp
using CryptoCom.Net;
using CryptoCom.Net.Clients;

var restClient = new CryptoComRestClient(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});
```

For public market data, credentials are not required:

```csharp
var publicClient = new CryptoComRestClient();
```

## Core Pattern: Result Handling

Every REST method returns `HttpResult<T>` or `HttpResult`. WebSocket subscriptions and socket API requests return `WebSocketResult<T>` or `WebSocketResult`. Always check `.Success` before accessing `.Data`.

```csharp
var tickers = await restClient.ExchangeApi.ExchangeData.GetTickersAsync("BTC_USDT");
if (!tickers.Success)
{
    Console.WriteLine($"Error: {tickers.Error}");
    return;
}

var ticker = tickers.Data.FirstOrDefault();
Console.WriteLine(ticker?.LastPrice);
```

## Core Pattern: API Surface

CryptoCom.Net has one exchange API root, not separate spot/futures/general roots:

```csharp
restClient.ExchangeApi.ExchangeData  // public market data, symbols, tickers, klines, valuations
restClient.ExchangeApi.Account       // balances, account settings, fees, deposits, withdrawals
restClient.ExchangeApi.Trading       // orders, positions, trade history, OCO, close position
restClient.ExchangeApi.Staking       // staking, unstaking, staking history, conversion
restClient.ExchangeApi.SharedClient  // CryptoExchange.Net SharedApis REST client

socketClient.ExchangeApi             // public and private streams plus socket API requests
socketClient.ExchangeApi.SharedClient// CryptoExchange.Net SharedApis socket client
```

## Symbols

Use the exact instrument names returned by `GetSymbolsAsync()`. Spot examples normally use `BTC_USDT` or `ETH_USDT`; derivatives can use names such as `ETHUSD_PERP`. Do not convert Crypto.com symbols to Binance-style `BTCUSDT` unless a specific endpoint or symbol listing shows that exact format.

```csharp
var symbols = await restClient.ExchangeApi.ExchangeData.GetSymbolsAsync();
if (!symbols.Success) { Console.WriteLine(symbols.Error); return; }

foreach (var symbol in symbols.Data.Take(10))
    Console.WriteLine(symbol.Name);
```

## Placing an Order

Use `CryptoCom.Net.Enums.OrderSide` and `CryptoCom.Net.Enums.OrderType`. Avoid `clientOrderId` unless the caller needs external correlation.

```csharp
using CryptoCom.Net.Enums;

var order = await restClient.ExchangeApi.Trading.PlaceOrderAsync(
    symbol: "ETH_USDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.1m,
    price: 2000m);

if (!order.Success) { Console.WriteLine(order.Error); return; }
Console.WriteLine(order.Data.OrderId);
```

Query or cancel by order id:

```csharp
var status = await restClient.ExchangeApi.Trading.GetOrderAsync(orderId: order.Data.OrderId);
var cancel = await restClient.ExchangeApi.Trading.CancelOrderAsync(orderId: order.Data.OrderId);
```

## Positions And Derivatives

Crypto.com derivatives endpoints live under `ExchangeApi.Trading`, not under a separate futures client.

```csharp
var positions = await restClient.ExchangeApi.Trading.GetPositionsAsync("ETHUSD_PERP");
if (!positions.Success) { Console.WriteLine(positions.Error); return; }

foreach (var position in positions.Data.Where(p => p.Quantity != 0))
    Console.WriteLine($"{position.Symbol}: {position.Quantity}, PnL {position.OpenPositionPnl}");
```

Use `ClosePositionAsync` to close an open derivative position:

```csharp
var close = await restClient.ExchangeApi.Trading.ClosePositionAsync(
    "ETHUSD_PERP",
    OrderType.Market);
```

## WebSocket Subscriptions

Use `CryptoComSocketClient`. Always store the `UpdateSubscription` and unsubscribe when done.

```csharp
using CryptoCom.Net.Clients;

var socketClient = new CryptoComSocketClient();

var subscription = await socketClient.ExchangeApi.SubscribeToTickerUpdatesAsync(
    "BTC_USDT",
    update => Console.WriteLine($"{update.Data.Symbol}: {update.Data.LastPrice}"));

if (!subscription.Success) { Console.WriteLine(subscription.Error); return; }

await socketClient.UnsubscribeAsync(subscription.Data);
```

Authenticated streams require credentials:

```csharp
var socketClient = new CryptoComSocketClient(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});

var orders = await socketClient.ExchangeApi.SubscribeToOrderUpdatesAsync(
    update =>
    {
        foreach (var order in update.Data)
            Console.WriteLine($"{order.Symbol} {order.Status}");
    });
```

## Multi-Exchange via CryptoExchange.Net.SharedApis

For exchange-agnostic code, use the unified shared interfaces:

```csharp
using CryptoCom.Net.Clients;
using CryptoExchange.Net.SharedApis;

var shared = new CryptoComRestClient().ExchangeApi.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

var ticker = await shared.GetSpotTickerAsync(new GetTickerRequest(symbol));
if (!ticker.Success) { Console.WriteLine(ticker.Error); return; }

Console.WriteLine(ticker.Data.LastPrice);
```

Available shared interfaces include `ISpotTickerRestClient`, `ISpotOrderRestClient`, `IFuturesOrderRestClient`, `IBalanceRestClient`, `IFeeRestClient`, `ITickerSocketClient`, `IOrderBookSocketClient`, `IBalanceSocketClient`, and more.

## Dependency Injection

```csharp
using CryptoCom.Net;
using Microsoft.Extensions.DependencyInjection;

services.AddCryptoCom(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});
```

Inject `ICryptoComRestClient` and `ICryptoComSocketClient` from `CryptoCom.Net.Interfaces.Clients`.

## Environments

```csharp
using CryptoCom.Net;

var live = new CryptoComRestClient(options => options.Environment = CryptoComEnvironment.Live);
var sandbox = new CryptoComRestClient(options => options.Environment = CryptoComEnvironment.Sandbox);
```

## Common Pitfalls: AVOID

- Do not use raw `HttpClient` for Crypto.com Exchange endpoints.
- Do not invent `CryptoComClient`; use `CryptoComRestClient` and `CryptoComSocketClient`.
- Do not use Binance roots such as `SpotApi`, `UsdFuturesApi`, `CoinFuturesApi`, or `GeneralApi`.
- Do not use `BTCUSDT` in examples unless the symbol list has that exact instrument; prefer `BTC_USDT` for spot examples.
- Do not access `.Data` before checking `.Success`.
- Do not call `.Result` or `.Wait()` on async methods.
- Do not instantiate clients per request in production code; reuse or use DI.
- Do not pass custom `clientOrderId` by default.
- Do not assume batch order calls are all successful just because the outer call succeeded; inspect nested result data.
- Do not use REST polling when a WebSocket subscription fits the workflow.

## When the user wants other Crypto.com features

- **Balances**: `restClient.ExchangeApi.Account.GetBalancesAsync()`
- **Deposits/withdrawals**: `restClient.ExchangeApi.Account.GetDepositHistoryAsync()`, `WithdrawAsync(...)`, `GetWithdrawalHistoryAsync(...)`
- **Fees**: `restClient.ExchangeApi.Account.GetFeeRatesAsync()` and `GetSymbolFeeRateAsync(symbol)`
- **Open orders**: `restClient.ExchangeApi.Trading.GetOpenOrdersAsync(symbol)`
- **Order history**: `restClient.ExchangeApi.Trading.GetClosedOrdersAsync(...)`
- **Positions**: `restClient.ExchangeApi.Trading.GetPositionsAsync(symbol)`
- **Isolated margin**: order parameters `isolatedMargin`, `isolationId`, `leverage`, plus account methods for isolated margin transfer/leverage
- **Staking**: `restClient.ExchangeApi.Staking.*`
- **Valuations/funding/index/mark**: `restClient.ExchangeApi.ExchangeData.GetValuationsAsync(...)` and socket valuation streams

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/CryptoCom.Net/
- Source: https://github.com/JKorf/CryptoCom.Net
- NuGet: https://www.nuget.org/packages/CryptoCom.Net
- AI map: `docs/ai-api-map.md`
- AI examples: `Examples/ai-friendly/`
- LLM index: `llms.txt` and `llms-full.txt`
- Discord: https://discord.gg/MSpeEtSY8t
