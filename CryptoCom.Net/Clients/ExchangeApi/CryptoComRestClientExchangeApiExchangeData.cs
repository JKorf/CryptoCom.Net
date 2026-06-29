using System;
using System.Net.Http;
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
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            // No dedicated endpoint, use ticker endpoint which returns a timestamp
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", "BTC_USD");
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "public/get-tickers", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComTickersWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            return HttpResult.Ok(result, result.Data.Tickers.Single().Timestamp);
        }

        #endregion

        #region Get Risk Parameters

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComRiskParameters>> GetRiskParametersAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/public/get-risk-parameters", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComRiskParameters>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "public/get-instruments", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComSymbolWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComSymbol[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComOrderBook>> GetOrderBookAsync(string symbol, int depth, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("depth", depth);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "public/get-book", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComOrderBookWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComOrderBook>(result);

            return HttpResult.Ok(result, result.Data.Data.First());
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("timeframe", interval);
            parameters.Add("start_ts", startTime);
            parameters.Add("end_ts", endTime);
            parameters.Add("count", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "public/get-candlestick", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComKlineWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComKline[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComTicker[]>> GetTickersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "public/get-tickers", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComTickersWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Tickers);
        }

        #endregion

        #region Get Trade History

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComTrade[]>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("start_ts", DateTimeConverter.ConvertToNanoseconds(startTime));
            parameters.Add("end_ts", DateTimeConverter.ConvertToNanoseconds(endTime));
            parameters.Add("count", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "public/get-trades", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComTradeWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComTrade[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Valuations

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComValuation[]>> GetValuationsAsync(string symbol, ValuationType type, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("valuation_type", type);
            parameters.Add("start_ts", startTime);
            parameters.Add("end_ts", endTime);
            parameters.Add("count", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "public/get-valuations", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComValuationWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComValuation[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Expired Settlement Price

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComExpiredPrice[]>> GetExpiredSettlementPriceAsync(SymbolType symbolType, int? pageNumber = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_type", symbolType);
            parameters.Add("page", pageNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "public/get-expired-settlement-price", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComExpiredPriceWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComExpiredPrice[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Insurance

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComValuation[]>> GetInsuranceAsync(string asset, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", asset);
            parameters.Add("start_ts", startTime);
            parameters.Add("end_ts", endTime);
            parameters.Add("count", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "public/get-insurance", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComValuationWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComValuation[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Announcements

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComAnnouncement[]>> GetAnnouncementsAsync(AnnouncementCategory? category = null, string? productType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("category", category);
            parameters.Add("product_type", productType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.ClientOptions.Environment.RestClientAddress, "v1/public/get-announcements", CryptoComExchange.RateLimiter.RestPublic, 1, false);
            var result = await _baseClient.SendAsync<CryptoComAnnouncementWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComAnnouncement[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

    }
}
