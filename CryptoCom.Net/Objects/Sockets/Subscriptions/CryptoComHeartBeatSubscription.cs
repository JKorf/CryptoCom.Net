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
    internal class CryptoComHeartBeatSubscription : SystemSubscription
    {
        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public CryptoComHeartBeatSubscription(ILogger logger) : base(logger, false)
        {
            ListenerIdentifiers = new HashSet<string> { "public/heartbeat" };
        }

        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = (CryptoComResponse)message.Data;
            connection.Send(ExchangeHelpers.NextId(), new CryptoComRequest
            {
                Id = data.Id,
                Method = "public/respond-heartbeat"
            }, 1);
            return message.ToCallResult();
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(CryptoComResponse);
    }
}
