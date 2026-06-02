using CryptoCom.Net;
using CryptoCom.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the CryptoCom services
builder.Services.AddCryptoCom();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddCryptoCom(options =>
{    
   options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
   options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoint and inject the rest client
app.MapGet("/{Symbol}", async ([FromServices] ICryptoComRestClient client, string symbol) =>
{
    var result = await client.ExchangeApi.ExchangeData.GetTickersAsync(symbol);
    return result.Success
        ? Results.Ok(result.Data.SingleOrDefault()?.LastPrice)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();


app.MapGet("/Balances", async ([FromServices] ICryptoComRestClient client) =>
{
    var result = await client.ExchangeApi.Account.GetBalancesAsync();
    return result.Success
        ? Results.Ok(result.Data)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();

app.Run();
