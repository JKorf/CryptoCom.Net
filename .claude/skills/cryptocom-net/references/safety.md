# Operational considerations

## Credentials and environments

Private REST, socket subscriptions, and socket queries use `CryptoComCredentials(key, secret)` without a passphrase. Keep credentials outside source and select permissions for the intended operations. Clients default to `Live`; select `Sandbox` explicitly with credentials for that environment.

## Orders, derivatives, and isolation

Use the exact listed instrument and its price/quantity constraints. Choose base `quantity` versus market `quoteQuantity` deliberately. Limit orders can execute immediately if the price crosses the book. Cancellation can race with a fill; reconcile order status and trades rather than assuming cancellation prevented execution.

Spot margin, isolated margin, trigger orders, OCO, and reduce-only fields change order semantics. If `isolationId` is absent on an isolated order, the request can create a new isolated position; supplying it targets an existing position. Leverage and isolated margin transfer amounts change exposure and collateral requirements.

Account leverage/settings, isolated margin transfers, and close-position operations affect account risk. Select the intended account and isolation scope before applying them. Shared leverage setting defaults to the master account when no `AccountId` exchange parameter is supplied.

`SetCancelOnDisconnectAsync` changes connection-loss behavior; configure it deliberately and account for reconnects. Subscription cleanup does not itself cancel orders.

## Batch outcomes and uncertain submissions

REST batch placement returns `HttpResult<CallResult<CryptoComOrderResult>[]>`; socket batch placement returns `QueryResult<CallResult<CryptoComListOrderResult>[]>`. Check the outer request and every inner `Success`/`Error` before using order IDs. Treat OCO/list results according to their item outcomes rather than assuming outer success means all legs succeeded.

A timeout or lost response can occur after the exchange accepted an order, cancellation, withdrawal, or settings change. Use supported order/client-ID lookups and account/transaction state to reconcile before deciding whether to resubmit. Preserve an explicit client-order ID when durable correlation is needed; it does not by itself prove retry idempotency. Transient errors do not make account-changing requests safe to repeat; bound public-read retries separately.

## Staking, conversion, and withdrawals

Staking, unstaking, and conversion can lock, unlock, or convert funds. Preserve the selected asset, quantity, conversion-rate/slippage parameters, and resulting request state. Withdrawals require the intended asset, network, address, applicable tag, and amount. Request acceptance is distinct from completed settlement.
