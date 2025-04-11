using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using CryptoCom.Net.Objects.Models;
using System.Drawing;
using System;
using CryptoCom.Net.Enums;
using CryptoCom.Net.Objects;

namespace CryptoCom.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// CryptoCom Exchange trading endpoints, placing and managing orders.
    /// </summary>
    public interface ICryptoComRestClientExchangeApiTrading
    {
        /// <summary>
        /// Get positions for the account
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-positions" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSD_PERP`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);

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
        Task<WebCallResult<CryptoComOrderId>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, bool? postOnly = null, TimeInForce? timeInForce = null, decimal? triggerPrice = null, PriceType? triggerPriceType = null, bool? margin = null, SelfTradePreventionScope? selfTradePreventionScope = null, SelfTradePreventionMode? selfTradePreventionMode = null, string? selfTradePreventionId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order by id
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-cancel-order" /></para>
        /// </summary>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComOrderId>> CancelOrderAsync(string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all order fitting the parameters
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-cancel-all-orders" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="type">Filter by type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, OrderTypeFilter? type = null, CancellationToken ct = default);

        /// <summary>
        /// Close an open position
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-close-position" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSD_PERP`</param>
        /// <param name="orderType">Type of order to use</param>
        /// <param name="price">Price for limit order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComOrderId>> ClosePositionAsync(string symbol, OrderType orderType, decimal? price = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get user open orders
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-open-orders" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComOrder[]>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific order
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-order-detail" /></para>
        /// </summary>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComOrder>> GetOrderAsync(string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed order history
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-order-history" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComOrder[]>> GetClosedOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trade history
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-trades" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComUserTrade[]>> GetUserTradesAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders in a single call. Note that this call will return success even when all or some of the requests fail. Make sure to check the result data.
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-create-order-list-list" /></para>
        /// </summary>
        /// <param name="orders">Orders to place, max 10</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CallResult<CryptoComOrderResult>[]>> PlaceMultipleOrdersAsync(IEnumerable<CryptoComOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders in a single call. Note that this call will return success even when all or some of the requests fail. Make sure to check the result data.
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-cancel-order-list-list" /></para>
        /// </summary>
        /// <param name="orders">Orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComCancelOrderResult[]>> CancelOrdersAsync(IEnumerable<CryptoComCancelOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Place a new OCO (One Cancels Other) order
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-create-order-list-oco" /></para>
        /// </summary>
        /// <param name="order1">First order</param>
        /// <param name="order2">Second order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComOcoResult>> PlaceOcoOrderAsync(CryptoComOrderRequest order1, CryptoComOrderRequest order2, CancellationToken ct = default);

        /// <summary>
        /// Cancel an OCO order
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-cancel-order-list-oco" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="listId">List id to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelOcoOrderAsync(string symbol, string listId, CancellationToken ct = default);

        /// <summary>
        /// Get info on an OCO order
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-order-list-oco" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="listId">Id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CryptoComOrder[]>> GetOcoOrderAsync(string symbol, string listId, CancellationToken ct = default);
    }
}
