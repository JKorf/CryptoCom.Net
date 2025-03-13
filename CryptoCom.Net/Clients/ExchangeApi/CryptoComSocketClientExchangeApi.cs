using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoCom.Net.Objects.Models;
using CryptoCom.Net.Objects.Options;
using CryptoCom.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CryptoCom.Net.Objects.Internal;
using CryptoCom.Net.Enums;
using CryptoCom.Net.Objects.Sockets;
using CryptoCom.Net.Objects;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    /// <summary>
    /// Client providing access to the CryptoCom Exchange websocket Api
    /// </summary>
    internal partial class CryptoComSocketClientExchangeApi : SocketApiClient, ICryptoComSocketClientExchangeApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _methodPath = MessagePath.Get().Property("method");
        private static readonly MessagePath _subscriptionPath = MessagePath.Get().Property("result").Property("subscription");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("result").Property("channel");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal CryptoComSocketClientExchangeApi(ILogger logger, CryptoComSocketOptions options) :
            base(logger, options.Environment.SocketClientAddress!, options, options.ExchangeOptions)
        {
            RateLimiter = CryptoComExchange.RateLimiter.Socket;

            AddSystemSubscription(new CryptoComHeartBeatSubscription(_logger));
        }
        #endregion

        #region Subscriptions

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(CryptoComExchange.SerializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(CryptoComExchange.SerializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CryptoComAuthenticationProvider(credentials);

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(string symbol, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookSnapshotUpdatesAsync([symbol], depth, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => $"book.{x}.{depth}").ToArray();
            var subscription = new CryptoComSubscription<CryptoComOrderBookUpdateInt[]>(_logger, topics, symbols.ToArray(), x => onMessage(x.As<CryptoComOrderBookUpdate>(x.Data.Data.First()).WithUpdateType(SocketUpdateType.Snapshot)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync([symbol], depth, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => $"book.{x}.{depth}").ToArray();
            var subscription = new CryptoComSubscription<CryptoComOrderBookUpdateInt[]>(_logger, topics, symbols.ToArray(), 
                x => onMessage(
                    x.As(x.Data.Data.First().Update ?? x.Data.Data.First())
                    .WithUpdateType(x.Data.Channel == "book.update" ? SocketUpdateType.Update : SocketUpdateType.Snapshot)
                    .WithDataTimestamp(x.Data.Data.First().UpdateTime)),
                false, new Dictionary<string, object> { { "book_subscription_type", "SNAPSHOT_AND_UPDATE" } });
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<CryptoComTicker>> onMessage, CancellationToken ct = default)
            => SubscribeToTickerUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComTicker>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "ticker." + x).ToArray();
            var subscription = new CryptoComSubscription<CryptoComTicker[]>(_logger, topics, symbols.ToArray(), x => onMessage(
                x.As(x.Data.Data.First())
                .WithDataTimestamp(x.Data.Data.Max(x => x.Timestamp))), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<CryptoComTrade[]>> onMessage, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComTrade[]>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "trade." + x).ToArray();
            var subscription = new CryptoComSubscription<CryptoComTrade[]>(_logger, topics, symbols.ToArray(), x => onMessage(
                x.As(x.Data.Data)
                .WithDataTimestamp(x.Data.Data.Max(x => x.Timestamp))), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<CryptoComKline[]>> onMessage, CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<CryptoComKline[]>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => $"candlestick.{EnumConverter.GetString(interval)}.{x}").ToArray();
            var subscription = new CryptoComSubscription<CryptoComKline[]>(_logger, topics, symbols.ToArray(),
                x => onMessage(x.As(x.Data.Data)),
                false, new Dictionary<string, object> { { "book_subscription_type", "SNAPSHOT_AND_UPDATE" } });
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
            => SubscribeToIndexPriceUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "index." + x).ToArray();
            var subscription = new CryptoComSubscription<CryptoComValuation[]>(_logger, topics, symbols.ToArray(), x => onMessage(
                x.As(x.Data.Data.First())
                .WithDataTimestamp(x.Data.Data.Max(x => x.Timestamp))), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
            => SubscribeToMarkPriceUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "mark." + x).ToArray();
            var subscription = new CryptoComSubscription<CryptoComValuation[]>(_logger, topics, symbols.ToArray(), x => onMessage(
                x.As(x.Data.Data.First())
                .WithDataTimestamp(x.Data.Data.Max(x => x.Timestamp))), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
            => SubscribeToSettlementUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "settlement." + x).ToArray();
            var subscription = new CryptoComSubscription<CryptoComValuation[]>(_logger, topics, symbols.ToArray(), x => onMessage(
                x.As(x.Data.Data.First())
                .WithDataTimestamp(x.Data.Data.Max(x => x.Timestamp))), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = new[] { "settlement" };
            var subscription = new CryptoComSubscription<CryptoComValuation[]>(_logger, topics, [], x => onMessage(
                x.As(x.Data.Data.First())
                .WithDataTimestamp(x.Data.Data.Max(x => x.Timestamp))), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
            => SubscribeToFundingRateUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "funding." + x).ToArray();
            var subscription = new CryptoComSubscription<CryptoComValuation[]>(_logger, topics, symbols.ToArray(), x => onMessage(
                x.As(x.Data.Data.First())
                .WithDataTimestamp(x.Data.Data.Max(x => x.Timestamp))), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
            => SubscribeToEstimatedFundingRateUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "estimatedfunding." + x).ToArray();
            var subscription = new CryptoComSubscription<CryptoComValuation[]>(_logger, topics, symbols.ToArray(), x => onMessage(
                x.As(x.Data.Data.First())
                .WithDataTimestamp(x.Data.Data.Max(x => x.Timestamp))), true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<DataEvent<CryptoComOrder[]>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComOrder[]>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "user.order." + x).ToArray();
            var subscription = new CryptoComSubscription<CryptoComOrder[]>(_logger, topics, symbols.ToArray(), x => onMessage(
                x.As(x.Data.Data)
                .WithDataTimestamp(x.Data.Data.Max(x => x.UpdateTime))), true, firstUpdateSnapshot: true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<CryptoComOrder[]>> onMessage, CancellationToken ct = default)
        {
            var topics = new[] { "user.order" };
            var subscription = new CryptoComSubscription<CryptoComOrder[]>(_logger, topics, [], x => onMessage(
                x.As(x.Data.Data)
                .WithDataTimestamp(x.Data.Data.Max(x => x.UpdateTime))), true, firstUpdateSnapshot: true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string symbol, Action<DataEvent<CryptoComUserTrade[]>> onMessage, CancellationToken ct = default)
            => SubscribeToUserTradeUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComUserTrade[]>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "user.trade." + x).ToArray();
            var subscription = new CryptoComSubscription<CryptoComUserTrade[]>(_logger, topics, symbols.ToArray(), x => onMessage(
                x.As(x.Data.Data)
                .WithDataTimestamp(x.Data.Data.Max(x => x.CreateTime))), true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<CryptoComUserTrade[]>> onMessage, CancellationToken ct = default)
        {
            var topics = new [] {"user.trade"};
            var subscription = new CryptoComSubscription<CryptoComUserTrade[]>(_logger, topics, [], x => onMessage(
                x.As(x.Data.Data)
                .WithDataTimestamp(x.Data.Data.Max(x => x.CreateTime))), true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<CryptoComBalances>> onMessage, CancellationToken ct = default)
        {
            var topics = new [] { "user.balance" };
            var subscription = new CryptoComSubscription<CryptoComBalances[]>(_logger, topics, [], x => onMessage(
                x.As(x.Data.Data.First())), true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<CryptoComPosition[]>> onMessage, CancellationToken ct = default)
        {
            var topics = new [] { "user.positions" };
            var subscription = new CryptoComSubscription<CryptoComPosition[]>(_logger, topics, [], x => onMessage(
                x.As(x.Data.Data)
                .WithDataTimestamp(x.Data.Data.Max(x => x.UpdateTime))), true, firstUpdateSnapshot: true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionBalanceUpdatesAsync(Action<DataEvent<CryptoComBalancePositionUpdate>> onMessage, CancellationToken ct = default)
        {
            var topics = new [] { "user.position_balance" };
            var subscription = new CryptoComSubscription<CryptoComBalancePositionUpdate[]>(_logger, topics, [], x => onMessage(
                x.As(x.Data.Data.First())), true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        #endregion

        #region Queries

        /// <inheritdoc />
        public async Task<CallResult<CryptoComBalances[]>> GetBalancesAsync(CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/user-balance"
            };

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery<CryptoComBalancesWrapper>(request, true, 1), ct).ConfigureAwait(false);
            return result.As<CryptoComBalances[]>(result.Data?.Result.Data);
        }

        /// <inheritdoc />
        public async Task<CallResult<CryptoComPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/get-positions",
                Parameters = new ParameterCollection()
            };
            request.Parameters.AddOptional("instrument_name", symbol);

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery<CryptoComPositionWrapper>(request, true, 1), ct).ConfigureAwait(false);
            return result.As<CryptoComPosition[]>(result.Data?.Result.Data);
        }

        /// <inheritdoc />
        public async Task<CallResult<CryptoComOrderId>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, bool? postOnly = null, TimeInForce? timeInForce = null, decimal? triggerPrice = null, PriceType? triggerPriceType = null, bool? margin = null, SelfTradePreventionScope? selfTradePreventionScope = null, SelfTradePreventionMode? selfTradePreventionMode = null, string? selfTradePreventionId = null, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/create-order",
                Parameters = new ParameterCollection()
            };

            request.Parameters.Add("instrument_name", symbol);
            request.Parameters.AddEnum("side", side);
            request.Parameters.AddEnum("type", type);
            request.Parameters.AddOptionalString("quantity", quantity);
            request.Parameters.AddOptionalString("notional", quoteQuantity);
            request.Parameters.AddOptionalString("price", price);
            request.Parameters.Add("client_oid", clientOrderId ?? ExchangeHelpers.RandomString(32));
            request.Parameters.AddOptional("exec_inst", postOnly == true ? new[] { "POST_ONLY" } : null);
            request.Parameters.AddOptionalEnum("time_in_force", timeInForce);
            request.Parameters.AddOptionalString("ref_price", triggerPrice);
            request.Parameters.AddOptionalEnum("ref_price_type", triggerPriceType);
            request.Parameters.AddOptional("spot_margin", margin == true ? "MARGIN" : null);
            request.Parameters.AddOptionalEnum("stp_scope", selfTradePreventionScope);
            request.Parameters.AddOptionalEnum("stp_inst", selfTradePreventionMode);
            request.Parameters.AddOptional("stp_id", selfTradePreventionId);

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery<CryptoComOrderId>(request, true, 1), ct).ConfigureAwait(false);
            return result.As<CryptoComOrderId>(result.Data?.Result);
        }

        /// <inheritdoc />
        public async Task<CallResult<CryptoComOrderId>> CancelOrderAsync(string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/cancel-order",
                Parameters = new ParameterCollection()
            };
            request.Parameters.AddOptional("order_id", orderId);
            request.Parameters.AddOptional("client_oid", clientOrderId);

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery<CryptoComOrderId>(request, true, 1), ct).ConfigureAwait(false);
            return result.As<CryptoComOrderId>(result.Data?.Result);
        }

        /// <inheritdoc />
        public async Task<CallResult> CancelAllOrdersAsync(string? symbol = null, OrderTypeFilter? type = null, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/cancel-all-orders",
                Parameters = new ParameterCollection()
            };
            request.Parameters.AddOptional("instrument_name", symbol);
            request.Parameters.AddOptionalEnum("type", type);

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery(request, true, 1), ct).ConfigureAwait(false);
            return result.AsDataless();
        }

        /// <inheritdoc />
        public async Task<CallResult<CryptoComOrderId>> ClosePositionAsync(string symbol, OrderType orderType, decimal? price = null, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/close-position",
                Parameters = new ParameterCollection()
            };
            request.Parameters.Add("instrument_name", symbol);
            request.Parameters.AddEnum("type", orderType);
            request.Parameters.AddOptionalString("price", price);

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery<CryptoComOrderId>(request, true, 1), ct).ConfigureAwait(false);
            return result.As<CryptoComOrderId>(result.Data?.Result);
        }


        public async Task<CallResult<CryptoComOrder[]>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/get-open-orders",
                Parameters = new ParameterCollection()
            };

            request.Parameters.AddOptional("instrument_name", symbol);

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery<CryptoComOrderWrapper>(request, true, 1), ct).ConfigureAwait(false);
            return result.As<CryptoComOrder[]>(result.Data?.Result.Data);
        }

        public async Task<CallResult<CryptoComOrderResult[]>> PlaceMultipleOrdersAsync(IEnumerable<CryptoComOrderRequest> orders, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/create-order-list",
                Parameters = new ParameterCollection()
            };

            foreach (var order in orders)
                order.ClientOrderId ??= ExchangeHelpers.RandomString(32);

            request.Parameters.Add("contingency_type", "LIST");
            request.Parameters.Add("order_list", orders.ToArray());

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery<CryptoComOrderResult[]>(request, true, 1), ct).ConfigureAwait(false);
            return result.As<CryptoComOrderResult[]>(result.Data?.Result);
        }

        public async Task<CallResult<CryptoComCancelOrderResult[]>> CancelOrdersAsync(IEnumerable<CryptoComCancelOrderRequest> orders, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/cancel-order-list",
                Parameters = new ParameterCollection()
            };

            request.Parameters.Add("contingency_type", "LIST");
            request.Parameters.Add("order_list", orders.ToArray());

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery<CryptoComCancelOrderResult[]>(request, true, 1), ct).ConfigureAwait(false);
            return result.As<CryptoComCancelOrderResult[]>(result.Data?.Result);
        }

        public async Task<CallResult<CryptoComOcoResult>> PlaceOcoOrderAsync(CryptoComOrderRequest order1, CryptoComOrderRequest order2, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/create-order-list",
                Parameters = new ParameterCollection()
            };

            order1.ClientOrderId ??= ExchangeHelpers.RandomString(32);
            order2.ClientOrderId ??= ExchangeHelpers.RandomString(32);

            request.Parameters.Add("contingency_type", "OCO");
            request.Parameters.Add("order_list", new[] { order1, order2 });

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery<CryptoComOcoResult>(request, true, 1), ct).ConfigureAwait(false);
            return result.As<CryptoComOcoResult> (result.Data?.Result);
        }

        public async Task<CallResult> CancelOcoOrderAsync(string symbol, string listId, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/cancel-order-list",
                Parameters = new ParameterCollection()
            };

            request.Parameters.Add("contingency_type", "OCO");
            request.Parameters.Add("list_id", listId);
            request.Parameters.Add("instrument_name", symbol);

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery(request, true, 1), ct).ConfigureAwait(false);
            return result.AsDataless();
        }

        public async Task<CallResult<CryptoComWithdrawalResult>> WithdrawAsync(string asset, decimal quantity, string address, string? addressTag = null, string? network = null, string? clientWithdrawId = null, CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/create-withdrawal",
                Parameters = new ParameterCollection()
            };

            request.Parameters.Add("currency", asset);
            request.Parameters.Add("amount", quantity);
            request.Parameters.Add("address", address);
            request.Parameters.AddOptional("address_tag", addressTag);
            request.Parameters.AddOptional("network_id", network);
            request.Parameters.AddOptional("client_wid", clientWithdrawId);

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery<CryptoComWithdrawalResult>(request, true, 1), ct).ConfigureAwait(false);
            return result.As<CryptoComWithdrawalResult>(result.Data?.Result);
        }

        public async Task<CallResult> SetCancelOnDisconnectAsync(CancellationToken ct = default)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "private/set-cancel-on-disconnect",
                Parameters = new ParameterCollection()
                {
                    { "scope", "CONNECTION" }
                }
            };

            var result = await QueryAsync(BaseAddress.AppendPath("exchange/v1/user"), new CryptoComQuery(request, true, 1), ct).ConfigureAwait(false);
            return result.AsDataless();
        }

        #endregion


        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var method = message.GetValue<string>(_methodPath);
            if (method?.Equals("public/heartbeat") == true)
                return method;

            var id = message.GetValue<long>(_idPath);
            if (id >= 0)
                return id.ToString();

            var channel = message.GetValue<string>(_channelPath);
            if (channel == "user.order" || channel == "user.trade")
                return channel;

            var subscription = message.GetValue<string>(_subscriptionPath);
            return subscription;
        }

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection)
        {
            var authProvider = (CryptoComAuthenticationProvider)AuthenticationProvider!;
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                ApiKey = authProvider.ApiKey,
                Method = "public/auth"
            };

            authProvider.AuthenticateRequest(null, request);
            return Task.FromResult<Query?>(new CryptoComQuery(request, false, 1));
        }

        /// <inheritdoc />
        public ICryptoComSocketClientExchangeApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => CryptoComExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
