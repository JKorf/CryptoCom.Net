using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using CryptoCom.Net.Objects.Models;
using CryptoCom.Net.Objects.Internal;

namespace CryptoCom.Net.Objects.Sockets
{
    internal class CryptoComQuery<T> : Query<CryptoComResponse<T>>
    {
        public CryptoComQuery(CryptoComRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<CryptoComResponse<T>>(request.Id.ToString(), HandleMessage);
        }

        public CallResult<CryptoComResponse<T>> HandleMessage(SocketConnection connection, DataEvent<CryptoComResponse<T>> message)
        {
            if (message.Data.Code != 0)
                return new CallResult<CryptoComResponse<T>>(new ServerError(message.Data.Code, message.Data.Message!));

            return message.ToCallResult();
        }
    }

    internal class CryptoComQuery : Query<CryptoComResponse>
    {
        public CryptoComQuery(CryptoComRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<CryptoComResponse>(request.Id.ToString(), HandleMessage);
        }

        public CallResult<CryptoComResponse> HandleMessage(SocketConnection connection, DataEvent<CryptoComResponse> message)
        {
            if (message.Data.Code != 0)
                return new CallResult<CryptoComResponse>(new ServerError(message.Data.Code, message.Data.Message!));

            return message.ToCallResult();
        }
    }
}
