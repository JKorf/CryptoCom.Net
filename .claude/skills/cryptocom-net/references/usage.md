# Usage examples

Snippets are independent. Those using `client` assume a reused `CryptoComRestClient`, configured with credentials for private calls. Trading and account writes change live state when executed; see [operational considerations](safety.md).

## Imports

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoCom.Net;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Enums;
using CryptoCom.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
```

## Public tickers and results

```csharp
using var client = new CryptoComRestClient();
var result = await client.ExchangeApi.ExchangeData.GetTickersAsync("BTC_USDT");
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.FirstOrDefault()?.LastPrice);
```

## Credentials and balances

```csharp
using var client = new CryptoComRestClient(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});
var balances = await client.ExchangeApi.Account.GetBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine(balances.Error);
    return;
}
foreach (var balance in balances.Data)
    Console.WriteLine($"{balance.AssetName}: available={balance.TotalAvailableBalance}");
```

## Limit order and cancellation

```csharp
var order = await client.ExchangeApi.Trading.PlaceOrderAsync(
    "ETH_USDT", OrderSide.Buy, OrderType.Limit, quantity: 0.1m, price: 1000m);
if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
var cancel = await client.ExchangeApi.Trading.CancelOrderAsync(orderId: order.Data.OrderId);
if (!cancel.Success)
    Console.WriteLine(cancel.Error);
```

## Derivatives positions

```csharp
var positions = await client.ExchangeApi.Trading.GetPositionsAsync();
if (!positions.Success)
{
    Console.WriteLine(positions.Error);
    return;
}
foreach (var position in positions.Data.Where(x => x.Quantity != 0))
    Console.WriteLine($"{position.Symbol}: {position.Quantity}, PnL={position.OpenPositionPnl}");
```

Select a listed derivative instrument before using symbol-specific position, close, or margin operations.

## Subscription lifecycle

```csharp
using var socketClient = new CryptoComSocketClient();
var subscription = await socketClient.ExchangeApi.SubscribeToTickerUpdatesAsync(
    "BTC_USDT", update => Console.WriteLine(update.Data.LastPrice));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socketClient.UnsubscribeAsync(subscription.Data);
}
```

Configure credentials on the socket client for private balance, position, order, and trade subscriptions. Retain the same success checks and cleanup.

## Socket order query

This submits an authenticated order. Its query result is not a subscription.

```csharp
using var socketClient = new CryptoComSocketClient(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});
var order = await socketClient.ExchangeApi.PlaceOrderAsync(
    "ETH_USDT", OrderSide.Buy, OrderType.Limit, quantity: 0.1m, price: 1000m);
if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
Console.WriteLine(order.Data.OrderId);
```

## Shared API V2

```csharp
IGetTickerRest ticker = client.ExchangeApi.SharedApi;
var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");
var result = await ticker.GetTickerAsync(new GetTickerRequest(symbol));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

See [API surfaces](api-surfaces.md#shared-api-v2) for symbol catalogs, runtime selection, and request prerequisites.

## Dependency injection

```csharp
var services = new ServiceCollection();
services.AddCryptoCom(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});
using var provider = services.BuildServiceProvider();
var client = provider.GetRequiredService<ICryptoComRestClient>();
var shared = provider.GetRequiredService<ICryptoComSharedApiClient>();
```

## Complete examples (checkout-only)

The documentation test compiles these programs without executing exchange calls. Running them can contact Crypto.com and execute the illustrated orders and account changes.

- [Exchange quickstart](../../../../Examples/ai-friendly/01-exchange-quickstart.cs)
- [Derivatives](../../../../Examples/ai-friendly/02-derivatives.cs)
- [WebSockets](../../../../Examples/ai-friendly/03-websocket.cs)
- [Shared API](../../../../Examples/ai-friendly/04-multi-exchange.cs)
- [Error handling](../../../../Examples/ai-friendly/05-error-handling.cs)
