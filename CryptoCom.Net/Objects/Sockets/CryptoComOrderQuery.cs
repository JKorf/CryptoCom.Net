using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using CryptoCom.Net.Objects.Models;
using CryptoCom.Net.Objects.Internal;
using System.Linq;

namespace CryptoCom.Net.Objects.Sockets
{
    internal class CryptoComOrderQuery : Query<CryptoComResponse<CryptoComListOrderResult>, CryptoComListOrderResult[]>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private List<CryptoComListOrderResult> _result = new List<CryptoComListOrderResult>();

        public CryptoComOrderQuery(CryptoComRequest request, int expectedResponses) : base(request, true, 1)
        {
            ListenerIdentifiers = new HashSet<string> { request.Id.ToString() };
            RequiredResponses = expectedResponses;
        }

        public override bool ValidateMessage(DataEvent<CryptoComResponse<CryptoComListOrderResult>> message)
        {
            if (message.Data.Result == null)
            {
                // Request fails, only a single response
                RequiredResponses = 1;
            }

            return true;
        }

        public override CallResult<CryptoComListOrderResult[]> HandleMessage(SocketConnection connection, DataEvent<CryptoComResponse<CryptoComListOrderResult>> message)
        {
            if (message.Data.Result == null)
            {
                // Request fails, only a single response
                return new CallResult<CryptoComListOrderResult[]>(new ServerError(message.Data.Code, message.Data.Message!));
            }

            if (message.Data.Code != 0)
                _result.Add(message.Data.Result with { ErrorCode = message.Data.Code, ErrorMessage = message.Data.Message });
            else
                _result.Add(message.Data.Result);
            return new CallResult<CryptoComListOrderResult[]>(_result.OrderBy(x => x.OrderId).ToArray());
        }
    }
}
