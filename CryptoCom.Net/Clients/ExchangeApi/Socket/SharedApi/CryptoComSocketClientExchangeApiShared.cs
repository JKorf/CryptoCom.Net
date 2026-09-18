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
    internal partial class CryptoComSocketClientExchangeSharedApi : 
        SharedApiBase,
        ICryptoComSocketClientExchangeApiShared,
        ICryptoComSocketClientExchangeSharedApi
    {
        private readonly CryptoComSocketClientExchangeApi _api;

        private const string _topicSpotId = "CryptoComSpot";
        private const string _topicFuturesId = "CryptoComFutures";
        private const string _exchangeName = "CryptoCom";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(CryptoComExchange.Metadata, this);

        public CryptoComSocketClientExchangeSharedApi(CryptoComSocketClientExchangeApi api)
            : base(
                  SharedTransport.Socket,
                  api,
                  [TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.DeliveryLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeBookTickerOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeTradeOptions,
                SubscribeUserTradeOptions,
                SubscribeSpotOrderOptions,
                SubscribeFuturesOrderOptions,
                SubscribePositionOptions,
                SubscribeBalanceOptions,
                PlaceSpotOrderOptions,
                CancelSpotOrderOptions,
                PlaceFuturesOrderOptions,
                CancelFuturesOrderOptions
                );
        }
    }
}
