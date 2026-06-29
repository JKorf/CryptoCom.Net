// 03-websocket.cs
//
// Demonstrates: WebSocket subscriptions: public ticker, klines, trades,
// authenticated order and balance streams. Includes proper teardown.
//
// Setup: dotnet add package CryptoCom.Net

using CryptoCom.Net;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Enums;

const string spotSymbol = "BTC_USDT";

// ---- 1. PUBLIC SOCKET CLIENT - for market data streams ----
// Reuse a single client instance across subscriptions. The client manages
// connection pooling, reconnects, and subscription recovery.
// Subscription methods return WebSocketResult<UpdateSubscription>.
var publicSocket = new CryptoComSocketClient();

var tickerSub = await publicSocket.ExchangeApi.SubscribeToTickerUpdatesAsync(
    spotSymbol,
    update =>
    {
        Console.WriteLine($"{update.Data.Symbol}: {update.Data.LastPrice} bid={update.Data.BestBidPrice} ask={update.Data.BestAskPrice}");
    });

if (!tickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe ticker: {tickerSub.Error}");
    return;
}

var klineSub = await publicSocket.ExchangeApi.SubscribeToKlineUpdatesAsync(
    spotSymbol,
    KlineInterval.OneMinute,
    update =>
    {
        var last = update.Data.LastOrDefault();
        if (last != null)
            Console.WriteLine($"1m candle: O={last.OpenPrice} H={last.HighPrice} L={last.LowPrice} C={last.ClosePrice}");
    });

if (!klineSub.Success)
{
    Console.WriteLine($"Failed to subscribe klines: {klineSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    return;
}

var tradeSub = await publicSocket.ExchangeApi.SubscribeToTradeUpdatesAsync(
    spotSymbol,
    update =>
    {
        foreach (var trade in update.Data)
            Console.WriteLine($"Trade: {trade.Quantity} at {trade.Price}");
    });

if (!tradeSub.Success)
{
    Console.WriteLine($"Failed to subscribe trades: {tradeSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    return;
}

// ---- 2. AUTHENTICATED SOCKET CLIENT - for user data ----
var authSocket = new CryptoComSocketClient(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});

var orderSub = await authSocket.ExchangeApi.SubscribeToOrderUpdatesAsync(
    update =>
    {
        foreach (var order in update.Data)
            Console.WriteLine($"Order {order.OrderId} {order.Symbol}: {order.Status} filled={order.QuantityFilled}/{order.Quantity}");
    });

if (!orderSub.Success)
{
    Console.WriteLine($"Failed to subscribe order updates: {orderSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    await publicSocket.UnsubscribeAsync(tradeSub.Data);
    return;
}

var balanceSub = await authSocket.ExchangeApi.SubscribeToBalanceUpdatesAsync(
    update =>
    {
        foreach (var balance in update.Data.PositionBalances)
            Console.WriteLine($"Balance {balance.Asset}: {balance.Quantity}");
    });

if (!balanceSub.Success)
{
    Console.WriteLine($"Failed to subscribe balance updates: {balanceSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    await publicSocket.UnsubscribeAsync(tradeSub.Data);
    await authSocket.UnsubscribeAsync(orderSub.Data);
    return;
}

Console.WriteLine("All subscriptions active. Press Enter to teardown...");
Console.ReadLine();

// ---- 3. TEARDOWN - IMPORTANT ----
// Always unsubscribe on shutdown to release resources cleanly.
await publicSocket.UnsubscribeAsync(tickerSub.Data);
await publicSocket.UnsubscribeAsync(klineSub.Data);
await publicSocket.UnsubscribeAsync(tradeSub.Data);
await authSocket.UnsubscribeAsync(orderSub.Data);
await authSocket.UnsubscribeAsync(balanceSub.Data);

Console.WriteLine("Clean shutdown complete.");

// Common variations:
//   Multiple symbols:          SubscribeToTickerUpdatesAsync(new[] { "BTC_USDT", "ETH_USDT" }, handler)
//   Order book snapshots:      SubscribeToOrderBookSnapshotUpdatesAsync(symbol, 50, handler)
//   Order book deltas:         SubscribeToOrderBookUpdatesAsync(symbol, 50, handler)
//   User trades:               SubscribeToUserTradeUpdatesAsync(handler)
//   Position stream:           SubscribeToPositionUpdatesAsync(handler)
//   Socket API order:          authSocket.ExchangeApi.PlaceOrderAsync(...)
