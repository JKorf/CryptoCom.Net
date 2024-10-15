using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using CryptoCom.Net.Objects.Models;

namespace CryptoCom.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CryptoComSubscription<T> : Subscription<CryptoComModel, CryptoComModel>
    {
        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly Action<DataEvent<T>> _handler;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            return typeof(T);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="topics"></param>
        /// <param name="handler"></param>
        /// <param name="auth"></param>
        public CryptoComSubscription(ILogger logger, string[] topics, Action<DataEvent<T>> handler, bool auth) : base(logger, auth)
        {
            _handler = handler;
            ListenerIdentifiers = new HashSet<string>(topics);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Query? GetUnsubQuery()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            _handler.Invoke(message.As((T)message.Data!, null, null, SocketUpdateType.Update));
            return new CallResult(null);
        }
    }
}
