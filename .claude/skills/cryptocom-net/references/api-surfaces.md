# API surfaces

## Client roots and symbols

`client` below is a `CryptoComRestClient`; `socketClient` is a `CryptoComSocketClient`. Both use `ExchangeApi` for spot and derivatives. REST has `ExchangeData`, `Account`, `Trading`, and `Staking` groups. Socket subscriptions and request/response operations live directly under `ExchangeApi`.

Spot symbols commonly use `BTC_USDT` and `ETH_USDT`. Use exact instrument names returned by `GetSymbolsAsync()` for derivatives. `CryptoComExchange.FormatSymbol` formats spot with underscores and perpetuals with a `-PERP` suffix; delivery formatting also requires a delivery date. Confirm formatted names against available instruments before trading. Candles use `KlineInterval`, valuation requests use the selected valuation type, and orders use `OrderSide` / `OrderType`.

## Exchange Data REST

| User intent | CryptoCom.Net member |
|---|---|
| Get server time | `client.ExchangeApi.ExchangeData.GetServerTimeAsync()` |
| Get instruments / symbols | `client.ExchangeApi.ExchangeData.GetSymbolsAsync()` |
| Get risk parameters | `client.ExchangeApi.ExchangeData.GetRiskParametersAsync()` |
| Get one ticker | `client.ExchangeApi.ExchangeData.GetTickersAsync("BTC_USDT")` |
| Get all tickers | `client.ExchangeApi.ExchangeData.GetTickersAsync()` |
| Get order book | `client.ExchangeApi.ExchangeData.GetOrderBookAsync("BTC_USDT", 50)` |
| Get recent public trades | `client.ExchangeApi.ExchangeData.GetTradeHistoryAsync("BTC_USDT")` |
| Get klines/candles | `client.ExchangeApi.ExchangeData.GetKlinesAsync("BTC_USDT", KlineInterval.OneMinute)` |
| Get index/mark/funding valuation history | `client.ExchangeApi.ExchangeData.GetValuationsAsync(symbol, valuationType)` |
| Get expired settlement prices | `client.ExchangeApi.ExchangeData.GetExpiredSettlementPriceAsync(SymbolType.DeliveryFuture)` |
| Get insurance fund history | `client.ExchangeApi.ExchangeData.GetInsuranceAsync(asset)` |
| Get announcements | `client.ExchangeApi.ExchangeData.GetAnnouncementsAsync()` |

## Account REST

| User intent | CryptoCom.Net member |
|---|---|
| Get balances | `client.ExchangeApi.Account.GetBalancesAsync()` |
| Get balance history | `client.ExchangeApi.Account.GetBalanceHistoryAsync(Timeframe.OneDay)` |
| Get account info | `client.ExchangeApi.Account.GetAccountInfoAsync()` |
| Get account settings | `client.ExchangeApi.Account.GetAccountSettingsAsync()` |
| Set account settings | `client.ExchangeApi.Account.SetAccountSettingsAsync(...)` |
| Set account leverage | `client.ExchangeApi.Account.SetAccountLeverageAsync(accountId, leverage)` |
| Get transaction history | `client.ExchangeApi.Account.GetTransactionHistoryAsync(...)` |
| Get fee rates | `client.ExchangeApi.Account.GetFeeRatesAsync()` |
| Get symbol fee rate | `client.ExchangeApi.Account.GetSymbolFeeRateAsync("BTC_USDT")` |
| Get asset network info | `client.ExchangeApi.Account.GetAssetsAsync()` |
| Get deposit addresses | `client.ExchangeApi.Account.GetDepositAddressesAsync("USDT")` |
| Get deposit history | `client.ExchangeApi.Account.GetDepositHistoryAsync(...)` |
| Get withdrawal history | `client.ExchangeApi.Account.GetWithdrawalHistoryAsync(...)` |
| Withdraw asset | `client.ExchangeApi.Account.WithdrawAsync(...)` |
| Create isolated margin transfer | `client.ExchangeApi.Account.CreateIsolatedMarginTransferAsync(...)` |
| Set isolated margin leverage | `client.ExchangeApi.Account.SetIsolatedMarginLeverageAsync(...)` |

## Trading REST

| User intent | CryptoCom.Net member |
|---|---|
| Get positions | `client.ExchangeApi.Trading.GetPositionsAsync()` |
| Get one symbol positions | `client.ExchangeApi.Trading.GetPositionsAsync(symbol)` |
| Place order | `client.ExchangeApi.Trading.PlaceOrderAsync(...)` |
| Place spot limit order | `client.ExchangeApi.Trading.PlaceOrderAsync("BTC_USDT", OrderSide.Buy, OrderType.Limit, quantity: ..., price: ...)` |
| Place market order by base quantity | `client.ExchangeApi.Trading.PlaceOrderAsync(symbol, side, OrderType.Market, quantity: ...)` |
| Place market order by quote quantity | `client.ExchangeApi.Trading.PlaceOrderAsync(symbol, side, OrderType.Market, quoteQuantity: ...)` |
| Place trigger order | `client.ExchangeApi.Trading.PlaceOrderAsync(..., triggerPrice: ..., triggerPriceType: ...)` |
| Place isolated margin order | `client.ExchangeApi.Trading.PlaceOrderAsync(..., isolatedMargin: true, leverage: ...)` |
| Cancel order | `client.ExchangeApi.Trading.CancelOrderAsync(orderId: orderId)` |
| Cancel by client order id | `client.ExchangeApi.Trading.CancelOrderAsync(clientOrderId: clientOrderId)` |
| Cancel all orders | `client.ExchangeApi.Trading.CancelAllOrdersAsync(symbol)` |
| Close position | `client.ExchangeApi.Trading.ClosePositionAsync(symbol, OrderType.Market)` |
| Get open orders | `client.ExchangeApi.Trading.GetOpenOrdersAsync(symbol)` |
| Get order detail | `client.ExchangeApi.Trading.GetOrderAsync(orderId: orderId)` |
| Get order by client id | `client.ExchangeApi.Trading.GetOrderAsync(clientOrderId: clientOrderId)` |
| Get closed orders | `client.ExchangeApi.Trading.GetClosedOrdersAsync(...)` |
| Get user trades | `client.ExchangeApi.Trading.GetUserTradesAsync(...)` |
| Place multiple orders | `client.ExchangeApi.Trading.PlaceMultipleOrdersAsync(orders)` |
| Cancel multiple orders | `client.ExchangeApi.Trading.CancelOrdersAsync(orders)` |
| Place OCO order | `client.ExchangeApi.Trading.PlaceOcoOrderAsync(order1, order2)` |
| Cancel OCO order | `client.ExchangeApi.Trading.CancelOcoOrderAsync(symbol, listId)` |
| Get OCO order | `client.ExchangeApi.Trading.GetOcoOrderAsync(symbol, listId)` |
| Edit order | `client.ExchangeApi.Trading.EditOrderAsync(newQuantity, newPrice, orderId: orderId)` |

## Staking REST

| User intent | CryptoCom.Net member |
|---|---|
| Stake | `client.ExchangeApi.Staking.StakeAsync(symbol, quantity)` |
| Unstake | `client.ExchangeApi.Staking.UnstakeAsync(symbol, quantity)` |
| Get staking positions | `client.ExchangeApi.Staking.GetStakingPositionsAsync(symbol)` |
| Get staking symbols | `client.ExchangeApi.Staking.GetStakingSymbolsAsync()` |
| Get open stake/unstake requests | `client.ExchangeApi.Staking.GetOpenStakingRequestsAsync(...)` |
| Get staking history | `client.ExchangeApi.Staking.GetStakingHistoryAsync(...)` |
| Get staking reward history | `client.ExchangeApi.Staking.GetStakingRewardHistoryAsync(...)` |
| Convert staking asset | `client.ExchangeApi.Staking.ConvertAsync(...)` |
| Get open conversion requests | `client.ExchangeApi.Staking.GetOpenConvertRequestsAsync(...)` |
| Get conversion rate | `client.ExchangeApi.Staking.GetConvertRateAsync(symbol)` |

## Public WebSocket

| User intent | CryptoCom.Net member |
|---|---|
| Subscribe ticker updates | `socketClient.ExchangeApi.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe many ticker updates | `socketClient.ExchangeApi.SubscribeToTickerUpdatesAsync(symbols, handler)` |
| Subscribe order book snapshots | `socketClient.ExchangeApi.SubscribeToOrderBookSnapshotUpdatesAsync(symbol, depth, handler)` |
| Subscribe order book deltas | `socketClient.ExchangeApi.SubscribeToOrderBookUpdatesAsync(symbol, depth, handler)` |
| Subscribe klines | `socketClient.ExchangeApi.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe public trades | `socketClient.ExchangeApi.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe index price | `socketClient.ExchangeApi.SubscribeToIndexPriceUpdatesAsync(symbol, handler)` |
| Subscribe mark price | `socketClient.ExchangeApi.SubscribeToMarkPriceUpdatesAsync(symbol, handler)` |
| Subscribe settlement prices | `socketClient.ExchangeApi.SubscribeToSettlementUpdatesAsync(symbol, handler)` |
| Subscribe funding rates | `socketClient.ExchangeApi.SubscribeToFundingRateUpdatesAsync(symbol, handler)` |
| Subscribe estimated funding rates | `socketClient.ExchangeApi.SubscribeToEstimatedFundingRateUpdatesAsync(symbol, handler)` |

## Private WebSocket And Socket API

| User intent | CryptoCom.Net member |
|---|---|
| Subscribe user orders | `socketClient.ExchangeApi.SubscribeToOrderUpdatesAsync(handler)` |
| Subscribe symbol user orders | `socketClient.ExchangeApi.SubscribeToOrderUpdatesAsync(symbol, handler)` |
| Subscribe user trades | `socketClient.ExchangeApi.SubscribeToUserTradeUpdatesAsync(handler)` |
| Subscribe balances | `socketClient.ExchangeApi.SubscribeToBalanceUpdatesAsync(handler)` |
| Subscribe positions | `socketClient.ExchangeApi.SubscribeToPositionUpdatesAsync(handler)` |
| Subscribe position and balance updates | `socketClient.ExchangeApi.SubscribeToPositionBalanceUpdatesAsync(handler)` |
| Socket API get balances | `socketClient.ExchangeApi.GetBalancesAsync()` |
| Socket API get positions | `socketClient.ExchangeApi.GetPositionsAsync(symbol)` |
| Socket API place order | `socketClient.ExchangeApi.PlaceOrderAsync(...)` |
| Socket API cancel order | `socketClient.ExchangeApi.CancelOrderAsync(orderId: orderId)` |
| Socket API cancel all orders | `socketClient.ExchangeApi.CancelAllOrdersAsync(symbol)` |
| Socket API close position | `socketClient.ExchangeApi.ClosePositionAsync(symbol, OrderType.Market)` |
| Socket API get open orders | `socketClient.ExchangeApi.GetOpenOrdersAsync(symbol)` |
| Socket API place multiple orders | `socketClient.ExchangeApi.PlaceMultipleOrdersAsync(orders)` |
| Socket API cancel multiple orders | `socketClient.ExchangeApi.CancelOrdersAsync(orders)` |
| Socket API place OCO order | `socketClient.ExchangeApi.PlaceOcoOrderAsync(order1, order2)` |
| Socket API withdraw | `socketClient.ExchangeApi.WithdrawAsync(...)` |
| Cancel orders on disconnect | `socketClient.ExchangeApi.SetCancelOnDisconnectAsync()` |

## Shared API V2

Use `client.ExchangeApi.SharedApi` and `socketClient.ExchangeApi.SharedApi`. The DI aggregate `ICryptoComSharedApiClient` exposes `Rest` and `Socket`.

| Intent | Representative capabilities |
| --- | --- |
| Symbols and tickers | `IGetSpotSymbolsRest`, `IGetFuturesSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetBookTickerRest` |
| Market data | `IGetKlinesRest`, `IGetOrderBookRest`, `IGetRecentTradesRest`, `IGetFundingRateHistoryRest`, `IGetOpenInterestRest` |
| Accounts | `IGetAssetRest`, `IGetAllAssetsRest`, `IGetBalancesRest`, deposit/withdrawal history and placement, `IGetFeesRest`, `IGetLeverageRest`, `ISetLeverageRest` |
| Spot trading | `IPlaceSpotOrderRest`, `ICancelSpotOrderRest`, order/client-ID lookup, order/trade history, and spot trigger-order capabilities |
| Futures trading | `IPlaceFuturesOrderRest`, `ICancelFuturesOrderRest`, order/client-ID lookup, `IGetPositionsRest`, `ICloseFullPositionRest`, futures trigger-order and TP/SL capabilities |
| Public socket subscriptions | `ISubscribeTickerSocket`, `ISubscribeBookTickerSocket`, `ISubscribeKlinesSocket`, `ISubscribeOrderBookSocket`, `ISubscribeTradesSocket` |
| Private socket subscriptions | `ISubscribeUserTradesSocket`, `ISubscribeSpotOrdersSocket`, `ISubscribeFuturesOrdersSocket`, `ISubscribePositionsSocket`, `ISubscribeBalancesSocket` |
| Socket order queries | `IPlaceSpotOrderSocket`, `ICancelSpotOrderSocket`, `IPlaceFuturesOrderSocket`, `ICancelFuturesOrderSocket` |

Use the aggregate's `GetCapability` with a descriptor such as `SharedCapabilities.Tickers.GetTicker.Rest` and the required trading mode when selecting implementations dynamically. Check for a missing match before accessing `Capability` and `Options`. `GetCapabilities` enumerates implementations and `Discover()` describes supported operations. With multiple exchanges registered, resolve `IEnumerable<TCapability>` and select by exchange and supported mode.

### Catalogs, quantity representations, and prerequisites

Call `IGetSpotSymbolsRest.GetSpotSymbolsAsync(new GetSymbolsRequest())` or `IGetFuturesSymbolsRest.GetFuturesSymbolsAsync(new GetSymbolsRequest())` successfully before reading `SpotSymbolCatalog` or `FuturesSymbolCatalog`. Requests support base/quote asset type and subtype filters; results include `DisplayName`, trading constraints, and classifications (`Crypto`, `Fiat`, `TradFi`; applicable `StableCoin`, `Equity`, `Commodity` subtypes).

Shared quantity-bearing models use `SharedOrderQuantity`. Spot limit orders and market sells use base-asset quantity; spot market buys can use base or quote quantity. Futures order capabilities advertise base-asset quantities. Shared REST and socket order books use `SharedQuantityType.BaseAsset`; do not copy contract quantity semantics from another exchange.

Shared futures placement does not support inline `StopLossPrice`, `TakeProfitPrice`, or `PositionSide`; use the appropriate separate trigger/TP-SL workflow. Shared all-ticker requests require `TradingMode`. Shared TP/SL setting requires quantity, and TP/SL cancellation requires an order ID. Shared leverage setting accepts `AccountId` in `ExchangeParameters` (alias `account_id`); without it, the adapter uses the master account ID. Read capability options and exchange parameter rules before constructing portable writes.

Shared REST uses `HttpResult<T>` / `HttpResult`, socket subscriptions use `WebSocketResult<UpdateSubscription>`, socket order queries use `QueryResult<T>` / `QueryResult`, and non-I/O symbol/cache helpers can return `ExchangeCallResult<T>`.

## Environments and helpers

Clients default to `CryptoComEnvironment.Live`. Select `CryptoComEnvironment.Sandbox` explicitly when testing with sandbox credentials. `CreateCustom` takes a name, REST address, and socket address.

Checkout-only: [public interfaces](../../../../CryptoCom.Net/Interfaces/Clients/), [local order books](../../../../CryptoCom.Net/SymbolOrderBooks/), and [tracker factory](../../../../CryptoCom.Net/CryptoComTrackerFactory.cs). A maintained local book combines snapshots and deltas; an individual incremental message is not a full book.
