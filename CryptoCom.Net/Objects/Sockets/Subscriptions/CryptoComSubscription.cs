using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Objects.Internal;
using System.Linq;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets.Default;

namespace CryptoCom.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CryptoComSubscription<T> : Subscription
    {
        private readonly SocketApiClient _client;
        private readonly string[] _symbols;
        private readonly Action<DateTime, string?, int, CryptoComSubscriptionEvent<T>> _handler;
        private readonly Dictionary<string, object>? _parameters;
        private readonly string[] _listenerIdentifiers;

        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComSubscription(ILogger logger, SocketApiClient client, string topic, string[]? filters, string[]? symbols, Action<DateTime, string?, int, CryptoComSubscriptionEvent<T>> handler, bool auth, Dictionary<string, object>? parameters = null) : base(logger, auth)
        {
            _client = client;
            _handler = handler;
            _parameters = parameters;
            _symbols = symbols ?? [];
            _listenerIdentifiers = filters == null ? [topic] : filters.Select(x => $"{topic}.{x}").ToArray();

            MessageMatcher = MessageMatcher.Create<CryptoComResponse<CryptoComSubscriptionEvent<T>>>(_listenerIdentifiers, DoHandleMessage);
            MessageRouter = MessageRouter.CreateWithOptionalTopicFilters<CryptoComResponse<CryptoComSubscriptionEvent<T>>>(topic, filters, DoHandleRouteMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            var request = new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "subscribe",
                Nonce = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow),
                Parameters = new ParameterCollection
                {
                    { "channels", _listenerIdentifiers }
                }
            };

            if (_parameters != null)
            {
                foreach (var kvp in _parameters)
                    request.Parameters.Add(kvp.Key, kvp.Value);
            }

            return new CryptoComQuery<CryptoComSubscriptionEvent<T>>(_client, request, Authenticated)
            {
                RequiredResponses = _listenerIdentifiers.Length
            };
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new CryptoComQuery<object>(_client, new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "unsubscribe",
                Nonce = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow),
                Parameters = new ParameterCollection
                {
                    { "channels", _listenerIdentifiers.ToArray() }
                }
            }, Authenticated);
        }

        /// <inheritdoc />
        public override void HandleSubQueryResponse(object? message)
        {
            var data = (CryptoComResponse<CryptoComSubscriptionEvent<T>>?)message;

            if (data?.Code != 0 || data?.Result == null)
                return;

            _handler.Invoke(DateTime.UtcNow, null, ConnectionInvocations, data.Result);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CryptoComResponse<CryptoComSubscriptionEvent<T>> message)
        {
            if (_symbols.Any() && !_symbols.Contains(message.Result.Symbol))
                return CallResult.SuccessResult;

            _handler.Invoke(receiveTime, originalData, ConnectionInvocations, message.Result);
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleRouteMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CryptoComResponse<CryptoComSubscriptionEvent<T>> message)
        {
            _handler.Invoke(receiveTime, originalData, ConnectionInvocations, message.Result);
            return CallResult.SuccessResult;
        }
    }
}
