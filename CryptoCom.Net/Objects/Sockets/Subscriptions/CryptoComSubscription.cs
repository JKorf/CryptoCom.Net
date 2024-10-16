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

namespace CryptoCom.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CryptoComSubscription<T> : Subscription<CryptoComResponse<CryptoComSubscriptionEvent<T>>, CryptoComResponse>
    {
        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly Action<DataEvent<CryptoComSubscriptionEvent<T>>> _handler;
        private readonly Dictionary<string, object>? _parameters;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            return typeof(CryptoComResponse<CryptoComSubscriptionEvent<T>>);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public CryptoComSubscription(ILogger logger, string[] topics, Action<DataEvent<CryptoComSubscriptionEvent<T>>> handler, bool auth, Dictionary<string, object>? parameters = null) : base(logger, auth)
        {
            _handler = handler;
            _parameters = parameters;
            ListenerIdentifiers = new HashSet<string>(topics);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
        {
            var request = new Internal.CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "subscribe",
                Nonce = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow),
                Parameters = new ParameterCollection
                {
                    { "channels", ListenerIdentifiers }
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
            return new CryptoComQuery(new Internal.CryptoComRequest
            {
                Id = ExchangeHelpers.NextId(),
                Method = "unsubscribe",
                Nonce = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow),
                Parameters = new ParameterCollection
                {
                    { "channels", ListenerIdentifiers }
                }
            }, Authenticated);
        }

        public override void HandleSubQueryResponse(CryptoComResponse<CryptoComSubscriptionEvent<T>> message)
        {
            if (message.Code != 0 || message.Result == null)
                return;

            _handler.Invoke(new DataEvent<CryptoComSubscriptionEvent<T>>(message.Result, message.Result.Subscription, message.Result.Symbol, null, DateTime.UtcNow, SocketUpdateType.Snapshot));
        }

        /// <inheritdoc />
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = (CryptoComResponse<CryptoComSubscriptionEvent<T>>)message.Data;

            _handler.Invoke(message.As(data.Result!, data.Result.Subscription, data.Result.Symbol, SocketUpdateType.Update));
            return new CallResult(null);
        }
    }
}
