using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoCom.Net.Objects.Models;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Linq;
using CryptoCom.Net.Enums;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    /// <inheritdoc />
    internal class CryptoComRestClientExchangeApiExchangeData : ICryptoComRestClientExchangeApiExchangeData
    {
        private readonly CryptoComRestClientExchangeApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal CryptoComRestClientExchangeApiExchangeData(ILogger logger, CryptoComRestClientExchangeApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            // No dedicated endpoint, use ticker endpoint which returns a timestamp
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", "BTC_USD");
            var request = _definitions.GetOrCreate(HttpMethod.Get, "public/get-tickers", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComTickersWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data?.Tickers.Single().Timestamp ?? default);
        }

        #endregion

        #region Get Risk Parameters

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComRiskParameters>> GetRiskParametersAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/public/get-risk-parameters", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComRiskParameters>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "public/get-instruments", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComSymbolWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComSymbol[]>(result.Data?.Data);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComOrderBook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            parameters.Add("depth", depth);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "public/get-book", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComOrderBookWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComOrderBook> (result.Data?.Data.First());
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            parameters.AddEnum("timeframe", interval);
            parameters.AddOptionalMillisecondsString("start_ts", startTime);
            parameters.AddOptionalMillisecondsString("end_ts", endTime);
            parameters.AddOptional("count", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "public/get-candlestick", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComKlineWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComKline[]>(result.Data?.Data);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComTicker[]>> GetTickersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "public/get-tickers", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComTickersWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComTicker[]>(result.Data?.Tickers);
        }

        #endregion

        #region Get Trade History

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComTrade[]>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            parameters.AddOptional("start_ts", DateTimeConverter.ConvertToNanoseconds(startTime));
            parameters.AddOptional("end_ts", DateTimeConverter.ConvertToNanoseconds(endTime));
            parameters.AddOptional("count", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "public/get-trades", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComTradeWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComTrade[]>(result.Data?.Data);
        }

        #endregion

        #region Get Valuations

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComValuation[]>> GetValuationsAsync(string symbol, ValuationType type, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            parameters.AddEnum("valuation_type", type);
            parameters.AddOptionalMillisecondsString("start_ts", startTime);
            parameters.AddOptionalMillisecondsString("end_ts", endTime);
            parameters.AddOptional("count", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "public/get-valuations", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComValuationWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComValuation[]>(result.Data?.Data);
        }

        #endregion

        #region Get Expired Settlement Price

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComExpiredPrice[]>> GetExpiredSettlementPriceAsync(SymbolType symbolType, int? pageNumber = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("instrument_type", symbolType);
            parameters.AddOptional("page", pageNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "public/get-expired-settlement-price", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComExpiredPriceWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComExpiredPrice[]>(result.Data?.Data);
        }

        #endregion

        #region Get Insurance

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComValuation[]>> GetInsuranceAsync(string asset, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", asset);
            parameters.AddOptionalMillisecondsString("start_ts", startTime);
            parameters.AddOptionalMillisecondsString("end_ts", endTime);
            parameters.AddOptional("count", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "public/get-insurance", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComValuationWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComValuation[]>(result.Data?.Data);
        }

        #endregion

        #region Get Announcements

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComAnnouncement[]>> GetAnnouncementsAsync(AnnouncementCategory? category = null, string? productType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("category", category);
            parameters.AddOptional("product_type", productType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v1/public/get-announcements", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendToAddressAsync<CryptoComAnnouncementWrapper>(_baseClient.ClientOptions.Environment.RestClientAddress, request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComAnnouncement[]>(result.Data?.Data);
        }

        #endregion

    }
}
