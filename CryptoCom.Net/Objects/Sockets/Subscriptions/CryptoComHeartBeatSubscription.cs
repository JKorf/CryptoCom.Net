using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using CryptoExchange.Net;
using CryptoCom.Net.Objects.Internal;
using CryptoExchange.Net.Sockets.Default;

namespace CryptoCom.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CryptoComHeartBeatSubscription : SystemSubscription
    {
        public CryptoComHeartBeatSubscription(ILogger logger) : base(logger, false)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<CryptoComResponse>("public/heartbeat", DoHandleMessage);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CryptoComResponse message)
        {
            _ = connection.SendAsync(ExchangeHelpers.NextId(), new CryptoComRequest
            {
                Id = message.Id,
                Method = "public/respond-heartbeat"
            }, 1);
            return CallResult.SuccessResult;
        }
    }
}
