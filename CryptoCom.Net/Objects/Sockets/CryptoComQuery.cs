using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoCom.Net.Objects.Internal;
using CryptoExchange.Net.Clients;
using System;
using CryptoExchange.Net.Sockets.Default;

namespace CryptoCom.Net.Objects.Sockets
{
    internal class CryptoComQuery<T> : Query<CryptoComResponse<T>>
    {
        private readonly SocketApiClient _client;

        public CryptoComQuery(SocketApiClient client, CryptoComRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _client = client;

            MessageMatcher = MessageMatcher.Create<CryptoComResponse<T>>(request.Id.ToString(), HandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<CryptoComResponse<T>>(request.Id.ToString(), HandleMessage);
        }

        public CallResult<CryptoComResponse<T>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CryptoComResponse<T> message)
        {
            if (message.Code != 0)
                return new CallResult<CryptoComResponse<T>>(new ServerError(message.Code, _client.GetErrorInfo(message.Code, message.Message!)), originalData);

            return new CallResult<CryptoComResponse<T>>(message, originalData, null);
        }
    }
}
