---
name: cryptocom-net
description: Implement or review CryptoCom.Net market data, spot and derivatives trading, margin and account workflows, staking, WebSocket subscriptions and order queries, authentication, Shared API integration, and dependency injection in .NET applications.
---
# CryptoCom.Net

Install with `dotnet add package CryptoCom.Net`.

- Reuse `CryptoComRestClient` and `CryptoComSocketClient`, or use dependency injection. Both expose `ExchangeApi`; REST groups are `ExchangeData`, `Account`, `Trading`, and `Staking`.
- Private operations use `CryptoComCredentials(key, secret)` without a passphrase. Public market data needs no credentials.
- Spot instruments commonly use underscores, such as `BTC_USDT`. Select exact derivative names from `GetSymbolsAsync()` rather than copying suffixes from old examples.
- REST returns `HttpResult<T>` / `HttpResult`; subscriptions return `WebSocketResult<UpdateSubscription>`; socket requests return `QueryResult<T>` / `QueryResult`. Check `Success` before reading `Data`, and handle `Error` on failure. Batch responses require per-item checks after outer success.
- Keep the concrete socket client and unsubscribe successful subscriptions on shutdown. Socket order/query results are not subscriptions.
- Native order placement uses `OrderSide` and `OrderType`. Choose base `quantity`, market `quoteQuantity`, trigger, margin, isolation, and reduce-only parameters for the intended workflow. Client-order IDs can be omitted unless durable correlation is needed.
- Use `ExchangeApi.SharedApi` for Shared API V2 and depend on the required capability interface. The shared socket surface includes both subscriptions and spot/futures order queries; use the result contract appropriate to the operation.

## References

- Read [API surfaces](references/api-surfaces.md) for routing, Shared API capabilities, symbol catalogs, quantities, and exchange-specific prerequisites.
- Read [usage](references/usage.md) for authentication, market/account reads, trading, subscription cleanup, socket queries, Shared API V2, and DI snippets.
- Read [operational considerations](references/safety.md) for margin exposure, batch outcomes, submission recovery, staking, and withdrawals.

Checkout-only: [repository instructions](../../../AGENTS.md) and [complete examples](../../../Examples/ai-friendly/).
