using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using CryptoCom.Net.Objects.Models;
using CryptoCom.Net.Objects.Internal;
using System.Linq;
using CryptoExchange.Net.Interfaces;
using System;
using CryptoExchange.Net.Clients;

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

            MessageMatcher = MessageMatcher.Create<CryptoComResponse<CryptoComListOrderResult>>(request.Id.ToString(), HandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<CryptoComResponse<CryptoComListOrderResult>>(request.Id.ToString(), HandleMessage);
        }

        public override CallResult<object> Deserialize(IMessageAccessor message, Type type)
        {
#warning apply this for updated deserialization
            var result = base.Deserialize(message, type);
            if (result)
            {
                var success = result.Data is CryptoComResponse<CryptoComListOrderResult> { Result: not null };
                if (!success)
                {
                    // Request fails, only a single response
                    RequiredResponses = 1;
                }
            }

            return result;
        }

        public CallResult<CryptoComListOrderResult[]> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CryptoComResponse<CryptoComListOrderResult> message)
        {
            if (message.Result == null)
            {
                // Request fails, only a single response
                return new CallResult<CryptoComListOrderResult[]>(new ServerError(message.Code, _client.GetErrorInfo(message.Code, message.Message!)), originalData);
            }

            if (message.Code != 0)
                _result.Add(message.Result with { ErrorCode = message.Code, ErrorMessage = message.Message });
            else
                _result.Add(message.Result);
            return new CallResult<CryptoComListOrderResult[]>(_result.OrderBy(x => x.OrderId).ToArray(), originalData, null);
        }
    }
}
