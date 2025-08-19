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
            MessageMatcher = MessageMatcher.Create<CryptoComResponse<CryptoComListOrderResult>>(request.Id.ToString(), HandleMessage);
            RequiredResponses = expectedResponses;
        }

        public override CallResult<object> Deserialize(IMessageAccessor message, Type type)
        {
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

        public CallResult<CryptoComListOrderResult[]> HandleMessage(SocketConnection connection, DataEvent<CryptoComResponse<CryptoComListOrderResult>> message)
        {
            if (message.Data.Result == null)
            {
                // Request fails, only a single response
                return new CallResult<CryptoComListOrderResult[]>(new ServerError(message.Data.Code, _client.GetErrorInfo(message.Data.Code, message.Data.Message!)));
            }

            if (message.Data.Code != 0)
                _result.Add(message.Data.Result with { ErrorCode = message.Data.Code, ErrorMessage = message.Data.Message });
            else
                _result.Add(message.Data.Result);
            return new CallResult<CryptoComListOrderResult[]>(_result.OrderBy(x => x.OrderId).ToArray());
        }
    }
}
