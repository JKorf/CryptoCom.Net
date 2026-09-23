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
        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        #region Subscribe To Balance Updates

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToBalanceUpdatesAsync(
                update =>
                {
                    handler(update.ToType<SharedBalance[]>(update.Data.PositionBalances.Select(x =>
                        new SharedBalance(
                            SupportedTradingModes, 
                            x.Asset, 
                            x.Quantity - x.ReservedQuantity, 
                            x.Quantity)).ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
