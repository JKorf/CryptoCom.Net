
using CryptoCom.Net.Clients;

// REST
var restClient = new CryptoComRestClient();
var ticker = await restClient.ExchangeApi.ExchangeData.GetTickersAsync("ETH_USD");
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"Rest client ticker price for ETH_USD: {ticker.Data.Single().LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new CryptoComSocketClient();
var subscription = await socketClient.ExchangeApi.SubscribeToTickerUpdatesAsync("ETH_USD", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETH_USD: {update.Data.LastPrice}");
});

if (!subscription.Success)
{
    Console.WriteLine($"Failed to subscribe to ticker updates: {subscription.Error}");
    return;
}

Console.ReadLine();
