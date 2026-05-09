# Copilot Instructions for CryptoCom.Net

This repository is **CryptoCom.Net**, a strongly typed C#/.NET client library for the Crypto.com Exchange REST and WebSocket APIs. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes CryptoCom.Net, follow these conventions:

## Use CryptoCom.Net, not raw HTTP

Never generate raw `HttpClient` calls to `exchange-docs.crypto.com` or `api.crypto.com`. Always use `CryptoComRestClient` or `CryptoComSocketClient`. This keeps request signing, rate limiting, response parsing, and WebSocket reconnect behavior inside the library.

## Client setup

```csharp
using CryptoCom.Net;
using CryptoCom.Net.Clients;

var restClient = new CryptoComRestClient(options =>
{
    options.ApiCredentials = new CryptoComCredentials("API_KEY", "API_SECRET");
});
```

Public market data can use `new CryptoComRestClient()` without credentials.

## Result handling

REST methods return `WebCallResult<T>` or `WebCallResult`. WebSocket subscriptions and socket API requests return `CallResult<T>` or `CallResult`. Always check `.Success` before reading `.Data`; the error is on `.Error`.

## API structure

- `restClient.ExchangeApi.ExchangeData` - public market data, symbols, tickers, order books, klines, valuations, announcements
- `restClient.ExchangeApi.Account` - balances, account info, account settings, fees, deposits, withdrawals, isolated margin transfers
- `restClient.ExchangeApi.Trading` - positions, orders, order history, trades, OCO orders, close position
- `restClient.ExchangeApi.Staking` - staking, unstaking, staking positions/history, staking conversions
- `restClient.ExchangeApi.SharedClient` - CryptoExchange.Net SharedApis REST interfaces
- `socketClient.ExchangeApi` - public streams, authenticated streams, and socket API requests
- `socketClient.ExchangeApi.SharedClient` - CryptoExchange.Net SharedApis socket interfaces

## Symbols

Crypto.com symbols commonly use underscores for spot pairs (`ETH_USDT`, `BTC_USDT`) and suffixes for derivatives (`ETHUSD_PERP`). Use the exact symbol names returned by `ExchangeApi.ExchangeData.GetSymbolsAsync()`.

## Order placement

Do not pass `clientOrderId` unless the caller needs their own external correlation id. Use the exchange order id returned in `CryptoComOrderId.OrderId` for follow-up calls.

## WebSocket pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown via `socketClient.UnsubscribeAsync(sub.Data)`.

## Cross-exchange

For code that needs to work across multiple exchanges, use `CryptoExchange.Net.SharedApis` interfaces (`ISpotTickerRestClient`, `ISpotOrderRestClient`, `IFuturesOrderRestClient`, etc.) accessed via `.ExchangeApi.SharedClient`.

## Avoid

- Legacy or guessed clients such as `CryptoComClient`
- Binance-style roots such as `SpotApi`, `UsdFuturesApi`, or `GeneralApi`
- Generic `ApiCredentials` when `CryptoComCredentials` is available
- Synchronous `.Result` / `.Wait()`
- Instantiating clients per request
- Reading `.Data` without checking `.Success`
- Invented method names; inspect `CryptoCom.Net/Interfaces/Clients/ExchangeApi/**`

## Reference

For detailed patterns and pitfalls see `AGENTS.md`, `llms.txt`, and `llms-full.txt` in the repository root. See `Examples/ai-friendly/` for compilable examples and `docs/ai-api-map.md` for an intent-to-method map.
