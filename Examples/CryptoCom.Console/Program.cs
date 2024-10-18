
using CryptoCom.Net.Clients;

// REST
var restClient = new CryptoComRestClient();
var ticker = await restClient.ExchangeApi.ExchangeData.GetTickerAsync("ETH_USD");
Console.WriteLine($"Rest client ticker price for ETH_USD: {ticker.Data.LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new CryptoComSocketClient();
var subscription = await socketClient.ExchangeApi.SubscribeToTickerUpdatesAsync("ETH_USD", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETH_USD: {update.Data.LastPrice}");
});

Console.ReadLine();
