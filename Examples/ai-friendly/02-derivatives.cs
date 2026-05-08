// 02-derivatives.cs
//
// Demonstrates: Crypto.com derivatives/perpetuals using the ExchangeApi trading
// surface: positions, derivative order placement, and close-position workflow.
//
// Setup: dotnet add package CryptoCom.Net
// Substitute API_KEY / API_SECRET. The API key must have the required trading permissions.

using CryptoCom.Net;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Enums;

var client = new CryptoComRestClient(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});

const string symbol = "ETHUSD_PERP";

// ---- 1. GET CURRENT POSITIONS ----
// CryptoCom.Net has one ExchangeApi root. Derivatives are not under a separate
// UsdFuturesApi or CoinFuturesApi branch.
var positions = await client.ExchangeApi.Trading.GetPositionsAsync(symbol);
if (!positions.Success)
{
    Console.WriteLine($"Failed to get positions: {positions.Error}");
    return;
}

foreach (var p in positions.Data)
{
    Console.WriteLine($"{p.Symbol}: qty={p.Quantity}, cost={p.Cost}, pnl={p.OpenPositionPnl}, liquidation={p.LiquidationPrice}");
}

// ---- 2. PLACE MARKET ORDER (open or increase a position) ----
// Derivative instruments use the same PlaceOrderAsync method as spot, with a
// derivative instrument name such as ETHUSD_PERP.
var openOrder = await client.ExchangeApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Buy,
    type: OrderType.Market,
    quantity: 0.01m);

if (!openOrder.Success)
{
    Console.WriteLine($"Failed to place derivative order: {openOrder.Error}");
    return;
}

Console.WriteLine($"Placed derivative order {openOrder.Data.OrderId}");

// ---- 3. ISOLATED MARGIN VARIATION ----
// To create an isolated position, pass isolatedMargin and leverage. If isolationId
// is omitted, Crypto.com creates a new isolated position. If it is supplied, the
// order is created against the existing isolated position.
var isolatedOrder = await client.ExchangeApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.01m,
    price: 2000m,
    isolatedMargin: true,
    leverage: 3);

if (!isolatedOrder.Success)
{
    Console.WriteLine($"Isolated order was not accepted: {isolatedOrder.Error}");
}

// ---- 4. CLOSE THE POSITION ----
// ClosePositionAsync asks the exchange to close the open position for the symbol.
// For a limit close, pass OrderType.Limit and a price.
var close = await client.ExchangeApi.Trading.ClosePositionAsync(
    symbol: symbol,
    orderType: OrderType.Market);

if (close.Success)
{
    Console.WriteLine($"Close position order: {close.Data.OrderId}");
}

// Common variations:
//   Limit order:       type: OrderType.Limit, add price
//   Stop order:        type: OrderType.StopLoss or StopLimit, add triggerPrice
//   Take profit:       type: OrderType.TakeProfit or TakeProfitLimit
//   Reduce only:       PlaceOrderAsync(..., reduceOnly: true)
//   Account leverage:  Account.SetAccountLeverageAsync(accountId, leverage)
