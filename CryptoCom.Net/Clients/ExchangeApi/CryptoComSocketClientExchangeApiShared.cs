using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;
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
    internal partial class CryptoComSocketClientExchangeApi : ICryptoComSocketClientExchangeApiShared
    {
        private const string _topicSpotId = "CryptoComSpot";
        private const string _topicFuturesId = "CryptoComFutures";
        public string Exchange => "CryptoCom";

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.DeliveryLinear };
        public TradingMode[] SupportedFuturesModes => new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Ticker client
        SubscribeTickerOptions ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscribeTickerOptions()
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)) : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToTickerUpdatesAsync(symbols, update => handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicSpotId, update.Data.Symbol) ?? ExchangeSymbolCache.ParseSymbol(_topicFuturesId, update.Data.Symbol), update.Data.Symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.Volume, update.Data.PriceChange * 100))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Book Ticker client

        EndpointOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new EndpointOptions<SubscribeBookTickerRequest>(false)
        {
            MaxSymbolCount = 200,
            SupportsMultipleSymbols = true
        };
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)) : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToTickerUpdatesAsync(symbols, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(ExchangeSymbolCache.ParseSymbol(_topicSpotId, update.Data.Symbol) ?? ExchangeSymbolCache.ParseSymbol(_topicFuturesId, update.Data.Symbol), update.Data.Symbol, update.Data.BestAskPrice ?? 0, update.Data.BestAskQuantity ?? 0, update.Data.BestBidPrice ?? 0, update.Data.BestBidQuantity ?? 0))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.ThreeMinutes,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.TwoHours,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.TwelveHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 140
        };
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)) : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToKlineUpdatesAsync(symbols, interval, update =>
            {
                if (update.UpdateType == SocketUpdateType.Snapshot)
                    return;

                foreach(var item in update.Data)
                    handler(update.AsExchangeEvent(Exchange, new SharedKline(ExchangeSymbolCache.ParseSymbol(_topicSpotId, update.Symbol) ?? ExchangeSymbolCache.ParseSymbol(_topicFuturesId, update.Symbol), update.Symbol!, item.OpenTime, item.ClosePrice, item.HighPrice, item.LowPrice, item.OpenPrice, item.Volume)));
            }, ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 5, 10, 20 })
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)) : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToOrderBookSnapshotUpdatesAsync(symbols, request.Limit ?? 10, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        EndpointOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new EndpointOptions<SubscribeTradeRequest>(false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)) : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToTradeUpdatesAsync(symbols, update =>
            {
                if (update.UpdateType == SocketUpdateType.Snapshot)
                    return;

                handler(update.AsExchangeEvent<SharedTrade[]>(Exchange, update.Data.Select(x =>
                new SharedTrade(ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol) ?? ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), x.Symbol, x.Quantity, x.Price, x.Timestamp)
                {
                    Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
                }).ToArray()));
            }, ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region User Trade client

        EndpointOptions<SubscribeUserTradeRequest> IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new EndpointOptions<SubscribeUserTradeRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<ExchangeEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToUserTradeUpdatesAsync(update =>
            {
                if (update.UpdateType == SocketUpdateType.Snapshot)
                    return;

                handler(update.AsExchangeEvent<SharedUserTrade[]>(Exchange, update.Data.Select(x => 
                new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol) ?? ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol),
                    x.Symbol, 
                    x.OrderId, 
                    x.TradeId, 
                    x.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    x.Quantity,
                    x.Price,
                    x.CreateTime)
                {
                    ClientOrderId = x.ClientOrderId,
                    FeeAsset = x.FeeAsset,
                    Fee = Math.Abs(x.Fee),
                    Role = x.Role == Enums.TradeRole.Taker ? SharedRole.Taker : SharedRole.Maker
                }).ToArray()));
            }, ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Spot Order client

        EndpointOptions<SubscribeSpotOrderRequest> ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } = new EndpointOptions<SubscribeSpotOrderRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<ExchangeEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
        {
            var validationError = ((ISpotOrderSocketClient)this).SubscribeSpotOrderOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToOrderUpdatesAsync(
                update =>
                {
                    if (update.UpdateType == SocketUpdateType.Snapshot)
                        return;

                    // Check for spot orders
                    var data = update.Data.Where(x => x.Symbol.Contains('_'));
                    if (!data.Any())
                        return;

                    handler(update.AsExchangeEvent<SharedSpotOrder[]>(Exchange, data.Select(x =>
                        new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol),
                            x.Symbol,
                            x.OrderId,
                            ParseOrderType(x.OrderType),
                            x.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseStatus(x.Status),
                            x.CreateTime)
                        {
                            ClientOrderId = x.ClientOrderId,
                            OrderPrice = x.Price == 0 ? null : x.Price,
                            OrderQuantity = new SharedOrderQuantity(x.Quantity, Math.Max(x.OrderValue, x.QuoteQuantityFilled)), // For market orders the value can be returned as smaller than the filled value
                            QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                            UpdateTime = x.UpdateTime,
                            Fee = Math.Abs(x.Fee),
                            FeeAsset = x.FeeAsset,
                            TimeInForce = x.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : x.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                            AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                            TriggerPrice = x.TriggerPrice,
                            IsTriggerOrder = x.TriggerPrice != null
                        }
                    ).ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        private SharedOrderType ParseOrderType(OrderType orderType)
        {
            if (orderType == OrderType.Market || orderType == OrderType.StopLoss || orderType == OrderType.TakeProfit)
                return SharedOrderType.Market;

            if (orderType == OrderType.Limit || orderType == OrderType.StopLimit || orderType == OrderType.TakeProfitLimit)
                return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedOrderStatus ParseStatus(OrderStatus status)
        {
            if (status == OrderStatus.Pending || status == OrderStatus.New || status == OrderStatus.Active) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.Rejected || status == OrderStatus.Expired) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }
        #endregion

        #region Futures Order client

        EndpointOptions<SubscribeFuturesOrderRequest> IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new EndpointOptions<SubscribeFuturesOrderRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<ExchangeEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderSocketClient)this).SubscribeFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToOrderUpdatesAsync(
                update =>
                {
                    if (update.UpdateType == SocketUpdateType.Snapshot)
                        return;

                    // Check for Futures orders
                    var data = update.Data.Where(x => !x.Symbol.Contains('_'));
                    if (!data.Any())
                        return;

                    handler(update.AsExchangeEvent<SharedFuturesOrder[]>(Exchange, data.Select(x =>
                        new SharedFuturesOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol),
                            x.Symbol,
                            x.OrderId,
                            x.OrderType == OrderType.Limit ? SharedOrderType.Limit : x.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            x.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseStatus(x.Status),
                            x.CreateTime)
                        {
                            ClientOrderId = x.ClientOrderId,
                            OrderPrice = x.Price,
                            OrderQuantity = new SharedOrderQuantity(x.Quantity, x.OrderValue, x.Quantity),
                            QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled, x.QuantityFilled),
                            UpdateTime = x.UpdateTime,
                            Fee = x.Fee,
                            FeeAsset = x.FeeAsset,
                            TimeInForce = x.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : x.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                            AveragePrice = x.AveragePrice,
                            TriggerPrice = x.TriggerPrice,
                            IsTriggerOrder = x.TriggerPrice > 0
                        }
                    ).ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Position client
        EndpointOptions<SubscribePositionRequest> IPositionSocketClient.SubscribePositionOptions { get; } = new EndpointOptions<SubscribePositionRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<ExchangeEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = ((IPositionSocketClient)this).SubscribePositionOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToPositionUpdatesAsync(
                update =>
                {
                    if (update.UpdateType == SocketUpdateType.Snapshot)
                        return;

                    handler(update.AsExchangeEvent<SharedPosition[]>(Exchange, update.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), x.Symbol, Math.Abs(x.Quantity), x.UpdateTime)
                    {
                        AverageOpenPrice = x.OpenPosCost,
                        PositionSide = x.Quantity < 0 ? SharedPositionSide.Short : SharedPositionSide.Long,
                        UnrealizedPnl = x.SessionPnl
                    }).ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Balance client
        EndpointOptions<SubscribeBalancesRequest> IBalanceSocketClient.SubscribeBalanceOptions { get; } = new EndpointOptions<SubscribeBalancesRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<ExchangeEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToBalanceUpdatesAsync(
                update =>
                {
                    handler(update.AsExchangeEvent<SharedBalance[]>(Exchange, update.Data.PositionBalances.Select(x => new SharedBalance(x.Asset, x.Quantity - x.ReservedQuantity, x.Quantity)).ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion
    }
}
