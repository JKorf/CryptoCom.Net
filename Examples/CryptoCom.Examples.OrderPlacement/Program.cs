using CryptoCom.Net;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Enums;

const string spotSymbol = "BTC_USDT";
const string futuresSymbol = "ETHUSD-PERP";

// Replace with valid credentials or order placement will always fail
var apiKey = "KEY";
var apiSecret = "SECRET";

Console.WriteLine("CryptoCom.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new CryptoComRestClient(options =>
{
    options.ApiCredentials = new CryptoComCredentials(apiKey, apiSecret);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceFuturesReduceOnlyOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(CryptoComRestClient client)
{
    Console.WriteLine($"Placing spot limit buy order for {spotSymbol}...");

    var tickers = await client.ExchangeApi.ExchangeData.GetTickersAsync(spotSymbol);
    if (!tickers.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {tickers.Error}");
        return;
    }

    var ticker = tickers.Data.SingleOrDefault();
    if (ticker == null || !ticker.LastPrice.HasValue)
    {
        Console.WriteLine($"No spot ticker price returned for {spotSymbol}");
        return;
    }

    var safePrice = Math.Round(ticker.LastPrice.Value * 0.95m, 2);
    var order = await client.ExchangeApi.Trading.PlaceOrderAsync(
        symbol: spotSymbol,
        side: OrderSide.Buy,
        type: OrderType.Limit,
        quantity: 0.001m,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCancel);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.OrderId}");

    var orderStatus = await client.ExchangeApi.Trading.GetOrderAsync(orderId: order.Data.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.Status}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.ExchangeApi.Trading.CancelOrderAsync(orderId: order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.OrderId}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceFuturesReduceOnlyOrderExampleAsync(CryptoComRestClient client)
{
    Console.WriteLine($"Placing futures reduce-only limit sell order for {futuresSymbol}...");

    var tickers = await client.ExchangeApi.ExchangeData.GetTickersAsync(futuresSymbol);
    if (!tickers.Success)
    {
        Console.WriteLine($"Failed to get futures ticker: {tickers.Error}");
        return;
    }

    var ticker = tickers.Data.SingleOrDefault();
    if (ticker == null || !ticker.LastPrice.HasValue)
    {
        Console.WriteLine($"No futures ticker price returned for {futuresSymbol}");
        return;
    }

    var safePrice = Math.Round(ticker.LastPrice.Value * 1.05m, 2);
    var order = await client.ExchangeApi.Trading.PlaceOrderAsync(
        symbol: futuresSymbol,
        side: OrderSide.Sell,
        type: OrderType.Limit,
        quantity: 0.01m,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCancel,
        reduceOnly: true);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place futures order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed futures order {order.Data.OrderId}");

    var orderStatus = await client.ExchangeApi.Trading.GetOrderAsync(orderId: order.Data.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Futures order status: {orderStatus.Data.Status}, executed: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query futures order: {orderStatus.Error}");

    var cancel = await client.ExchangeApi.Trading.CancelOrderAsync(orderId: order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled futures order {order.Data.OrderId}"
        : $"Failed to cancel futures order: {cancel.Error}");
}
