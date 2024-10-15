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
        Task<WebCallResult<IEnumerable<CryptoComPosition>>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#introduction-2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
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
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderType">Type of order to use</param>
        /// <param name="price">Price for limit order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComOrderId>> ClosePositionAsync(string symbol, OrderType orderType, decimal? price = null, CancellationToken ct = default);

    }
}
