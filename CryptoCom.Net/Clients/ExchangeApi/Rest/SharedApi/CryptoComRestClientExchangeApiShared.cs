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
    internal partial class CryptoComRestClientExchangeSharedApi :
        SharedApiBase,
        ICryptoComRestClientExchangeApiShared,
        ICryptoComRestClientExchangeSharedApi
    {
        private readonly CryptoComRestClientExchangeApi _api;

        private const string _topicSpotId = "CryptoComSpot";
        private const string _topicFuturesId = "CryptoComFutures";
        private const string _exchangeName = "CryptoCom";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(CryptoComExchange.Metadata, this);

        private readonly static HashSet<string> _knownExchangeFiat = ["EUR", "USD", "BRL", "GBP", "CAD", "HKD", "KRW", "TWD", "RUB", "SGD"];

        public CryptoComRestClientExchangeSharedApi(CryptoComRestClientExchangeApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  [TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.DeliveryLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetAssetOptions,
                GetAllAssetsOptions,
                GetBalancesOptions,
                GetDepositAddressesOptions,
                GetDepositHistoryOptions,
                GetKlinesOptions,
                GetOrderBookOptions,
                GetRecentTradesOptions,
                GetWithdrawalHistoryOptions,
                WithdrawOptions,
                GetSpotSymbolsOptions,
                GetTickerOptions,
                GetAllTickersOptions,
                GetBookTickerOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                CancelSpotOrderOptions,
                GetSpotOrderTradesOptions,
                GetSpotUserTradeHistoryOptions,
                GetSpotOrderByClientOrderIdOptions,
                CancelSpotOrderByClientOrderIdOptions,
                GetFundingRateHistoryOptions,
                GetFuturesSymbolsOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetOpenInterestOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                CancelFuturesOrderOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                GetPositionsOptions,
                CloseFullPositionOptions,
                GetFuturesOrderByClientOrderIdOptions,
                CancelFuturesOrderByClientOrderIdOptions,
                GetFeeOptions,
                PlaceSpotTriggerOrderOptions,
                GetSpotTriggerOrderOptions,
                CancelSpotTriggerOrderOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions,
                SetFuturesTpSlOptions,
                CancelFuturesTpSlOptions
                );
        }
    }
}
