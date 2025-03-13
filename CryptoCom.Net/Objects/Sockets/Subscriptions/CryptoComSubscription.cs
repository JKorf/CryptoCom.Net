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

namespace CryptoCom.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CryptoComSubscription<T> : Subscription<CryptoComResponse<CryptoComSubscriptionEvent<T>>, CryptoComResponse>
    {
        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly string[] _symbols;
        private readonly Action<DataEvent<CryptoComSubscriptionEvent<T>>> _handler;
        private readonly Dictionary<string, object>? _parameters;
        private readonly bool _firstUpdateSnapshot;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            return typeof(CryptoComResponse<CryptoComSubscriptionEvent<T>>);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComSubscription(ILogger logger, string[] listenerIdentifiers, string[] symbols, Action<DataEvent<CryptoComSubscriptionEvent<T>>> handler, bool auth, Dictionary<string, object>? parameters = null, bool firstUpdateSnapshot = false) : base(logger, auth)
        {
            _handler = handler;
            _parameters = parameters;
            _symbols = symbols;
            _firstUpdateSnapshot = firstUpdateSnapshot;
            ListenerIdentifiers = new HashSet<string>(listenerIdentifiers);
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
                    { "channels", ListenerIdentifiers.ToArray() }
                }
            };

            if (_parameters != null)
            {
                foreach (var kvp in _parameters)
                    request.Parameters.Add(kvp.Key, kvp.Value);
            }

            return new CryptoComQuery<CryptoComSubscriptionEvent<T>>(request, Authenticated)
            {
                RequiredResponses = ListenerIdentifiers.Count
            };
        }

        /// <inheritdoc />
        public override Query? GetUnsubQuery()
        {
            return new CryptoComQuery(new CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "unsubscribe",
                Nonce = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow),
                Parameters = new ParameterCollection
                {
                    { "channels", ListenerIdentifiers.ToArray() }
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
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = (CryptoComResponse<CryptoComSubscriptionEvent<T>>)message.Data;

            if (_symbols.Any() && !_symbols.Contains(data.Result.Symbol))
                return message.ToCallResult();

            var updateType = (ConnectionInvocations == 1 && _firstUpdateSnapshot) ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
            _handler.Invoke(message.As(data.Result!, data.Result.Subscription, data.Result.Symbol, updateType));
            return message.ToCallResult();
        }
    }
}
