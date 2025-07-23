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
        public CryptoComHeartBeatSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<CryptoComResponse>("public/heartbeat", DoHandleMessage);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<CryptoComResponse> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new CryptoComRequest
            {
                Id = message.Data.Id,
                Method = "public/respond-heartbeat"
            }, 1);
            return message.ToCallResult();
        }
    }
}
