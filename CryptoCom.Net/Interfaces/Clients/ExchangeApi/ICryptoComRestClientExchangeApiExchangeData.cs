using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoCom.Net.Enums;
using CryptoCom.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace CryptoCom.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// CryptoCom Exchange exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface ICryptoComRestClientExchangeApiExchangeData
    {
        /// <summary>
        /// 
        /// <para><a href="XXX" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get risk parameters
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-get-risk-parameters" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComRiskParameters>> GetRiskParametersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbols/instruments
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-get-instruments" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComSymbol>>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the order book for a symbol
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-get-book" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="depth">Order book depth, max 50</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComOrderBook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default);

        /// <summary>
        /// Get tickers
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-get-tickers" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComTicker>>> GetTickersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get trade history
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-get-trades" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results, max 150</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComTrade>>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get Kline/Candlestick data
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-get-candlestick" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get various statistics
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-get-valuations" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="type">Stat type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComValuation>>> GetValuationsAsync(string symbol, ValuationType type, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get expired contracts settlement price
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-get-expired-settlement-price" /></para>
        /// </summary>
        /// <param name="symbolType">Symbol type</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComExpiredPrice>>> GetExpiredSettlementPriceAsync(SymbolType symbolType, int? pageNumber = null, CancellationToken ct = default);

        /// <summary>
        /// Get the balance of Insurance Fund for a particular asset.
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-get-insurance" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComValuation>>> GetInsuranceAsync(string asset, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);


    }
}
