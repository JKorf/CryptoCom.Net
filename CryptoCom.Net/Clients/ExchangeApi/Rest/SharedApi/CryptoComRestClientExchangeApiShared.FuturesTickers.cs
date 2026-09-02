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
        #region Futures Ticker client

        public GetFuturesTickerOptions GetFuturesTickerOptions { get; } = new GetFuturesTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTickerOptions.ValidateRequest(request, this);
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

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllFuturesTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllFuturesTickersOptions;

        public GetAllFuturesTickersOptions GetAllFuturesTickersOptions { get; } = new GetAllFuturesTickersOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllFuturesTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(result);

            var time = DateTime.UtcNow;
            var nextFundingTime = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0, DateTimeKind.Utc).AddHours(1);
            return HttpResult.Ok(result, result.Data.Select(x =>
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
