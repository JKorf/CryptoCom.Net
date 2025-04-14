using CryptoCom.Net.Enums;
using CryptoCom.Net.Objects;
using CryptoCom.Net.Objects.Internal;
using CryptoCom.Net.Objects.Models;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoCom.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// CryptoCom Exchange streams
    /// </summary>
    public interface ICryptoComSocketClientExchangeApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Subscribe to order book snapshot updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#book-instrument_name-depth" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="depth">The book depth, either 10 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(string symbol, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book snapshot updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#book-instrument_name-depth" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="depth">The book depth, either 10 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook delta updates. Initially the orderbook snapshot is pushed, after which only changes are pushed.
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#book-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="depth">The book depth, either 10 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook delta updates. Initially the orderbook snapshot is pushed, after which only changes are pushed.
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#book-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="depth">The book depth, either 10 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#candlestick-time_frame-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the kline/candles</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<CryptoComKline[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#candlestick-time_frame-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the kline/candles</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<CryptoComKline[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<CryptoComTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#trade-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<CryptoComTrade[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#trade-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComTrade[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#index-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#index-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#mark-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#mark-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to settlement prices
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#settlement-instrument_name" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to settlement prices
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#settlement-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to settlement prices
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#settlement-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#funding-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#funding-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to estimated funding rate updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#estimatedfunding-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to estimated funding rate updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#estimatedfunding-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#user-order-instrument_name" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<CryptoComOrder[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#user-order-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<DataEvent<CryptoComOrder[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#user-order-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComOrder[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#user-trade-instrument_name" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<CryptoComUserTrade[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#user-trade-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string symbol, Action<DataEvent<CryptoComUserTrade[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#user-trade-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComUserTrade[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user balance updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#user-balance" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<CryptoComBalances>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user position updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#user-positions" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<CryptoComPosition[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user position and balance updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#user-position_balance" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionBalanceUpdatesAsync(Action<DataEvent<CryptoComBalancePositionUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Get user account balances
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-user-balance" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<CryptoComBalances[]>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get positions for the account
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-positions" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSD_PERP`</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<CryptoComPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#introduction-2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="quoteQuantity">Quantity in quote asset</param>
        /// <param name="price">Limit price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="postOnly">Post only order</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="triggerPriceType">Type of trigger price</param>
        /// <param name="margin">True for spot margin order</param>
        /// <param name="selfTradePreventionScope">Scope for self trade prevention</param>
        /// <param name="selfTradePreventionMode">Mode for self trade prevention</param>
        /// <param name="selfTradePreventionId">Id for self trade prevention</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<CryptoComOrderId>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, bool? postOnly = null, TimeInForce? timeInForce = null, decimal? triggerPrice = null, PriceType? triggerPriceType = null, bool? margin = null, SelfTradePreventionScope? selfTradePreventionScope = null, SelfTradePreventionMode? selfTradePreventionMode = null, string? selfTradePreventionId = null, CancellationToken ct = default);
        
        /// <summary>
        /// Cancel an order by id
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-cancel-order" /></para>
        /// </summary>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<CryptoComOrderId>> CancelOrderAsync(string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all order fitting the parameters
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-cancel-all-orders" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="type">Filter by type</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult> CancelAllOrdersAsync(string? symbol = null, OrderTypeFilter? type = null, CancellationToken ct = default);

        /// <summary>
        /// Close an open position
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-close-position" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSD_PERP`</param>
        /// <param name="orderType">Type of order to use</param>
        /// <param name="price">Price for limit order</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<CryptoComOrderId>> ClosePositionAsync(string symbol, OrderType orderType, decimal? price = null, CancellationToken ct = default);

        /// <summary>
        /// Get user open orders
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-open-orders" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<CryptoComOrder[]>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders in a single call. Note that this call will return success even when all or some of the requests fail. Make sure to check the result data.
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-create-order-list-list" /></para>
        /// </summary>
        /// <param name="orders">Orders to place, max 10</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<CallResult<CryptoComListOrderResult>[]>> PlaceMultipleOrdersAsync(IEnumerable<CryptoComOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders in a single call. Note that this call will return success even when all or some of the requests fail. Make sure to check the result data.
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-cancel-order-list-list" /></para>
        /// </summary>
        /// <param name="orders">Orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<CryptoComListOrderResult[]>> CancelOrdersAsync(IEnumerable<CryptoComCancelOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Place a new OCO (One Cancels Other) order
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-create-order-list-oco" /></para>
        /// </summary>
        /// <param name="order1">First order</param>
        /// <param name="order2">Second order</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<CryptoComOcoResult>> PlaceOcoOrderAsync(CryptoComOrderRequest order1, CryptoComOrderRequest order2, CancellationToken ct = default);

        /// <summary>
        /// Cancel an OCO order
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-cancel-order-list-oco" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="listId">List id to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult> CancelOcoOrderAsync(string symbol, string listId, CancellationToken ct = default);
        
        /// <summary>
        /// Withdraw funds
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-create-withdrawal" /></para>
        /// </summary>
        /// <param name="asset">Asset to withdraw</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="address">Target address</param>
        /// <param name="addressTag">Address tag</param>
        /// <param name="network">Network to use</param>
        /// <param name="clientWithdrawId">Client id</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<CryptoComWithdrawalResult>> WithdrawAsync(string asset, decimal quantity, string address, string? addressTag = null, string? network = null, string? clientWithdrawId = null, CancellationToken ct = default);

        /// <summary>
        /// Set all open orders created by this connection to cancel when the connection is interrupted. There is no way to cancel this once set, unsubscribing is considered a loss of connection.
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-set-cancel-on-disconnect" /></para>
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult> SetCancelOnDisconnectAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public ICryptoComSocketClientExchangeApiShared SharedClient { get; }
    }
}
