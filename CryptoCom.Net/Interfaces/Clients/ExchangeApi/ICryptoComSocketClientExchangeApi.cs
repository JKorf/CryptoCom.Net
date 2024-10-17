using CryptoCom.Net.Enums;
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
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="depth">The book depth, either 10 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(string symbol, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book snapshot updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="depth">The book depth, either 10 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook delta updates. Initially the orderbook snapshot is pushed, after which only changes are pushed.
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="depth">The book depth, either 10 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to orderbook delta updates. Initially the orderbook snapshot is pushed, after which only changes are pushed.
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="depth">The book depth, either 10 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the kline/candles</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<IEnumerable<CryptoComKline>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the kline/candles</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<IEnumerable<CryptoComKline>>> onMessage, CancellationToken ct = default);

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
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<CryptoComTrade>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#trade-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<CryptoComTrade>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to settlement prices
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to settlement prices
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to settlement prices
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to estimated funding rate updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to estimated funding rate updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<IEnumerable<CryptoComOrder>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<CryptoComOrder>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<CryptoComOrder>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<IEnumerable<CryptoComUserTrade>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<CryptoComUserTrade>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<CryptoComUserTrade>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user balance updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<CryptoComBalances>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user position updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<IEnumerable<CryptoComPosition>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user position and balance updates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#ticker-instrument_name" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionBalanceUpdatesAsync(Action<DataEvent<CryptoComBalancePositionUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exhanges to allow for a common implementation for different exchanges.
        /// </summary>
        public ICryptoComSocketClientExchangeApiShared SharedClient { get; }
    }
}
