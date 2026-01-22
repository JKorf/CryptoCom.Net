using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using CryptoCom.Net.Objects.Models;
using CryptoCom.Net.Objects.Internal;
using System.Linq;
using System;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets.Default;

namespace CryptoCom.Net.Objects.Sockets
{
    internal class CryptoComOrderQuery : Query<CryptoComListOrderResult[]>
    {
        private readonly SocketApiClient _client;
        private List<CryptoComListOrderResult> _result = new List<CryptoComListOrderResult>();

        public CryptoComOrderQuery(SocketApiClient client, CryptoComRequest request, int expectedResponses) : base(request, true, 1)
        {
            _client = client;
            RequiredResponses = expectedResponses;

            MessageRouter = MessageRouter.CreateWithoutTopicFilter<CryptoComResponse<CryptoComListOrderResult>>(request.Id.ToString(), HandleMessage);
        }

        public CallResult<CryptoComListOrderResult[]> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CryptoComResponse<CryptoComListOrderResult> message)
        {
            if (message.Result == null)
                return new CallResult<CryptoComListOrderResult[]>(new ServerError(message.Code, _client.GetErrorInfo(message.Code, message.Message!)), originalData);            

            if (message.Code != 0)
                _result.Add(message.Result with { ErrorCode = message.Code, ErrorMessage = message.Message });
            else
                _result.Add(message.Result);
            return new CallResult<CryptoComListOrderResult[]>(_result.OrderBy(x => x.OrderId).ToArray(), originalData, null);
        }
    }
}
