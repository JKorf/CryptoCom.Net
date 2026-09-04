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

        #region Get Book Ticker

        async Task<ICallResult<SharedBookTicker>> IGetBookTicker.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
            => await GetBookTickerAsync(request, ct).ConfigureAwait(false);

        public GetBookTickerOptions GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, false);
        public async Task<HttpResult<SharedBookTicker>> GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = await _api.ExchangeData.GetOrderBookAsync(symbol, 1, ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            if (resultTicker.Data.Asks.Length == 0)
                return HttpResult.Fail<SharedBookTicker>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, "Symbol not found")));

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(request.Symbol!.TradingMode == TradingMode.Spot ? _topicSpotId : _topicFuturesId, _api.EnvironmentName, null, symbol),
                symbol,
                resultTicker.Data.Asks[0].Price,
                new SharedOrderQuantity(resultTicker.Data.Asks[0].Quantity),
                resultTicker.Data.Bids[0].Price,
                new SharedOrderQuantity(resultTicker.Data.Bids[0].Quantity)));
        }

        #endregion

    }
}
