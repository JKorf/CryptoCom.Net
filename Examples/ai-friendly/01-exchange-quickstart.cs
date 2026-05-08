// 01-exchange-quickstart.cs
//
// Demonstrates: client setup, public market data, symbol metadata,
// authenticated balance, limit order placement, order status check.
//
// Setup:
//   dotnet new console -n CryptoComQuickstart && cd CryptoComQuickstart
//   dotnet add package CryptoCom.Net
//   Copy this file content into Program.cs
//   Substitute API_KEY / API_SECRET below for private endpoints
//   dotnet run

using CryptoCom.Net;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Enums;

// ---- 1. PUBLIC CLIENT (no credentials needed for market data) ----
// Reuse this client across the application. Do not create a new client per request.
var publicClient = new CryptoComRestClient();

const string spotSymbol = "BTC_USDT";

var tickers = await publicClient.ExchangeApi.ExchangeData.GetTickersAsync(spotSymbol);
if (!tickers.Success)
{
    // .Error contains Code, Message, and error category information.
    Console.WriteLine($"Failed to get ticker: {tickers.Error}");
    return;
}

var ticker = tickers.Data.FirstOrDefault();
if (ticker == null)
{
    Console.WriteLine($"No ticker returned for {spotSymbol}.");
    return;
}

Console.WriteLine($"{ticker.Symbol} last price: {ticker.LastPrice}");
Console.WriteLine($"24h volume: {ticker.Volume}");

// Fetch symbol metadata before trading. Crypto.com instrument names and tick sizes
// should come from the exchange rather than from hard-coded assumptions.
var symbols = await publicClient.ExchangeApi.ExchangeData.GetSymbolsAsync();
if (!symbols.Success)
{
    Console.WriteLine($"Failed to get symbols: {symbols.Error}");
    return;
}

var symbolInfo = symbols.Data.FirstOrDefault(s => s.Symbol == spotSymbol);
if (symbolInfo != null)
{
    Console.WriteLine($"{symbolInfo.Symbol}: price tick {symbolInfo.PriceTickSize}, quantity tick {symbolInfo.QuantityTickSize}");
}

// ---- 2. AUTHENTICATED CLIENT (for account / trading) ----
var tradingClient = new CryptoComRestClient(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});

var balances = await tradingClient.ExchangeApi.Account.GetBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine($"Failed to get balances: {balances.Error}");
    return;
}

foreach (var accountBalance in balances.Data)
{
    foreach (var balance in accountBalance.PositionBalances.Where(b => b.Quantity != 0))
        Console.WriteLine($"{balance.Asset}: {balance.Quantity} available for account {accountBalance.AssetName}");
}

// ---- 3. PLACE A LIMIT BUY ORDER ----
// Limit, Buy, 0.001 BTC at a price 5% below current. This is likely not filled immediately.
// Do not pass clientOrderId unless an external system needs its own correlation id.
var safePrice = ticker.LastPrice.HasValue
    ? Math.Round(ticker.LastPrice.Value * 0.95m, symbolInfo?.PriceDecimals ?? 2)
    : 50000m;

var order = await tradingClient.ExchangeApi.Trading.PlaceOrderAsync(
    symbol: spotSymbol,
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.001m,
    price: safePrice);

if (!order.Success)
{
    Console.WriteLine($"Failed to place order: {order.Error}");
    return;
}

Console.WriteLine($"Placed order {order.Data.OrderId} at {safePrice}");

// ---- 4. CHECK ORDER STATUS ----
var status = await tradingClient.ExchangeApi.Trading.GetOrderAsync(orderId: order.Data.OrderId);
if (status.Success)
{
    Console.WriteLine($"Order status: {status.Data.Status}, filled: {status.Data.QuantityFilled}");
}

// ---- 5. CANCEL THE ORDER (cleanup for this example) ----
var cancel = await tradingClient.ExchangeApi.Trading.CancelOrderAsync(orderId: order.Data.OrderId);
if (cancel.Success)
{
    Console.WriteLine($"Cancelled order {order.Data.OrderId}");
}

// Common variations:
//   Market order:         type: OrderType.Market, omit price
//   Quote quantity buy:   use quoteQuantity instead of quantity for market orders
//   Trigger order:        add triggerPrice and triggerPriceType
//   Isolated margin:      add isolatedMargin: true and leverage / isolationId as needed
//   Symbols:              use exact instrument names returned by GetSymbolsAsync()
