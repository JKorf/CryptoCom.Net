using CryptoExchange.Net.SharedApis;
using System;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Objects;
using System.Linq;
using CryptoCom.Net.Enums;
using CryptoExchange.Net;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    internal partial class CryptoComSocketClientExchangeSharedApi
    {
        #region Position client
        public SubscribePositionOptions SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToPositionUpdatesAsync(
                update =>
                {
                    if (update.UpdateType == SocketUpdateType.Snapshot)
                        return;

                    handler(update.ToType<SharedPosition[]>(update.Data.Select(x => 
                        new SharedPosition(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol, 
                            new SharedOrderQuantity(x.Quantity),
                            x.UpdateTime)
                        {
                            AverageOpenPrice = x.OpenPosCost,
                            PositionMode = SharedPositionMode.OneWay,
                            PositionSide = x.Quantity < 0 ? SharedPositionSide.Short : SharedPositionSide.Long,
                            UnrealizedPnl = x.SessionPnl
                        }).ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
