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

            AddSystemSubscription(new CryptoComHeartBeatSubscription(_logger));
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

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
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComOrderBookUpdateInt>>(_logger, topics, symbols.ToArray(), x => onMessage(x.As<CryptoComOrderBookUpdate>(x.Data.Data.First()).WithUpdateType(SocketUpdateType.Snapshot)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync([symbol], depth, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<CryptoComOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => $"book.{x}.{depth}").ToArray();
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComOrderBookUpdateInt>>(_logger, topics, symbols.ToArray(), 
                x => onMessage(x.As(x.Data.Data.First().Update ?? x.Data.Data.First()).WithUpdateType(x.Data.Channel == "book.update" ? SocketUpdateType.Update : SocketUpdateType.Snapshot)),
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
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComTicker>>(_logger, topics, symbols.ToArray(), x => onMessage(x.As(x.Data.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<CryptoComTrade>>> onMessage, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<CryptoComTrade>>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "trade." + x).ToArray();
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComTrade>>(_logger, topics, symbols.ToArray(), x => onMessage(x.As(x.Data.Data)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<IEnumerable<CryptoComKline>>> onMessage, CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<IEnumerable<CryptoComKline>>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => $"candlestick.{EnumConverter.GetString(interval)}.{x}").ToArray();
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComKline>>(_logger, topics, symbols.ToArray(),
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
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComValuation>>(_logger, topics, symbols.ToArray(), x => onMessage(x.As(x.Data.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
            => SubscribeToMarkPriceUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "mark." + x).ToArray();
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComValuation>>(_logger, topics, symbols.ToArray(), x => onMessage(x.As(x.Data.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
            => SubscribeToSettlementUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "settlement." + x).ToArray();
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComValuation>>(_logger, topics, symbols.ToArray(), x => onMessage(x.As(x.Data.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSettlementUpdatesAsync(Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = new[] { "settlement" };
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComValuation>>(_logger, topics, [], x => onMessage(x.As(x.Data.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
            => SubscribeToFundingRateUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "funding." + x).ToArray();
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComValuation>>(_logger, topics, symbols.ToArray(), x => onMessage(x.As(x.Data.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateUpdatesAsync(string symbol, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
            => SubscribeToEstimatedFundingRateUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CryptoComValuation>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "estimatedfunding." + x).ToArray();
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComValuation>>(_logger, topics, symbols.ToArray(), x => onMessage(x.As(x.Data.Data.First())), true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<CryptoComOrder>>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<CryptoComOrder>>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "user.order." + x).ToArray();
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComOrder>>(_logger, topics, symbols.ToArray(), x => onMessage(x.As(x.Data.Data)), true, firstUpdateSnapshot: true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<IEnumerable<CryptoComOrder>>> onMessage, CancellationToken ct = default)
        {
            var topics = new[] { "user.order" };
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComOrder>>(_logger, topics, [], x => onMessage(x.As(x.Data.Data)), true, firstUpdateSnapshot: true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<CryptoComUserTrade>>> onMessage, CancellationToken ct = default)
            => SubscribeToUserTradeUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<CryptoComUserTrade>>> onMessage, CancellationToken ct = default)
        {
            var topics = symbols.Select(x => "user.trade." + x).ToArray();
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComUserTrade>>(_logger, topics, symbols.ToArray(), x => onMessage(x.As(x.Data.Data)), true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<IEnumerable<CryptoComUserTrade>>> onMessage, CancellationToken ct = default)
        {
            var topics = new [] {"user.trade"};
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComUserTrade>>(_logger, topics, [], x => onMessage(x.As(x.Data.Data)), true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<CryptoComBalances>> onMessage, CancellationToken ct = default)
        {
            var topics = new [] { "user.balance" };
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComBalances>>(_logger, topics, [], x => onMessage(x.As(x.Data.Data.First())), true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<IEnumerable<CryptoComPosition>>> onMessage, CancellationToken ct = default)
        {
            var topics = new [] { "user.positions" };
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComPosition>>(_logger, topics, [], x => onMessage(x.As(x.Data.Data)), true, firstUpdateSnapshot: true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionBalanceUpdatesAsync(Action<DataEvent<CryptoComBalancePositionUpdate>> onMessage, CancellationToken ct = default)
        {
            var topics = new [] { "user.position_balance" };
            var subscription = new CryptoComSubscription<IEnumerable<CryptoComBalancePositionUpdate>>(_logger, topics, [], x => onMessage(x.As(x.Data.Data.First())), true);
            return await SubscribeAsync(BaseAddress.AppendPath("exchange/v1/user"), subscription, ct).ConfigureAwait(false);
        }

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
        protected override Query? GetAuthenticationRequest(SocketConnection connection)
        {
            var authProvider = (CryptoComAuthenticationProvider)AuthenticationProvider!;
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                ApiKey = authProvider.ApiKey,
                Method = "public/auth"
            };

            authProvider.AuthenticateRequest(null, request);
            return new CryptoComQuery(request, false, 1);
        }

        /// <inheritdoc />
        public ICryptoComSocketClientExchangeApiShared SharedClient => this;


        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
        {
            if (tradingMode == TradingMode.Spot)
                return $"{baseAsset.ToUpperInvariant()}_{quoteAsset.ToUpperInvariant()}";

            if (tradingMode == TradingMode.PerpetualLinear)
                return $"{baseAsset.ToUpperInvariant()}{quoteAsset.ToUpperInvariant()}-PERP";

            if (deliverDate == null)
                throw new ArgumentException("DeliverDate required to format delivery futures symbol");

            return $"{baseAsset.ToUpperInvariant()}{quoteAsset.ToUpperInvariant()}-{deliverDate.Value.ToString("yyMMdd")}";
        }
    }
}
