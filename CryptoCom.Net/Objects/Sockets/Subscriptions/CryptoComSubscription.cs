using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using CryptoCom.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Objects.Internal;
using System.Linq;
using CryptoExchange.Net.Clients;

namespace CryptoCom.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CryptoComSubscription<T> : Subscription<CryptoComResponse<CryptoComSubscriptionEvent<T>>, CryptoComResponse>
    {
        private readonly SocketApiClient _client;
        private readonly string[] _symbols;
        private readonly Action<DataEvent<CryptoComSubscriptionEvent<T>>> _handler;
        private readonly Dictionary<string, object>? _parameters;
        private readonly bool _firstUpdateSnapshot;
        private readonly string[] _listenerIdentifiers;

        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComSubscription(ILogger logger, SocketApiClient client, string[] listenerIdentifiers, string[] symbols, Action<DataEvent<CryptoComSubscriptionEvent<T>>> handler, bool auth, Dictionary<string, object>? parameters = null, bool firstUpdateSnapshot = false) : base(logger, auth)
        {
            _client = client;
            _handler = handler;
            _parameters = parameters;
            _symbols = symbols;
            _firstUpdateSnapshot = firstUpdateSnapshot;
            _listenerIdentifiers = listenerIdentifiers;

            MessageMatcher = MessageMatcher.Create<CryptoComResponse<CryptoComSubscriptionEvent<T>>>(_listenerIdentifiers, DoHandleMessage);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
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
        public override Query? GetUnsubQuery()
        {
            return new CryptoComQuery(_client, new CryptoComRequest
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

        public override void HandleSubQueryResponse(CryptoComResponse<CryptoComSubscriptionEvent<T>> message)
        {
            if (message?.Code != 0 || message?.Result == null)
                return;

            _handler.Invoke(new DataEvent<CryptoComSubscriptionEvent<T>>(message.Result, message.Result.Subscription, message.Result.Symbol, null, DateTime.UtcNow, SocketUpdateType.Snapshot));
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<CryptoComResponse<CryptoComSubscriptionEvent<T>>> message)
        {
            if (_symbols.Any() && !_symbols.Contains(message.Data.Result.Symbol))
                return message.ToCallResult();

            var updateType = (ConnectionInvocations == 1 && _firstUpdateSnapshot) ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
            _handler.Invoke(message.As(message.Data.Result!, message.Data.Result.Subscription, message.Data.Result.Symbol, updateType));
            return message.ToCallResult();
        }
    }
}
