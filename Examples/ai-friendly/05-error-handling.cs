// 05-error-handling.cs
//
// Demonstrates: HttpResult, WebSocketResult, QueryResult, and ExchangeCallResult
// patterns, retry logic, symbol precision, and batch-order nested result checks.
//
// Setup: dotnet add package CryptoCom.Net

using CryptoCom.Net;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Enums;
using CryptoCom.Net.Objects.Models;
using CryptoExchange.Net.Objects;

var client = new CryptoComRestClient(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});

const string symbolName = "BTC_USDT";

// ---- 1. THE BASIC PATTERN ----
// REST methods return HttpResult<T> or HttpResult.
// WebSocket subscriptions return WebSocketResult<UpdateSubscription>.
// Socket API requests return QueryResult<T> or QueryResult.
// Some SharedApis symbol helper methods return ExchangeCallResult<T>.
// .Success is true/false. .Data is only valid after success.
var result = await client.ExchangeApi.ExchangeData.GetTickersAsync(symbolName);

if (result.Success)
{
    Console.WriteLine($"Price: {result.Data.FirstOrDefault()?.LastPrice}");
}
else
{
    Console.WriteLine($"Code:      {result.Error?.Code}");
    Console.WriteLine($"Message:   {result.Error?.Message}");
    Console.WriteLine($"Type:      {result.Error?.ErrorType}");
    Console.WriteLine($"Transient: {result.Error?.IsTransient}");
}

// ---- 2. SIMPLE RETRY WITH BACKOFF ----
// Retry only on transient errors such as rate limits, temporary network failures,
// or server overload. Do not retry validation or authentication errors blindly.
async Task<HttpResult<T>> WithRetry<T>(
    Func<Task<HttpResult<T>>> call,
    int maxAttempts = 3)
{
    HttpResult<T> last = default!;
    for (int attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success) return last;
        if (last.Error?.IsTransient != true) return last;

        await Task.Delay(TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt)));
    }
    return last;
}

var ticker = await WithRetry(
    () => client.ExchangeApi.ExchangeData.GetTickersAsync(symbolName));

if (ticker.Success)
{
    Console.WriteLine($"Retried ticker succeeded: {ticker.Data.FirstOrDefault()?.LastPrice}");
}

// ---- 3. SYMBOL PRECISION BEFORE ORDER PLACEMENT ----
// Use GetSymbolsAsync() for exact instrument names and tick sizes. Crypto.com
// spot examples usually use BTC_USDT, not BTCUSDT.
var symbols = await client.ExchangeApi.ExchangeData.GetSymbolsAsync();
if (!symbols.Success)
{
    Console.WriteLine($"Cannot fetch symbol info: {symbols.Error}");
    return;
}

var symbol = symbols.Data.FirstOrDefault(s => s.Symbol == symbolName);
if (symbol == null)
{
    Console.WriteLine($"{symbolName} not listed. Use an exact symbol returned by GetSymbolsAsync().");
    return;
}

decimal rawQuantity = 0.00123456m;

// Round down to the exchange quantity step before placing an order.
decimal validQuantity = symbol.QuantityTickSize > 0
    ? Math.Floor(rawQuantity / symbol.QuantityTickSize) * symbol.QuantityTickSize
    : rawQuantity;

var order = await client.ExchangeApi.Trading.PlaceOrderAsync(
    symbol: symbolName,
    side: OrderSide.Buy,
    type: OrderType.Market,
    quantity: validQuantity);

if (!order.Success)
{
    string category = order.Error?.IsTransient == true
        ? "Transient - should retry with backoff if the operation is idempotent"
        : "Permanent - surface to the caller or fix request parameters";

    Console.WriteLine($"{category}: {order.Error?.Code} {order.Error?.Message}");
}

// ---- 4. BATCH ORDER CALLS CAN HAVE NESTED FAILURES ----
// Some batch endpoints can return outer success even when one or more individual
// order requests failed. Always inspect the nested CallResult entries.
var batch = await client.ExchangeApi.Trading.PlaceMultipleOrdersAsync(new[]
{
    new CryptoComOrderRequest
    {
        Symbol = symbolName,
        OrderSide = OrderSide.Buy,
        OrderType = OrderType.Limit,
        Quantity = validQuantity,
        Price = 100m
    }
});

if (!batch.Success)
{
    Console.WriteLine($"Batch request failed before processing: {batch.Error}");
}
else
{
    foreach (var item in batch.Data)
    {
        if (!item.Success)
            Console.WriteLine($"One batch item failed: {item.Error}");
        else
            Console.WriteLine($"One batch item accepted: {item.Data.OrderId}");
    }
}

// ---- 5. EXCEPTIONS VS ERROR RESULTS ----
// CryptoCom.Net returns API errors via result.Error, not thrown exceptions.
// Exceptions are generally reserved for misconfiguration, disposal, cancellation,
// or programming errors. Use CancellationToken via the `ct:` parameter where needed.

// Common scenarios:
//   Rate limit / network issue: result.Error.IsTransient may be true; retry with backoff
//   Invalid signature:          credentials, permissions, or environment issue
//   Invalid instrument:         use GetSymbolsAsync() and exact symbol names
//   Invalid precision:          use PriceTickSize / QuantityTickSize
//   Unknown order:              wrong order id, already canceled, or already filled
