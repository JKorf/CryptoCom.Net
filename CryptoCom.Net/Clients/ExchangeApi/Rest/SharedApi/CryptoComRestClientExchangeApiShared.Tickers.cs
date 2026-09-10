using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using CryptoExchange.Net.Objects;
using CryptoCom.Net.Enums;
using CryptoExchange.Net;
using CryptoCom.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    internal partial class CryptoComRestClientExchangeSharedApi
    {

        #region Get Ticker

        async Task<ICallResult<SharedTicker>> IGetTicker.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await ((IGetTickerRest)this).GetTickerAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker>> IGetTickerRest.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            if (request.Symbol!.TradingMode == TradingMode.Spot)
            {
                var result = await GetSpotTickerAsync(request, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedTicker>(result);

                return HttpResult.Ok<SharedTicker>(result, result.Data);
            }
            else
            {
                var result = await GetFuturesTickerAsync(request, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedTicker>(result);

                return HttpResult.Ok<SharedTicker>(result, result.Data);
            }
        }

        GetTickerOptions ISpotTickerRestClient.GetSpotTickerOptions => GetTickerOptions;
        GetTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions => GetTickerOptions;

        public GetTickerOptions GetTickerOptions { get; } = new GetTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedSpotTicker>> GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker>(result);

            if (!result.Data.Any())
                return HttpResult.Fail<SharedSpotTicker>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, "Symbol not found")));

            var symbol = result.Data.Single();
            return HttpResult.Ok(result, new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, symbol.Symbol),
                symbol.Symbol,
                symbol.LastPrice,
                symbol.HighPrice,
                symbol.LowPrice,
                new SharedOrderQuantity(symbol.Volume),
                symbol.PriceChange * 100));
        }

        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var tickerTask = _api.ExchangeData.GetTickersAsync(symbol, ct);
            var markTask = _api.ExchangeData.GetValuationsAsync(symbol, ValuationType.MarkPrice, limit: 1, ct: ct);
            var indexTask = _api.ExchangeData.GetValuationsAsync(symbol, ValuationType.IndexPrice, limit: 1, ct: ct);
            var fundingTask = _api.ExchangeData.GetValuationsAsync(symbol, ValuationType.FundingRate, limit: 1, ct: ct);

            await Task.WhenAll(tickerTask, markTask, indexTask, fundingTask).ConfigureAwait(false);

            if (!tickerTask.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(tickerTask.Result);

            if (!tickerTask.Result.Data.Any())
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, "Symbol not found")));

            var ticker = tickerTask.Result.Data.Single();
            var time = DateTime.UtcNow;
            return HttpResult.Ok(tickerTask.Result,
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, ticker.Symbol),
                    ticker.Symbol,
                    ticker.LastPrice,
                    ticker.HighPrice,
                    ticker.LowPrice,
                    new SharedOrderQuantity(ticker.Volume),
                    ticker.PriceChange * 100)
                {
                    FundingRate = fundingTask.Result.Data?.Single().Value,
                    MarkPrice = markTask.Result.Data?.Single().Value,
                    IndexPrice = indexTask.Result.Data?.Single().Value,
                    NextFundingTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0, DateTimeKind.Utc).AddHours(1),
                });
        }

        #endregion

        #region Get All Tickers

        async Task<ICallResult<SharedTicker[]>> IGetAllTickers.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await ((IGetAllTickersRest)this).GetAllTickersAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker[]>> IGetAllTickersRest.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTicker[]>(Exchange, validationError);

            if (request.TradingMode == TradingMode.Spot)
            {
                var result = await GetAllSpotTickersAsync(request, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedTicker[]>(result);

                return HttpResult.Ok<SharedTicker[]>(result, result.Data);
            }
            else
            {
                var result = await GetAllFuturesTickersAsync(request, ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedTicker[]>(result);

                return HttpResult.Ok<SharedTicker[]>(result, result.Data);
            }
        }

        Task<HttpResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllSpotTickersAsync(
                request.TradingMode == null ? request with { TradingMode = TradingMode.Spot } : request,
                ct);
        GetAllTickersOptions ISpotTickerRestClient.GetSpotTickersOptions => GetAllTickersOptions;

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(
                request.TradingMode == null ? request with { TradingMode = TradingMode.PerpetualLinear } : request,
                ct);
        GetAllTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllTickersOptions;

        public GetAllTickersOptions GetAllTickersOptions { get; } = new GetAllTickersOptions(_exchangeName)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<GetTickersRequest>.Required(x => x.TradingMode)
                ]
        };

        public async Task<HttpResult<SharedSpotTicker[]>> GetAllSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Where(x => x.Symbol.Contains("_")).Select(x => new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.LastPrice,
                x.HighPrice,
                x.LowPrice,
                new SharedOrderQuantity(x.Volume),
                x.PriceChange * 100)).ToArray());
        }

        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(result);

            var data = result.Data.Where(x => !x.Symbol.Contains("_"));
            data = request.TradingMode == TradingMode.PerpetualLinear
                ? data.Where(x => x.Symbol.EndsWith("-PERP"))
                : data.Where(x => !x.Symbol.EndsWith("-PERP"));

            var time = DateTime.UtcNow;
            var nextFundingTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0, DateTimeKind.Utc).AddHours(1);
            return HttpResult.Ok(result, data.Select(x =>
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    new SharedOrderQuantity(x.Volume),
                    x.PriceChange * 100)
                {
                    NextFundingTime = nextFundingTime
                }).ToArray());
        }

        #endregion

    }
}
