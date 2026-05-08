# CryptoCom.Net AI API Quick Map

Use this file to route common user intents to the correct CryptoCom.Net client member. If a method name or parameter is not listed here, inspect `CryptoCom.Net/Interfaces/Clients/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new CryptoComRestClient()` |
| WebSocket streams and socket API requests | `new CryptoComSocketClient()` |
| API key authentication | `options.ApiCredentials = new CryptoComCredentials("key", "secret")` |
| Live environment | `CryptoComEnvironment.Live` |
| Sandbox environment | `CryptoComEnvironment.Sandbox` |
| Dependency injection | `services.AddCryptoCom(options => { ... })` |
| REST API root | `client.ExchangeApi` |
| Socket API root | `socketClient.ExchangeApi` |

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
| Get expired settlement prices | `client.ExchangeApi.ExchangeData.GetExpiredSettlementPriceAsync(SymbolType.Perpetual)` |
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
| Get one symbol positions | `client.ExchangeApi.Trading.GetPositionsAsync("ETHUSD_PERP")` |
| Place order | `client.ExchangeApi.Trading.PlaceOrderAsync(...)` |
| Place spot limit order | `client.ExchangeApi.Trading.PlaceOrderAsync("BTC_USDT", OrderSide.Buy, OrderType.Limit, quantity: ..., price: ...)` |
| Place market order by base quantity | `client.ExchangeApi.Trading.PlaceOrderAsync(symbol, side, OrderType.Market, quantity: ...)` |
| Place market order by quote quantity | `client.ExchangeApi.Trading.PlaceOrderAsync(symbol, side, OrderType.Market, quoteQuantity: ...)` |
| Place trigger order | `client.ExchangeApi.Trading.PlaceOrderAsync(..., triggerPrice: ..., triggerPriceType: ...)` |
| Place isolated margin order | `client.ExchangeApi.Trading.PlaceOrderAsync(..., isolatedMargin: true, leverage: ...)` |
| Cancel order | `client.ExchangeApi.Trading.CancelOrderAsync(orderId: orderId)` |
| Cancel by client order id | `client.ExchangeApi.Trading.CancelOrderAsync(clientOrderId: clientOrderId)` |
| Cancel all orders | `client.ExchangeApi.Trading.CancelAllOrdersAsync(symbol)` |
| Close position | `client.ExchangeApi.Trading.ClosePositionAsync("ETHUSD_PERP", OrderType.Market)` |
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

## SharedApis

Use SharedApis for exchange-agnostic code across Crypto.com, Binance, Bybit, OKX, Kraken, and other CryptoExchange.Net libraries.

| User intent | CryptoCom.Net member or interface |
|---|---|
| Shared REST client | `new CryptoComRestClient().ExchangeApi.SharedClient` |
| Shared socket client | `new CryptoComSocketClient().ExchangeApi.SharedClient` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared futures ticker REST | `IFuturesTickerRestClient.GetFuturesTickerAsync(new GetTickerRequest(symbol))` |
| Shared spot order REST | `ISpotOrderRestClient.PlaceSpotOrderAsync(...)` |
| Shared futures order REST | `IFuturesOrderRestClient.PlaceFuturesOrderAsync(...)` |
| Shared balances REST | `IBalanceRestClient.GetBalancesAsync(...)` |
| Shared fees REST | `IFeeRestClient.GetFeeAsync(...)` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared order book socket | `IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(...)` |
| Shared balance socket | `IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(...)` |
| Shared position socket | `IPositionSocketClient.SubscribeToPositionUpdatesAsync(...)` |

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | `if (!sub.Success) { Console.WriteLine(sub.Error); return; }` |
| Read REST data | Read `result.Data` only after `result.Success` |
| Retry decision | Retry only when `result.Error?.IsTransient == true` |
| Cancellation | Pass `ct: cancellationToken` |
| Batch order result | Check each nested `CallResult<T>` in the response data |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| `CryptoComClient` | `CryptoComRestClient` |
| `ApiCredentials` | `CryptoComCredentials` |
| `SpotApi` | `ExchangeApi` |
| `UsdFuturesApi` / `CoinFuturesApi` | `ExchangeApi.Trading` with derivative symbols |
| `GeneralApi` | `ExchangeApi.Account`, `ExchangeApi.Trading`, or `ExchangeApi.Staking` |
| `GetTickerAsync` | `GetTickersAsync` |
| `BTCUSDT` by default | `BTC_USDT` for spot examples, or exact symbol from `GetSymbolsAsync()` |
| `.Data` without `.Success` check | Check `.Success` first |
| `ITickerSocketClient.UnsubscribeAsync(...)` | Keep the concrete socket client and call `socketClient.UnsubscribeAsync(subscription.Data)` |
| Custom `clientOrderId` by default | Omit it unless external correlation is required |
